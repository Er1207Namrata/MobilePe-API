using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class MinistatementEcomm
    {
        public long transId { get; set; }

        public int type { get; set; }
      
        public string transanctionId { get; set; }
        public string transDate { get; set; }
        public string status { get; set; }
        public string paymentType { get; set; }
        public string remarks { get; set; }
        public decimal credit { get; set; }
        public decimal debit { get; set; }
        public string creditAmount { get; set; }
        public string debitAmount { get; set; }
    }
    public class MinistatementV2ResponseEcomm
    {
        public  List<MinistatementEcomm> data { get; set; }
        public  List<CommonOrderdataEcomm> transType { get; set; }
        public List<CommonOrderdataEcomm> categoryType { get; set; }
    }
}
