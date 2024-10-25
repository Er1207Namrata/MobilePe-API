using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class RecentRecharge
    {
        public string status { get; set; }
        public string optrId { get; set; }
        public string ImageUrl { get; set; }
        public string Number { get; set; }
        public string TransactionId { get; set; }
        public string transdate { get; set; }
        public int Amount { get; set; }
        public string Remark { get; set; }
        public string Description { get; set; }

    }
    public class RecentRechargeRespone
    {

        public List<RecentRecharge> result { get; set; }
    }
}
