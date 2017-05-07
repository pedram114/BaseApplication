using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Users
{
    public class GlobalUsersContext : IGlobalUsersContext
    {
        private ICollection<GlobalUserModel> _UserSession;
        public GlobalUsersContext()
        {
            _UserSession = new List<GlobalUserModel>() {   };
        }
        public ICollection<GlobalUserModel> UserSession
        {
            get
            {
                return _UserSession;
            }

            set
            {
                _UserSession = value;
            }
        }
    }
}
