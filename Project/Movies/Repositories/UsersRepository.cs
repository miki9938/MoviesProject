using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Repositories
{
    public class UsersRepository
    {
        MoviesEntities db;

        UsersRepository()
        {
            db = new MoviesEntities();
        }
    }
}