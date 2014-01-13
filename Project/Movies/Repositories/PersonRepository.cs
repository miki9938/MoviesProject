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

        public person getPersonById(int id)
        { 
            return db.person.Where(a => a.id.Equals(id)).FirstOrDefault();
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

        public IQueryable<person> getPersonByNameSubstring(string substring)
        {
            return db.person.Take(10)
                            .Where(a => a.name.Contains(substring));
        }

        public IQueryable<cast> getCastByPersonId(int id)
        {
            return db.cast.Where(a => a.id.Equals(id));

        }

        public bool deletePersonById(int id)
        {
            try
            {
                foreach (cast c in getCastByPersonId(id))
                {
                    db.cast.Remove(c);
                }

                db.person.Remove(getPersonById(id));

                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool addRole(int personId, int role, int movieId)
        {
            cast temp = new cast();

            temp.person_id = personId;
            temp.role = role;
            temp.movie_id = movieId;

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

        public bool addRole(int personId, int movieId, int role, string characterName)
        {
            cast temp = new cast();

            temp.person_id = personId;
            temp.role = role;
            temp.movie_id = movieId;
            temp.character_name = characterName;

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

        public cast getRoleById(int castId)
        { 
            return db.cast.Where(a => a.id.Equals(castId)).FirstOrDefault();
        }

        public void deleteRole(int castId)
        {
            db.cast.Remove(getRoleById(castId));
        }

    }
}