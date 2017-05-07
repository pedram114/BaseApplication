using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Core.Domain.NotDbClasses.BreadCrumbs
{
    public class BreadCrumbModel
    {
        public string ShowText { set; get; }
        public BreadCrumbAddress Address { set; get; }
    }
}