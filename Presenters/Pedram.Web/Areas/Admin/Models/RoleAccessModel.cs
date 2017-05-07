using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Areas.Admin.Models
{
    public class RoleAccessModel
    {
        public int Id { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

      
    }
}