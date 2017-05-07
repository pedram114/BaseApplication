using Pedram.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Pedram.Services.Services.Users;
using Pedram.Framework.StartUpClasses;
using Pedram.Core.Domain.Users;
using System.Web.Script.Serialization;

namespace Pedram.Framework.Helpers
    {
    public static class HtmlExtensions
        {
            public static string ToJson(this Object obj)
            {
                return new JavaScriptSerializer().Serialize(obj);
            }
        public static MvcHtmlString SecureActionLink( this HtmlHelper htmlHelper, string linkText, string actionName )
            {
            return SecureActionLink( htmlHelper, linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary() );
            }

        public static MvcHtmlString SecureActionLink( this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues )
            {
            return SecureActionLink( htmlHelper, linkText, actionName, null, routeValues, (IDictionary<string, object>)new RouteValueDictionary() );
            }

        public static MvcHtmlString SecureActionLink( this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes )
            {
            return SecureActionLink( htmlHelper, linkText, actionName, null, routeValues, htmlAttributes );
            }

        public static MvcHtmlString SecureActionLink( this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues )
            {
            return SecureActionLink( htmlHelper, linkText, actionName, null, routeValues, new RouteValueDictionary() );
            }

        public static MvcHtmlString SecureActionLink( this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes )
            {
            return SecureActionLink( htmlHelper, linkText, actionName, null, routeValues, htmlAttributes );
            }

        public static MvcHtmlString SecureActionLink( this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName )
            {
            return SecureActionLink( htmlHelper, linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary() );
            }

        public static MvcHtmlString SecureActionLink( this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes )
            {
            //var url = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);

            return CanAccess( htmlHelper, actionName, controllerName )
                ? htmlHelper.ActionLink( linkText, actionName, controllerName, routeValues, htmlAttributes )
                : new MvcHtmlString( string.Empty );
            }

        public static MvcHtmlString SecureActionLink( this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes )
            {
            return CanAccess( htmlHelper, actionName, controllerName )
                ? htmlHelper.ActionLink( linkText, actionName, controllerName, routeValues, htmlAttributes )
                : new MvcHtmlString( string.Empty );
            }

        public static MvcHtmlString SecureActionLink( this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes )
            {
            return CanAccess( htmlHelper, actionName, controllerName )
                ? htmlHelper.ActionLink( linkText, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes )
                : new MvcHtmlString( string.Empty );
            }

        private static bool CanAccess( HtmlHelper htmlHelper, string actionName, string controllerName )
            {
            ApplicationRoleManager _ApplicationRoleManager = SmObjectFactory.Container.GetInstance<ApplicationRoleManager>();
            ApplicationUserManager _ApplicationUserManager = SmObjectFactory.Container.GetInstance<ApplicationUserManager>();
            var httpContext = htmlHelper.ViewContext.HttpContext;
            var user = httpContext.User;
            var userId = _ApplicationUserManager.GetCurrentUserId();
            var roleIds = _ApplicationRoleManager.GetRoleIdsByUserId( userId );
            ICollection<RoleAccess> roleAccesss=new List<RoleAccess>();
            foreach (var item in roleIds)
                {
                var roleAccess = _ApplicationRoleManager.GetRoleAccessByRoleID( item );
                foreach (var item1 in roleAccess)
                    {
                    roleAccesss.Add( item1 );
                    }
               
                }
           

            if (string.IsNullOrWhiteSpace( controllerName ))
                controllerName = htmlHelper.ViewContext.Controller.ToString().Split( '.' ).Last().Replace( "Controller", "" );

            if (roleAccesss.Any( ra =>
                 ra.Controller.Equals( controllerName, StringComparison.InvariantCultureIgnoreCase ) &&
                 ra.Action.Equals( actionName, StringComparison.InvariantCultureIgnoreCase ) ))
                return true;

            return false;
            }
        }
    }
