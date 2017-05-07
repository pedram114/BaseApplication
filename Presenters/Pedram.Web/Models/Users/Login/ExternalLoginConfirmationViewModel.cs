using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.Users.Login
    {
    public class ExternalLoginConfirmationViewModel
        {
        [Required]
        [PedramDisplay( ResourceName: "Pedram.ExternalLoginConfirmation.Email" )]
        public string Email { get; set; }
        }
    }