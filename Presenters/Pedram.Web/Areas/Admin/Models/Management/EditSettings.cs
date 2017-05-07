using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Areas.Admin.Models.Management
{
    public class EditSettings
    {
        public IList<Settings> Settings { set; get; }
        [PedramDisplay("Pedram.EditSetting.UseEmailInsteadUserName")]
        public bool UseEmailInsteadUserName { set; get; }
        [PedramDisplay("Pedram.EditSetting.WebsiteDomainName")]
        public string WebsiteDomainName { set; get; }
       [PedramDisplay("Pedram.EditSetting.UseCachToLoadLanguage")]
        public bool UseCachToLoadLanguage { set; get; }
        public string LogoAddress { set; get; }
        [PedramDisplay("Pedram.EditSetting.LogoImageUpload")]
        public HttpPostedFileBase LogoImageUpload { get; set; }
    }
}