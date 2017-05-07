using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Areas.Admin.Models.Management
{
    public class Settings
    {
        public int LanguageId { set; get; }
        public string LanguageName { set; get; }

        public int Id { set; get; }
        [PedramDisplay("Pedram.EditSetting.WebApplicationName")]
        public string WebApplicationName { set; get; }
        [PedramDisplay("Pedram.EditSetting.WebApplicationDescription")]
        public string WebApplicationDescription { set; get; }
        [PedramDisplay("Pedram.EditSetting.copywrite")]
        public string copywrite { set; get; }

    }
}