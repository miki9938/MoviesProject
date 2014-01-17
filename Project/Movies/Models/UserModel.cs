using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class RegistrationUserModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(25)]
        public string login { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Format of email address is incorrect")]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Password is too short (min. 6 characters)", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogInUserModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(25)]
        public string login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "Password is too short (min. 6 characters)", MinimumLength = 6)]
        public string password { get; set; }
    }
}