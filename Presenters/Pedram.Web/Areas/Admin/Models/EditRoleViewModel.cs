using Pedram.Framework.CustomizedAttributes;
using Pedram.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Areas.Admin.Models
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel() { }
       
        public int Id { set; get; }
        [PedramDisplay("Pedram.Admin.RoleManagement.Model.RoleName")]
        public string Name { set; get; }

        [PedramDisplay("Pedram.Controller.AdminPanel.Access.AccessList")]
        public virtual ICollection<RoleAccessModel> RoleAccesses { get; set; }
        public IEnumerable<ControllerDescription> SelectedControllers { get; set; }

        public IEnumerable<ControllerDescription> Controllers { get; set; }

    }
}