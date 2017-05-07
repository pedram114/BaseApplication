using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Pedram.Core.Domain.Users;
using Pedram.Services.Services.Users.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Pedram.Data;
using Microsoft.Owin;
using System.Data.Entity;
using Pedram.Core;
using System.Linq;

namespace Pedram.Services.Services.Users
    {
    public class ApplicationRoleManager : RoleManager<CustomRole, int>, IApplicationRoleManager
        {
        private readonly IUnitOfWork _uow;
        private readonly IRoleStore<CustomRole, int> _roleStore;
        private readonly IDbSet<ApplicationUser> _users;
        public ApplicationRoleManager( IUnitOfWork uow, IRoleStore<CustomRole, int> roleStore )
            : base( roleStore )
            {
            _uow = uow;
            _roleStore = roleStore;
            _users = _uow.Set<ApplicationUser>();
            }

        public CustomRole FindRoleByName( string roleName )
            {
            return this.FindByName( roleName ); // RoleManagerExtensions
            }

        public IdentityResult CreateRole( CustomRole role )
            {
            return this.Create( role ); // RoleManagerExtensions
            }

        public IList<CustomUserRole> GetCustomUsersInRole( string roleName )
            {
            return this.Roles.Where( role => role.Name == roleName )
                             .SelectMany( role => role.Users )
                             .ToList();
       }

        public IList<ApplicationUser> GetApplicationUsersInRole( string roleName )
            {
            var roleUserIdsQuery = from role in this.Roles
                                   where role.Name == roleName
                                   from user in role.Users
                                   select user.UserId;
            return _users.Where( applicationUser => roleUserIdsQuery.Contains( applicationUser.Id ) )
                         .ToList();
            }

        public IList<CustomRole> FindUserRoles( int userId )
            {
            var userRolesQuery = from role in this.Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.OrderBy( x => x.Name ).ToList();
            }

        public IList<int> GetRoleIdsByUserId(int userId) {
            return Roles.Where( s => s.Users.Select( ss => ss.UserId ).Contains( userId ) ).Select( ss => ss.Id ).ToList();
            }

        public string[] GetRolesForUser( int userId )
            {
            var roles = FindUserRoles( userId );
            if (roles == null || !roles.Any())
                {
                return new string[] { };
                }

            return roles.Select( x => x.Name ).ToArray();
            }

        public bool IsUserInRole( int userId, string roleName )
            {
            var userRolesQuery = from role in this.Roles
                                 where role.Name == roleName
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
            }

        public Task<List<CustomRole>> GetAllCustomRolesAsync()
            {
            return this.Roles.ToListAsync();
            }
        public IList<RoleAccess> GetRoleAccessByRoleID(int RoleId) {
            var query = from role in Roles
                        where role.Id == RoleId
                        select role;
            var qeury = query.ToList().FirstOrDefault();
            var model= query.Select(s=>s.RoleAccesses).FirstOrDefault();
            return model.ToList();
            }

        

        }
}