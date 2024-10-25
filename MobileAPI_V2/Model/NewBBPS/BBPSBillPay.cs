using System.Collections.Generic;
using System.Data;

namespace MobileAPI_V2.Model
{
    public class BBPSBillPay
    {
        public string response_code { get; set; }
        public string response_message { get; set; }
        public string amount { get; set; }
        public string duedate { get; set; }
        public string billdate { get; set; }
        public string billperiod { get; set; }
        public string billNumber { get; set; }
        public string customerName { get; set; }
        public string transactionID { get; set; }
        public string transactionStatus { get; set; }
        public string transactionDate { get; set; }
        public string fetchRequirement { get; set; }
        public string paymentAmountExactness { get; set; }
        public string supportBillValidation { get; set; }
        public string billerAcceptsAdhoc { get; set; }
        public string fetchamount { get; set; }
        public string aadhaarNumber { get; set; }
        public string pan { get; set; }
        public string email { get; set; }
        public string vendorRefTD { get; set; }
        public string custConvFee { get; set; }
        public string custConvDesc { get; set; }

    }

    public class BBSPBillPayReq
    {
        public long Fk_MemId { get; set; }
        public string Type { get; set; }
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public string transactionID { get; set; }
        public string customerMobileNo { get; set; }
        public string CPID { get; set; }
        public string EntityID { get; set; }
        public string biller_id { get; set; }
        public string textboxname1 { get; set; }
        public string textboxname2 { get; set; }
        public string textboxname3 { get; set; }
        public string textboxname4 { get; set; }
        public string textboxname5 { get; set; }
        public string textboxvalue1 { get; set; }
        public string textboxvalue2 { get; set; }
        public string textboxvalue3 { get; set; }
        public string textboxvalue4 { get; set; }
        public string textboxvalue5 { get; set; }
        public string amount { get; set; }
        public string duedate { get; set; }
        public string billdate { get; set; }
        public string billnumber { get; set; }
        public string billperiod { get; set; }
        public string CustomerName { get; set; }
        public string nooftextbox { get; set; }
        public string ifscCode { get; set; }
        public string postalCode { get; set; }
        public string geoCode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string SecretID { get; set; }
        public string Password { get; set; }
        public string ip { get; set; }
        public string mac { get; set; }
        public string imei { get; set; }

    }

    public class BBPSBillPayres
    {
        public string responsecode { get; set; }
        public string responsedescription { get; set; }
        public string complianceReason { get; set; }
        public string amount { get; set; }
        public string duedate { get; set; }
        public string billdate { get; set; }
        public string billperiod { get; set; }
        public string billNumber { get; set; }
        public string customerName { get; set; }
        public string transactionID { get; set; }
        public string transactionStatus { get; set; }
        public string transactionDate { get; set; }
        public string TptransactionID { get; set; }
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
    }
    public class billreqres
    {
        public string flag { get; set; }
        public string message { get; set; }
    }
    public class BillTraansStatus
    {

        public string responsecode { get; set; }
        public string response_descrption { get; set; }
        public string billerid { get; set; }
        public string clientagentid { get; set; }
        public string tpTransactionID { get; set; }


    }
}
