namespace MobileAPI_V2.Model.BillPayment
{
    public class BillPayment
    {
        public string ServiceName { get; set; }
        public string OpertorCode { get; set; }
        public string Amount { get; set; }
        public string RechargeNumber { get; set; }
        public string TransId { get; set; }
        public string circle { get; set; }
        public string MobileNo { get; set; }
        public string PinNo { get; set; }
        public string Fk_MemId { get; set; }
        public string RechargeType { get; set; }
        public string PaymentId { get; set; }
        public string OrderId { get; set; }

        public string CustomerId { get; set; }
    }
    public class BillPaymentResponse
    {
        public string Fk_MemId { get; set; }
        public string Status { get; set; }
        public string TransId { get; set; }
        public string Balance { get; set; }
        public string ServiceName { get; set; }
        public string Amount { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public string RechargeType { get; set; }
        public string RechargeNumber { get; set; }
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public string circle { get; set; }
        public string OpertorCode { get; set; }
        public string CustomerId { get; set; }

    }
}
