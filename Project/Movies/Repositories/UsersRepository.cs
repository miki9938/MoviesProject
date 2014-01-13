using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Repositories
{
    public class UsersRepository
    {
        MoviesEntities db;

        public UsersRepository()
        {
            db = new MoviesEntities();
        }
        
    }
}