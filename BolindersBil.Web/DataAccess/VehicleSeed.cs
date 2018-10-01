using BolindersBil.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.Repositories;

namespace BolindersBil.Web.DataAccess
{
    public static class VehicleSeed
    {
        internal static void FillIfEmpty(ApplicationDbContext ctx)
        {
            if (!ctx.Brands.Any())
            {
                ctx.Brands.Add(new Brand { Name = "Audi", LogoUrl = "http://www.carlogos.org/logo/Audi-emblem-2016-black-1920x1080.png" });
                ctx.Brands.Add(new Brand { Name = "Volvo", LogoUrl = "http://www.carlogos.org/logo/Volvo-logo-2014-1920x1080.png" });
                ctx.Brands.Add(new Brand { Name = "Saab", LogoUrl = "http://www.carlogos.org/logo/Saab-logo-2013-2000x450.png" });
                ctx.Brands.Add(new Brand { Name = "Volkswagen", LogoUrl = "http://www.carlogos.org/logo/Volkswagen-logo-2015-1920x1080.png" });
                ctx.Brands.Add(new Brand { Name = "Ford", LogoUrl = "http://www.carlogos.org/logo/Ford-logo-2003-1366x768.png" });
                ctx.Brands.Add(new Brand { Name = "BMW", LogoUrl = "http://www.carlogos.org/logo/BMW-logo-2000-2048x2048.png" });
                ctx.Brands.Add(new Brand { Name = "Mercedes-Benz", LogoUrl = "http://www.carlogos.org/logo/Mercedes-Benz-logo-2011-1920x1080.png" });
                ctx.Brands.Add(new Brand { Name = "Peugeot", LogoUrl = "http://www.carlogos.org/logo/Peugeot-logo-2010-1920x1080.png" });
                ctx.SaveChanges();

            }
            if (!ctx.Dealerships.Any())
            {
                ctx.Dealerships.Add(new Dealership { Name = "BB1", City = "Jönköping", Phone = "036-123456" });
                ctx.Dealerships.Add(new Dealership { Name = "BB2", City = "Värnamo", Phone = "0370-123456" });
                ctx.Dealerships.Add(new Dealership { Name = "BB3", City = "Göteborg", Phone = "031-123456" });
                ctx.SaveChanges();

            }


            if (!ctx.Vehicles.Any())
            {
                var vehicles = new List<Vehicle>{
                    new Vehicle {Model = "V60", ModelDescription="T4 Business", RegistrationNumber="ABC123", Year = "1998", Mileage = 142323, Price = 129999, Body="Småbil", Color="Grön", Transmission="Manuell", Fuel="Bensin", Horsepower="198", Used=false, DateAdded=DateTime.Now, DateUpdated=DateTime.Now, ImageUrl="https://via.placeholder.com/350x150", Lease=true, BrandId=1, DealerShipId=2},
                    new Vehicle {Model = "V70", ModelDescription="Turbo", RegistrationNumber="DEF456", Year = "1997", Mileage = 1340, Price = 57000, Body="Sedan", Color="Blå", Transmission="Manuell", Fuel="Diesel", Horsepower="433", Used=true, DateAdded=DateTime.Now, DateUpdated=DateTime.Now, ImageUrl="https://via.placeholder.com/350x150", Lease=false, BrandId=2, DealerShipId=2},
                    new Vehicle {Model = "V50", ModelDescription="Elegance", RegistrationNumber="ABC223", Year = "1998", Mileage = 323, Price = 2300, Body="Yrkesfordon", Color="Vit", Transmission="Manuell", Fuel="El", Horsepower="199", Used=false, DateAdded=DateTime.Now, DateUpdated=null, ImageUrl="https://via.placeholder.com/350x150", Lease=true, BrandId=3, DealerShipId=2},
                    new Vehicle {Model = "M3", ModelDescription="4x4", RegistrationNumber="CBF321", Year = "1982", Mileage = 45423, Price = 12000, Body="Familjebuss", Color="Rosa", Transmission="Automatisk", Fuel="Miljöbränsle", Horsepower="100", Used=false, DateAdded=DateTime.Now,DateUpdated=null, ImageUrl="https://via.placeholder.com/350x150", Lease=false, BrandId=5, DealerShipId=1},
                    new Vehicle {Model = "Corsa", ModelDescription="Turbo", RegistrationNumber="ARC153", Year = "2019", Mileage = 1342, Price = 199999, Body="Coupé", Color="Svart", Transmission="Manuell", Fuel="Hybrid", Horsepower="54", Used=true, DateAdded=DateTime.Now, DateUpdated=null, ImageUrl="https://via.placeholder.com/350x150", Lease=true, BrandId=6, DealerShipId=2},
                    new Vehicle {Model = "Mondeo", ModelDescription="Turbo", RegistrationNumber="CLA322", Year = "70-talet", Mileage = 45423, Price = 1809999, Body="Sedan", Color="Röd", Transmission="Automatisk", Fuel="Bensin", Horsepower="32", Used=false, DateAdded=DateTime.Now,DateUpdated=null, ImageUrl="https://via.placeholder.com/350x150", Lease=false, BrandId=7, DealerShipId=1},
                    new Vehicle {Model = "CLK", ModelDescription="Avantgarde", RegistrationNumber="AGC154", Year = "1998", Mileage = 1345523, Price = 34534, Body="SUV", Color="Grå", Transmission="Manuell", Fuel="Diesel", Horsepower="550", Used=true, DateAdded=DateTime.Now, DateUpdated=null, ImageUrl="https://via.placeholder.com/350x150", Lease=true, BrandId=8, DealerShipId=2},
                    new Vehicle {Model = "CLS", ModelDescription="Turbo", RegistrationNumber="CBJ324", Year = "1982", Mileage = 4223, Price = 32333, Body="Cab", Color="Svart", Transmission="Automatisk", Fuel="El", Horsepower="26", Used=false, DateAdded=DateTime.Now,DateUpdated=null, ImageUrl="https://via.placeholder.com/350x150", Lease=false, BrandId=5, DealerShipId=3},
                    new Vehicle {Model = "V90", ModelDescription="Lyx", RegistrationNumber="ABK125", Year = "1994", Mileage = 1887323, Price = 45533, Body="Halvkombi", Color="Blå", Transmission="Manuell", Fuel="Hybrid", Horsepower="99", Used=true, DateAdded=DateTime.Now, DateUpdated=null, ImageUrl="https://via.placeholder.com/350x150", Lease=true, BrandId=1, DealerShipId=2},
                    new Vehicle {Model = "S70", ModelDescription="Turbo", RegistrationNumber="EDR321", Year = "1989", Mileage = 452223, Price = 2323, Body="Kombi", Color="Vit", Transmission="Automatisk", Fuel="Bensin", Horsepower="50", Used=false, DateAdded=DateTime.Now,DateUpdated=null, ImageUrl="https://via.placeholder.com/350x150", Lease=false, BrandId=5, DealerShipId=3},

                };

                ctx.Vehicles.AddRange(vehicles);
                ctx.SaveChanges();

            }


        }
    }
}
