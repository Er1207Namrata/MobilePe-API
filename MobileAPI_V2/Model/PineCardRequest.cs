using System.Collections.Generic;

namespace MobileAPI_V2.Model
{
    public class AcquirerData
    {
        public string rrn { get; set; }
    }

    public class Entity
    {
        public string id { get; set; }
        public string entity { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string status { get; set; }
        public string order_id { get; set; }
        public object invoice_id { get; set; }
        public bool international { get; set; }
        public string method { get; set; }
        public int amount_refunded { get; set; }
        public object refund_status { get; set; }
        public bool captured { get; set; }
        public object description { get; set; }
        public object card_id { get; set; }
        public object bank { get; set; }
        public object wallet { get; set; }
        public string vpa { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public Notes notes { get; set; }
        public int fee { get; set; }
        public int tax { get; set; }
        public object error_code { get; set; }
        public object error_description { get; set; }
        public object error_source { get; set; }
        public object error_step { get; set; }
        public object error_reason { get; set; }
        public AcquirerData acquirer_data { get; set; }
        public int created_at { get; set; }
        public Upi upi { get; set; }
        public int base_amount { get; set; }
    }

    public class Notes
    {
        public string custom_field { get; set; }
    }

   
    public class PineCardRequest
    {
        public string entity { get; set; }
        public string account_id { get; set; }
        public string @event { get; set; }
        public List<string> contains { get; set; }
        public Payload payload { get; set; }
        public int created_at { get; set; }
    }

    public class Upi
    {
        public string payer_account_type { get; set; }
        public string vpa { get; set; }
    }
}
