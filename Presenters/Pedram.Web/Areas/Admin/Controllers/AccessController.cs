using Microsoft.AspNet.Identity.EntityFramework;
using Pedram.Data;
using Pedram.Framework.Controller;
using Pedram.Framework.CustomizedAttributes;
using Pedram.Services.Services.Users;
using Pedram.Services.Services.Users.Interfaces;
using Pedram.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Pedram.Web.Areas.Admin.Controllers
{
    [PedramAuthorize()]
    [PedramDescription( "Pedram.Controller.AdminPanel.Access" )]
    public class AccessController : BaseController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _RoleManager;

        public AccessController( IApplicationUserManager userManager, IApplicationRoleManager RoleManager ) {
            _userManager = userManager;
            _RoleManager = RoleManager;

            }
        // GET: Access


        [PedramDescription("Pedram.Controller.AdminPanel.Access.AccessList")]
        public async Task<ActionResult> Index()
            {

            var Users = await _userManager.GetAllUsersAsync();
            var users = Users.ToList().Select( s => new UserRoleViewModel {
                UserId = s.Id,
                UserName = s.UserName,  
                Email=s.Email,                         
                Roles = _RoleManager.GetAllCustomRolesAsync().Result.ToList().Where( r => s.Roles.Any( ur => ur.RoleId == r.Id ) ).Select( r => r.Name )

                } );



            
            return View( users );
            }

        #region Edit
        // GET: Access/Edit
        [PedramDescription("Pedram.Controller.AdminPanel.Access.EditAccess")]
        public async Task<ActionResult> Edit( string id )
            {
            var Users =await _userManager.GetAllUsersAsync();

            var model = Users.ToList().Where(rrr=>rrr.Id==int.Parse(id)).Select(s => new EditUserRoleViewModel
            {
                UserId = s.Id,
                UserName = s.UserName,
                Email=s.Email,
                Roles = _RoleManager.GetAllCustomRolesAsync().Result.ToList().Select(ss => new CustomRoleModel
                {
                    Id = ss.Id,
                    Name = ss.Name,
                    RoleAccesses = ss.RoleAccesses.Select(f => new RoleAccessModel
                    {
                        Action = f.Action,
                        Controller = f.Controller,
                        Id = f.Id

                    }).ToList()
                }),
                SelectedRoleAccess = _RoleManager.GetAllCustomRolesAsync().Result.ToList().Where(r => s.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.Name)

            }).FirstOrDefault();
      
            return View( model );
            }
       
        // POST: Access/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( EditUserRoleViewModel viewModel )
            {
            //_dbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            if (!ModelState.IsValid)
            {
                viewModel.Roles = _RoleManager.GetAllCustomRolesAsync().Result.ToList().Select(s=>new CustomRoleModel {
                     Id=s.Id,
                     Name=s.Name,                                          
                     RoleAccesses = s.RoleAccesses.Select(f => new RoleAccessModel
                    {
                        Action = f.Action,
                        Controller = f.Controller,
                        Id = f.Id

                    }).ToList()                   
                });

               
                return View(viewModel);
            }

            var user = _userManager.FindById(viewModel.UserId);
            user.Roles.Clear();
            if(viewModel.SelectedRoleAccess != null)
                if(viewModel.SelectedRoleAccess.Count()>0)
                    foreach (var roleId in viewModel.SelectedRoleAccess)
                    {
                        user.Roles.Add(new Core.Domain.Users.CustomUserRole { RoleId = int.Parse(roleId)});
                    }
          
            var dd = await _userManager.UpdateAsync(user);

            return RedirectToAction( "Index","AdminHome" );
            }
        #endregion
    }
}