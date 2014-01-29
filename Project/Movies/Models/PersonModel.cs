using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Drawing;

namespace Movies.Models
{
    public class AddNewPersonModel
    {
        [Required]
        [DisplayName("Person name")]
        public string name { get; set; }

        [DisplayName("Birth date (not required)")]
        [DefaultValue(null)]
        public DateTime? birthDate { get; set; }

        [DisplayName("Birth place (not required)")]
        [DefaultValue(null)]
        public string birthPlace { get; set; }

        [DisplayName("Biography (not required)")]
        [DefaultValue(null)]
        public string biography { get; set; }
    }

    public class AddNewRoleModel
    {
        [Required]
        public int personId { get; set; }

        [Required]
        [DisplayName("Movie title")]
        public string movieTitle { get; set; }

        [Required]
        [DisplayName("Role in movie (Accepted words: Director, Writer, Actor")]
        public string role { get; set; }

        [DisplayName("Name of played character (not required)")]
        [DefaultValue(null)]
        public string characterName { get; set; }
    }

    public class AddImageToPersonModel
    {
        [Required]
        public int personId { get; set; }

        [Required]
        public Image image { get; set; }

        public bool isPortrait { get; set; }

        [DefaultValue("")]
        public string source { get; set; }
    }

    public class AddCommentToPersonModel
    {
        [Required]
        public int personId { get; set; }

        [Required]
        public int userId { get; set; }

        [Required]
        public string comment { get; set; }
    }
}