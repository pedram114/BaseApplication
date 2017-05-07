using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.CommonModel
{
    public class SearchBoxListViewModel
    {
        public int ID { set; get; }
        public string ProductName { set; get; }
        public string ProductDescription { set; get; }
        public string Description { set; get; }
        public string ImageSrc { set; get; }
    }
}