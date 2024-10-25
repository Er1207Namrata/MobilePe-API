using System.Collections.Generic;

namespace MobileAPI_V2.Model.Svaas
{
    public class SvaasOrdersResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public OrderResult result { get; set; }
    }
    public class OrderResult
    {
        public List<OrderData> lstOrders { get; set; }
    }
    public class OrderData
    {
        public string TransactionNo { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Amount { get; set; }
        public string BillerName { get; set; }
        public string BillerMobileNo { get; set; }
        public string BillerEmail { get; set; }
        public string BillerAddress { get; set; }
        public string Pincode { get; set; }

        


    }
}
