using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Areas.Admin.Models
    {
    public class UserRoleViewModel
        {
        public int UserId { get; set; }

        [PedramDisplay("Pedram.Admin.Access.Edit.Model.UserName")]
        public string UserName { get; set; }

        [PedramDisplay("Pedram.Admin.Access.Edit.Model.Email")]
        public string Email { get; set; }

        [PedramDisplay("Pedram.Admin.Access.Edit.LabelRoles")]
        public IEnumerable<string> Roles { get; set; }


        }
    }