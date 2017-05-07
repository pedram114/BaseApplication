
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Language
    {
    public class Language 
        {
        public Language() {

            LocalResources = new List<LocalResource>();
            CreatedDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            RightToLeft = false;
            DefaultLang = false;
            }
        
        public int LanguageId { set; get; }
        public string DefaultEnglishName { set; get; }
        public string LangName { set; get; }
        public string LangIconAddress { set; get; }
        public string LangCalture { set; get; }
        public DateTime CreatedDate {set; get;}
        public DateTime UpdateDate { set; get; }
        public ICollection<LocalResource> LocalResources { set; get; }
        public ICollection<Menu> Menu { set; get; }
        public bool RightToLeft { set; get; }
        public bool DefaultLang { set; get; }

        }
    }
