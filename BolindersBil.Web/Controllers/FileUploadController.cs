using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.DataAccess;
using BolindersBil.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolindersBil.Web.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FileUploadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Goes to DB
        public IActionResult Index()
        {
            //var fileUploads = _context.Vehicles.Include(v => v.FileUpload).ToList();

            IList<FileUpload> fileUploads = _context.FileUploads.Include(v => v.Vehicle).ToList();

            return View(fileUploads);
        }

        public IActionResult Vehicle(int id)
        {
            if(id == 0)
            {
                return Redirect("/Admin");
            }

            IList<FileUpload> theImage = _context.FileUploads
                .Include(v => v.Vehicle)
                .Where(v => v.VehicleId == id)
                .ToList();

            ViewBag.Title = "Images in Vehicle: " + theImage;

            return View("Index", theImage);
        }
    }
}