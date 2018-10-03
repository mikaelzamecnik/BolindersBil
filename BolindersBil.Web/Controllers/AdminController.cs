using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.Constants.Admin;
using BolindersBil.Web.DataAccess;
using BolindersBil.Web.Infrastructure;
using BolindersBil.Web.Models;
using BolindersBil.Web.Repositories;
using BolindersBil.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BolindersBil.Web.Controllers
{
    public class AdminController : Controller
    {
        private IVehicleRepository vehicleRepo;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ApplicationDbContext _context;

        public AdminController(IVehicleRepository vehicleRepository, IHostingEnvironment appEnvironment, ApplicationDbContext context)
        {
            vehicleRepo = vehicleRepository;
            _appEnvironment = appEnvironment;
            _context = context;
        }


        //Lista alla fordon när man går in på /admin
        //public IActionResult Index()
        //{
        //    return View(vehicleRepo.Vehicles);
        //}

        public string Test()
        {
            return "Hello world";
        }


        // Save to disk
        [HttpPost]
        public IActionResult FileUpload(IFormFile image)
        {
            if(image != null)
            {
                var fileName = Path.Combine(_appEnvironment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                image.CopyTo(new FileStream(fileName, FileMode.Create));
                ViewData["fileLocation"] = "/Images/" + Path.GetFileName(image.FileName);
            }
            return View();
        }


        // TODO
        /* Make this to a component to prevent DRY.
         * Lambda expression here if we got time over.
         * 
         * #param searchString
         */
        public IActionResult Index(string searchString)
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

            return View(vehicles);
        }

        // Sök/filtrera efter fordon
        public IActionResult Search()
        {
            return View();
        }

        // Redigera fordon
        [HttpGet]
        public IActionResult Edit(int vehicleId)
        {
            var yearData = from Year e in Enum.GetValues(typeof(Year))
                           select new
                           {
                             ID = (int)e,
                             Name = e.ToString()
                           };
            var vehicle = vehicleRepo.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
            var vm = new EditVehicleViewModel
            {
                DealerShips = vehicleRepo.Dealerships.ToSelectList(vehicle),
                Brands = vehicleRepo.Brands.ToSelectList(vehicle),
                //Year = new SelectList(yearData, "ID", "Name"),
                Vehicle = vehicle,

            };
            return View(vm);

        }

        [HttpPost]
        public IActionResult Edit(EditVehicleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Vehicle.DateUpdated = DateTime.Now;
                vehicleRepo.SaveVehicle(vm.Vehicle);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(vm);
            }
        }

        // Skapa nytt fordon
        public IActionResult Create()
        {
            return View();
        }

        // Ta bort fordon
        public IActionResult Delete()
        {
            return View();
        }

 
        //public IActionResult GetImagesToVehicle(int id)
        //{

            //var fileUpload = _context.Vehicles.Include(f => f.FileUpload).Where(f => f.Id == id);
            //var vm = new VehicleFileUploadViewModel
            //{
            //    VehicleId = id,
            //    FileUpload = fileUpload
            //};
            //var fileUploads = _context.FileUploads
            //    .Include(v => v.VehicleId)
            //    .ToList();
            //Vehicle vehicle = _context.Vehicles
            //    .Include(v => v.FileUpload)
            //    .Single(v => v.Id == fileUploadId);

            //IList<FileUpload> fileUploads = _context.FileUploads.Where(v => v.Id == id).ToList();

            //return View("Index", vm);
        //}
    }
}