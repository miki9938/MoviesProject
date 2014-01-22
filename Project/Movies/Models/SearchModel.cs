using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class GlassSearchModel
    {
        public string title { get; set; }

        public string date { get; set; }

        public int? pictureId { get; set; }
    }
}