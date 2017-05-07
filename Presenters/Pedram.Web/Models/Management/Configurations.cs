using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.Management
    {
    public class Configurations
        {
       
        public Configurations()
            {
            UseEmailInsteadUserName = true;
            WebApplicationName = "Pedram.WebApplicationName";
            WebApplicationDescription = "Pedram.WebApplicationDescription";
            copywrite = "Pedram.CopyWrite";
            UseCachToLoadLanguage = true;
            SendConfirmEmail = false;
            WebsiteDomainName = "http://www.IranDejak.com";
        }
        public static bool UseEmailInsteadUserName { set ; get; }
        public static string WebsiteDomainName { set; get; }
        public static string WebApplicationName { set; get; }
        public static string WebApplicationDescription { set; get; }
        public static string copywrite { set; get; }
        public static bool UseCachToLoadLanguage { set; get; }
        public static string LogoAddress { set; get; }
        public static bool SendConfirmEmail { set; get; }
      


    }
}