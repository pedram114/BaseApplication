using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Users
{
    public class GlobalUserModel
    {
        public int UserId { set; get; }
        public string SessionName { set; get; }
        public string SessionId { set; get; }
    }
}
