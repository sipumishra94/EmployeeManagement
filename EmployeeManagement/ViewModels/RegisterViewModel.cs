﻿using EmployeeManagement.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [ValidEmailDomain(allowedDomain: "pragimtech.com", ErrorMessage ="Email domain must be pragimtech.com")]
        [Remote(action: "IsEmailInUse", controller:"Account")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("Password", ErrorMessage ="Password and Confirm password should match")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
    }
}
