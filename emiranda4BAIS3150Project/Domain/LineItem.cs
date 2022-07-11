using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emiranda4BAIS3150Project.Domain
{
    public class LineItem
    {
        public int LineItemId { get; set; }
        public int Quantity { get; set; }
        public string ItemCode { get; set; }
        public int SaleNumber { get; set; }
    }
}
