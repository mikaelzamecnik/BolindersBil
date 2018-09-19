using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Models
{
    public class VehicleDealership
    {
        public int Id { get; set; }

        public Vehicle VehicleId { get; set; }
        public Dealership DealershipId { get; set; }


    }
}
