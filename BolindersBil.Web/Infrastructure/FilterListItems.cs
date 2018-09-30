using BolindersBil.Web.Constants.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Infrastructure
{

    public static class FilterListItems
    {
        public static List<SelectListItem> Bodies { get; } = new List<SelectListItem>
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
    }
    
       
        



    }

