using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string Model { get; set; }
        public string ModelDescription { get; set; }
        public string RegistrationNumber { get; set; }
        public int Year { get; set; }
        public double Mileage { get; set; }
        public decimal Price { get; set; }
        public string Body { get; set; }
        public string Color { get; set; }
        public string Transmission { get; set; }
        public string Fuel { get; set; }
        public string Horsepower { get; set; }
        public bool Used { get; set; }
        public string ImageUrl { get; set; }
        public bool Lease { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public int DealerShipId { get; set; }
        public virtual Dealership Dealership { get; set; }

    }
}
