using Pedram.Framework.CustomizedAttributes;
using Pedram.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pedram.Web.Areas.Admin.Models
    {
    public class CreateRoleViewModel
        {

        [PedramDisplay("Pedram.Admin.Role.Create.Name")]
        [Required]
        [StringLength( 256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6 )]
        public string Name { get; set; }
   
        public IEnumerable<ControllerDescription> SelectedControllers { get; set; }

        public IEnumerable<ControllerDescription> Controllers { get; set; }
        }
    }