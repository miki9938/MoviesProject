using System;
using System.Linq;
using System.Web.Mvc;
using Movies.Mappings;
using Movies.Repositories;
using System.Web.Security;
using System.Web.Helpers;
using Movies.Models;
using System.Web;
using Movies.Security;

namespace Movies.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        private  UsersRepository dbUser;

        public UserController()
        {
            dbUser = new UsersRepository();
        }

        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(LogInUserModel user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.login, user.password))
                {
                    string userData;

                    if (dbUser.getUserByLogin(user.login).admin == true)
                        userData = "Admin";
                    else
                        userData = "User";

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        user.login,
                        DateTime.Now,
                        DateTime.Now.AddDays(1),
                        false,
                        userData,
                        FormsAuthentication.FormsCookiePath);

                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    
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
        public ActionResult Registration(RegistrationUserModel temp)
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

        [MyAuthorize]
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
