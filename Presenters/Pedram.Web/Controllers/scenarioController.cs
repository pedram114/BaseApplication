using Pedram.Framework.Controller;
using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pedram.Web.Controllers
{
    [Authorize]

    [PedramDescription("Pedram.Controller.Scenario")]
    public class ScenarioController : BaseController
    {
        // GET: scenario
        [PedramAuthorize()]
        [PedramDescription("Pedram.Controller.Createscenario")]
        public ActionResult Createscenario()
        {
            return View();
        }
    }
}