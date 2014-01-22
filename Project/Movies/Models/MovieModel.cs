using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;

namespace Movies.Models
{
    public class AddNewMovieModel
    {
        [Required]
        public string title { get; set; }

        [Required]
        public DateTime releaseDate { get; set; }
        
        [Required]
        public string plot { get; set; }
    }

    public class AddCastToMovieModel
    {
     
    }

    public class AddSimilarMovieModel
    { 
    
    }
}