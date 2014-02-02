using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using Movies.Mappings;

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
        [Display(Name = "Movie id")]
        public int movieId { get; set; }

        [Required]
        public HttpPostedFileBase file { get; set; }
        
        [Display(Name = "Source of image")]
        public string source { get; set; }

        [Required]
        [Display(Name = "Main poster image")]
        public bool isPoster { get; set; }
    }

    public class AddCommentToMovieModel
    {
        [Required]
        public int movieId { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        [Display(Name = "Your comment")]
        public string comment { get; set; }
    }

    public class ViewSimilarMovieModel
    {
        public int id { get; set; }

        public string title { get; set; }

        public System.DateTime releaseDate { get; set; }

        public string description { get; set; }

        public string trailerLink { get; set; }

        public string posterId { get; set; }
    }
}