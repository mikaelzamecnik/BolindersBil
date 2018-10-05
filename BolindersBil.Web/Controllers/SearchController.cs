using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.Repositories;
using BolindersBil.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using BolindersBil.Web.Infrastructure;

namespace BolindersBil.Web.Controllers
{
    public class SearchController : Controller
    {

        private IVehicleRepository vehicleRepo;
        public int PageLimit = 8;

        public SearchController(IVehicleRepository vehicleRepository)
        {
            vehicleRepo = vehicleRepository;
        }

        [Route("sök")]
        public IActionResult Index(string searchString, bool? used = null, int minYear = 0, int maxYear = int.MaxValue, int minPrice = 0, int maxPrice = int.MaxValue,
           int minKm = 0, int maxKm = int.MaxValue, string fuel = null, string body = null, string transmission = null, int page = 1)
        {
            var vehicles = vehicleRepo.Vehicles;

            if (!String.IsNullOrEmpty(searchString))
            {
                string[] searchStringWords = searchString.Split();

                foreach (string word in searchStringWords)
                {
                    {
                        vehicles = vehicles.Where(s =>
                            (s.Brand.Name ?? "").Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            (s.Model ?? "").Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            (s.RegistrationNumber ?? "").Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            (s.ModelDescription ?? "").Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            (s.Attributes ?? "").Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            (s.Horsepower ?? "").Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            (s.Color ?? "").Contains(word, StringComparison.InvariantCultureIgnoreCase)
                        );
                    }
                }
            }

            var filtered1 = vehicles.FilterByPrice(minPrice, maxPrice);
            var filtered2 = filtered1.FilterByKm(minKm, maxKm);
            var filtered3 = filtered2.FilterByYear(minYear, maxYear);

            if (used != null)
            {
                filtered3 = filtered3.Where(s =>
                                s.Used.Equals(used));
            }

            if (fuel != null)
            {
                filtered3 = filtered3.Where(s =>
                                s.Fuel.Equals(fuel));
            }

            if (body != null)
            {
                filtered3 = filtered3.Where(s =>
                                s.Body.Equals(body));
            }

            if (transmission != null)
            {
                filtered3 = filtered3.Where(s =>
                                s.Transmission.Equals(transmission));
            }

            var toSkip = (page - 1) * PageLimit;
            var vehiclesInPageLimit = filtered3
                .OrderBy(x => x.Id)
                .Skip(toSkip)
                .Take(PageLimit);

            var totalNumberOfVehicles = filtered3.Count();

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
    }
}