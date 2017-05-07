using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Users
    {
    public class RoleAccess
        {
        public int Id { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; } 

        public virtual CustomRole Role { get; set; }
        }
    }
