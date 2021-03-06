using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BolindersBil.Web.Models;
using BolindersBil.Web.Repositories;
using BolindersBil.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using System.Web.Http;
using BolindersBil.Web.Infrastructure;
using BolindersBil.Web.DataAccess;
using Microsoft.EntityFrameworkCore;
using BolindersBil.Web.Models.NewsModels;
using BolindersBil.Web.Constants;

namespace BolindersBil.Web.Controllers
{
    public class HomeController : Controller
    {
        private IVehicleRepository vehicleRepo;
        private ApplicationDbContext _context;
        public int PageLimit = 8;
        private CustomAppSettings _appSettings;

        public HomeController(IVehicleRepository vehicleRepository,IOptions<CustomAppSettings> settings, ApplicationDbContext context)
        {
            vehicleRepo = vehicleRepository;
            _appSettings = settings.Value;
            _context = context;
        }
        [Route("Errors/{code:int}")]
        public IActionResult Errors(int code)
        {
            return View(code.ToString());
        }
        public IActionResult Index(string state, int page = 1)
       {

            var vehicles = vehicleRepo.Vehicles;

            if (state == "nya")
            {
                vehicles = vehicles.Where(s =>
                s.Used is false);
            }

            if (state == "begagnade")
            {
                vehicles = vehicles.Where(s =>
                s.Used is true);
            }

           // Generate a list with the BrandId of cars in stock and place in the viewmodel further down

           List<Brand> brandsInStock = new List<Brand>();

            foreach (var v in vehicles)
            {
                if (!brandsInStock.Contains(v.Brand))
                {
                    brandsInStock.Add(v.Brand);
                }
            }

            var toSkip = (page - 1) * PageLimit;
            var vehiclesInPageLimit = vehicles
                .OrderBy(x => x.Id)
                .Skip(toSkip)
                .Take(PageLimit);

            var totalNumberOfVehicles = vehicles.Count();

            var showButton = true;

            if (page * PageLimit > totalNumberOfVehicles)
            {
                showButton = false;
            }

            var newsApiClient = new NewsApiClient(_appSettings.NewsApiKey, _appSettings.NewsApiUrl);

            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Sources = { "the-new-york-times" },
                Q = "Apple",
                SortBy = SortBys.PublishedAt,
                Language = Languages.EN,
                From = new DateTime(2018, 09, 24)
            });

            var vm = new VehicleListViewModel
            {
                Vehicles = vehiclesInPageLimit,
                BrandsInStock = brandsInStock,
                ShowButton = showButton,
                NextPage = ++page,
                ArticlesResults = articlesResponse
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Vehicle(int vehicleId)
        {
            var vehicle = vehicleRepo.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
            if (vehicle == null)
            {
                return NotFound();
            }
            var relatedVehicles = vehicleRepo.Vehicles.Where(x => x.BrandId.Equals(vehicle.BrandId)).Where(x => x.Price > vehicle.Price).Take(4);
            var vm = new SingleVehicleViewModel
            {
                Vehicle = vehicle,
                RelatedVehicles = relatedVehicles
            };

            return View(vm);

        }

        [HttpPost]
        public IActionResult SendLink(SingleVehicleViewModel model)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("mail.bolinder.bil@gmail.com"));
                message.To.Add(new MailboxAddress(model.SendMail));
                message.Subject = "Här kommer din drömbil från BolindersBil";
                message.Body = new TextPart("html")
                {
                    Text = "<h2><strong>Klicka på länken för att se fordonet</strong></h2>" + "<br>" +
                    $"<a href='{model.Url}' target='_blank'>{model.Url}</a>"
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                client.Connect(_appSettings.FormSmtpServer, _appSettings.FormPort);
                client.Authenticate(_appSettings.FormUserName, _appSettings.FormPassWord);
                client.Send(message);
                client.Disconnect(true);
            }
                ModelState.Clear();
            return Redirect(model.Url);
        }


    }
}