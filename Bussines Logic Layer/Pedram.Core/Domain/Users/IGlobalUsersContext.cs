using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Users
{
    public interface IGlobalUsersContext
    {
        ICollection<GlobalUserModel> UserSession { set; get; }
    }
}
