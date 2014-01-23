using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class GlassSearchModel
    {
        public int id { get; set; }

        public string title { get; set; }

        public int releaseDate { get; set; }

        public int? pictureId { get; set; }
    }
}