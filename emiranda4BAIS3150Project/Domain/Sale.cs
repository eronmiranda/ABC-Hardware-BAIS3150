using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emiranda4BAIS3150Project.Domain
{
    public class Sale
    {
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public int SalespersonId { get; set; }
        public int CustomerId { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
}
