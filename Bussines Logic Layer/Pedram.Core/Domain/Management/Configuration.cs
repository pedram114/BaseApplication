using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Management
    {
    public class Configuration
        {
        public Configuration() {
            UseEmailInsteadUserName = true;
            WebApplicationName = "Pedram.WebApplicationName";
            WebApplicationDescription = "Pedram.WebApplicationDescription";
            copywrite = "Pedram.CopyWrite";
            UseCachToLoadLanguage = true;
            WebsiteDomainName = "http://www.IranDejak.com";

        }
        public int ID { set; get; }
        public bool UseEmailInsteadUserName { set; get; }
        public string WebApplicationName { set; get; }
        public string WebApplicationDescription { set; get; }
        public string copywrite { set; get; }
        public bool UseCachToLoadLanguage { set; get; }
        public string LogoAddress { set; get; }
        public string WebsiteDomainName { set; get; }
    }
    }
