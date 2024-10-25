using System.Collections.Generic;
using static OfficeOpenXml.FormulaParsing.EpplusExcelDataProvider;
using System.Security.Cryptography.Xml;
using System.Data.SqlClient;
using System.Data;
using System;

namespace MobileAPI_V2.Model
{
    public class RegisterWalletRequest
    {
        public string entityId { get; set; }
        public string kycType { get; set; }
        public string channelName { get; set; }
        public string entityType { get; set; }
        public string businessType { get; set; }
        public string businessId { get; set; }
        public string otp { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public bool isNRICustomer { get; set; }
        public bool isMinor { get; set; }
        public bool isDependant { get; set; }
        public string maritalStatus { get; set; }
        public string countryCode { get; set; }
        public string employmentIndustry { get; set; }
        public string employmentType { get; set; }
        public string plasticCode { get; set; }
        public string pin { get; set; }
        public string password { get; set; }
        public string IsLoungeCards { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public List<KitInfo> kitInfo { get; set; }
        public List<AddressInfo> addressInfo { get; set; }
        public List<CommunicationInfo> communicationInfo { get; set; }
        public List<KycInfo> kycInfo { get; set; }
        public List<DateInfo> dateInfo { get; set; }
        public string TransactionId { get; set; }



    }


    
    public class RegisterWallet 
    {

        public int Id { get; set; }
        public string channelName { get; set; }
        public string entityId { get; set; }
        public string entityType { get; set; }
        public string businessType { get; set; }
        public string businessId { get; set; }
        public string otp { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public bool isNRICustomer { get; set; }
        public bool isMinor { get; set; }
        public bool isDependant { get; set; }
        public string maritalStatus { get; set; }
        public string countryCode { get; set; }
        public string employmentIndustry { get; set; }
        public string employmentType { get; set; }
        public string plasticCode { get; set; }
        public List<KitInfo> kitInfo { get; set; }
        public List<AddressInfo> addressInfo { get; set; }
        public List<CommunicationInfo> communicationInfo { get; set; }
        public List<KycInfo> kycInfo { get; set; }
        public List<DateInfo> dateInfo { get; set; }
        public string cardType { get; set; }
        public string cardCategory { get; set; }
        public string cardRegStatus { get; set; }
        public string Paddress1 { get; set; }
        public string Paddress2 { get; set; }
        public string Paddress3 { get; set; }
        public string Pcity { get; set; }
        public string Pstate { get; set; }
        public string Pcountry { get; set; }
        public string PpinCode { get; set; }
        public string Caddress1 { get; set; }
        public string Caddress2 { get; set; }
        public string Caddress3 { get; set; }
        public string Ccity { get; set; }
        public string Cstate { get; set; }
        public string Ccountry { get; set; }
        public string CpinCode { get; set; }
        public string contactNo { get; set; }
        public bool notification { get; set; }
        public string emailId { get; set; }
        public string documentType { get; set; }
        public string documentNo { get; set; }
        public string documentExpiry { get; set; }
        public string dob { get; set; }
        public int ismerchant { get; set; }
        public string KITNO { get; set; }
        public string Merchant { get; set; }
        public string token { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Utility { get; set; }
        public bool IsChecked { get; set; }
        public string ProductName { get; set; }
        public string CFirmType { get; set; }
        public string COneTimeFee { get; set; }
        public string CchannelPartnerShair { get; set; }
        public string PTechMobileNo { get; set; }
        public string PTechEmail { get; set; }
        public string PTechName { get; set; }
        public string TechnicalTeam { get; set; }
        public string Ppan { get; set; }
        public string password { get; set; }
        public string DeviceId { get; set; }
        public string FileBaseToken { get; set; }
        public string kycType { get; set; }
        public string pin { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public DataSet SaveRegistration()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@entityId",entityId),
                new SqlParameter("@channelName",channelName),
                new SqlParameter("@entityType",entityType),
                new SqlParameter("@businessType",businessType),
                new SqlParameter("@businessId",businessId),
                new SqlParameter("@title",title),
                new SqlParameter("@firstName",firstName),
                new SqlParameter("@middleName",middleName),
                new SqlParameter("@lastName",lastName),
                new SqlParameter("@gender",gender),
                new SqlParameter("@isNRICustomer",isNRICustomer),
                new SqlParameter("@isMinor",isMinor),
                new SqlParameter("@isDependant",isDependant),
                new SqlParameter("@maritalStatus",maritalStatus),
                new SqlParameter("@countryCode",countryCode),
                new SqlParameter("@employmentIndustry",employmentIndustry),
                new SqlParameter("@employmentType",employmentType),
                new SqlParameter("@plasticCode",plasticCode),
                new SqlParameter("@cardType",cardType),
                new SqlParameter("@cardCategory",cardCategory),
                new SqlParameter("@cardRegStatus",cardRegStatus),
                new SqlParameter("@Paddress1",Paddress1),
                new SqlParameter("@Paddress2",Paddress2),
                new SqlParameter("@Paddress3",Paddress3),
                new SqlParameter("@Pcity",Pcity),
                new SqlParameter("@Pstate",Pstate),
                new SqlParameter("@Pcountry",Pcountry),
                new SqlParameter("@PpinCode",PpinCode),
                new SqlParameter("@Caddress1",Caddress1),
                new SqlParameter("@Caddress2",Caddress2),
                new SqlParameter("@Caddress3",Caddress3),
                new SqlParameter("@Ccity",Ccity),
                new SqlParameter("@Cstate",Cstate),
                new SqlParameter("@Ccountry",Ccountry),
                new SqlParameter("@CpinCode",CpinCode),
                new SqlParameter("@contactNo",contactNo),
                new SqlParameter("@notification",notification),
                new SqlParameter("@emailId",emailId),
                new SqlParameter("@documentType",documentType),
                new SqlParameter("@documentNo",documentNo),
                new SqlParameter("@documentExpiry",documentExpiry),
                new SqlParameter("@dob",dob),
                new SqlParameter("@ismerchant",ismerchant),
                new SqlParameter("@KITNO",KITNO),
                new SqlParameter("@Merchant",Merchant),
                new SqlParameter("@token",token),
                new SqlParameter("@password",password),
                new SqlParameter("@pin",pin),
                new SqlParameter("@DeviceId", DeviceId),
                new SqlParameter("@FileBaseToken", FileBaseToken),
                new SqlParameter("@kycType", kycType)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.SaveRegistration, para);
            return ds;
        }

    }
    public class AddressInfo
    {
        public string addressCategory { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pinCode { get; set; }
    }

    public class CommunicationInfo
    {
        public string contactNo { get; set; }
        public bool notification { get; set; }
        public string emailId { get; set; }
    }

    public class DateInfo
    {
        public string dateType { get; set; }
        public string date { get; set; }
    }

    public class KitInfo
    {
        public string cardType { get; set; }
        public string kitNo { get; set; }
        public string cardCategory { get; set; }
        public string cardRegStatus { get; set; }
        public string aliasName { get; set; }
       
    }

    public class KycInfo
    {
        public string documentType { get; set; }
        public string documentNo { get; set; }
        public string documentExpiry { get; set; }
    }
    public class ReigterWalletResponse
    {
        public ExceptionData exception { get; set; }
        public ResultWallet result { get; set; }
    }
    public class ResultWallet
    {
        public string entityId { get; set; }
        public string kycStatus { get; set; }
        public string token { get; set; }
        public string KitNo { get; set; }
        public bool valid { get; set; }
    }
    public class ExceptionData
    {
        public string detailMessage { get; set; }
        public string cause { get; set; }
        public string shortMessage { get; set; }
        public string languageCode { get; set; }
        public string errorCode { get; set; }
        public object fieldErrors { get; set; }
        public List<object> suppressed { get; set; }
    }
}
