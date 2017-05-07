using Microsoft.AspNet.Identity.EntityFramework;
using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pedram.Web.Areas.Admin.Models
    {
    public class EditUserRoleViewModel
        {
        [Required]
        public int UserId { get; set; }

        [PedramDisplay("Pedram.Admin.Access.Edit.Model.UserName")]
        public string UserName { get; set; }
        [PedramDisplay("Pedram.Admin.Access.Edit.Model.Email")]
        public string Email { get; set; }

        public IEnumerable<string> SelectedRoleAccess { get; set; }

        public IEnumerable<CustomRoleModel> Roles { get; set; }
        }
    }