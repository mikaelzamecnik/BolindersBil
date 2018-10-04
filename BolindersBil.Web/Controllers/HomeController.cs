﻿using System;
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

        public IActionResult Index(string searchString, int page = 1)
        {
            // LINQ query to select vehicles
            var vehicles = from v in vehicleRepo.Vehicles
                           select v;

            // If the searchstring parameter contains a string the vehicle
            // query is modified to filter on the value of the search string
            // TODO
            /* orderby lambda expression for filter */
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(s =>
                    s.Model.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) ||
                    s.RegistrationNumber.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
                );
            }

            var toSkip = (page - 1) * PageLimit;
            var vehicles2 = vehicles
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
                Vehicles = vehicles2,
                ShowButton = showButton,
                NextPage = ++page
            };

            return View(vm);
        }

        public IActionResult Vehicle(int vehicleId)
        {
            var vehicle = vehicleRepo.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));

            return View(vehicle);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {

            return View();
        }
        public IActionResult SendLink(string url, string sendmail)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("bolindersbil@hotmail.com"));
            message.To.Add(new MailboxAddress(sendmail));

            message.Subject = "Här kommer din drömbil från BolindersBil";
            message.Body = new TextPart("html")
            {
                Text = "<h6><strong>BolindersBil</strong></h2>" + "<br>" +
                $"<a href='{url}' target='_blank'>{url}</a>"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_appSettings.FormSmtpServer, _appSettings.FormPort);
                client.Authenticate(_appSettings.FormUserName, _appSettings.FormPassWord);
                client.Send(message);
                client.Disconnect(true);
            }
            ModelState.Clear();
            return View();
        }
    }
}