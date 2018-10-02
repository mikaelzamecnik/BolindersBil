using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.Models;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace BolindersBil.Web.Controllers
{
    public class ContactController : Controller
    {
       
        public ViewResult Index()
        {
            return View();
        }

        public IActionResult SendMail (string name, string title, string email, string msg, string dealershipEmail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("sankec@hotmail.com"));
            message.To.Add(new MailboxAddress (dealershipEmail)); 

            message.Subject = title;
            message.Body = new TextPart("html")
            {
                Text = "Från: " + name + "<br>" +
                "Kontakt e-post: " + email + "<br>" +
                "Meddelande: " + msg
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587);
                client.Authenticate("sankec@hotmail.com", "okanoviic0702");
                client.Send(message);
                client.Disconnect(true);
            }
            ModelState.Clear();
            return View("Index");
        }


    }
}