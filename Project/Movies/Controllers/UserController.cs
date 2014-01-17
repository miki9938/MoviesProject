using System;
using System.Linq;
using System.Web.Mvc;
using Movies.Mappings;
using Movies.Repositories;
using System.Web.Security;
using System.Web.Helpers;

namespace Movies.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        UsersRepository dbUser;

        public UserController()
        {
            dbUser = new UsersRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(Models.LogInUserModel user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.login, user.password))
                {
                    FormsAuthentication.SetAuthCookie(user.login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login and password doesn't match");
                }
            }

            return View(user);
        }

        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Models.RegistrationUserModel temp)
        {
            if (ModelState.IsValid)
            {
                user newUser = new user();
                newUser.login = temp.login;
                newUser.password =  Crypto.HashPassword(temp.password);
                newUser.admin = false;
                newUser.email = temp.email;

                if (dbUser.addUser(newUser))
                {
                    return RedirectToAction("LogIn", "User");
                }

                ModelState.AddModelError("", "Login or email address is already taken");
            }

            return View(temp);
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string login, string password)
        {
            user temp = dbUser.getUserByLogin(login);

            bool IsValid = false;

            if ((login != null) && (password != null) && (temp != null))
            {
                if(Crypto.VerifyHashedPassword(temp.password, password))
                {
                    IsValid = true;
                }
            }

            return IsValid;
        }
    }
}
