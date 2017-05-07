using Pedram.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Data.Mappings.Users
    {
    public class RoleAccessMap: EntityTypeConfiguration<RoleAccess>
        {
        public RoleAccessMap() {
            ToTable( "RoleAccess" );
            this.Property( ra => ra.Action )
            .IsUnicode( false ).HasMaxLength( 70 ).IsRequired();
            this.Property( ra => ra.Controller )
                .IsUnicode( false ).HasMaxLength( 70 ).IsRequired();
            this.HasRequired(x => x.Role)
                     .WithMany(x => x.RoleAccesses)
                     .WillCascadeOnDelete(true);
        }
        }
    }
