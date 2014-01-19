using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;

namespace Movies.Models
{
    public class RegistrationUserModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3)]
        public string login { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email address is incorrect.")]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 6)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "retype password")]
        [Compare("password", ErrorMessage = "Passwords do not match.")]
        public string retypedPassword { get; set; }
    }

    public class LogInUserModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(25)]
        public string login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(25)]
        public string password { get; set; }
    }
}