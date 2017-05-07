﻿using Microsoft.AspNet.Identity;
using Pedram.Core.Domain.Users;
using Pedram.Services.Services.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Services.Services.Users
    {
    public class CustomRoleStore : ICustomRoleStore
        {
        private readonly IRoleStore<CustomRole, int> _roleStore;

        public CustomRoleStore( IRoleStore<CustomRole, int> roleStore )
            {
            _roleStore = roleStore;
            }
        }
    }