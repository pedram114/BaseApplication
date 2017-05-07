using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedram.Core.Domain.Indexes
{
    public class ProductIndex
    {
        public int ID { set; get; }
        public string IndexName { set; get; }
        public int ProductID { set; get; }
    }
}
