using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.CommonModel
    {
    public class SelectLanguageModel
        {
        public SelectLanguageModel() {
            Selected = false;
            }
        public int Index { set; get; }
        public int LanguageId { set; get; }
        public string DefaultEnglishName { set; get; }
        public string LangName { set; get; }
        public string LangIconAddress { set; get; }
        public string LangCalture { set; get; }
        public bool Selected { set; get; }            
        }
    }