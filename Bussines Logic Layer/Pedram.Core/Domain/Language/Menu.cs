using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Language
{
    public class Menu
    {
        public int ID { set; get; }
        public string MenuName { set; get; }
        public int Parity { set; get; }
        public int Level { set; get; }
        public Menu Menu1 { set; get; }   
        public virtual Language Language { set; get; }

    }
}
