using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Pedram.Core.Domain.Users;
using Pedram.Services.Services.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Services.Services.Users
    {
    public class ApplicationSignInManager :
    SignInManager<ApplicationUser, int>, IApplicationSignInManager
        {
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;

        public ApplicationSignInManager( ApplicationUserManager userManager,
                   IAuthenticationManager authenticationManager ) :
         base( userManager, authenticationManager )
            {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            }
        }
    }
