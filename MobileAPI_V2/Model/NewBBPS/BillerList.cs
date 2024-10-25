using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace MobileAPI_V2.Model
{
    public class BillerList
    {
        public string billerCategoryID { get; set; }
        public string billerCategory { get; set; }
        public string billerID { get; set; }
        public string billerName { get; set; }
        public string billerAlaisName { get; set; }
        public string noofTextBox { get; set; }
        public string textboxDescription1 { get; set; }
        public string optionalParam1 { get; set; }
        public string dataType1 { get; set; }
        public string minlimit1 { get; set; }
        public string maxlimit1 { get; set; }
        public string regex1 { get; set; }
        public string textboxDescription2 { get; set; }
        public string optionalParam2 { get; set; }
        public string dataType2 { get; set; }
        public string minlimit2 { get; set; }
        public string maxlimit2 { get; set; }
        public string regex2 { get; set; }
        public string textboxDescription3 { get; set; }
        public string optionalParam3 { get; set; }
        public string dataType3 { get; set; }
        public string minlimit3 { get; set; }
        public string maxlimit3 { get; set; }
        public string regex3 { get; set; }
        public string textboxDescription4 { get; set; }
        public string optionalParam4 { get; set; }
        public string dataType4 { get; set; }
        public string minlimit4 { get; set; }
        public string maxlimit4 { get; set; }
        public string regex4 { get; set; }
        public string textboxDescription5 { get; set; }
        public string optionalParam5 { get; set; }
        public string dataType5 { get; set; }
        public string minlimit5 { get; set; }
        public string maxlimit5 { get; set; }
        public string regex5 { get; set; }
        public string fetchRequirement { get; set; }
        public string paymentAmountExactness { get; set; }
        public string supportBillValidation { get; set; }
        public string minLimit { get; set; }
        public string maxLimit { get; set; }
        public string status { get; set; }
        public string supportPendingStatus { get; set; }
        public string billerAcceptsAdhoc { get; set; }
        public string billerCoverage { get; set; }
        public string billerMode { get; set; }
        public string state { get; set; }
        public string stateRefID { get; set; }
    }
    public class BillerRequest
    {
        public string CategoryID { get; set; }
        public string LocationID { get; set; }
        public DataSet SaveBillProviders(string BillerCode,string Type,string BillerName)
        {
            SqlParameter[] para =
                 {
                  new SqlParameter("@BillerCode", BillerCode),
                  new SqlParameter("@Type", Type),
                  new SqlParameter("@BillerName", BillerName)

                };
            DataSet ds = Connection.ExecuteQuery("SaveBillProviders",para);
            return ds;
        }

    }
    public class BillerSessionDetails
    {
        public string session_id { get; set; }
        public string token_id { get; set; }
        public string sid { get; set; }
    }
    public class BillerResponse
    {
        public string response_code { get; set; }
        public string response_message { get; set; }
        public BillerSessionDetails session_details { get; set; }
        public List<BillerList> Data { get; set; }
    }

    
}
