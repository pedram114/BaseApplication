using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.CommonModel
{
    public class MenuModel
    {
        public int ID { set; get; }
        public string MenuName { set; get; }
        public int Parity { set; get; }
        public int Level { set; get; }
        public MenuModel Menu { set; get; }
    }
}