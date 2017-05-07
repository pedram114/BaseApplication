﻿using Microsoft.AspNet.Identity;
using Pedram.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Services.Services.Users.Interfaces
    {
    public interface IApplicationRoleManager : IDisposable
        {
        /// <summary>
        /// Used to validate roles before persisting changes
        /// </summary>
        IIdentityValidator<CustomRole> RoleValidator { get; set; }

        /// <summary>
        /// Create a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> CreateAsync( CustomRole role );

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> UpdateAsync( CustomRole role );

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> DeleteAsync( CustomRole role );

        /// <summary>
        /// Returns true if the role exists
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<bool> RoleExistsAsync( string roleName );

        /// <summary>
        /// Find a role by id
        /// </summary>
        /// <param name="roleId"/>
        /// <returns/>
        Task<CustomRole> FindByIdAsync( int roleId );

        /// <summary>
        /// Find a role by name
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<CustomRole> FindByNameAsync( string roleName );


        // Our new custom methods

        CustomRole FindRoleByName( string roleName );
        IdentityResult CreateRole( CustomRole role );
        IList<CustomUserRole> GetCustomUsersInRole( string roleName );
        IList<ApplicationUser> GetApplicationUsersInRole( string roleName );
        IList<CustomRole> FindUserRoles( int userId );
        string[] GetRolesForUser( int userId );
        bool IsUserInRole( int userId, string roleName );
        Task<List<CustomRole>> GetAllCustomRolesAsync();
        IList<RoleAccess> GetRoleAccessByRoleID( int RoleId );
        IList<int> GetRoleIdsByUserId( int userId );
        }
    }
