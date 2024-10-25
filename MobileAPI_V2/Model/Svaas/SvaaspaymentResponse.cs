using System.Runtime.CompilerServices;

namespace MobileAPI_V2.Model.Svaas
{
    public class SvaaspaymentResponse
    {
        public string MobileNo { get; set; }
        public string UserId { get; set; }
        public string TransactionNo { get; set; }
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string CustomerMobileNo { get; set; }
        public string CustomerEmail { get; set; }
        public string Amount { get; set; }
        public string BillerName { get; set; }
        public string BillerMobileNo { get; set; }
        public string BillerEmail { get; set; }
        public string BillerAddress { get; set; }
        public string Pincode { get; set; }
        public string ProductDescription { get; set; }
        public string ProductLink { get; set; }
    }
    public class SvaasSaveResponse
    {
        public int flag { get; set; }
        public string Message { get; set; }
    }
}
