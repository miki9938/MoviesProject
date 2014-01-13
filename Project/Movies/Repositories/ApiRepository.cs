using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Movies.Repositories
{
    public class ApiRepository
    {
        MoviesEntities db;


        public ApiRepository()
        {
            db = new MoviesEntities();
        }

        public XDocument getXmlByMovieId(int id)
        { 
            
            movie temp = db.movie.Where(a => a.id.Equals(id)).FirstOrDefault();

            XDocument xmlTemp = new XDocument(
                new XElement("Title", temp.title),
                new XElement("Plot", temp.description),
                new XElement("Date", temp.release_date),
                new XElement("Directors", temp.cast.Where(a => a.role.Equals(1))),
                new XElement("Writers", temp.cast.Where(a => a.role.Equals(3))),
                new XElement("Actors", temp.cast.Where(a => a.role.Equals(2)))
                                            );

            return xmlTemp; 
        }

        public XDocument getXmlByPersonId(int id)
        {
            person temp = db.person.Where(a => a.id.Equals(id)).FirstOrDefault();

            XDocument xmlTemp = new XDocument(
                new XElement("Name", temp.name),
                new XElement("Biography", temp.description),
                new XElement("Birth date", temp.birth_date),
                new XElement("Birth place", temp.birth_place),
                new XElement("Directed movies", temp.cast.Where(a => a.role.Equals(1))),
                new XElement("Wrote movies", temp.cast.Where(a => a.role.Equals(3))),
                new XElement("Played movies", temp.cast.Where(a => a.role.Equals(2)))
                                            );

            return xmlTemp;
        }
    }
}