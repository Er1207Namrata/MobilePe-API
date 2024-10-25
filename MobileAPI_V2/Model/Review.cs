using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Review
    {
        public long productId { get; set; }
        public long userId { get; set; }
        public int rating { get; set; }
        public string review { get; set; }
    }
}
