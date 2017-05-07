using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.Users.Manage
    {
    public class VerifyPhoneNumberViewModel
        {
        [Required]
        [PedramDisplay( ResourceName: "Pedram.VerifyPhoneNumber.Code" )]
        public string Code { get; set; }

        [Required]
        [Phone]
        [PedramDisplay( ResourceName: "Pedram.VerifyPhoneNumber.PhoneNumber" )]
        public string PhoneNumber { get; set; }
        }
    }