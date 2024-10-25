using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class ExpenseEcomm
    {
        public string name { get; set; }
        public string loginId { get; set; }
        public decimal amount { get; set; }
        public string imageUrl { get; set; }
    }
    public class Super30ExpenseEcomm
    {
        public List<ExpenseEcomm> result { get; set; }
    }
}
