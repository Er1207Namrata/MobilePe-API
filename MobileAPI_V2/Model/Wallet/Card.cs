using MobileAPI_V2.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;

namespace MobileAPI_V2.Models
{
    public class Card
    {
        public string entityId { get; set; }
    }
    public class NewCardResponse
    {
        public dynamic result { get; set; }
    }
    public class CardLockUnlockRequest
    {
        public string entityId { get; set; }
        public string kitNo { get; set; }
        public string flag { get; set; }
        public string reason { get; set; }
    }

    public class SaveCardLockUnlock
    {
        public string entityId { get; set; }
        public string kitNo { get; set; }
        public string flag { get; set; }
        public string reason { get; set; }
        public string IsBlock { get; set; }
        public string IsLock { get; set; }
        public int OpCode { get; set; }

        public DataSet SaveResponse()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@entityId", entityId),
                    new SqlParameter("@kitNo" , kitNo),
                    new SqlParameter("@reason", reason),
                    new SqlParameter("@IsBlock", IsBlock),
                    new SqlParameter("@IsLock", IsLock),
                    new SqlParameter("@OpCode", OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCardLockUnlock, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
    public class UpdateEntity
    {
        public string entityId { get; set; }
        public string plasticCode { get; set; }
        public AddressDto addressDto { get; set; }
    }
    public class PlastciCodereq
    {
        public string entityId { get; set; }
        public string plasticCode { get; set; }
        public AddressDto addressDto { get; set; }
    }
    public class Address
    {
        public string title { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string fourthLine { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pinCode { get; set; }
        public string aliasName { get; set; }
    }

    public class AddressDto
    {
        public List<Address> address { get; set; }
    }
    public class CardLockUnlockResponse
    {
        public string result { get; set; }
        public CardException exception { get; set; }
        public string status { get; set; }
    }
    public class CardPreferenceResponse
    {
        public CardResult result { get; set; }
        public CardException exception { get; set; }
        public string status { get; set; }
    }

    public class CardResult
    {
        public string[] allowedRuleConfig { get; set; }
        public string[] disallowedRuleConfig { get; set; }
        public string atm { get; set; }
        public string pos { get; set; }
        public string ecom { get; set; }
        public string international { get; set; }
        public string dcc { get; set; }
        public string contactless { get; set; }
    }
    public class SetPinRequest
    {
        public string token { get; set; }
        public string kitNo { get; set; }
        public string entityId { get; set; }
        public string appGuid { get; set; }
        public string business { get; set; }
        public string callbackUrl { get; set; }
        public string dob { get; set; }
    }
    public class GetCardListResponse
    {
        public GetCardListResult result { get; set; }
        public CardException exception { get; set; }
        public object pagination { get; set; }
    }
   
    public class GetCardListResult
    {
        public List<string> cardList { get; set; }
        public List<string> kitList { get; set; }
        public List<string> expiryDateList { get; set; }
        public List<string> cardStatusList { get; set; }
        public List<string> cardTypeList { get; set; }
        public List<string> networkTypeList { get; set; }
    }
    public class CardException
    {
        public string detailMessage { get; set; }
        public string shortMessage { get; set; }
        public Cause? cause { get; set; }
        public string errorCode { get; set; }
        public string message { get; set; }
        public string languageCode { get; set; }
        public List<string>? errors { get; set; }
        public List<Suppressed>? suppressed { get; set; }
        public string localizedMessage { get; set; }
    }
    public class Cause
    {
        public List<StackTrace> stackTrace { get; set; }
        public string message { get; set; }
        public List<Suppressed> suppressed { get; set; }
        public string localizedMessage { get; set; }
    }
    public class Suppressed
    {
        public List<StackTrace> stackTrace { get; set; }
        public string message { get; set; }
        public string localizedMessage { get; set; }
    }
    public class RefundCustomerRequest
    {
        public string toEntityId { get; set; }
        public string fromEntityId { get; set; }
        public string productId { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
        public string transactionType { get; set; }
        public string transactionOrigin { get; set; }
        public string businessType { get; set; }
        public string businessEntityId { get; set; }
        public string externalTransactionId { get; set; }
    }

    public class SaveRefundCustomerRequest
    {
        public string Pk_RefundId { get; set; }
        public string toEntityId { get; set; }
        public string fromEntityId { get; set; }
        public string productId { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
        public string transactionType { get; set; }
        public string transactionOrigin { get; set; }
        public string businessType { get; set; }
        public string businessEntityId { get; set; }
        public string externalTransactionId { get; set; }
        public int OpCode { get; set; }

       
    }

    public class RefundCustomerResponse
    {
        public RefundCustomerResult? result { get; set; }
        public CardException? exception { get; set; }
        public string pagination { get; set; }
        public string status { get; set; }
    }

    public class RefundCustomerResult
    {
        public string txId { get; set; }
    }

    public class LoadCustomerRequest
    {
        public string fromEntityId { get; set; }
        public string toEntityId { get; set; }
        public string yapcode { get; set; }
        public string productId { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
        public string transactionType { get; set; }
        public string business { get; set; }
        public string businessEntityId { get; set; }
        public string transactionOrigin { get; set; }
        public string externalTransactionId { get; set; }
    }

    public class SaveLoadCustomerRequest
    {
        public string fromEntityId { get; set; }
        public string toEntityId { get; set; }
        public string yapcode { get; set; }
        public string productId { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
        public string transactionType { get; set; }
        public string business { get; set; }
        public string businessEntityId { get; set; }
        public string transactionOrigin { get; set; }
        public string externalTransactionId { get; set; }
        public int OpCode { get; set; }

      
    }

    public class CardRequest
    {
        public string entityId { get; set; }
        public string kitNo { get; set; }
        public string IsLoungeCards { get; set; }
        public string cardType { get; set; }
        public string business { get; set; }
        public string reason { get; set; }
        public string oldKitNo { get; set; }
        public string newKitNo { get; set; }
        public string PaymentId { get; set; }

        public string flag { get; set; }
    }

    public class ReplaceCardRequest
    {
        public string entityId { get; set; }
        public string oldKitNo { get; set; }
        public string newKitNo { get; set; }
    }

    public class SetPreferenceRequest
    {
        public string entityId { get; set; }
        public string contactless { get; set; }
        public string atm { get; set; }
        public string ecom { get; set; }
        public string pos { get; set; }
        public string dcc { get; set; }
        public string nfs { get; set; }
        public string international { get; set; }
    }

    public class SaveSetPreference
    {
        public string Pk_Id { get; set; }
        public string entityId { get; set; }
        public string contactless { get; set; }
        public string atm { get; set; }
        public string ecom { get; set; }
        public string pos { get; set; }
        public string dcc { get; set; }
        public string nfs { get; set; }
        public string international { get; set; }
        public int OpCode { get; set; }


       
    }

    public class SetPreferenceResponse
    {
        public string result { get; set; }
        public CardException exception { get; set; }
        public string pagination { get; set; }
        public string status { get; set; }
    }
    public class FetchTransResponse
    {
        public string respcode { get; set; }
        public string respdesc { get; set; }
        public string txnid { get; set; }
        public string rrn { get; set; }
        public string status { get; set; }
        public string EntityId { get; set; }
        public string UID { get; set; }
        public FetchTransKycdetails kycdetails { get; set; }

      
    }
    public class OCRResponse
    {
        public string respcode { get; set; }
        public string respdesc { get; set; }
    }
    public class FetchTransKycdetails
    {
        public string name { get; set; }
        public string fname { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string uid { get; set; }
        public string pht { get; set; }
        public string address { get; set; }
        public string zipped_aadhaar_xml { get; set; }
    }
    public class RecivedCardRequest
    {
        public string KitNo { get; set; }
        public string OTP { get; set; }
        public string Fk_MemId { get; set; }
    }
}
