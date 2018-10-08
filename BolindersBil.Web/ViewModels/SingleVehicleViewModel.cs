using BolindersBil.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BolindersBil.Web.ViewModels
{
    public class SingleVehicleViewModel
    {
        public Vehicle Vehicle { get; set; }
        public IEnumerable<Vehicle> RelatedVehicles { get; set; }
        public string Url { get; set; }
        public string SendMail { get; set; }
        public int ResendId { get; set; }
    }
}
