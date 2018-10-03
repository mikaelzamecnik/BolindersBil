using BolindersBil.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.ViewModels
{
    public class VehicleFileUploadViewModel
    {
        public int VehicleId { get; set; }

        public FileUpload FileUpload { get; set; }
    }
}
