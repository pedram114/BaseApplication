using Pedram.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Data.Mappings.Users
    {
    public class CustomRoleMap : EntityTypeConfiguration<CustomRole>
        {
        public CustomRoleMap() {
            ToTable( "Role" );
            }
        }
    }
