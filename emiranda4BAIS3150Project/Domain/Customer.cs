using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emiranda4BAIS3150Project.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Status { get; set; }
    }
}
