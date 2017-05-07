using AutoMapper;
using Pedram.Core;
using Pedram.Core.Domain.Language;
using Pedram.Data;
using Pedram.Services.Services.Language.Interfaces;
using Pedram.Services.Services.Users.Interfaces;
using Pedram.Framework.Helpers;
using System;
using System.Web;
using Pedram.Framework.StartUpClasses;

namespace Pedram.Framework.MyCodes
    {
    public class InitialCodes
        {
        
        private void InitialFirstRoleAccess() {
            var controllers = new ControllerHelper().GetControllersNameAnDescription();
            var AdminRole = SmObjectFactory.Container.GetInstance<IApplicationRoleManager>().FindRoleByName("Admin");
            foreach (var item in controllers)
            {
                foreach (var item1 in item.Actions)
                {
                    AdminRole.RoleAccesses.Add(new Core.Domain.Users.RoleAccess()
                    {
                        Controller = item.Name,
                        Action=item1.Name
                    });
                }
               
            }
            

        }
        public void InitialFirstData() {
            InitialFirstRoleAccess();
        }

        
    }
    }
