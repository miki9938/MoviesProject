using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movies.Repositories;
using Movies.Security;
using Movies.Mappings;
using Movies.Models;

namespace Movies.Controllers
{
    public class PersonController : Controller
    {
        //
        // GET: /Person/

        private PersonRepository dbPerson;
        private MoviesRepository dbMovie;

        public PersonController()
        {
            dbPerson = new PersonRepository();
            dbMovie = new MoviesRepository();
        }


        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Movies(int id)
        {
            person temp = dbPerson.getPersonById(id);

            ///Imie i Nazwisko
            ViewBag.Name = temp.name;
            ///Biografia
            ViewBag.Biography = temp.description;
            ///Data urodzenia
            ViewBag.BirthDate = temp.birth_date;
            ///Filmografia typ: List<movie>
            ViewBag.Casts = dbPerson.getRolesByPersonId(id);

            return View();
        }

        [MyAuthorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [MyAuthorize(Roles = "Admin")]
        public ActionResult Add(AddNewPersonModel newPerson)
        {
            person temp = new person();

            temp.name = newPerson.name;
            temp.birth_date = newPerson.birthDate;
            temp.birth_place = newPerson.birthPlace;
            temp.description = newPerson.biography;

            dbPerson.addNewPerson(temp);

            return View();
        }

        [MyAuthorize(Roles="Admin")]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [MyAuthorize(Roles="Admin")]
        public ActionResult AddRole(AddNewRoleModel newCast)
        {
            cast temp = new cast();

            temp.person_id = newCast.personId;
            temp.movie_id = dbMovie.getMovieByTitle(newCast.movieTitle).id;
            temp.role =  (int)Enum.Parse(typeof(movieRoles), newCast.role.ToLower().Trim());
            temp.character_name = newCast.characterName;

            dbPerson.addRole(temp);

            return View();
        }

        [HttpGet]
        [MyAuthorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            dbPerson.deletePersonById(id);

            return View();
        }
    }
}
