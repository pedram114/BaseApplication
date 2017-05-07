using Microsoft.AspNet.Identity.EntityFramework;
using Pedram.Core.Domain.Users;
using Pedram.Data;
using Pedram.Services.Services.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Services.Services.Users
    {
    public class CustomUserStore :
          UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
          ICustomUserStore
        {
        //private readonly IDbSet<ApplicationUser> _myUserStore;
        public CustomUserStore(PedramDbContext context )
            : base( context )
            {
            //_myUserStore = context.Set<ApplicationUser>();
            }

        //public override Task<ApplicationUser> FindByIdAsync(int userId)
        //{
        //   return Task.FromResult(_myUserStore.Find(userId));
        //}
        }
    }
