using Pedram.Core.Domain.Management;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Data.Mappings.Management
{
    public class ConfigurationMap : EntityTypeConfiguration<Configuration>
    {
        public ConfigurationMap() {
            ToTable("Configuration");
        }
    }
}
