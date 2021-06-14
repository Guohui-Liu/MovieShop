using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Request
{
   public class UserRegisterRequestModel
    {

        [Required(ErrorMessage = "Make sure you enter First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Make sure you enter First Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Make sure you enter Last Name")]
        [StringLength(128)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage =
            "Password Should have minimum 8 with at least one upper, lower, number and special character")]
        public string Password { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }


    }
}
