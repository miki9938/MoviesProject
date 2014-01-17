using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Movies.Repositories
{
    public class UsersRepository
    {
        MoviesEntities db;

        public UsersRepository()
        {
            db = new MoviesEntities();
        }

        public user getUserById(int id)
        {
            return db.user.Where(u => u.id.Equals(id)).FirstOrDefault();
        }

        public user getUserByEmail(string email)
        {
            return db.user.Where(u => u.email.Equals(email)).FirstOrDefault();
        }

        public user getUserByLogin(string login)
        {
            return db.user.Where(u => u.login.Equals(login)).FirstOrDefault();
        }

        public bool addUser(user temp)
        {
            try
            {
                db.user.Add(temp);
                db.SaveChanges();
                return true;
            }

            catch
            {
                db.user.Remove(temp);

                return false;
            }
        }  
    }
}