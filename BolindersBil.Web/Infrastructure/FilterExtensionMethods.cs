using BolindersBil.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Infrastructure
{
    public static class FilterExtensionMethods
    {
        public static IEnumerable<Vehicle> FilterByPrice(this IEnumerable<Vehicle> vehicles, int minPrice = 0, int maxPrice = int.MaxValue)
        {
            foreach (var v in vehicles)
            {
                if (v.Price >= minPrice && v.Price <= maxPrice)
                {
                    yield return v;
                }
            }
        }

        public static IEnumerable<Vehicle> FilterByKm(this IEnumerable<Vehicle> vehicles, int minKm = 0, int maxKm = int.MaxValue)
        {
            foreach (var v in vehicles)
            {
                if (v.Mileage >= minKm && v.Mileage <= maxKm)
                {
                    yield return v;
                }
            }
        }

        public static IEnumerable<Vehicle> FilterByYear(this IEnumerable<Vehicle> vehicles, int minYear = 0, int maxYear= int.MaxValue)
        {
            foreach (var v in vehicles)
            {
                if (v.Year >= minYear && v.Year<= maxYear)
                {
                    yield return v;
                }
            }
        }
    }
}
