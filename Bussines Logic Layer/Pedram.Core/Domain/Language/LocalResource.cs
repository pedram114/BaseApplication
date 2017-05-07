using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Language
    {
    public class LocalResource
        {
        public int LocalResourceId { set; get; }
        public string LocalName { set; get; }
        public string LocalValue { set; get; }        
        public virtual Language Language { set; get; }
        }
    }
