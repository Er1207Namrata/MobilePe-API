using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Dashboard
    {
        public List<UserWallet> UserWallet { get; set; }
        public decimal totalAmount { get; set; }
        public List<CompanyTransactionDetails> CompanyTransactionDetails { get; set; }
    }
    public class DashboardResponse
    {
        public List<UserWallet> UserWallet { get; set; }
        public List<transactiondetails> transactiondetails { get; set; }
        public decimal totalAmount { get; set; }
    }
    public class AllLedgerResponse
    {
        public List<ResultCommon> summarydata { get; set; }
        public List<ResultCommon> transactiondata { get; set; }
    }

    public class TeamStatusResponseV2
    {
        public List<BindDropDown> TopHeader { get; set; }
        public CugItemRoot data { get; set; }

    }

    public class Datum
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public int LevelId { get; set; }
    }

   

    public class CugItem
    {
        public string header { get; set; }
        public List<Datum> data { get; set; }
    }

    public class CugItemRoot
    {
        public List<CugItem> cugItems { get; set; }
    }


    public class CugItemTemp
    {
        public List<BindDropDown> header1 { get; set; }
        public List<BindDropDown> header2 { get; set; }
        public List<Datum> data { get; set; }
    }




    public class TeamStatusData
    {
        public string Level { get; set; }
        public string Team { get; set; }
        public string Card { get; set; }
        public string SuperUser { get; set; }
        public string ActivatedCard { get; set; }
        public string PhysicalCard { get; set; }
    }

    public class UserWallet
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string name { get; set; }
    }
    public class CompanyTransactionDetails
    {
        public string Type { get; set; }

        public List<transactiondetails> data { get; set; }

    }
    public class transactiondetails
    {
        public string Type { get; set; }
        public string value { get; set; }
        public string name { get; set; }
        public string amountInWords { get; set; }
    }
    public class FREECARDCOSTRequest
    {
        public int ProductId { get; set; }
        public int ProcId { get; set; }
        public decimal Amount { get; set; }
        public string MobileNo { get; set; }
        public string transactionId { get; set; }
        public long memberId { get; set; }
    }

    public class Bank_accountRequest
    {
        public string name { get; set; }
        public string ifsc { get; set; }
        public string account_number { get; set; }

    }
    public class FundAccountRequest
    {
        public string contact_id { get; set; }
        public string account_type { get; set; }

        public Bank_accountRequest bank_account { get; set; }

    }

    public class Bank_accountResponse
    {

        public string bank_name { get; set; }
        public string name { get; set; }
        public string account_number { get; set; }
        public string ifsc { get; set; }

    }
    public class Bankdetails_accountResponse
    {
        public string ifsc { get; set; }
        public string bank_name { get; set; }
        public string name { get; set; }
        public string account_number { get; set; }
        public string bankId { get; set; }
        public decimal amount { get; set; }

    }
    public class FundAccountResponse
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string contact_id { get; set; }
        public string account_type { get; set; }
        public Bank_accountResponse bank_account { get; set; }
        public bool active { get; set; }
        public int created_at { get; set; }

    }
    public class razorpayresponse
    {
        public int statuscode { get; set; }
        public string responseText { get; set; }
    }
    public class CreateContactResponse
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string type { get; set; }
        public string reference_id { get; set; }
        public string id { get; set; }
        public bool active { get; set; }
        public string created_at { get; set; }
        public string entity { get; set; }

    }
    public class CreateRazorPayContactRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string type { get; set; }
        public string reference_id { get; set; }
        public string transactionId { get; set; }
        public int amount { get; set; }
        public int Fk_CategoryId { get; set; }
        public long memberId { get; set; }
        public string account_number { get; set; }
        public string ifsc { get; set; }


    }
    public class payoutsRequest
    {
        public string account_number { get; set; }
        public string fund_account_id { get; set; }
        public int amount { get; set; }
        public long Pk_CustomerPayoutId { get; set; }
        public string currency { get; set; }
        public string mode { get; set; }
        public string purpose { get; set; }
        public bool queue_if_low_balance { get; set; }
        public string reference_id { get; set; }
        public string narration { get; set; }
        public string created_at { get; set; }
        public string status { get; set; }
        public string entity { get; set; }
        public string payoutId { get; set; }


    }
    public class payoutsdataRequest
    {
        public string account_number { get; set; }
        public string fund_account_id { get; set; }
        public int amount { get; set; }

        public string currency { get; set; }
        public string mode { get; set; }
        public string purpose { get; set; }
        public bool queue_if_low_balance { get; set; }
        public string reference_id { get; set; }
        public string narration { get; set; }




    }
    public class payoutresponse
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string fund_account_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }

        public int fees { get; set; }
        public int tax { get; set; }
        public string status { get; set; }
        public IList<object> utr { get; set; }
        public string mode { get; set; }
        public string purpose { get; set; }
        public string reference_id { get; set; }
        public string narration { get; set; }

        public int created_at { get; set; }

    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Topheader
    {
        public string header { get; set; }
        public List<DashboardCommondata> data { get; set; }
    }
    public class DashboardCommondata
    {
        public string text { get; set; }
        public string value { get; set; }
        public string url { get; set; }
        public string link { get; set; }
        public string applink { get; set; }
        public bool IsActive { get; set; }
        public string Backcolor { get; set; }
    }

    public class Data1Ecom
    {
        public string header { get; set; }
        public List<DashboardCommondataEcomm> data { get; set; }
    }

    public class Data1
    {
        public string header { get; set; }
        public List<DashboardCommondata> data { get; set; }
    }

    public class Data2
    {
        public string header { get; set; }
        public List<DashboardCommondata> data { get; set; }
    }

    public class Data3
    {
        public string header { get; set; }
        public List<DashboardCommondata> data { get; set; }
    }

    public class DashboardV2Response
    {
        public bool IsCardApplied { get; set; }
        public bool IsChangePassword { get; set; }
        public bool IsLogout { get; set; }
        public List<banner> topbanner { get; set; }
        public Topheader topheader { get; set; }
        public Data1 data1 { get; set; }
        public List<banner> middlebanner { get; set; }
        public Data2 data2 { get; set; }
        public Data3 data3 { get; set; }
      
    }


    public class DashboardV3Response
    {

        public DashboardCardResponse card { get; set; }
        public List<DashboardCommondata> reward { get; set; }
        public List<banner> banner1 { get; set; }
        public List<banner> banner2 { get; set; }       
        public Data1 data1 { get; set; }
        public Data1 data2 { get; set; }
        public Data1 data3 { get; set; }
        public Data1 data4 { get; set; }
        public Data1 data5 { get; set; }


    }
    public class DashboardV5Response
    {

        public int Status { get; set; }

        public DashboardCardResponseV5 card { get; set; }
        public List<DashboardCommondata> reward { get; set; }
        public List<banner> banner1 { get; set; }
        public List<banner> banner2 { get; set; }
        public List<ThriweData> thriweList { get; set; }
        public List<ThriweText> thriwetext { get; set; }
        public List<Topheader> maindata { get; set; }
        public Data1 voucher { get; set; }
        public Data1 mall { get; set; }
       

    }
    public class banner
    {
        public string Link { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
    }
    public class DashboardCardResponse
    {
        public string ScrolleText { get; set; }
        public bool IsChangePassword { get; set; }
        public bool IsLogout { get; set; }
        public decimal cardAppliedFee{get;set; }
        public bool IsCardApplied { get; set; }
        public decimal cardUpgradeFee { get; set; }
        public bool IsCardUpgrade { get; set; }
    }

    public class AvailableKitForVirtual
    {

        public string OldKit { get; set; }
        public string KitNo { get; set; }
    }
    public class DashboardCardResponseV5
    {
        public string ScrolleText { get; set; }
        public string Status { get; set; }
        public string ToPayToken { get; set; }
        public string EntityId { get; set; }
        public string JwtToken { get; set; }
        public int ThriweCoupon { get; set; }
        public bool IsChangePassword { get; set; }
        public bool IsLogout { get; set; }
        public decimal cardAppliedFee { get; set; }
        public bool IsCardApplied { get; set; }
        public decimal cardUpgradeFee { get; set; }
        public bool IsCardUpgrade { get; set; }
        public bool IsWalletActive { get; set; }
        public bool IsPaymentGatewayActive { get; set; }
        public List<BindDropDown2> cardVarient { get; set; }
    }
}
