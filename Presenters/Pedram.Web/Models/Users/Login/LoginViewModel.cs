using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.Users.Login
    {
    public class LoginViewModel
        {
       
        [PedramDisplay( ResourceName: "Pedram.Login.Email" )]
        [EmailAddress]
        public string Email { get; set; }

       
        [PedramDisplay(ResourceName: "Pedram.Login.UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType( DataType.Password )]
        [PedramDisplay( ResourceName : "Pedram.Login.Password" )]
        public string Password { get; set; }

        [PedramDisplay( ResourceName : "Pedram.Login.RememberMe" )]
        public bool RememberMe { get; set; }
        }
    }