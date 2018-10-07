using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using BolindersBil.Web.Models;
using Microsoft.Extensions.Options;

namespace BolindersBil.Web.Controllers
{
    public class ContactController : Controller
    {
        private CustomAppSettings _appSettings;

        public ContactController(IOptions<CustomAppSettings> settings)
        {
            _appSettings = settings.Value;
        }
        public ViewResult Index()
        {
            return View();
        }

        public IActionResult SendMail(string name, string title, string email, string msg, string dealershipEmail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("bolindersbil@hotmail.com"));
            message.To.Add(new MailboxAddress(dealershipEmail));

            message.Subject = title;
            message.Body = new TextPart("html")
            {
                Text = "Från: " + name + "<br>" +
                "Kontakt e-post: " + email + "<br>" +
                "Meddelande: " + msg
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_appSettings.FormSmtpServer, _appSettings.FormPort);
                //client.Authenticate(_appSettings.FormUserName, _appSettings.FormPassWord); Change when you have a smtp server
                client.Send(message);
                client.Disconnect(true);
            }
            ModelState.Clear();
            return View("Index");
        }

    }
}