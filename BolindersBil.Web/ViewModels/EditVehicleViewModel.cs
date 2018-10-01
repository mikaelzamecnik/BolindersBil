using BolindersBil.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.ViewModels
{
    public class EditVehicleViewModel
    {

        public Vehicle Vehicle { get; set; }
        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> DealerShips { get; set; }


    }
}
