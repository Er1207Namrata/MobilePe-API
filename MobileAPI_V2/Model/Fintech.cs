using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;

namespace MobileAPI_V2.Model
{
    public class FintechResponse
    {
        public int statuscode { get; set; }
        public string responseText { get; set; }
    }
    public class Fintech
    {
        string WalletV2BaseURL = "";
        string BBPSBaseURL = "";

        public Fintech(IConfiguration configuration)
        {
            WalletV2BaseURL = configuration["WalletV2BaseURL"];
            BBPSBaseURL = configuration["BBPSBaseURL"];
        }

        public FintechResponse sendRequest(string url, string Method, string body, long MemberId)
        {
            FintechResponse res = new FintechResponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {
                url = WalletV2BaseURL + url;

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                if (request != null)
                {


                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;

                    request.ContentType = "application/json";

                    request.Headers.Add("Authorization", "Bearer 1|kzC8wycMtL3SKg3USDZV6DleEDDAo9gDbUrkBbMW");
                    if (Method == "POST")
                    {

                        using (Stream s = request.GetRequestStream())
                        {
                            using (StreamWriter sw = new StreamWriter(s))
                                sw.Write(body);
                        }

                    }
                    response = (HttpWebResponse)request.GetResponse();
                    using (Stream s = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                            responseText = sr.ReadToEnd();
                        res.responseText = responseText;

                        res.statuscode = (int)response.StatusCode;
                    }


                }

            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {

                            res.responseText = wex.Message;
                        }
                    }
                }
            }
            return res;
        }

        public FintechResponse sendBBPSRequest(string url, string Method, string body, long MemberId)
        {
            FintechResponse res = new FintechResponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {
                url = BBPSBaseURL + url;

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                if (request != null)
                {


                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;

                    request.ContentType = "application/json";

                    request.Headers.Add("Authorization", "Bearer 6|oa1TR67v768gNYYiwtv1bvL2Izy1p11K5NpNqBYO");
                    if (Method == "POST")
                    {

                        using (Stream s = request.GetRequestStream())
                        {
                            using (StreamWriter sw = new StreamWriter(s))
                                sw.Write(body);
                        }

                    }
                    response = (HttpWebResponse)request.GetResponse();
                    using (Stream s = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                            responseText = sr.ReadToEnd();
                        res.responseText = responseText;

                        res.statuscode = (int)response.StatusCode;
                    }


                }

            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {

                            res.responseText = wex.Message;
                        }
                    }
                }
            }
            return res;
        }
        public object ToApiRequest(object requestObject)
        {
            switch (requestObject)
            {
                case JObject jObject: // objects become Dictionary<string,object>
                    return ((IEnumerable<KeyValuePair<string, JToken>>)jObject).ToDictionary(j => j.Key, j => ToApiRequest(j.Value));
                case JArray jArray: // arrays become List<object>
                    return jArray.Select(ToApiRequest).ToList();
                case JValue jValue: // values just become the value
                    return jValue.Value;
                default: // don't know what to do here
                    throw new Exception($"Unsupported type: {requestObject.GetType()}");
            }
        }
        public static string HITAPIIRCTC(string APIurl)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }

    public class RequestTokenModel
    {
        public string STATUS { get; set; }
        public string TOKEN { get; set; }
        public string SECRETKEY { get; set; }
    }
    public class RequestTokenResult
    {
        public RequestTokenModel OAUTH { get; set; }

    }
    public class Request_Response
    {

        public string CHECKSUM { get; set; }
        public string ENCVALUE { get; set; }
    }
    public class CommonWallet
    {

        public string access_token { get; set; }
        public string access_secret { get; set; }
        public string AGENTID { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class WalletCreationRequest : CommonWallet
    {
        public long TRANSID { get; set; }
        public long MemberId { get; set; }
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string TRANSACTIONID { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string CUSTOMERMIDDLENAME { get; set; }
        public string CUSTOMERLASTNAME { get; set; }
        public string CUSTOMERMOTHERSMAIDENNAME { get; set; }
        public string CUSTOMERDOB { get; set; }
        public string CUSTOMEREMAILID { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string CUSTOMERADDRESS { get; set; }
        public string PINCODE { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string GENDER { get; set; }
        public string CUSTOMERIDIDPROOFTYPE { get; set; }
        public string CUSTOMERIDPROOFNO { get; set; }
        public string ADDRESSPROOFTYPE { get; set; }
        public string ADDRESSPROOFNO { get; set; }
        public string IDPROOFURL { get; set; }
        public string ADDPROOFURL { get; set; }
        public string KYCURL { get; set; }
        public string OTHERPROOF1 { get; set; }
        public string OTHERPROOF2 { get; set; }
        public string KYCFLAG { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WalletCreationResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string TRANSACTIONID { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class RequestResult
    {
        public Request_Response REQUEST { get; set; }
    }

    public class ResponseResult
    {
        public Request_Response RESPONSE { get; set; }
    }

    public class WalletCreationOTPValidateRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string OTP { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class WalletCreationOTPValidateResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string ACCOUNTNO { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string TRANSACTIONID { get; set; }
        public string CUSTOMERNAME { get; set; }

        public string CUSTOMERMIDDLENAME { get; set; }
        public string CUSTOMERLASTNAME { get; set; }
        public string CUSTOMERMOTHERSMAIDENNAME { get; set; }
        public string CUSTOMERDOB { get; set; }
        public string CUSTOMEREMAILID { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string CUSTOMERADDRESS { get; set; }
        public string PINCODE { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string GENDER { get; set; }
        public string CUSTOMERIDIDPROOFTYPE { get; set; }
        public string CUSTOMERIDPROOFNO { get; set; }
        public string ADDRESSPROOFTYPE { get; set; }
        public string ADDRESSPROOFNO { get; set; }
        public string IDPROOFURL { get; set; }
        public string ADDPROOFURL { get; set; }
        public string KYCURL { get; set; }
        public string OTHERPROOF1 { get; set; }
        public string OTHERPROOF2 { get; set; }
        public string KYCSTATUS { get; set; }
        public string KYCVERIFICATIONSTATUS { get; set; }
        public string WALLETREACTIVESTATUS { get; set; }
        public string WALLETSTATUS { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }


    }
    public class WalletCreationResendOTPRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class WalletCreationResendOTPResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WalletInfoRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WalletInfoResponse
    {
        public int STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string ACCOUNTNO { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string TRANSACTIONID { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string CUSTOMERMIDDLENAME { get; set; }
        public string CUSTOMERLASTNAME { get; set; }
        public string CUSTOMERMOTHERSMAIDENNAME { get; set; }
        public string CUSTOMERDOB { get; set; }
        public string CUSTOMEREMAILID { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string CUSTOMERADDRESS { get; set; }
        public string PINCODE { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string GENDER { get; set; }
        public string CUSTOMERIDIDPROOFTYPE { get; set; }
        public string CUSTOMERIDPROOFNO { get; set; }
        public string ADDRESSPROOFTYPE { get; set; }
        public string ADDRESSPROOFNO { get; set; }
        public string IDPROOFURL { get; set; }
        public string ADDPROOFURL { get; set; }
        public string KYCURL { get; set; }
        public string OTHERPROOF1 { get; set; }
        public string OTHERPROOF2 { get; set; }
        public string KYCSTATUS { get; set; }
        public string KYCVERIFICATIONSTATUS { get; set; }
        public string WALLETREACTIVESTATUS { get; set; }
        public string WALLETSTATUS { get; set; }
        public string CREATEDON { get; set; }
        public string KYCVERIFICATIONREMARKS { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class MobileValidationRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class MobileValidationResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string BALANCE { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WalletRechargeRequest : CommonWallet
    {
        public long memberId { get; set; }
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string TOPUPAMOUNT { get; set; }
        public string TRANSACTIONID { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WalletRechargeResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string TOPUPAMOUNT { get; set; }
        public string CURRENTBALALNCE { get; set; }
        public string PREVIOUSBALANCE { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WALLETBALANCERequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WALLETBALANCEResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string WALLETBALANCE { get; set; }
        public string monthlyloadlimit { get; set; }
        public string monthlyremaininglimit { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class RechargeRequeryRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string TRANSACTIONID { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class RechargeRequeryResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string TOPUPAMOUNT { get; set; }
        public string CURRENTBALALNCE { get; set; }
        public string PREVIOUSBALANCE { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WalletMiniStatementRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string RECORDCOUNTPERPAGE { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WalletMiniStatementResponse
    {
        public int STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public int NOOFPAGE { get; set; }
        public int RECORDPERPAGE { get; set; }
        public int TOTALRECORD { get; set; }
        public List<PAGE> PAGE { get; set; }
    }
    public class WalletMiniStatement
    {
        public string TRANSDATETIME { get; set; }
        public string CHANNEL { get; set; }
        public string TRANSACTIONID { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string TRANSACTIONTYPE { get; set; }
        public string TRANSAMOUNT { get; set; }
        public string TRANSACTIONMODE { get; set; }
        public string REMARKS { get; set; }
        public string INFO { get; set; }
        public object PARAM1 { get; set; }
        public object PARAM2 { get; set; }
        public object PARAM3 { get; set; }
        public object PARAM4 { get; set; }
        public object PARAM5 { get; set; }
    }

    public class PAGE
    {
        public int PAGEFROM { get; set; }
        public int PAGETO { get; set; }
        public List<WalletMiniStatement> ITEM { get; set; }
    }
    public class WallettoWalletTransferRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string RECEIVERMOBILENO { get; set; }
        public string TRANSACTIONID { get; set; }
        public string TRANSAMOUNT { get; set; }
        public string OTP { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class WallettoWalletTransferResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string CURRENTBALANCE { get; set; }
        public string PREVIOUSBALANCE { get; set; }
        public string TRANSACTIONAMOUNT { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string TRANSACTIONID { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class GenerateTransactionOTPRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }

    }

    public class GenerateTransactionOTPResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class WalletBlockunblockOTPRequest : CommonWallet
    {

        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string BLOCKSTATUS { get; set; }
        public string REMARKS { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WalletBlockunblockOTPResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string TRANSACTIONID { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class DebitTransactionRequeryRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string TRANSACTIONID { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class DebitTransactionRequeryResponse
    {

        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string CURRENTBALANCE { get; set; }
        public string TRANSACTIONAMOUNT { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string TRANSACTIONID { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class WalletBlockunblockOTPValidationRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string OTP { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class WalletBlockunblockOTPValidationResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }


    public class WalletTransactionRequest : CommonWallet
    {
        public string mid { get; set; }
        public string transData { get; set; }
        public string TransType { get; set; }
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string TRANSAMOUNT { get; set; }
        public string TRANSACTIONID { get; set; }
        public string SESSIONID { get; set; }
        public string TIMESTAMP { get; set; }
        public string REMARKS { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
        public long MemberId { get; set; }
        public string OTP { get; set; }
    }

    public class WalletTransactionPINRequest : CommonWallet
    {
        public string REDIRECTURL { get; set; }
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string TRANSAMOUNT { get; set; }
        public string TRANSACTIONID { get; set; }
        public string SESSIONID { get; set; }
        public string TIMESTAMP { get; set; }
        public string REMARKS { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
        public long MemberId { get; set; }
    }


    public class WalletTransactionHandShakeRequest
    {
        public string Status { get; set; }
        public int ProcId { get; set; }
        public string type { get; set; }
        public long memberId { get; set; }
        public string MOBILENO { get; set; }
        public string TRANSACTIONID { get; set; }
        public string AMOUNT { get; set; }
        public string SESSIONID { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class WalletTransactionHandShakeResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string TRANSACTIONID { get; set; }
        public string AMOUNT { get; set; }
        public string SESSIONID { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }


    public class WalletTop
    {
        public long MemberId { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public decimal creditamount { get; set; }
        public decimal debitamount { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string Remarks { get; set; }
        public int procId { get; set; }

    }
    public class WalletRefundRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public long memberId { get; set; }

        public string CUSTOMERMOBILENO { get; set; }
        public string AMOUNT { get; set; }
        public string DEBITTRANSACTIONID { get; set; }
        public string REFUNDTRANSACTIONID { get; set; }
        public string REMARKS { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class WalletRefundResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string CURRENTBALANCE { get; set; }
        public string REFUNDAMOUNT { get; set; }
        public string DEBITAPITRANSACTIONID { get; set; }
        public string DEBITTRANSACTIONID { get; set; }
        public string REFUNDTRANSACTIONID { get; set; }
        public string APIREFUNDTRANSACTIONID { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class PostWalletTransactionRequest
    {
        public int ProcId { get; set; }
        public string TransactionId { get; set; }
        public string RefundId { get; set; }
        public string APITransactionId { get; set; }
        public string MobileNo { get; set; }
        public DateTime TransDate { get; set; }
        public decimal Amount { get; set; }
        public long memberId { get; set; }
    }


    public class WalletTransactionLog : Common
    {
        public string RequestFrom { get; set; }
        public int ProcId { get; set; }
        public long MemberId { get; set; }
        public string Request { get; set; }
        public string WalletResponse { get; set; }
        public string Type { get; set; }
        public string WalletTransactionId { get; set; }
        public long memberId { get; set; }
        public string pincode { get; set; }
        public string mobile { get; set; }


    }


    public class WebHookEntity
    {
       
       
        public string id { get; set; }
        public string entity { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string status { get; set; }
        public string order_id { get; set; }
        public object invoice_id { get; set; }
        public bool international { get; set; }
        public string method { get; set; }
        public int amount_refunded { get; set; }
        public object refund_status { get; set; }
        public bool captured { get; set; }
        public object description { get; set; }
        public object card_id { get; set; }
        public object bank { get; set; }
        public object wallet { get; set; }
        public string vpa { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public Notes notes { get; set; }
        public int fee { get; set; }
        public int tax { get; set; }
        public object error_code { get; set; }
        public object error_description { get; set; }
        public object error_source { get; set; }
        public object error_step { get; set; }
        public object error_reason { get; set; }
        public AcquirerData acquirer_data { get; set; }
        public int created_at { get; set; }
        public Upi upi { get; set; }
        public int base_amount { get; set; }
    }

    public class Payload
    {
        public PaymentWebHook payment { get; set; }
    }

    public class PaymentWebHook
    {
        public WebHookEntity entity { get; set; }
    }

    public class PaymentWebHookRequest
    {
        public List<string> contains { get; set; }
        public Payload payload { get; set; }
    }
    public class PendingWalletTransaction
    {
        public string RequestFrom { get; set; }

        public string Type { get; set; }

        public string TransactionId { get; set; }

        public string Request { get; set; }
    }
    public class ActivateRupayCardOTPGenRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string KITNUMBER { get; set; }
        public string TRANSACTIONID { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class ActivateRupayCardOTPGenResponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string TRANSACTIONID { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class ActivateRupayCardResendOTPGenRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class ActivateRupayCardResendOTPGenRespose
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class ActivateRupayCardRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string KITNUMBER { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string OTP { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    public class ActivateRupayCardReponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string TRANSACTIONID { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string KITNUMBER { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class CardReplacementOTPRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string NEWKITNUMBER { get; set; }
        public string TRANSACTIONID { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class CardReplacementOTPReponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string TRANSACTIONID { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class CardReplacementResendRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class CardReplacementResendReponse
    {
        public string STATUSCODE { get; set; }
        public string STATUS { get; set; }
        public string RESPONSECODE { get; set; }
        public string RESPONSEDESC { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }
    public class CardReplacementRequest : CommonWallet
    {
        public string PARTNERID { get; set; }
        public string SECURITYKEY { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string NEWKITNUMBER { get; set; }
        public string APITRANSACTIONID { get; set; }
        public string OTP { get; set; }
        public string IPADDRESS { get; set; }
        public string DEVICEINFO { get; set; }
        public string PARAM1 { get; set; }
        public string PARAM2 { get; set; }
        public string PARAM3 { get; set; }
        public string PARAM4 { get; set; }
        public string PARAM5 { get; set; }
    }

    #region New Wallet API
    public class WalletV2ResponseModel
    {
        public string response { get; set; }
        public string message { get; set; }

        //  public object result { get; set; }
        public result result { get; set; }
    }
    public class WalletV2WalletCreationResponseModel
    {
        public string response { get; set; }
        public string message { get; set; }

        //  public object result { get; set; }
        public WalletCreationresult result { get; set; }
    }
    public class WalletCreationresult
    {
        public string kit_no { get; set; }
        public string card_id { get; set; }
        public string CardType { get; set; }
    }
    public class WalletV2WalletCommonResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public object result { get; set; }

    }
    public class CreationOTPResult
    {
        public object result { get; set; }
    }
    public class WalletCreationRequestV2
    {
        public string OTP { get; set; }
        public string CUSTOMERTITLE { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string CUSTOMERMIDDLENAME { get; set; }
        public string CUSTOMERLASTNAME { get; set; }
        public string GENDER { get; set; }
        public string PASSWORD { get; set; }
        public string CONFIRMPASSWORD { get; set; }
        public string CARDTYPE { get; set; }
        public string CUSTOMERIDPROOFNO { get; set; }
        public string CUSTOMERIDIDPROOFTYPE { get; set; }
        public string CUSTOMERIDIDPROOFEXPIRY { get; set; }
        public string CUSTOMERDOB { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string PINCODE { get; set; }
        public string CUSTOMERADDRESS { get; set; }
        public string CUSTOMERADDRESS2 { get; set; }
        public string CUSTOMERADDRESS3 { get; set; }
        public int KYCFLAG { get; set; }
        public string PANCARDNO { get; set; }
        public string ADDRESSPROOFEXPIRY { get; set; }
        public string ADDRESSPROOFNO { get; set; }
        public string ADDRESSPROOFTYPE { get; set; }
        public string CUSTOMEREMAILID { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public bool ENABLENOTIFICATION { get; set; }
       public long memberId { get; set; }
        public string WALLETPIN { get; set; }
        public string CONFIRMWALLETPIN { get; set; }
        public string KITNO { get; set; }
    }
    public class WalletCreationRequestV2DTO
    {
        public string OTP { get; set; }
        public string CUSTOMERTITLE { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string CUSTOMERMIDDLENAME { get; set; }
        public string CUSTOMERLASTNAME { get; set; }
        public string GENDER { get; set; }
        public string PASSWORD { get; set; }
        public string CONFIRMPASSWORD { get; set; }
        public string CARDTYPE { get; set; }
        public string CUSTOMERIDPROOFNO { get; set; }
        public string CUSTOMERIDIDPROOFTYPE { get; set; }
        public string CUSTOMERIDIDPROOFEXPIRY { get; set; }
        public string CUSTOMERDOB { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string PINCODE { get; set; }
        public string CUSTOMERADDRESS { get; set; }
        public string CUSTOMERADDRESS2 { get; set; }
        public string CUSTOMERADDRESS3 { get; set; }
        public int KYCFLAG { get; set; }
        public string PANCARDNO { get; set; }
        public string ADDRESSPROOFEXPIRY { get; set; }
        public string ADDRESSPROOFNO { get; set; }
        public string ADDRESSPROOFTYPE { get; set; }
        public string CUSTOMEREMAILID { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public bool ENABLENOTIFICATION { get; set; }
        public long memberId { get; set; }
        public string WALLETPIN { get; set; }
        public string CONFIRMWALLETPIN { get; set; }
        public string KITNO { get; set; }
    }

    public class WalletCreationOTPRequest
    {
        public string CUSTOMERMOBILENO { get; set; }
        //public string CARDID { get; set; }
        public long memberId { get; set; }
    }
    public class WalletPrefrenceRequest
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string KITNO { get; set; }
        public long CARDID { get; set; }
    }
    public class WalletPrefrenceResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public WalletPrefrenceResult result { get; set; }

    }
    public class WalletPrefrenceResult
    {
        public PrefrenceModes modes { get; set; }
        public WalletPrefrenceDailyLimit daily_limits { get; set; }
    }
    public class PrefrenceModes
    {
        public bool atm { get; set; }
        public bool pos { get; set; }
        public bool ecom { get; set; }
        public bool contactless { get; set; }
        
    }
    public class WalletPrefrenceDailyLimit
    {
        public decimal atm { get; set; }
        public decimal pos { get; set; }
        public decimal ecom { get; set; }
        public decimal contactless { get; set; }
    }
    public class WalletCardInfoRequest
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string CARDID { get; set; }
        public long memberId { get; set; }
        public string KITNO { get; set; }
    }
    public class WalletSetPinResponse
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string CARDID { get; set; }
        public long memberId { get; set; }
        public string KITNO { get; set; }
    }
    public class WalletSetPreferences
    {
        public string CUSTOMERMOBILENO { get; set; }
        public bool CONTACTLESS { get; set; }
        public bool ATM { get; set; }
        public bool ECOM { get; set; }
        public bool POS { get; set; }
        public bool DCC { get; set; }
        public bool NFS { get; set; }
        public bool INTERNATIONAL { get; set; }
        public long memberId { get; set; }
        public string KITNO { get; set; }

    }

    public class WalletSetlimit
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string TXNTYPE { get; set; }
        public string LIMIT { get; set; }
        public string CARDID { get; set; }
        public long memberId { get; set; }
        public string KITNO { get; set; }
    }

    public class WalletTopUpV2
    {
        public string CUSTOMERMOBILENO { get; set; }
        public decimal TOPUPAMOUNT { get; set; }
        public string TRANSACTIONID { get; set; }
        public string REMARKS { get; set; }
        public long memberId { get; set; }
    }
    public class WalletDebitV2:AddedOnRequest
    {
       
        public decimal TRANSAMOUNT { get; set; }
        public string TRANSACTIONID { get; set; }
        public string REMARKS { get; set; }
        public string WALLETPIN { get; set; }
        public string Type { get; set; }
       
        public int Fk_CardId { get; set; }
        public int OldCardId { get; set; }
        public string  Request { get; set; }

    }

    public class WalletBlockUnblockV2
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string BLOCKSTATUS { get; set; }
        public string REMARKS { get; set; }
        public string CARDID { get; set; }
        public long memberId { get; set; }
        public string KITNO { get; set; }

    }
    public class WallettransactionsV2
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public int PAGE { get; set; }
        public int PERPAGE { get; set; }
        
    }
    public class WallettransactionStatusV2
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string TRANSACTIONID { get; set; }
    }
    #endregion

    public class walletTopUp
    {

        public int msg { get; set; }
        public string TransactionId { get; set; }
        public string Result { get; set; }
        public long FK_MemId { get; set; }
        public string MobileNO { get; set; }
        public decimal TopupAmount { get; set; }

        public string Remarks { get; set; }
        public string response { get; set; }
        public string message { get; set; }

    }

    public class walletOTP_Log
    {

        public int msg { get; set; }
        public long FK_MemId { get; set; }
        public string MobileNO { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public string Opcode { get; set; }
        public string OTP { get; set; }

    }



    public class ToPaywalletReg
    {

        public int msg { get; set; }
        public string Result { get; set; }
        public long FK_MemId { get; set; }
        public string MobileNO { get; set; }
        public string KitType { get; set; }
        public string OTP { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public string kit_no { get; set; }
        public string card_id { get; set; }
        



    }

    public class KitNoResponse
    {
        public int Code { get; set; }
        public string KitNo { get; set; }
        public string Remark { get; set; }
        public int OpCode { get; set; }
        public long FK_MemId { get; set; }
        public string MobileNO { get; set; }
        public string KitType { get; set; }
    }
    public class WalletBalance
    {

        public int msg { get; set; }
        public string Result { get; set; }
        public long FK_MemId { get; set; }
        public string MobileNO { get; set; }

        public decimal walletbalance { get; set; }
        public long Monthlyloadlimit { get; set; }
        public long Monthlyremaininglimit { get; set; }
        public string response { get; set; }
        public string message { get; set; }



    }
    public class result
    {
        public decimal walletbalance { get; set; }
        public long Monthlyloadlimit { get; set; }
        public long Monthlyremaininglimit { get; set; }
        public string kit_no { get; set; }
        public string card_id { get; set; }
        public string ISCARDIDAPPLY { get; set; }
        public bool Iswalletexists { get; set; }
    }

    public class WalletSetPreferencesSave
    {
        public string CUSTOMERMOBILENO { get; set; }
        public bool CONTACTLESS { get; set; }
        public bool ATM { get; set; }
        public bool ECOM { get; set; }
        public bool POS { get; set; }
        public bool DCC { get; set; }
        public bool NFS { get; set; }
        public bool INTERNATIONAL { get; set; }
        public long Fk_MemId { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public string Result { get; set; }
        public string KITNO { get; set; }
    }

    public class WalletBlockUnblock_log
    {
        public string MobileNO { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public string BlockStatus { get; set; }
        public string Remarks { get; set; }
        public string CardId { get; set; }
        public long FK_MemId { get; set; }
        public string KITNO { get; set; }
        public string Result { get; set; }
    }

    public class WalletDebit_log
    {
        public string MobileNo { get; set; }
        public string Remarks { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public string TransactionId { get; set; }
        public string REMARKS { get; set; }
        public long FK_MemId { get; set; }
        public string walletPin { get; set; }
        public string Type { get; set; }
        public int OpCode { get; set; }
        public string Result { get; set; }
        public int Fk_CardId { get; set; }
        public int card_id { get; set; }
        public string KitNo { get;  set; }
    }

    public class WalletSetlimit_log
    {
        public string MobileNo { get; set; }
        public string TXNTYPE { get; set; }
        public string LIMIT { get; set; }
        public string CARDID { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public long FK_MemId { get; set; }
        public int ProcId { get; set; }
        public string KITNO { get; set; }


    }
    public class WalletSetPIN_log
    {
        public string MobileNo { get; set; }
        public string CardId { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public long FK_MemId { get; set; }
        public string KITNO { get; set; }

    }
    public class WalletDashboardRes

    {
        public int IsCardApply { get; set; }
        public int IsDispatched { get; set; }
       
        public int Isupgrade { get; set; }
        public int IsOldCard { get; set; }
        public string KYCStatus { get; set; }
        
    }
    public class WalletDashboardV2
    {
        public string CUSTOMERMOBILENO { get; set; }
        public long memberId { get; set; }
        public string Result { get; set; }
        
    }

    public class WalletDashboard_log
    {

        public int msg { get; set; }
        public string Result { get; set; }
        public long FK_MemId { get; set; }
        public string MobileNo { get; set; }
        public int IsCardApply { get; set; }
        public int IsDispatched { get; set; }
        public string DocketNo { get; set; }
        public string URL { get; set; }
        public string AddDate { get; set; }
        public decimal walletbalance { get; set; }
        public long Monthlyloadlimit { get; set; }
        public long Monthlyremaininglimit { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public int IsOldCard { get; set; }



    }
    public class WalletV2DashboardResponseModel
    {
        public string response { get; set; }
        public string message { get; set; }

        //  public object result { get; set; }
        public Dashboardresult result { get; set; }
    }
    public class Dashboardresult
    {
        public decimal walletbalance { get; set; }
        public long Monthlyloadlimit { get; set; }
        public long Monthlyremaininglimit { get; set; }
        public int IsCardApply { get; set; }
        public int IsDispatched { get; set; }
        public string DocketNo { get; set; }
        public string URL { get; set; }
        public string AddDate { get; set; }
        public int IsUpgrade { get; set; }
        public int IsOldCard { get; set; }


    }
    public class WalletSetPin
    { 
        public string CUSTOMERMOBILENO { get; set; }
        public string WALLETPIN { get; set; }
        public string CONFIRMWALLETPIN { get; set; }
        public string OLDWALLETPIN { get; set; }
        //public string CARDID { get; set; }
    }
    public class ApplyCard
    { 
        public string Fk_MemId { get; set; }
        public string Fk_CardId { get; set; }
        public int IsPhotoCard { get; set; }
        public string PhotoPath { get; set; }
        public string OrderId { get; set; }
        //public string CARDID { get; set; }
    }

    public class UpdateMobile
    {
        public string Fk_MemId { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string CUSTOMERMOBILENONEW { get; set; }


    }
    public class UpdateMobileLog
    {
        public string Fk_MemId { get; set; }
        public string OldMobileNo { get; set; }
        public string NewMobileNo { get; set; }
        public string response { get; set; }
        public string message { get; set; }


    }
    public class UpdateResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public object result { get; set; }
    }

    public class WalletResetOTPRequest
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string OTP { get; set; }
        public string WALLETPIN { get; set; }
        public string CONFIRMWALLETPIN { get; set; }
        public long memberId { get; set; }
           
    }
    public class WalletRestOTPPINResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public WalletRestOtp result { get; set; }

    }
    public class WalletRestOtp
    {
        public string otp { get; set; }
    }
   
    public class walletResetOTP_Log
    {

        public int msg { get; set; }
        public long FK_MemId { get; set; }
        public string MobileNO { get; set; }
        public string WalletPin { get; set; }
        public string ComfirmWalletPin { get; set; }
        public string OTP { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public string Opcode { get; set; }

    }
    public class CreateKYCRequest
    {
        public string CUSTOMERMOBILENO { get; set; }
        public long memberId { get; set; }
    }
    public class KYCResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public CreateKYCResult result { get; set; }
    }
    public class CreateKYCResult
    {
        public string link { get; set; }
    }
    public class CardViewDetails
    {
       
        public long Fk_MemId { get; set; }
    }
    public class CardViewDetails_log
    {

        public long Fk_MemId { get; set; }
       

    }
    public class CardViewResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public CardViewResult result { get; set; }
    }

    public class CardViewResult
    {
        public string kycStatus { get; set; }
        public string monthlyLimit { get; set; }
        public List<CardViewDetailResult> lstCard { get; set; }
    }
    public class CardViewDetailResult
    {
        public string Name { get; set; }
        public string CardName { get; set; }
        public string Mobile { get; set; }
        public string PhotoPath { get; set; }
        public int IsUpgrade { get; set; }
        public int ToPayCardId { get; set; }
        public int IsAddOnCard { get; set; }

        public int Fk_CardId { get; set; }

        public string KitNo { get; set; }
        public string ExpiryDate { get; set; }
        public string DispatchStatus { get; set; }
        public string ekidoozurl { get; set; }
        public string CardNo { get; set; }
        public string BlockStatus { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string dob { get; set; }
        public string ekidoozurlTxt { get; set; }
        public string dispatchTxt { get; set; }
        public string docketNumber { get; set; }
        public string trackingUrl { get; set; }
        public string kycStatus { get; set; }
        public string monthlyLimit { get; set; }

    }

    public class AddedOnRequest
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string CARDTYPE { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public int addon_card_id { get; set; }
        public int card_id { get; set; }
        public string kit_no { get; set; }
        public string KITNO { get; set; }

        public long memberId { get; set; }

    }

    public class AddedOnResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public AddedOnRequest result { get; set; }
    }

    public class ReplaceCardRequest
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string CARDID { get; set; }
        public string KITNO { get; set; }
        public string OLDKITNO { get; set; }
        public string Fk_MemId { get; set; }
        
       

    }
    public class ReplaceCardRequest_log
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string CARDID { get; set; }
        public string KITNO { get; set; }
        public string OLDKITNO { get; set; }
        public string Fk_MemId { get; set; }



    }
    public class ReplaceCardResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public ReplaceCardResult result { get; set; }
    }
    public class ReplaceCardResult
    {
        public string response { get; set; }
        public string message { get; set; }
    }

    public class KitsAvailabilityResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public KitsAvailabilityResult result { get; set; }
    }
    public class KitsAvailabilityResult
    {
        public List<KitsAvailabilityList> lstKit { get; set; }
    }
 

    public class KitsAvailabilityList
    {
        public string KitType { get; set; }
        public string Status { get; set; }
        public object NoOfKits { get; set; }


    }

  
    public class BlockandUnblockStatusRequest
    {
        public string KITNO { get; set; }
        public string FK_MemId { get; set; }
        public int msg { get; set; }
        public string Result { get; set; }

    }
    public class CheckCardTypeRequest
    {
        public string KITNO { get; set; }
        public string FK_MemId { get; set; }


    }
    public class CheckCardTypeResponse
    {
        public string KitType { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string KitNo { get; set; }
        public string OldKitNo { get; set; }
        public string CardNo { get; set; }
        public string ExpiryDate { get; set; }
        public string NetworkType { get; set; }
        public string card_id { get; set; }
        public string FK_MemId { get; set; }
        public string MobileNo { get; set; }
        public string Type { get; set; }
        public int card_Id { get; set; }
        public int UpgradeStatus { get; set; }
        public string IsLoungeCards { get; set; }
        public string PaymentId { get; set; }
    }
}
