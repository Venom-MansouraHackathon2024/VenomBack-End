﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Application.Auth.Dto
{
    public class RegisterDto
    {
        [MinLength(3, ErrorMessage = "The field UserName must be a string with a minimum length of 3")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email address can't be embty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password don't match with ConfirmPassword")]
        public string ConfirmPassWord { get; set; }
    }
}

