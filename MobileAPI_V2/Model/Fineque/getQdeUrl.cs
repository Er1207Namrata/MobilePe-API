using MobileAPI_V2.Model.Travel.Hotel;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Fineque
{
    public class QdeUrl
    {
        public string partnerId {  get; set; }  
        public string mobile { get; set;}
        public string productCode { get; set;}
        public string ProductName { get; set;}
        public string partnerMerchantID { get; set;}
        public string partnerPincode { get; set;}
        public string partnerShopType { get; set;}
        public string Fk_MemId { get; set;}
        public string CreateDate { get; set;}
        public string LoanApplicationId { get; set;}
        public string fullName { get; set;}
        public string state { get; set;}
        public string city { get; set;}
        public string LoanAmount { get; set;}
        public string LoanStatus { get; set;}

    }
    public class QdeUrlAPiRequest
    {
        public string partnerId { get; set; }
        public string mobile { get; set; }
        public string productCode { get; set; }
        public string partnerMerchantID { get; set; }
        public string partnerPincode { get; set; }
        public string partnerShopType { get; set; }
    }
    public class QdeUrlResponse: FinqueError
    {
        public string Code { get; set; }
        public string result { get; set; }
        public string qdeUrl { get; set; }
        public string Tid { get; set; }
        public string type { get; set; }
        public string transactionId { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string traceId { get; set; }
        public List<QdeUrl> error { get; set; }
    }

    public class FinqueError
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
    }

    public class SaveLoanRegistrationRes
    {
        public string Flag { get; set; }
        public string Message { get; set; }
        
    }
}
