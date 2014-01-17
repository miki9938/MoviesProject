using System;
using System.Linq;
using System.Xml.Linq;
using Movies.Mappings;

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
            int i = 0;

            movie temp = db.movies.Where(a => a.id.Equals(id)).FirstOrDefault();

            XDocument xmlTemp = new XDocument(
                new XElement("Movie",
                    new XElement("Title", temp.title),
                    new XElement("Plot", temp.description),
                    new XElement("Date", temp.release_date),
                    new XElement("Directors"),
                    new XElement("Writers"),
                    new XElement("Actors")
                                                ));

            foreach (cast c in temp.casts.Where(a => a.role.Equals(1)))
            {
                i++;
                XElement tempElement = new XElement("D" + i.ToString(), c.person.name);
                xmlTemp.Root.Element("Directors").Add(tempElement);
            }

            i = 0;
            foreach (cast c in temp.casts.Where(a => a.role.Equals(3)))
            {
                i++;
                XElement tempElement = new XElement("W" + i.ToString(), c.person.name);
                xmlTemp.Root.Element("Writers").Add(tempElement);
            }

            i = 0;
            foreach (cast c in temp.casts.Where(a => a.role.Equals(2)))
            {
                i++;
                XElement tempElement = new XElement("A" + i.ToString(), c.person.name);
                xmlTemp.Root.Element("Actors").Add(tempElement);
            }

            return xmlTemp; 
        }

        public XDocument getXmlByPersonId(int id)
        {
            int i = 0;

            person temp = db.people.Where(a => a.id.Equals(id)).FirstOrDefault();

            XDocument xmlTemp = new XDocument(
                new XElement("Person",
                    new XElement("Name", temp.name),
                    new XElement("Biography", temp.description),
                    new XElement("Birth date", temp.birth_date),
                    new XElement("Birth place", temp.birth_place),
                    new XElement("Directed movies"),
                    new XElement("Wrote movies"),
                    new XElement("Played movies")
                                            ));

            foreach (cast c in temp.casts.Where(a => a.role.Equals(1)))
            {
                i++;
                XElement tempElement = new XElement("D" + i.ToString(), c.person.name);
                xmlTemp.Root.Element("Directed movies").Add(tempElement);
            }

            i = 0;
            foreach (cast c in temp.casts.Where(a => a.role.Equals(3)))
            {
                i++;
                XElement tempElement = new XElement("W" + i.ToString(), c.person.name);
                xmlTemp.Root.Element("Wrote movies").Add(tempElement);
            }

            i = 0;
            foreach (cast c in temp.casts.Where(a => a.role.Equals(2)))
            {
                i++;
                XElement tempElement = new XElement("A" + i.ToString(), c.person.name);
                xmlTemp.Root.Element("Played movies").Add(tempElement);
            }

            return xmlTemp;
        }
    }
}