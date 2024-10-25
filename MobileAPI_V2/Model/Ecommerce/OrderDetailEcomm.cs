using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{


    public class CommonOrderdataEcomm
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class OrderHeadEcomm
    {
        public string header { get; set; }
        public List<CommonOrderdataEcomm> data { get; set; }
    }

    public class OrderDetailsEcomm
    {
        public string header { get; set; }
        public string url { get; set; }
        public List<CommonOrderdataEcomm> data { get; set; }
    }

    public class OrderStatusEcomm
    {
        public string header { get; set; }
        public List<CommonOrderdata> data { get; set; }
    }

    public class PaymentEcomm
    {
        public string header { get; set; }
        public List<CommonOrderdataEcomm> data { get; set; }
    }

    public class SummaryDetailsEcomm
    {
        public string header { get; set; }
        public string total { get; set; }
        public List<CommonOrderdata> data { get; set; }
    }

    public class OrderDetailResponseEcomm
    {
        public OrderHead orderhead { get; set; }
        public OrderDetails orderdetails { get; set; }
        public OrderStatus orderstatus { get; set; }
        public Payment payment { get; set; }
        public SummaryDetails summarydetails { get; set; }
        public Helpdetail helpdetails { get; set; }


    }
    public class HelpdetailEcomm
    {
        public string Type { get; set; }
        public long transId { get; set; }
        public bool IsHelp { get; set; }
    }
}
