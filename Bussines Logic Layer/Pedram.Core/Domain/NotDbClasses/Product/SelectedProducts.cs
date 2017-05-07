using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.NotDbClasses.Product
{
    public class SelectedProducts
    {
        public string Type { set; get; }
        public string Name { set; get; }
        public int ID { set; get; }
        public SelectedProducts SubProduct { set; get; }
    }
}
