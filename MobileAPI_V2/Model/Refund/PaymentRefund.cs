namespace MobileAPI_V2.Model.Refund
{
    public class PaymentRefund
    {
        public string id { get; set; }
        public string entity { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string payment_id { get; set; }

        public string type { get; set; }
        public object receipt { get; set; }
        public int created_at { get; set; }
        public string status { get; set; }
        public string speed_processed { get; set; }
        public string speed_requested { get; set; }
    }
    public class PaymentRefundRequest
    {
        public string receipt { get; set; }
        public string speed { get; set; }
    }
}
