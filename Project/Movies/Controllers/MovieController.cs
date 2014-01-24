using System;
using System.Web;
using System.Web.Mvc;
using Movies.Repositories;
using Movies.Mappings;
using Movies.Security;


namespace Movies.Controllers
{
    public class MovieController : Controller
    {
        //
        // GET: /Movie/

        private MoviesRepository dbMovie;

        public MovieController()
        {
            dbMovie = new MoviesRepository();        
        }

        /// <summary>
        /// Funkcja zwraca Viebag'a z danymi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Movies(int id)
        {
            movie temp = dbMovie.getMovieById(id);
            
            ///Tytuł
            ViewBag.Name = temp.title;
            ///Fabuła
            ViewBag.Plot = temp.description;
            ///Data wydania
            ViewBag.Date = temp.release_date;
            ///Obsada, typ IQueryable
            ViewBag.Cast = dbMovie.getCastByMovieId(id);
            ///Podobne filmy, typ List<movies>
            ViewBag.Similars = dbMovie.getSimilarMoviesByMovieId(id); 
            
            return View();
        }

        [MyAuthorize(Roles="Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [MyAuthorize(Roles = "Admin")]
        public ActionResult Add(movie newMoive)
        {
            return View();
        }

        [HttpGet]
        [MyAuthorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}