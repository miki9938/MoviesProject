﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movies.Repositories;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Text;

namespace Movies.Controllers
{
    public class ApiController: Controller
    {
        ApiRepository data;

        public ApiController()
        {
            data = new ApiRepository();
        }

        public ActionResult Movie(int id)
        {
            return Content("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine + data.getXmlByMovieId(id).ToString(),"xml");
        }

        public ActionResult Person(int id)
        {
            return Content("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine + data.getXmlByPersonId(id).ToString(), "xml");
        }
    }
}