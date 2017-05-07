using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Areas.Admin.Models
{
    public class CustomRoleModel
    {
        public CustomRoleModel() { }
        public CustomRoleModel(string name) { Name = name; }

       
        public int Id { set; get; }
        [PedramDisplay("Pedram.Admin.RoleManagement.Model.RoleName")]
        public string Name { set; get; }
        public virtual ICollection<RoleAccessModel> RoleAccesses { get; set; }

    }
}