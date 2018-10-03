using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Categories
    {
        Business,
        Entertainment,
        Health,
        Science,
        Sports,
        Technology
    }
}
