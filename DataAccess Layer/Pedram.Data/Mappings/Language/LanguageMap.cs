using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Data.Mappings.Language
    {
    public class LanguageMap:EntityTypeConfiguration<Pedram.Core.Domain.Language.Language>
        {
        public LanguageMap() {
            this.ToTable( "Language" );
            this.HasKey( x=>x.LanguageId );
            
        }

        }
    }
