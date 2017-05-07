using Pedram.Core.Domain.Users;

namespace Pedram.Framework.Helpers
{
    public interface IContextHelper
    {
        IUserContext GetCurrentContext();
        IUserContext CreateContext();
        IGlobalUsersContext GetGlobalUsersContext();        
        void PostTransaction();
    }
}
