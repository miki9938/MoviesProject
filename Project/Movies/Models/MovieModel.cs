using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Drawing;

namespace Movies.Models
{
    public class AddNewMovieModel
    {
        [Required]
        [DisplayName("Movie title")]
        public string title { get; set; }

        [Required]
        [DisplayName("Release date")]
        public DateTime releaseDate { get; set; }
        
        [Required]
        [DisplayName("Plot (not required)")]
        [DefaultValue(null)]
        public string plot { get; set; }
    }

    public class AddCastToMovieModel
    {
        public int movieId { get; set; }

        [Required]
        [DisplayName("Person name")]
        public string personName { get; set; }

        [Required]
        [DisplayName("Role in movie (Accepted words: Director, Writer, Actor")]
        public string role { get; set; }

        [DisplayName("Name of played character (not required)")]
        [DefaultValue(null)]
        public string characterName { get; set; }
    }

    public class AddSimilarMovieModel
    {
        [Required]
        public int userId { get; set; }

        [Required]
        public int firstMovieId { get; set; }

        [Required]
        [DisplayName("Title of similar movie")]
        public string secondMovieTitle { get; set; }
    }

    public class AddImageToMovieModel
    {
        [Required]
        public int movieId { get; set; }

        [Required]
        public Image image { get; set; }
        
        public bool isPoster { get; set; }
        
        [DefaultValue("")]
        public string source { get; set; }
    }
}