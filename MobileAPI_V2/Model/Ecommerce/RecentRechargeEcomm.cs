using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class RecentRechargeEcomm
    {
        public string status { get; set; }
        public string optrId { get; set; }
        public string imageUrl { get; set; }
        public string number { get; set; }
        public string transactionId { get; set; }
        public string transDate { get; set; }
        public int amount { get; set; }
        public string remark { get; set; }
        public string description { get; set; }

    }
    public class RecentRechargeResponeEcomm
    {

        public List<RecentRechargeEcomm> result { get; set; }
    }
}
