using Microsoft.AspNet.Identity;
using Pedram.Core.Domain.Users;
using Pedram.Framework.Controller;
using Pedram.Framework.CustomizedAttributes;
using Pedram.Framework.Helpers;
using Pedram.Services.Services.Users;
using Pedram.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pedram.Web.Areas.Admin.Controllers
{
    [PedramAuthorize()]
    [PedramDescription("Pedram.Controller.AdminPanel.Role")]
    public class RoleController : BaseController
    {
        private readonly ApplicationRoleManager _roleManager;
        private readonly ApplicationUserManager _AppManager;
        private static Lazy<IEnumerable<ControllerDescription>> _controllerList =
                                 new Lazy<IEnumerable<ControllerDescription>>();
        public RoleController( ApplicationRoleManager roleManager , ApplicationUserManager AppManager)
            {
            _roleManager = roleManager;
            _AppManager = AppManager;
            }

        [PedramDescription("Pedram.Controller.AdminPanel.Role.Index")]
        public ActionResult Index() {
             
            var roles = _roleManager.GetAllCustomRolesAsync().Result.ToList();
            IEnumerable<CustomRoleModel> model = new List<CustomRoleModel>();
            AutoMapper.Mapper.Map(roles,model);
            return View(model);
        }

        #region Create Role

        [PedramDescription("Pedram.Controller.AdminPanel.Role.Create")]
            public ActionResult Create()
                {
                var createRoleViewModel = new CreateRoleViewModel
                    {
                    Controllers = GetControllers()
                    };
                return View( createRoleViewModel );
                }
        
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create( CreateRoleViewModel viewModel )
                {
                if (!ModelState.IsValid)
                    {
                    //viewModel.Controllers = GetControllers();
                    return View( viewModel );
                    }

                var role = new CustomRoleModel
                    {
                    Name = viewModel.Name,
                    RoleAccesses = new List<RoleAccessModel>()
                    };

                foreach (var controller in viewModel.SelectedControllers)
                    {
                    foreach (var action in controller.Actions)
                        {
                        role.RoleAccesses.Add( new RoleAccessModel { Controller = controller.Name, Action = action.Name } );
                        }
                    }
                CustomRole NewRole = new CustomRole();
                AutoMapper.Mapper.Map(role, NewRole);
            
                _roleManager.Create(NewRole);
                return RedirectToAction( "Index","AdminHome" );
                }
        #endregion

        #region Edit Roles
        [PedramDescription("Pedram.Controller.AdminPanel.Role.EditRoles")]
        public ActionResult EditRole(string RoleId)
        {
            var roles = _roleManager.GetAllCustomRolesAsync().Result.ToList().Where(s=>s.Id==int.Parse( RoleId)).FirstOrDefault();


            EditRoleViewModel model = new EditRoleViewModel();
            AutoMapper.Mapper.Map(roles, model);
            model.Controllers = GetControllers();
            return View(model);


        }
        #endregion

        [HttpPost]
        public ActionResult EditRole(EditRoleViewModel model, string[] checkedFiles)
        {

            if (!ModelState.IsValid)
            {
                model.Controllers = GetControllers();
                return View(model);
            }
            checkedFiles = checkedFiles ?? new string[0];
            model.RoleAccesses = new List<RoleAccessModel>();
            
            foreach (var item in checkedFiles)
            {
                string[] temp = item.Split('_');
                if(temp.Length==2)
                    model.RoleAccesses.Add(new RoleAccessModel { Controller = temp[0], Action = temp[1] });
            }
            var UpdateRole = _roleManager.FindById(model.Id);
            UpdateRole.RoleAccesses.Clear();
            AutoMapper.Mapper.Map(model.RoleAccesses, UpdateRole.RoleAccesses);
            var res= _roleManager.Update(UpdateRole);
            model.Controllers = GetControllers();
            if (res.Succeeded)
            {
               
                return View(model);
            }
            else
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError(item.ToString(),item.ToString());
                }
                return View(model);
                
            }
        }

        private void ConvertRoleAccessToControllerDescription (RoleAccessModel RoleModel,ControllerDescription Ctrs) {

           
        }
        private static IEnumerable<ControllerDescription> GetControllers()
        {
            //   if (_controllerList.IsValueCreated)
            //       return _controllerList.Value;

            _controllerList = new Lazy<IEnumerable<ControllerDescription>>(() => new ControllerHelper().GetControllersNameAnDescription());
            return _controllerList.Value;
        }

    }


}