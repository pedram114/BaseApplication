using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.Users.Login
    {
    public class VerifyCodeViewModel
        {
        [Required]
        public string Provider { get; set; }

        [Required]
        [PedramDisplay( ResourceName:  "Pedram.VerifyCode.Code" )]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [PedramDisplay( ResourceName : "Pedram.VerifyCode.RememberBrowser" )]
        public bool RememberBrowser { get; set; }
        }
    }