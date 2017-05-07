using Pedram.Core;
using Pedram.Services.Services.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Services.Services.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        IUnitOfWork _IUnitOfWork;
        private IDbSet<Pedram.Core.Domain.Management.Configuration> _Configuration;
        public ConfigurationService(IUnitOfWork IUnitOfWork) {
            _IUnitOfWork = IUnitOfWork;
            _Configuration = _IUnitOfWork.Set<Pedram.Core.Domain.Management.Configuration>();

        }
        public Pedram.Core.Domain.Management.Configuration GetConfiguration() {
            var query = from q in _Configuration
                        select q;
            return query.FirstOrDefault();

        }

        public int ClearConfigs() {
            foreach (var item in _Configuration)
            {
                _Configuration.Remove(item);
            }
            _IUnitOfWork.SaveAllChanges();
            return 1;
        }

        public int SetConfiguration(Pedram.Core.Domain.Management.Configuration model)
        {
            if (ClearConfigs()==1)
            {
                _Configuration.Add(model);
                _IUnitOfWork.SaveAllChanges();
                return 1;
            }
            else
                return 0;

        }
    }
}
