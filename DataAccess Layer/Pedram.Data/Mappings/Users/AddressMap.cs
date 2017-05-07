using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Data.Mappings.Users
    {
    public class AddressMap : EntityTypeConfiguration<Pedram.Core.Domain.Users.Address>
        {
        public AddressMap() {
            ToTable("Address");
            }
        }
    }
