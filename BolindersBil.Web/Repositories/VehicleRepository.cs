using BolindersBil.Web.DataAccess;
using BolindersBil.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Repositories
{
    public class VehicleRepository: IVehicleRepository
    {
        private ApplicationDbContext ctx;

        public VehicleRepository(ApplicationDbContext context)
        {
            ctx = context;
        }

        public IEnumerable<Vehicle> Vehicles => ctx.Vehicles.Include(c => c.Brand).Include(d => d.Dealership);
        public IEnumerable<Brand> Brands => ctx.Brands;
        public IEnumerable<Dealership> Dealerships => ctx.Dealerships;

        public void SaveVehicle(Vehicle v)
        {
            if (v.Id == 0)
            {
                ctx.Vehicles.Add(v);
            }
            else
            {
                var ctxVehicle = ctx.Vehicles.FirstOrDefault(x => x.Id.Equals(v.Id));
                if (ctxVehicle != null)
                {
                    var ctxBrand = ctx.Brands.FirstOrDefault(x => x.Id.Equals(v.BrandId));
                    if (ctxBrand != null)
                    {
                        var ctxDealership = ctx.Dealerships.FirstOrDefault(x => x.Id.Equals(v.DealerShipId));
                        if(ctxDealership != null)
                        {

                            ctxVehicle.Model = v.Model;
                            ctxVehicle.ModelDescription = v.ModelDescription;
                            ctxVehicle.RegistrationNumber = v.RegistrationNumber;
                            ctxVehicle.Year = v.Year;
                            ctxVehicle.Mileage = v.Mileage;
                            ctxVehicle.Price = v.Price;
                            ctxVehicle.Body = v.Body;
                            ctxVehicle.Color = v.Color;
                            ctxVehicle.Transmission = v.Transmission;
                            ctxVehicle.Fuel = v.Fuel;
                            ctxVehicle.Horsepower = v.Horsepower;
                            ctxVehicle.Used = v.Used;
                            ctxVehicle.Lease = v.Lease;
                            ctxVehicle.ImageUrl = v.ImageUrl;

                        ctxVehicle.Dealership = ctxDealership;
                        ctxVehicle.Brand = ctxBrand;
                        }

                    }
                }
            }
            ctx.SaveChanges();
        }
        public Vehicle DeleteVehicle(int vehicleId)
        {
            var ctxVehicle = ctx.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
            if (ctxVehicle != null)
            {
                ctx.Vehicles.Remove(ctxVehicle);
                ctx.SaveChanges();
            }
            return ctxVehicle;
        }



    }
}
