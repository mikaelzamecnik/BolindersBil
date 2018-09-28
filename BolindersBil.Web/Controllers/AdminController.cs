using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.Constants.Admin;
using BolindersBil.Web.Infrastructure;
using BolindersBil.Web.Models;
using BolindersBil.Web.Repositories;
using BolindersBil.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BolindersBil.Web.Controllers
{
    public class AdminController : Controller
    {
        private IVehicleRepository vehicleRepo;

        public AdminController(IVehicleRepository vehicleRepository)
        {
            vehicleRepo = vehicleRepository;
        }

        // Lista alla fordon när man går in på /admin
        public IActionResult Index()
        {
            return View(vehicleRepo.Vehicles);
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
    }
}