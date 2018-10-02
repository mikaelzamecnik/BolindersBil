using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Models
{
    public class CustomAppSettings
    {

        public CustomAppSettings()
        {
            //edit values in appsettings.json
            NewsApiUrl = "Http://www.apikeys.nu";
            NewsApiKey = "getyoureownapikey";
        }

        public string NewsApiUrl { get; set; }
        public string NewsApiKey { get; set; }
    }
}
