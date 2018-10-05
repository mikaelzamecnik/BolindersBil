using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.Repositories;
using BolindersBil.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BolindersBil.Web.Controllers
{
    public class HomeController : Controller
    {
        private IVehicleRepository vehicleRepo;
        public int PageLimit = 8;

        public HomeController(IVehicleRepository vehicleRepository)
        {
            vehicleRepo = vehicleRepository;
        }

        public IActionResult Index(string state, int page = 1)
       {
            var vehicles = vehicleRepo.Vehicles;

            if (state == "nya")
            {
                vehicles = vehicles.Where(s =>
                s.Used is false);
            }

            if (state == "begagnade")
            {
                vehicles = vehicles.Where(s =>
                s.Used is true);
            }

            var toSkip = (page - 1) * PageLimit;
            var vehiclesInPageLimit = vehicles
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
                Vehicles = vehiclesInPageLimit,
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
    }
}