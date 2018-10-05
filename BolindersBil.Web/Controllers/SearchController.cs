using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.Repositories;
using BolindersBil.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index(string searchString, int page = 1)
        {
            // LINQ query to select vehicles
            //var vehicles = from v in vehicleRepo.Vehicles select v;

            var vehicles = vehicleRepo.Vehicles;

            // If the searchstring parameter contains a string the vehicle
            // query is modified to filter on the value of the search string
            // TODO
            /* orderby lambda expression for filter */
            if (!String.IsNullOrEmpty(searchString))
            {
                string[] searchStringWords = searchString.Split();

                foreach (string word in searchStringWords)
                {
                    {
                        vehicles = vehicles.Where(s =>
                            s.Brand.Name.Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            s.Model.Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            s.RegistrationNumber.Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            s.ModelDescription.Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            //s.Attributes.Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            s.Horsepower.Contains(word, StringComparison.InvariantCultureIgnoreCase) ||
                            s.Color.Contains(word, StringComparison.InvariantCultureIgnoreCase)
                        );
                    }
                }
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
    }
}