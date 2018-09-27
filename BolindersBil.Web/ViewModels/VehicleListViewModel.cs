using BolindersBil.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.ViewModels
{
    public class VehicleListViewModel
    {
        public Vehicle Vehicle { get; set; }
        public Brand Brand { get; set; }
        public Dealership Dealership { get; set; }
    }
}
