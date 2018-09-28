using BolindersBil.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Infrastructure
{
    public static class VehicleExtensions
    {
        public static List<SelectListItem> ToSelectList(this IEnumerable<Brand> brands, Vehicle v = null)
        {
            var list = brands.Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == v?.BrandId)

                }).ToList();

            return list;


        }
        public static List<SelectListItem> ToSelectList(this IEnumerable<Dealership> dealerships, Vehicle v = null)
        {
            var list = dealerships.Select(
                x => new SelectListItem
                {
                    Text = x.City,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == v?.DealerShipId)

                }).ToList();

            return list;


        }
    }
}
