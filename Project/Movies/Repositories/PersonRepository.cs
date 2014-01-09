using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Repositories
{
    public class PersonRepository
    {
        MoviesEntities db;

        public PersonRepository()
        {
            db = new MoviesEntities();
        }

        public bool addNewPerson(person temp)
        { 
            try
            {
                db.person.Add(temp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                db.person.Remove(temp);
                return false;
            }
        }

        public bool addRole(cast temp)
        {
            try
            {
                db.cast.Add(temp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                db.cast.Remove(temp);
                return false;
            }                    
        }

        public person getPersonByName(string name)
        {
            return db.person.Where(a => a.name.Equals(name)).FirstOrDefault();
        }

    }
}