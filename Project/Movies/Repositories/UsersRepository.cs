using System;
using System.Linq;

using Movies.Mappings;

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
            return db.users.Where(u => u.id.Equals(id)).FirstOrDefault();
        }

        public user getUserByEmail(string email)
        {
            return db.users.Where(u => u.email.Equals(email)).FirstOrDefault();
        }

        public user getUserByLogin(string login)
        {
            return db.users.Where(u => u.login.Equals(login)).FirstOrDefault();
        }

        public bool addUser(user temp)
        {
            try
            {
                db.users.Add(temp);
                db.SaveChanges();
                return true;
            }

            catch
            {
                db.users.Remove(temp);

                return false;
            }
        }  
    }
}