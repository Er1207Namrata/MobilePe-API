using System.Collections.Generic;

namespace MobileAPI_V2.Model
{
    public class BBPSBillFetch
    {
        public string response_code { get; set; }
        public string response_message { get; set; }
        public string amount { get; set; }
        public string duedate { get; set; }
        public string billdate { get; set; }
        public string billperiod { get; set; }
        public string billNumber { get; set; }
        public string customerName { get; set; }
        public string billertransactionID { get; set; }
        public string aptTransactionID { get; set; }
    }
    public class BillFetchres
    {
        public string responsecode { get; set; }
        public string responsedescription { get; set; }
        public string amount { get; set; }
        public string duedate { get; set; }
        public string billdate { get; set; }
        public string billperiod { get; set; }
        public string billNumber { get; set; }
        public string customerName { get; set; }
        public string complianceReason { get; set; }
        public string complaincerespcd { get; set; }
        public string CpTransactionid { get; set; }
    }

    public class BillFetchReq
    {
        
        public string customer_mobile { get; set; }
        public string billerid { get; set; }
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
        public string nooftextbox { get; set; }
      
        public string AgentMobileNo { get; set; }
       
        public string CpTransactionid { get; set; }
      
        public string customername { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string GeoCode { get; set; }

        public string emailid { get; set; }
        public string imei { get; set; }

        public string ip { get; set; }
        public string mac { get; set; }

       
        public string EntityID { get; set; }
        public string SecretID { get; set; }
        public string Password { get; set; }

        public string CPID { get; set; }
       


    }

}
