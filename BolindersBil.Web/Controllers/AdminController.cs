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
            if(!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(s => 
                    s.Model.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) || 
                    s.RegistrationNumber.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
                );
            }

            return View(vehicles);
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

        // Ta bort fordon
        public IActionResult Delete()
        {
            return View();
        }
    }
}