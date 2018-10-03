using BolindersBil.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.ViewModels
{
    public class VehicleViewModel
    {
        public Brand Brand { get; set; }
        public Vehicle Vehicle { get; set; }
        public Dealership Dealership { get; set; }
        public List<FileUpload> FileUpload { get; set; }
    }
}
