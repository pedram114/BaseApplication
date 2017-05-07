using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Data.Mappings.Language
    {
    public class LocalResourceMap:EntityTypeConfiguration<Pedram.Core.Domain.Language.LocalResource>
        {
        public LocalResourceMap() {
            ToTable( "LocalResource" );
            HasKey( x => x.LocalResourceId );
            // one-to-many
            this.HasRequired( x => x.Language )
                .WithMany( x => x.LocalResources )
                .WillCascadeOnDelete(true);
            }
        }
    }
