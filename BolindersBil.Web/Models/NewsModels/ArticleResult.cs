using BolindersBil.Web.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Models.NewsModels
{
    public class ArticlesResult
    {
        public Statuses Status { get; set; }
        public Error Error { get; set; }
        public int TotalResults { get; set; }
        public List<Article> Articles { get; set; }
    }
}
