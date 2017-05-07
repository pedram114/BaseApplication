using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pedram.Core.Domain.Users
    {
    public class CustomRole : IdentityRole<int, CustomUserRole>
        {
        public CustomRole() { }
        public CustomRole( string name ) { Name = name; }
        public virtual ICollection<RoleAccess> RoleAccesses { get; set; }

        }
    }
