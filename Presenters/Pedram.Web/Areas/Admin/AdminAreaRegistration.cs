using System.Web.Mvc;

namespace Pedram.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            
            context.MapRoute(
                "AdminPedramURL",
                "{lang}/Admin/{controller}/{action}/{id}",
               new { lang = "en", controller = "Home", action = "Index", id = UrlParameter.Optional },
               new[] { "Pedram.Web.Areas.Admin.Controllers" }

          );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Pedram.Web.Areas.Admin.Controllers" }
            );

            }
    }
}