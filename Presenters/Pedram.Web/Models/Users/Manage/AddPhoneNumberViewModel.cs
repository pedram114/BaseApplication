using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.Users.Manage
    {
    public class AddPhoneNumberViewModel
        {
        [Required]
        [Phone]
        [PedramDisplay( ResourceName: "Pedram.AddPhoneNumber.PhoneNumber" )]
        public string Number { get; set; }
        }
    }