using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /*
        Lista alla fordon när man går in på /admin
        public IActionResult Index()
        {
            return View(vehicleRepo.Vehicles);
        }
        */

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
                vm.Vehicle.DateUpdated = DateTime.Now;
                vehicleRepo.SaveVehicle(vm.Vehicle);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(vm);
            }
        }


        [HttpGet]

        public IActionResult Create(int vehicleId)
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

        // Skapa nytt fordon
        [HttpPost]
        public IActionResult Create(EditVehicleViewModel vm)
        {

            if (ModelState.IsValid)
            {
                vm.Vehicle.DateAdded = DateTime.Now;
                vehicleRepo.SaveVehicle(vm.Vehicle);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(vm);
            }
        }

        // Ta bort fordon
        [HttpPost]
        public IActionResult DeleteBulk(string vehiclesIdToDelete)
        {
            if (vehiclesIdToDelete != null)
            {
                int[] vehiclesIdToDeleteArray = Array.ConvertAll(vehiclesIdToDelete.Split(','), int.Parse);

                foreach (var item in vehiclesIdToDeleteArray)
                {
                    Delete(item);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // Ta bort fordon
        [HttpPost]
        public IActionResult Delete(int vehicleId)
        {
            var deleted = vehicleRepo.DeleteVehicle(vehicleId);
            if (deleted != null)
            {
                //product was found and deleted
            }
            else
            {
                //TODO
                //product was not found - show error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}