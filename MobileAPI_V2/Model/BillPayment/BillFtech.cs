namespace MobileAPI_V2.Model.BillPayment
{
    public class BillPaymentFetch
    {
        public string serviceName { get; set; }
        public string customerNumber { get; set; }
        public string MobileNo { get; set; }
        public string PinNo { get; set; }
        public string Type { get; set; }
        public string Optional { get; set; }
        public string Optional1 { get; set; }
    }
   
    public class BillPayResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public string CustomerName { get; set; }
        public string Billamount { get; set; }
        public string Billdate { get; set; }
        public string Duedate { get; set; }
        public string desc { get; set; }
       
    }
}
