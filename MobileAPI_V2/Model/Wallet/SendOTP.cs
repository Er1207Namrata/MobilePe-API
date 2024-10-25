using System.Collections.Generic;

namespace MobileAPI_V2.Model
{ 
    public class SendOTP
    {
        public string entityId { get; set; }
        public string mobileNumber { get; set; }
        public string businessType { get; set; }
        public string entityType { get; set; }
    }
    public class SendOTPResponse
    {
        public ExceptionData exception { get; set; }
        public ResultSendOTP result { get; set; }
    }
    
    public class ResultSendOTP
    {
        public bool success { get; set; }
        public string entityId { get; set; }
    }
    public class ApplyCardResponse
    {
        public bool result { get; set; }
    }
    public class WalletRecharge
    {
        public long memberId { get; set; }
        public string TOPUPAMOUNT { get; set; }
        public string TRANSACTIONID { get; set; }
        public string entityId { get; set; }
        public string kitNo { get; set; }
        public string oldKitNo { get; set; }
        public string newKitNo { get; set; }
        public string flag { get; set; }
        public string reason { get; set; }
        public string IsLoungeCards { get; set; }


        public string CUSTOMERMOBILENO { get; set; }
        public dynamic PaymentId { get; set; }
        public string OrderId { get; set; }
        public string cardType { get; set; }
        public string business { get; set; }
        public long Fk_MemId { get; set; }
        public string KITNO { get; set; }
    }
}
