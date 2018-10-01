using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.Repositories;
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
        public IActionResult Edit()
        {
            return View();
        }

        // Skapa nytt fordon
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteBulk(string vehiclesIdToDelete)
        {
            int[] vehiclesIdToDelete2 = Array.ConvertAll(vehiclesIdToDelete.Split(','), int.Parse);

            foreach (var item in vehiclesIdToDelete2)
            {
                Delete(item);
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