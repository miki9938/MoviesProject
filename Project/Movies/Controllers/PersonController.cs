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
        private UsersRepository dbUser;

        public PersonController()
        {
            dbPerson = new PersonRepository();
            dbMovie = new MoviesRepository();
            dbUser = new UsersRepository();
        }

        [AllowAnonymous]
        public ActionResult People(int id)
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

        [MyAuthorize(Roles = "Admin")]
        public ActionResult addImageToPerson()
        {
            return View();
        }

        [HttpPost]
        [MyAuthorize(Roles = "Admin")]
        public ActionResult addImageToPerson(AddImageToPersonModel newImage)
        {
            image_person temp = new image_person();
            Guid newId = Guid.NewGuid();

            temp.id = newId;
            temp.perosn_id = newImage.personId;
            temp.source = newImage.source;
            temp.is_portrait = newImage.isPortrait;

            if (dbPerson.addImageToPerson(temp).Equals(true))
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/uploaded"),
                    temp.id.ToString() + ".png");
                newImage.file.SaveAs(path);
            }

            return RedirectToAction("AddImageToPerson", "Person");
        }

        [MyAuthorize]
        public ActionResult addComment()
        {
            return View();
        }

        [HttpPost]
        [MyAuthorize]
        public ActionResult addComment(AddCommentToPersonModel newComment)
        {
            comment temp = new comment();

            temp.date = DateTime.Now;
            temp.movie_id = newComment.personId;
            temp.user_id = newComment.userId;
            temp.text = newComment.comment;

            dbMovie.addCommentToMovie(temp);

            return View();
        }

        [HttpPost]
        [MyAuthorize]
        public ActionResult UploadImage(AddImageToPersonModel imagePack)
        {
            if (imagePack != null)
            {
                image_person temp = new image_person();
                Guid newId = Guid.NewGuid();

                temp.id = newId;
                temp.perosn_id = imagePack.personId;
                temp.source = imagePack.source;
                temp.is_portrait = imagePack.isPortrait;

                if (dbMovie.addImageToPerson(temp).Equals(true))
                {
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/uploaded"),
                        temp.id.ToString() + ".png");
                    imagePack.file.SaveAs(path);
                }
            }
            return RedirectToAction("AddImageToPerson", "Person");
        }

        [MyAuthorize(Roles = "Admin")]
        public ActionResult AddImageToPerson()
        {
            return View();
        }
    }
}
