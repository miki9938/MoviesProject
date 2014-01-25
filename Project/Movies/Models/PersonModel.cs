using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
}