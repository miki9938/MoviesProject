using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Movies.Repositories;
using Movies.Models;
using Movies.Mappings;
using Movies.Security;


namespace Movies.Controllers
{
    public enum movieRoles { director = 1, actor = 2, writer = 3 }

    public class MovieController : Controller
    {
        //
        // GET: /Movie/

        private MoviesRepository dbMovie;
        private PersonRepository dbPerson;
        private UsersRepository dbUser;

        public MovieController()
        {
            dbMovie = new MoviesRepository();
            dbPerson = new PersonRepository();
            dbUser = new UsersRepository();
        }

        /// <summary>
        /// Funkcja zwraca Viebag'a z danymi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Show(int id)
        {
            movie temp = dbMovie.getMovieById(id);
            
            ///Tytuł
            ViewBag.Name = temp.title;
            ///Fabuła
            ViewBag.Plot = temp.description;
            ///Data wydania
            ViewBag.Date = temp.release_date;
            ///Reżyserowie, typ List<person>
            ViewBag.Directors = dbMovie.getDirectorsByMovieId(id);
            ///Scenarzyści/pisarze, typ List<person>
            ViewBag.Writers = dbMovie.getWritersByMovieId(id);
            ///Actorzy, typ List<person>
            ViewBag.Actors = dbMovie.getActorsByMovieId(id);
            ///Podobne filmy, typ List<movies>
            ViewBag.Similars = dbMovie.getSimilarMoviesByMovieId(id); 
            //Url do plakatu
            ViewBag.Image = dbMovie.getImagebyMovieId(id);
            //id filmu
            ViewBag.MovieId = id;
            
            return View();
        }

        [MyAuthorize(Roles="Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [MyAuthorize(Roles = "Admin")]
        public ActionResult Add(AddNewMovieModel newMoive)
        {
            movie temp = new movie();

            temp.title = newMoive.title;
            temp.release_date = newMoive.releaseDate;
            temp.description = newMoive.plot;

            dbMovie.addMovie(temp);

            return View();
        }

        [MyAuthorize(Roles="Admin")]
        public ActionResult AddCast()
        {
            return View();
        }

        [HttpPost]
        [MyAuthorize(Roles="Admin")]
        public ActionResult AddCast(AddCastToMovieModel newCast)
        {
            cast temp = new cast();

            temp.movie_id = newCast.movieId;
            temp.person_id = dbPerson.getPersonByName(newCast.personName.ToLower().Trim()).id;
            temp.role = (int)Enum.Parse(typeof(movieRoles), newCast.role.ToLower().Trim());
            temp.character_name = newCast.characterName;

            dbMovie.AddCastToMovie(temp);

            return View();
        }

        [MyAuthorize]
        public ActionResult addSimilarMovie(int firstMovieId)
        {
            ViewBag.firstMovieId = firstMovieId;
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            ViewBag.userId = dbUser.getIdbyName(ticket.Name);
            return View();        
        }

        [HttpPost]
        [MyAuthorize]
        public ActionResult addSimilarMovie(AddSimilarMovieModel newSimilar)
        {
            movie_relation temp = new movie_relation();

            int secondMovieId = dbMovie.getMovieByTitle(newSimilar.secondMovieTitle.ToLower().Trim()).id;

            temp.movie_1_id = newSimilar.firstMovieId;
            temp.movie_2_id = secondMovieId;
            temp.auto_created = false;

            dbMovie.AddNewSimilarMovie(temp);

            users_vote tempVote = new users_vote();

            tempVote.vote = true;
            tempVote.user_id = newSimilar.userId;
            tempVote.movie_relation = dbMovie.getMovieRelationByMoviesIds(newSimilar.firstMovieId, secondMovieId);

            dbUser.addVote(tempVote);

            return RedirectToAction("Show", "Movie", new{ id = newSimilar.firstMovieId});
        }

        [HttpGet]
        [MyAuthorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            dbMovie.deleteMovieById(id);

            return View();
        }

        [MyAuthorize(Roles = "Admin")]
        public ActionResult AddImage()
        {
            return View();
        }

        [MyAuthorize]
        public ActionResult addComment()
        {
            return View();   
        }

        [HttpPost]
        [MyAuthorize]
        public ActionResult addComment(AddCommentToMovieModel newComment)
        {
            comment temp = new comment();

            temp.date = DateTime.Now;
            temp.movie_id = newComment.movieId;
            temp.user_id = newComment.userId;
            temp.text = newComment.comment;

            dbUser.addCommentToMovie(temp);

            return View();
        }

        [HttpPost]
        [MyAuthorize]
        public ActionResult UploadImage(AddImageToMovieModel imagePack)
        {
            if (imagePack != null)
            {
                image_movie temp = new image_movie();
                Guid newId = Guid.NewGuid();

                temp.id = newId;
                temp.movie_id = imagePack.movieId;
                temp.source = imagePack.source;
                temp.is_poster = imagePack.isPoster;

                if (dbMovie.addImageToMovie(temp).Equals(true))
                {
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/uploaded"),
                        temp.id.ToString() + ".png");
                    imagePack.file.SaveAs(path);
                }
            }
            return RedirectToAction("AddImage", "Movie");
        }
    }
}