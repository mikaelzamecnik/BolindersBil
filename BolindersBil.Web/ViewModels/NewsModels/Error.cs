using BolindersBil.Web.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Models.NewsModels
{
    public class Error
    {
        public ErrorCodes Code { get; set; }
        public string Message { get; set; }
    }

}
