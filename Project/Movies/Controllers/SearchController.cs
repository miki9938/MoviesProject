using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Movies.Mappings;
using Movies.Models;
using Movies.Repositories;

namespace Movies.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        private MoviesRepository movieRepo = new MoviesRepository();

        [HttpPost]
        [AllowAnonymous]
        public ActionResult glassSearchSubstring(GlassSearchModel[] glassTitle)
        {
            //glassTitle.title

            IQueryable<GlassSearchModel> moviePack = new EnumerableQuery<GlassSearchModel>(movieRepo.getGlassMovieBySubstring(glassTitle[0].title));

            return Json(moviePack);
        }
    }
}
