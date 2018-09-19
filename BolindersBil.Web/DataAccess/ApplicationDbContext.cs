using BolindersBil.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        DbSet<Vehicle> Vehicles {get;set;}
        DbSet<Brand> Brands { get; set; }
        DbSet<Dealership> Dealerships { get; set; }
        DbSet<VehicleDealership> VehicleDealerships { get; set; }


    }
}
