using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{


    public class CommonOrderdata
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class OrderHead
    {
        public string header { get; set; }
        public List<CommonOrderdata> data { get; set; }
    }

    public class OrderDetails
    {
        public string header { get; set; }
        public string url { get; set; }
        public List<CommonOrderdata> data { get; set; }
    }

    public class OrderStatus
    {
        public string header { get; set; }
        public List<CommonOrderdata> data { get; set; }
    }

    public class Payment
    {
        public string header { get; set; }
        public List<CommonOrderdata> data { get; set; }
    }

    public class SummaryDetails
    {
        public string header { get; set; }
        public string total { get; set; }
        public List<CommonOrderdata> data { get; set; }
    }

    public class OrderDetailResponse
    {
        public OrderHead orderhead { get; set; }
        public OrderDetails orderdetails { get; set; }
        public OrderStatus orderstatus { get; set; }
        public Payment payment { get; set; }
        public SummaryDetails summarydetails { get; set; }
        public Helpdetail helpdetails { get; set; }


    }
    public class Helpdetail
    {
        public string Type { get; set; }
        public long transId { get; set; }
        public bool IsHelp { get; set; }
    }
}
