using ASPTutorial.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTutorial.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailExist", controller: "Account")]
        //[ValidateEmailDomain(allowedDomain:"hotmail.com", ErrorMessage = "Email domain must be hotmail.com.")] //same validation done in the AccountController (isEmailExist)
        public string Email { get; set; }
        [Required]
        [Remote(action:"IsUsernameExist", controller:"Account")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string City { get; set; }
    }
}
