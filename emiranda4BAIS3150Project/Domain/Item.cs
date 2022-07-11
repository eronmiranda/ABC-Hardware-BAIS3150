using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emiranda4BAIS3150Project.Domain
{
    public class Item
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public string Status { get; set; }
        public int QuantityOnHand { get; set; }
    }
}
