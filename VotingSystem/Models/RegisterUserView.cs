using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class RegisterUserView : UserView
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The passwords does not match")]
        public string ConfirmPassword { get; set; }
    }
}