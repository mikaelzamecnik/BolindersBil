using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Infrastructure
{

    public static class FilterListItems
    {


        public static List<SelectListItem> Body { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Småbil", Text = "Småbil" },
            new SelectListItem { Value = "Sedan", Text = "Sedan" },
            new SelectListItem { Value = "Halvkombi", Text = "Halvkombi"  },
            new SelectListItem { Value = "Kombi", Text = "Kombi"  },
            new SelectListItem { Value = "SUV", Text = "SUV"  },
            new SelectListItem { Value = "Coupé", Text = "Coupé"  },
            new SelectListItem { Value = "Cab", Text = "Cab"  },
            new SelectListItem { Value = "Familjebuss", Text = "Familjebuss"  },
            new SelectListItem { Value = "Yrkesfordon", Text = "Yrkesfordon"  },
        };
        public static List<SelectListItem> Fuel { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Bensin", Text = "Bensin" },
            new SelectListItem { Value = "Diesel", Text = "Diesel" },
            new SelectListItem { Value = "El", Text = "El"  },
            new SelectListItem { Value = "Miljöbränsle", Text = "Miljöbränsle"  },
            new SelectListItem { Value = "Hybrid", Text = "Hybrid"  },
        };
        public static List<SelectListItem> Transmission { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Manuell", Text = "Manuell" },
            new SelectListItem { Value = "Automatisk", Text = "Automatisk" },
        };
    }
    }

