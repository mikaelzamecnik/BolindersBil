using BolindersBil.Web.Models;
using BolindersBil.Web.Models.NewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.ViewModels
{
    public class VehicleListViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public List<int> BrandsInStock { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public ArticlesResult ArticlesResults { get; set; }
        public bool ShowButton { get; set; }
        public int NextPage { get; set; }
    }
}
