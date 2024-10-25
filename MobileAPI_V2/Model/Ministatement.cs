using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Ministatement
    {
        public long transId { get; set; }

        public int Type { get; set; }
      
        public string transanctionId { get; set; }
        public string transDate { get; set; }
        public string status { get; set; }
        public string paymentType { get; set; }
        public string remarks { get; set; }
        public decimal credit { get; set; }
        public decimal debit { get; set; }
        public string creditAmount { get; set; }
        public string debitAmount { get; set; }
        public string totalcount { get; set; }
    }
    public class MinistatementV2Response
    {
        public  List<Ministatement> data { get; set; }
        public  List<CommonOrderdata> transtype { get; set; }
        public List<CommonOrderdata> categorytype { get; set; }
    }
}
