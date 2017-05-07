using Microsoft.AspNet.Identity;
using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.Users.Manage
    {
    public class IndexViewModel
        {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        }
    }