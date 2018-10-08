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

namespace BolindersBil.Web.Controllers
{
    public class HomeController : Controller
    {
        private IVehicleRepository vehicleRepo;
        public int PageLimit = 8;
        private CustomAppSettings _appSettings;

        public HomeController(IVehicleRepository vehicleRepository,IOptions<CustomAppSettings> settings)
        {
            vehicleRepo = vehicleRepository;
            _appSettings = settings.Value;
        }

        public IActionResult Index(string state, int page = 1)
       {
            var vehicles = vehicleRepo.Vehicles;
            var brands = vehicleRepo.Brands;

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

           List<int> brandsInStock = new List<int>();

            foreach (var b in vehicles)
            {
                if (!brandsInStock.Contains(b.BrandId))
                {
                    brandsInStock.Add(b.BrandId);
                }
            }

            // Code for PageLimit button at the bottom

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

            var vm = new VehicleListViewModel
            {
                Vehicles = vehiclesInPageLimit,
                Brands = brands,
                BrandsInStock = brandsInStock,
                ShowButton = showButton,
                NextPage = ++page
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Vehicle(int vehicleId)
        {
            var vehicle = vehicleRepo.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));

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