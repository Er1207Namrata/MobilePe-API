using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Expense
    {
        public string Name { get; set; }
        public string LoginId { get; set; }
        public decimal amount { get; set; }
        public string ImageUrl { get; set; }
    }
    public class Super30Expense 
    {
        public List<Expense> result { get; set; }
    }
}
