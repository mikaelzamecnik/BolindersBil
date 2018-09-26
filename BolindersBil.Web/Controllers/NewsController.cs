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

namespace BolindersBil.Web.Controllers
{
    public class NewsController : Controller
    {



        public IActionResult Index(ArticlesResult articlesResult)
        {

            var newsApiClient = new NewsApiClient("e6e66d30aaba4a1b8ad9c4a17a1a4117");

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