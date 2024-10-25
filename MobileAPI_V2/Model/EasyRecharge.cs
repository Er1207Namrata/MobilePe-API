namespace MobileAPI_V2.Model
{
    public class EasyRecharge
    {
    }
    public class EasyBillFetch
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Account { get; set; }
        public string Amount { get; set; }
        public string SPKey { get; set; }
        public string APIRequestID { get; set; }
        public string Optional1 { get; set; }
        public string Optional2 { get; set; }
        public string Optional3 { get; set; }
        public string Optional4 { get; set; }
        public string GEOCode { get; set; }
        public string CustomerNumber { get; set; }
        public string Pincode { get; set; }
        public string OutletID { get; set; }
    }
    public class EasyBillFetchResponses
    {
        public string account { get; set; }
        public double amount { get; set; }
        public string rpid { get; set; }
        public string agentid { get; set; }
        public string opid { get; set; }
        public double dueamount { get; set; }
        public string customername { get; set; }
        public string duedate { get; set; }
        public string billdate { get; set; }
        public string billnumber { get; set; }
        public string bilperiod { get; set; }
        public string refid { get; set; }
        public int fetchBillID { get; set; }
        public bool isRefundStatusShow { get; set; }
        public int status { get; set; }
        public string msg { get; set; }
        public double bal { get; set; }
        public string errorcode { get; set; }
    }
    public class EasyBillPay
    {
        public string type { get; set; }

        public long transId { get; set; }
        public long memberId { get; set; }
        public string Account { get; set; }
        public string Amount { get; set; }
        public string SPKey { get; set; }
        public string APIRequestID { get; set; }
        public string Optional1 { get; set; }
        public string Optional2 { get; set; }
        public string Optional3 { get; set; }
        public string Optional4 { get; set; }
        public string GEOCode { get; set; }
        public string CustomerNumber { get; set; }
        public string Pincode { get; set; }
        public string OutletID { get; set; }
        public string RefID { get; set; }

        public int ProcId { get; set; }
    }
    public class EasyBillPayResponse
    {
        public string STATUS { get; set; }
        public string MSG { get; set; }
        public string BAL { get; set; }
        public string ERRORCODE { get; set; }
        public string ACCOUNT { get; set; }
        public string AMOUNT { get; set; }
        public string RPID { get; set; }
        public string AGENTID { get; set; }
        public string OPID { get; set; }
    }
    public class StastusCheck
    {
        public string UserID { get; set; }
        public string Token { get; set; }
        public string RPID { get; set; }
        public string AGENTID { get; set; }
        public string Optional1 { get; set; }
        
    }
    public class StatusCheckResponse
    {
        public string Status { get; set; }
        public string Statuscode { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
