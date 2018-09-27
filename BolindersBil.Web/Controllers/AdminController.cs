using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.Infrastructure;
using BolindersBil.Web.Repositories;
using BolindersBil.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

            var vehicle = vehicleRepo.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
            var vm = new EditVehicleViewModel
            {
                DealerShips = vehicleRepo.Dealerships.ToSelectList(vehicle),
                Brands = vehicleRepo.Brands.ToSelectList(vehicle),
                Vehicle = vehicle,

            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(EditVehicleViewModel vm)
        {
            if (ModelState.IsValid)
            {
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