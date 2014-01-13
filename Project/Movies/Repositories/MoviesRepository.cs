using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Movies.Repositories
{
    public class MovieRepository
    {
        MoviesEntities db;

        public MovieRepository()
        {
            db = new MoviesEntities();
            MoviesEntities asd = new MoviesEntities();
           
        }

        public bool addMovie(movie temp)
        {
            try
            {
                db.movie.Add(temp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                db.movie.Remove(temp);
                return false;
            }
        }

        public movie getMovieById(int id)
        {
            return db.movie.Where(a => a.id.Equals(id)).FirstOrDefault();
        }

        public movie getMovieByTitle(string title)
        {
            return db.movie.Where(a => a.title.Equals(title)).FirstOrDefault();
        }

        /// <summary>
        /// Wyszukuje filmy zawierające argument w tytule
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public IQueryable<movie> getMovieByTitleSubstring(string substring)
        {
            return db.movie.Take(10) 
                           .Where(a => a.title.Contains(substring));
        }

        public IQueryable<cast> getCastByMovieId(int id)
        {
           return db.cast.Where(a => a.id.Equals(id));
            
        }

        public bool deleteMovieById(int id)
        {
            try
            {
                foreach(cast c in getCastByMovieId(id))
                {
                    db.cast.Remove(c);
                }

                db.movie.Remove(getMovieById(id));

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