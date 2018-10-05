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
            FormSmtpServer = "smtp.mailserver.com";
            FormPort = 1337;
            FormUserName = "LoremIpsum";
            FormPassWord = "mycutecat";
        }

        public string NewsApiUrl { get; set; }
        public string NewsApiKey { get; set; }
        public string FormSmtpServer { get; set; }
        public int FormPort { get; set; }
        public string FormUserName { get; set; }
        public string FormPassWord { get; set; }

    }
}
