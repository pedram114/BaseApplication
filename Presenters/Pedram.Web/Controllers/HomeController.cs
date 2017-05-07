using Pedram.Framework.Controller;
using Pedram.Framework.CustomizedAttributes;
using Pedram.Web.Models.CommonModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Pedram.Web.Controllers
{
    

    public class HomeController : BaseController
    {
        public HomeController() {
            
        }

        [PedramDescription( "Pedram.Controller.Home.Index" )]
        public ActionResult Index()
        {
            return View();
        }
                
        [PedramDescription( "Pedram.Controller.Home.About" )]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        [PedramDescription( "Pedram.Controller.Home.Contact" )]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
      

    }
}
