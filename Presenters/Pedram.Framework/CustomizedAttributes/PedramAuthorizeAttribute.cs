using Pedram.Core.Domain.Users;
using Pedram.Data;
using Pedram.Framework.StartUpClasses;
using Pedram.Services.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Pedram.Framework.CustomizedAttributes
    {
    public class PedramAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
        {
        private string _requestControllerName;
        private string _requestedActionName;
        ApplicationRoleManager _ApplicationRoleManager;
        ApplicationUserManager _ApplicationUserManager;



        public override void OnAuthorization( AuthorizationContext filterContext )
            {
            _requestControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            _requestedActionName = filterContext.ActionDescriptor.ActionName;
            _ApplicationRoleManager = SmObjectFactory.Container.GetInstance<ApplicationRoleManager>();
            _ApplicationUserManager = SmObjectFactory.Container.GetInstance<ApplicationUserManager>();
            base.OnAuthorization( filterContext );
            }

        protected override bool AuthorizeCore( HttpContextBase httpContext )
            {
            if (httpContext == null)
                throw new ArgumentNullException( "httpContext" );

            var user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
                return false;
            ApplicationRoleManager _ApplicationRoleManager = SmObjectFactory.Container.GetInstance<ApplicationRoleManager>();

            var userId = _ApplicationUserManager.GetCurrentUserId();
            if (_ApplicationRoleManager.GetRolesForUser(userId).Contains("Admin"))
                return true;
            var roleIds = _ApplicationRoleManager.GetRoleIdsByUserId( userId );

            ICollection<RoleAccess> roleAccesss = new List<RoleAccess>();
            foreach (var item in roleIds)
                {
                var roleAccess = _ApplicationRoleManager.GetRoleAccessByRoleID( item );
                foreach (var item1 in roleAccess)
                    {
                    roleAccesss.Add( item1 );
                    }

                }

            if (roleAccesss.Any( ra =>
                 ra.Controller.Equals( _requestControllerName, StringComparison.InvariantCultureIgnoreCase ) &&
                 ra.Action.Equals( _requestedActionName, StringComparison.InvariantCultureIgnoreCase ) ))
                return true;

             return false;
            
            }
        protected override void HandleUnauthorizedRequest( AuthorizationContext filterContext )
            {
            if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                filterContext.Result = new HttpStatusCodeResult( 403 );
                return;
                }

            base.HandleUnauthorizedRequest( filterContext );
            }
        }
    }
