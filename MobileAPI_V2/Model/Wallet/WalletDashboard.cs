using Nancy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace MobileAPI_V2.Model
{
    public class WalletDashboard
    {
        public string entityId { get; set; }
        public DataSet CustomerDashBoard()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@entityId",entityId),
                
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetWalletDashboard, para);
            return ds;
        }
    }
    public class DashBoardResponse
    {
        public LstDashBoard Response { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
    public class DashBoardList
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<DashBoardData> lstDashBoard { get; set; }

    }
    public class LstDashBoard
    {
        public List<DashBoardList> DashBoardList { get; set; }
        public List<UPIData> UPIList { get; set; }
        public List<PANVerificationOption> PANList { get; set; }
        public decimal WalletBalance { get; set; }
        public string KYCStatus { get; set; }

        //Change
        public string FullName { get; set; }
        public string DOB { get; set; }

        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string Caddress { get; set; }
        public string Paddress { get; set; }
        public string EmailId { get; set; }
        public string Token { get; set; }
        public string MonthlyLimit { get; set; }
        public string RemainingLimit { get; set; }
        public int IsCardApply { get; set; }
        public int Isupgrade { get; set; }
        public int IsOldCard { get; set; }
        public string KYCPaymentStatus { get;  set; }
        public string KYCText { get;  set; }
        public decimal KYCPayment { get; set; }
        public string PANVerification { get; set; }
    }
    public class UPIData
    {
        public string UPI { get; set; }
        public int IsPrimary { get; set; }

    }
    public class DashBoardData
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }

    }
    public class PANVerificationOption
    {
        public string Name { get; set; }
        public bool? Isvisible { get; set; }
    }
}
