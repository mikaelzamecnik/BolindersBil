﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Models
{
    public class FileUpload
    {
        public int Id { get; set; }
        public Guid FileTitle { get; set; }
        //public string Suffix { get; set; }
        public string FilePath { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        //public virtual Vehicle Vehicle { get; set; }
    }
}
