using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pedram.Core.Domain.NotDbClasses.BreadCrumbs
{
    public class BreadCrumbAddress
    {
        public string ActionName { set; get; }
        public string ControllerName { set; get; }
        public string Parameters { set; get; }
    }
}