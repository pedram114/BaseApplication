﻿using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.Users.Login
    {
    public class ResetPasswordViewModel
        {
        [Required]
        [EmailAddress]
        [PedramDisplay( ResourceName: "Pedram.ResetPassword.Email" )]
        public string Email { get; set; }

        [Required]
        [StringLength( 100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6 )]
        [DataType( DataType.Password )]
        [PedramDisplay( ResourceName : "Pedram.ResetPassword.Password" )]
        public string Password { get; set; }

        [DataType( DataType.Password )]
        [PedramDisplay( ResourceName: "Pedram.ResetPassword.ConfirmPassword" )]
        [Compare( "Password", ErrorMessage = "The password and confirmation password do not match." )]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
        }
    
    }