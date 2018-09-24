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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Dealership> Dealerships { get; set; }
    }

}
