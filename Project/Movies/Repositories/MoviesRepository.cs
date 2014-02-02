using System;
using System.Drawing;
using System.Linq;
using Movies.Mappings;
using System.Collections.Generic;
using Movies.Models;
using Movies.Repositories;

namespace Movies.Repositories
{
    public class MoviesRepository
    {
        MoviesEntities db;

        public MoviesRepository()
        {
            db = new MoviesEntities();
            MoviesEntities asd = new MoviesEntities();
           
        }

        public bool addMovie(movie temp)
        {
            try
            {
                db.movies.Add(temp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                db.movies.Remove(temp);
                return false;
            }
        }

        public movie getMovieById(int id)
        {
            return db.movies.Where(a => a.id.Equals(id)).FirstOrDefault();
        }

        public movie getMovieByTitle(string title)
        {
            return db.movies.Where(a => a.title.Equals(title)).FirstOrDefault();
        }

        /// <summary>
        /// Wyszukuje filmy zawierające argument w tytule
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public IQueryable<movie> getMovieByTitleSubstring(string substring)
        {
            return db.movies.Take(10) 
                           .Where(a => a.title.Contains(substring));
        }

        public IQueryable<GlassSearchModel> getGlassMovieBySubstring(string substring)
        {
            return
                db.movies
                        .Where(x => x.title.Contains(substring))
                        .Take(10)
                        .Select(x => new GlassSearchModel
                        {
                            id = x.id,
                            title = x.title,
                            releaseDate = x.release_date.Year,
                            pictureId = db.image_movie.Where(r => r.movie_id.Equals(x.id)).Select(r => r.id).FirstOrDefault()
                        });
        }

        private IQueryable<cast> getCast(int id)
        {
           return db.casts.Where(a => a.movie_id.Equals(id));
        }

        public List<CastModel> getDirectorsByMovieId(int id)
        {
            PersonRepository dbPerson = new PersonRepository();

            List<CastModel> tempList = new List<CastModel>();

            IQueryable<cast> temp = getCast(id);

            foreach(cast c in temp)
            {
                if (c.role == 1)
                {
                    person tempMen = dbPerson.getPersonById(c.person_id);

                    CastModel tempModel = new CastModel();

                    tempModel.id = tempMen.id;
                    tempModel.name = tempMen.name;
                    tempModel.photo = tempMen.image_person.Where(i => i.is_portrait.Equals(true))
                                                          .FirstOrDefault()
                                                          .id;

                    tempList.Add(tempModel);
                }
            }

            return tempList;
        }

        public List<CastModel> getWritersByMovieId(int id)
        {
            PersonRepository dbPerson = new PersonRepository();

            List<CastModel> tempList = new List<CastModel>();

            IQueryable<cast> temp = getCast(id);

            foreach (cast c in temp)
            {
                if (c.role == 3)
                {
                    person tempMen = dbPerson.getPersonById(c.person_id);

                    CastModel tempModel = new CastModel();

                    tempModel.id = tempMen.id;
                    tempModel.name = tempMen.name;
                    tempModel.photo = tempMen.image_person.Where(i => i.is_portrait.Equals(true))
                                                          .FirstOrDefault()
                                                          .id;

                    tempList.Add(tempModel);
                }
            }

            return tempList;
        }

        public List<CastModel> getActorsByMovieId(int id)
        {
            PersonRepository dbPerson = new PersonRepository();

            List<CastModel> tempList = new List<CastModel>();

            IQueryable<cast> temp = getCast(id);

            foreach (cast c in temp)
            {
                if (c.role == 2)
                {
                    person tempMen = dbPerson.getPersonById(c.person_id);

                    CastModel tempModel = new CastModel();

                    tempModel.id = tempMen.id;
                    tempModel.name = tempMen.name;
                    tempModel.photo = tempMen.image_person.Where(i => i.is_portrait.Equals(true))
                                                          .FirstOrDefault()
                                                          .id;

                    tempList.Add(tempModel);
                }
            }

            return tempList;
        }

        public bool deleteMovieById(int id)
        {
            try
            {
                IQueryable<cast> _cast = getCast(id);

                foreach(cast c in _cast)
                {
                    db.casts.Remove(c);
                }

                db.movies.Remove(getMovieById(id));

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*public List<movie> getSimilarMoviesByMovieId(int id)
        {
            IQueryable<movie_relation> temp = from a in db.movie_relation
                                              where
                                                  a.movie_1_id == id || a.movie_2_id == id
                                              select a;

            List<movie> movieList = new List<movie>();

            foreach (movie_relation tempRel in temp)
            {
                if (tempRel.movie_1_id.Equals(id))
                    movieList.Add(getMovieById(tempRel.movie_2_id));
                else
                    movieList.Add(getMovieById(tempRel.movie_1_id));
            }

            return movieList;       
        }*/

        public List<ViewSimilarMovieModel> getSimilarMoviesById(int id)
        {
            IQueryable<movie_relation> temp = from a in db.movie_relation
                                              where
                                                  a.movie_1_id == id || a.movie_2_id == id
                                              select a;

            List<ViewSimilarMovieModel> movieList = new List<ViewSimilarMovieModel>();
            

            foreach (movie_relation tempRel in temp)
            {
                if (tempRel.movie_1_id.Equals(id))
                {
                    movie temp2 = getMovieById(tempRel.movie_2_id);
                    ViewSimilarMovieModel t3 = new ViewSimilarMovieModel();

                    t3.id = temp2.id;
                    t3.description = temp2.description;
                    t3.releaseDate = temp2.release_date;
                    t3.posterId = getImagebyMovieId(t3.id);
                    t3.title = temp2.title;
                    t3.trailerLink = temp2.trailer_link;
                    t3.score = tempRel.vote_count;
                    t3.relationId = tempRel.id;

                    movieList.Add(t3);
                }


                else
                {
                    movie temp2 = getMovieById(tempRel.movie_1_id);
                    ViewSimilarMovieModel t3 = new ViewSimilarMovieModel();

                    t3.id = temp2.id;
                    t3.description = temp2.description;
                    t3.releaseDate = temp2.release_date;
                    t3.posterId = getImagebyMovieId(t3.id);
                    t3.title = temp2.title;
                    t3.trailerLink = temp2.trailer_link;
                    t3.score = tempRel.vote_count;

                    movieList.Add(t3);
                }
            }

            return movieList;
        }

        public bool AddCastToMovie(int personId, int role, int movieId)
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

        public bool AddCastToMovie(int personId, int role, int movieId, string characterName)
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

        public bool AddCastToMovie(cast temp)
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

        public bool AddNewSimilarMovie(movie_relation temp)
        {
            try
            {
                db.movie_relation.Add(temp);
                db.SaveChanges();

                return true;
            }
            catch
            {
                db.movie_relation.Remove(temp);

                return false;
            }                  
        }

        public movie_relation getMovieRelationById(int id)
        {
            return db.movie_relation.Where(r => r.id.Equals(id)).FirstOrDefault();
        }

        public movie_relation getMovieRelationByMoviesIds(int firstMovieId, int secondMovieId)
        {
            IQueryable<movie_relation> temp = from a in db.movie_relation
                                              where
                                                (a.movie_1_id == firstMovieId && a.movie_2_id == secondMovieId)
                                                    ||
                                                (a.movie_1_id == secondMovieId && a.movie_2_id == firstMovieId)
                                              select a;

            movie_relation movieRelation = new movie_relation();
            movieRelation = temp.FirstOrDefault();

            return movieRelation;               
        }

        public bool addImageToMovie(image_movie temp)
        {
            try
            {
                db.image_movie.Add(temp);
                db.SaveChanges();

                return true;
            }
            catch
            {
                db.image_movie.Remove(temp);

                return false;
            }         
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

        public string getImagebyMovieId(int id)
        {
            return db.image_movie.Where(r => r.movie_id.Equals(id)).Select(r => r.id).FirstOrDefault().ToString();
        }

        public Guid getPosterNameByMovieId(int id)
        {
            var temp = from a in db.image_movie
                       where
                           (a.movie_id == id)
                                &&
                           (a.is_poster == true)
                       select a.id;

            return temp.FirstOrDefault();
        }

        public List<Guid> getMovieImagesNamesByMovieId(int id)
        {
            IQueryable<image_movie> temp = db.image_movie.Where(m => m.movie_id.Equals(id));

            List<Guid> tempList = new List<Guid>();

            foreach (image_movie a in temp)
            {
                tempList.Add(a.id);
            }

            return tempList;
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

        public List<comment> getCommentsByMovieId(int movieId)
        {
            List<comment> commentsList = new List<comment>();

            foreach (comment comment in db.comments.Where(c => c.movie_id == movieId))
            {
                commentsList.Add(comment);
            }
            return commentsList;
        }

        public IQueryable<comment> getCommentsById(int id)
        {
            return db.comments.Where(c => c.id.Equals(id));
        }

        public bool deleteCommentById(int id)
        {
            try
            {
                comment tmp = db.comments.Where(u => u.id.Equals(id)).FirstOrDefault();
                db.comments.Remove(tmp);
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