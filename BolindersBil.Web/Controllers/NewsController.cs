using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BolindersBil.Web.Constants;
using BolindersBil.Web.Infrastructure;
using BolindersBil.Web.Models;
using BolindersBil.Web.Models.NewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BolindersBil.Web.Controllers
{
    public class NewsController : Controller
    {

        private CustomAppSettings _appSettings;

        public NewsController(IOptions<CustomAppSettings> settings)
        {
            _appSettings = settings.Value;
        }

        [Route("nyheter")]
        public IActionResult Index(ArticlesResult articlesResult)
        {
            //Use appsettings.json to change api url and key
            var newsApiClient = new NewsApiClient (_appSettings.NewsApiKey, _appSettings.NewsApiUrl);

            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Sources = { "the-new-york-times" },
                Q = "Apple",
                SortBy = SortBys.PublishedAt,
                Language = Languages.EN,
                From = new DateTime(2018, 09, 24)
            });


                return View(articlesResponse);







        }
    }
}