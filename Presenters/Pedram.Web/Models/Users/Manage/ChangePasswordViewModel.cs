using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.Users.Manage
    {
    public class ChangePasswordViewModel
        {
        [Required]
        [DataType( DataType.Password )]
        [PedramDisplay( ResourceName:  "Pedram.ChangePassword.CurrentPassword" )]
        public string OldPassword { get; set; }

        [Required]
        [StringLength( 100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6 )]
        [DataType( DataType.Password )]
        [PedramDisplay( ResourceName: "Pedram.ChangePassword.NewPassword" )]
        public string NewPassword { get; set; }

        [DataType( DataType.Password )]
        [PedramDisplay( ResourceName: "Pedram.ChangePassword.ConfirmNewPassword" )]
        [Compare( "NewPassword", ErrorMessage = "The new password and confirmation password do not match." )]
        public string ConfirmPassword { get; set; }
        }
    }