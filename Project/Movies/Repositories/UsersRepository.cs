using System;
using System.Data.Entity.Migrations;
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

        public bool addVote(users_vote temp)
        {
            try
            {
                db.users_vote.Add(temp);
                db.SaveChanges();
                return true;
            }

            catch
            {
               // db.users_vote.Remove(temp);

                return false;
            }            
        }

        public bool addCommentToMovie(comment temp)
        {
            try
            {
                db.comments.Add(temp);
                db.SaveChanges();
                return true;
            }

            catch
            {
                db.comments.Remove(temp);

                return false;
            }             
        }

        public bool addCommentToPerson(comment temp)
        {
            try
            {
                db.comments.Add(temp);
                db.SaveChanges();
                return true;
            }

            catch
            {
                db.comments.Remove(temp);

                return false;
            }
        }

        public int getIdbyName(string name)
        {
            return db.users.Where(u => u.login.Equals(name)).Select(u => u.id).FirstOrDefault();
        }

        public bool addAdminRights(string login, bool status)
        {
            try
            {
                user tmp = db.users.Where(u => u.login.Equals(login)).FirstOrDefault();
                tmp.admin = status;
                db.SaveChanges();
                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool deleteUserbyId(int id)
        {
            try
            {
                user tmp = db.users.Where(u => u.id.Equals(id)).FirstOrDefault();
                db.users.Remove(tmp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}