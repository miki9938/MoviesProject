using System;
using System.Linq;
using Movies.Mappings;

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
                db.people.Add(temp);
                db.SaveChanges();
                return true;}
            catch
            {
                db.people.Remove(temp);
                return false;
            }
        }

        public person getPersonById(int id)
        { 
            return db.people.Where(a => a.id.Equals(id)).FirstOrDefault();
        }

        public bool addRole(cast temp)
        {
            try
            {
                db.casts.Add(temp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                db.casts.Remove(temp);
                return false;
            }                    
        }

        public person getPersonByName(string name)
        {
            return db.people.Where(a => a.name.Equals(name)).FirstOrDefault();
        }

        public IQueryable<person> getPersonByNameSubstring(string substring)
        {
            return db.people.Take(10)
                            .Where(a => a.name.Contains(substring));
        }

        public IQueryable<cast> getCastByPersonId(int id)
        {
            return db.casts.Where(a => a.id.Equals(id));

        }

        public bool deletePersonById(int id)
        {
            try
            {
                foreach (cast c in getCastByPersonId(id))
                {
                    db.casts.Remove(c);
                }

                db.people.Remove(getPersonById(id));

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
                db.casts.Add(temp);
                db.SaveChanges();

                return true;
            }
            catch
            {
                db.casts.Remove(temp);

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
                db.casts.Add(temp);
                db.SaveChanges();

                return true;
            }
            catch
            {
                db.casts.Remove(temp);

                return false;
            }
        }

        public cast getRoleById(int castId)
        { 
            return db.casts.Where(a => a.id.Equals(castId)).FirstOrDefault();
        }

        public void deleteRole(int castId)
        {
            db.casts.Remove(getRoleById(castId));
        }

    }
}