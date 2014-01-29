using System;
using System.Linq;
using Movies.Mappings;
using Movies.Models;
using System.Collections.Generic;
using Movies.Repositories;

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

        public IQueryable<GlassSearchModel> getGlassPersonByNameSubstring(string substring)
        {
            return db.people
                            .Where(a => a.name.Contains(substring))
                            .Take(10)
                            .Select(x => new GlassSearchModel
                            {
                                id = x.id,
                                title = x.name
                            });
        }

        public IQueryable<cast> getCastByPersonId(int id)
        {
            return db.casts.Where(a => a.id.Equals(id));

        }

        public List<movie> getRolesByPersonId(int id)
        {
            MoviesRepository dbMovie = new MoviesRepository();

            List<movie> tempList = new List<movie>();

            IQueryable<cast> temp = getCastByPersonId(id);

            foreach (cast c in temp)
            {
                tempList.Add(dbMovie.getMovieById(c.movie_id));
            }

            return tempList;            
        }

        public bool deletePersonById(int id)
        {
            try
            {
                IQueryable<cast> _cast = getCastByPersonId(id);

                foreach (cast c in _cast)
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

        public bool addImageToPerson(image_person temp)
        {
            try
            {
                db.image_person.Add(temp);
                db.SaveChanges();

                return true;
            }
            catch
            {
                db.image_person.Remove(temp);

                return false;
            }
        }

        public Guid getPortraitNameByPersonId(int id)
        {
            var temp = from a in db.image_person
                       where
                           (a.perosn_id == id)
                                &&
                           (a.is_portrait == true)
                       select a.id;

            return temp.FirstOrDefault();
        }

        public List<Guid> getMovieImagesNamesByPersonId(int id)
        {
            IQueryable<image_person> temp = db.image_person.Where(m => m.perosn_id.Equals(id));

            List<Guid> tempList = new List<Guid>();

            foreach (image_person a in temp)
            {
                tempList.Add(a.id);
            }

            return tempList;
        }
    }
}