using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Repositories
{
    public class MovieRepository
    {
        MoviesEntities db;

        MovieRepository()
        {
            db = new MoviesEntities();
        }
    }
}