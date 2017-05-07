using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Services.Services.Configuration.Interfaces
{
    public interface IConfigurationService
    {
        Pedram.Core.Domain.Management.Configuration GetConfiguration();
        int ClearConfigs();
        int SetConfiguration(Pedram.Core.Domain.Management.Configuration model);
    }
}
