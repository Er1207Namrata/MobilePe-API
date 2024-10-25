using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class DashboardEcomm
    {
        public List<UserWalletEcomm> UserWallet { get; set; }
        public decimal totalAmount { get; set; }
        public List<CompanyTransactionDetailsEcomm> CompanyTransactionDetails { get; set; }
    }
    public class DashboardResponseEcomm
    {
        public List<UserWalletEcomm> UserWallet { get; set; }
        public List<transactiondetailsEcomm> transactiondetails { get; set; }
        public decimal totalAmount { get; set; }
    }
    public class AllLedgerResponseEcomm
    {
        public List<ResultCommonEcomm> summarydata { get; set; }
        public List<ResultCommonEcomm> transactiondata { get; set; }
    }

    public class TeamStatusResponseV2Ecomm
    {
        public List<BindDropDownEcomm> topHeader { get; set; }
        public CugItemRootEcomm data { get; set; }

    }

    public class DatumEcomm
    {
        public string text { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        public int levelId { get; set; }
    }

   

    public class CugItemEcomm
    {
        public string header { get; set; }
        public List<DatumEcomm> data { get; set; }
    }

    public class CugItemRootEcomm
    {
        public List<CugItemEcomm> cugItems { get; set; }
    }


    public class CugItemTempEcomm
    {
        public List<BindDropDownEcomm> header1 { get; set; }
        public List<BindDropDownEcomm> header2 { get; set; }
        public List<DatumEcomm> data { get; set; }
    }




    public class TeamStatusDataEcomm
    {
        public string Level { get; set; }
        public string Team { get; set; }
        public string Card { get; set; }
        public string SuperUser { get; set; }
        public string ActivatedCard { get; set; }
        public string PhysicalCard { get; set; }
    }

    public class UserWalletEcomm
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string name { get; set; }
    }
    public class CompanyTransactionDetailsEcomm
    {
        public string Type { get; set; }

        public List<transactiondetailsEcomm> data { get; set; }

    }
    public class transactiondetailsEcomm
    {
        public string Type { get; set; }
        public string value { get; set; }
        public string name { get; set; }
        public string amountInWords { get; set; }
    }
    public class FREECARDCOSTRequestEcomm
    {
        public int ProductId { get; set; }
        public int ProcId { get; set; }
        public decimal Amount { get; set; }
        public string MobileNo { get; set; }
        public string transactionId { get; set; }
        public long memberId { get; set; }
    }

    public class Bank_accountRequestEcomm
    {
        public string name { get; set; }
        public string ifsc { get; set; }
        public string account_number { get; set; }

    }
    public class FundAccountRequestEcomm
    {
        public string contact_id { get; set; }
        public string account_type { get; set; }

        public Bank_accountRequestEcomm bank_account { get; set; }

    }

    public class Bank_accountResponseEcomm
    {

        public string bank_name { get; set; }
        public string name { get; set; }
        public string account_number { get; set; }
        public string ifsc { get; set; }

    }
    public class Bankdetails_accountResponseEcomm
    {
        public string ifsc { get; set; }
        public string bank_name { get; set; }
        public string name { get; set; }
        public string account_number { get; set; }
        public string bankId { get; set; }
        public decimal amount { get; set; }

    }
    public class FundAccountResponseEcomm
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string contact_id { get; set; }
        public string account_type { get; set; }
        public Bank_accountResponseEcomm bank_account { get; set; }
        public bool active { get; set; }
        public int created_at { get; set; }

    }
    public class razorpayresponseEcomm
    {
        public int statuscode { get; set; }
        public string responseText { get; set; }
    }
    public class CreateContactResponseEcomm
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
    public class CreateRazorPayContactRequestEcomm
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
    public class payoutsRequestEcomm
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
    public class payoutsdataRequestEcomm
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
    public class payoutresponseEcomm
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
    public class TopheaderEcomm
    {
        public string header { get; set; }
        public List<DashboardCommondataEcomm> data { get; set; }
    }
    public class TopheaderNew
    {
        public string header { get; set; }
        public List<SubServices> data { get; set; }
        public string ImgUrl { get;  set; }
        public string Description { get;  set; }
        public string Notes { get;  set; }
        public int IsNew { get;  set; }
    }
    public class VocherNew
    {
        public string header { get; set; }
        public List<VoucherData> data { get; set; }
    }
    public class DashboardCommondataEcomm
    {
        public string text { get; set; }
        public string value { get; set; }
        public string url { get; set; }
        public string link { get; set; }
        public string applink { get; set; }
        public bool IsActive { get; set; }
        public string Backcolor { get; set; }
    }
    public class CriteriaData
    {
        public string text { get; set; }
        public string value { get; set; }
        public string url { get; set; }
       
        public bool IsActive { get; set; }
        public string Backcolor { get; set; }
    }
    public class Banner1
    {
        public string text { get; set; }
        public string link { get; set; }
        public string url { get; set; }

    }
    public class ReferEranDash
    {
        public string SrNo { get; set; }
        public string Text { get; set; }
       
    }
    public class VoucherData
    {
        public string Url { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }

    }
    public class MainServices
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
     
    }
    public class SubServices
    {
        public int ServiceId { get; set; }
        public string value { get; set; }
        public string DisplayName { get; set; }
        public string ImgUrl { get; set; }
        public string applink { get; set; }
        public string link { get; set; }
        public bool IsActive { get; set; }
        public bool IsNew { get; set; }
        public int billerCategoryID { get; set; }
        public bool IsPinCode { get; set; }

    }
    public class Data1Ecomm
    {
        public string header { get; set; }
        public List<DashboardCommondataEcomm> data { get; set; }
    }

    public class Data2Ecomm
    {
        public string header { get; set; }
        public List<DashboardCommondataEcomm> data { get; set; }
    }

    public class Data3Ecomm
    {
        public string header { get; set; }
        public List<DashboardCommondataEcomm> data { get; set; }
    }

    public class DashboardV2ResponseEcomm
    {
        public bool IsCardApplied { get; set; }
        public bool IsChangePassword { get; set; }
        public bool IsLogout { get; set; }
        public List<bannerEcomm> topbanner { get; set; }
        public TopheaderEcomm topheader { get; set; }
        public Data1Ecomm data1 { get; set; }
        public List<bannerEcomm> middlebanner { get; set; }
        public Data2Ecomm data2 { get; set; }
        public Data3Ecomm data3 { get; set; }
      
    }


    public class DashboardV3ResponseEcomm
    {

        public DashboardCardResponseEcomm card { get; set; }
        public List<DashboardCommondataEcomm> reward { get; set; }
        public List<bannerEcomm> banner1 { get; set; }
        public List<bannerEcomm> banner2 { get; set; }       
        public Data1Ecomm data1 { get; set; }
        public Data1Ecomm data2 { get; set; }
        public Data1Ecomm data3 { get; set; }
        public Data1Ecomm data4 { get; set; }
        public Data1Ecomm data5 { get; set; }


    }
    public class DashboardV5ResponseEcomm
    {
        public DashboardCardResponseV5Ecomm card { get; set; }
        public List<DashboardCommondataEcomm> reward { get; set; }
        public List<bannerEcomm> banner1 { get; set; }
        public List<bannerEcomm> banner2 { get; set; }
        public List<ThriweDataEcomm> thriweList { get; set; }
        public List<ThriweTextEcomm> thriwetext { get; set; }
        public List<TopheaderEcomm> maindata { get; set; }
        public Data1Ecomm voucher { get; set; }
        public Data1Ecomm mall { get; set; }

    }
    public class DashboardV5ResponseNew
    {
        public DashboardCardResponseV5Ecomm card { get; set; }
        public List<CriteriaData> reward { get; set; }
        public List<Banner1> banner1 { get; set; }
        public List<Banner1> banner2 { get; set; }
        public List<ReferEranDash> refertext { get; set; }
        public List<TopheaderNew> maindata { get; set; }
        public List<VocherNew> voucher { get; set; }

        public int IsTopayCard { get; set; }
        public int IsPineCard { get; set; }
        public string  LoanApplicationId { get; set; }


    }
    public class bannerEcomm
    {
        public string Link { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
    }
    public class DashboardCardResponseEcomm
    {
        public string ScrolleText { get; set; }
        public bool IsChangePassword { get; set; }
        public bool IsLogout { get; set; }
        public decimal cardAppliedFee{get;set; }
        public bool IsCardApplied { get; set; }
        public decimal cardUpgradeFee { get; set; }
        public bool IsCardUpgrade { get; set; }
    }
    public class DashboardCardResponseV5Ecomm
    {
        public string ScrolleText { get; set; }
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
       
        public List<BindDropDown2Ecomm> cardVarient { get; set; }
    }

    public class MallProduct
    {
        //public string Fk_MemId { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public string orderId { get; set; }
        public string url { get; set; }
      
    }
}
