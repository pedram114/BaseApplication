using Pedram.Core.Domain.Language;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Data.Mappings.Language
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap() {
            HasRequired(L => L.Language).WithMany(M => M.Menu).WillCascadeOnDelete(true);
        }
    }
}
