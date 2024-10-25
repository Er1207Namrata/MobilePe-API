using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobilePeAPI.Model.RazorPay
{
    public class RazorpayX
    {
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

    public class CreateContactRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string type { get; set; }
        public string reference_id { get; set; }
        public string ifsc { get; set; }
        public string account_number { get; set; }
        public int amount { get; set; }
        public int Fk_CategoryId { get; set; }
        public long memberId { get; set; }


    }
    public class ContactRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string type { get; set; }
        public string reference_id { get; set; }
    }
    public class AllContactItems
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string name { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string type { get; set; }
        public string reference_id { get; set; }
        public bool active { get; set; }
        public int created_at { get; set; }

    }
    public class AllContactResponse
    {
        public string entity { get; set; }
        public int count { get; set; }
        public IList<AllContactItems> items { get; set; }

    }
    public class AllContactAccountResponse
    {
        public string entity { get; set; }
        public int count { get; set; }
        public IList<FundAccountResponse> items { get; set; }

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
        public string ifsc { get; set; }
        public string bank_name { get; set; }
        public string name { get; set; }
        public string account_number { get; set; }

    }
    public class Bankdetails_accountResponse
    {
        public string ifsc { get; set; }
        public string bank_name { get; set; }
        public string name { get; set; }
        public string account_number { get; set; }
        public string bankId { get; set; }

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

    public class Notes
    {
        public string notes_key_1 { get; set; }
        public string notes_key_2 { get; set; }

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

    public class razorpayresponse
    {
        public int statuscode { get; set; }
        public string responseText { get; set; }
    }
    
    public class Vpa
    {
        public string descriptor { get; set; }

    }

    public class Receivers
    {
        public List<string> types { get; set; }
        public Vpa vpa { get; set; }

    }

    public class virtualaccountsRequest
    {
        public Receivers receivers { get; set; }
        public string description { get; set; }


    }


    public class Receiver
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string username { get; set; }
        public string handle { get; set; }
        public string address { get; set; }

    }

    public class virtualaccountsResponse
    {
        public string id { get; set; }
        public string responseText { get; set; }
        public string name { get; set; }
        public string entity { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public object amount_expected { get; set; }
        public List<object> notes { get; set; }
        public int amount_paid { get; set; }
        public object customer_id { get; set; }
        public List<Receiver> receivers { get; set; }
        public int? close_by { get; set; }
        public object closed_at { get; set; }
        public int created_at { get; set; }

    }
    public class CardTransactionRequest
    {
        public string MobileNo { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Balance { get; set; }
        public string TransStatus { get; set; }
        public string TransactionType { get; set; }
        public string BenificaryName { get; set; }
        public string TransactionOrigin { get; set; }
        public string MerchantCity { get; set; }
        public string Remark { get; set; }
    }

    public class CardTransactionResponse
    {
        public int Status { get; set; }
        public string Msg { get; set; }
    }


    public class VirtualCardUserMobileNumber
    {
        public int flag { get; set; }
        public string UserMobileNo { get; set; }
        public long fkMemId { get; set; }
    }
    public class MobilePeHarvestSendRequest
    {
        public string SendRequestToCashBag(string Key_value, string AFFILIATE_CODE, string CONVERSION_TIME, string DELIVERYMODE, string DIGITAL_DELIVERY, string IS_DELIVERED, string IS_FIRST_PURCHASE, string LANDING_SOURCE, string OTHER_PRODUCT_CODE
            , string OTHER_PRODUCT_IN_BASKET, string PRODUCT_CODE, string PRODUCT_NAME, decimal PRODUCT_PRICE, string PURCHASER_AREA, string PURCHASER_REFERAL_UQ_ID, string PURCHASER_UQ_ID, string PURCHASE_DATE
            , string PURCHASE_DEVICE, decimal SALE_PRICE, string SERVICE_PROVIDER, string TOTAL_CASHBAG_AMOUNT, string TRANSECTION_ID, string Method)
        {
            string responseText = "";
            try
            {
                //string url = ConfigurationManager.AppSettings["CashBagHarvestURL"].ToString();
                var request = HttpWebRequest.Create("");
                string postData = "Key_value=" + Key_value + "&PRODUCT_CODE=" + PRODUCT_CODE + "&PRODUCT_NAME=" + PRODUCT_NAME + "&PURCHASER_UQ_ID="
                                   + PURCHASER_UQ_ID + "&PURCHASE_DATE=" + PURCHASE_DATE + "&PRODUCT_PRICE=" + PRODUCT_PRICE + "&IS_FIRST_PURCHASE="
                                   + IS_FIRST_PURCHASE + "&SALE_PRICE=" + SALE_PRICE + "&TOTAL_CASHBAG_AMOUNT=" + TOTAL_CASHBAG_AMOUNT + "&PURCHASER_AREA="
                                   + PURCHASER_AREA + "&DIGITAL_DELIVERY=" + DIGITAL_DELIVERY + "&IS_DELIVERED=" + IS_DELIVERED + "&DELIVERYMODE="
                                   + DELIVERYMODE + "&AFFILIATE_CODE=" + AFFILIATE_CODE + "&PURCHASE_DEVICE=" + PURCHASE_DEVICE + "&CONVERSION_TIME="
                                   + CONVERSION_TIME + "&OTHER_PRODUCT_IN_BASKET=" + OTHER_PRODUCT_IN_BASKET + "&OTHER_PRODUCT_CODE=" + OTHER_PRODUCT_CODE
                                   + "&TRANSECTION_ID=" + TRANSECTION_ID + "&SERVICE_PROVIDER=" + SERVICE_PROVIDER + "&PURCHASER_REFERAL_UQ_ID="
                                   + PURCHASER_REFERAL_UQ_ID + "&LANDING_SOURCE=" + LANDING_SOURCE;
                ASCIIEncoding ascii = new ASCIIEncoding();
                byte[] postBytes = ascii.GetBytes(postData.ToString());

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postBytes.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(postBytes, 0, postBytes.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();

            }
            catch (Exception ex)
            {
                throw;
            }
            return responseText;
        }

    }
}
