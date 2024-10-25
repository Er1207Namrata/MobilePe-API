using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Services;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;

namespace MobileAPI_V2.Model
{
    public class CommonResponse<T> : Common
    {
        public T result { get; set; }

    }
   

    public class Common
    {

        public void WebHookEmail(string transactionId)
        {
            try
            {
                PaymentWebHookRequest paymentWebHookRequest = new PaymentWebHookRequest();
                paymentWebHookRequest.contains = new List<string>();
                paymentWebHookRequest.payload = new Payload();
                paymentWebHookRequest.payload.payment = new PaymentWebHook();
                paymentWebHookRequest.payload.payment.entity = new WebHookEntity();
                paymentWebHookRequest.contains.Add("payment");
                paymentWebHookRequest.payload.payment.entity.id = transactionId;
                paymentWebHookRequest.payload.payment.entity.status = "captured";
                paymentWebHookRequest.payload.payment.entity.order_id = transactionId;
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                StreamReader sr;
                js = new DataContractJsonSerializer(typeof(PaymentWebHookRequest));
                ms = new MemoryStream();
                js.WriteObject(ms, paymentWebHookRequest);
                ms.Position = 0;
                sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();

                var result = CommonJsonPostRequest.CommonSendRequest("https://api.mobilepe.co.in/webhook/payment", "POST", CustData);
                var response = JsonConvert.DeserializeObject<object>(result);
            }
            catch
            {
                throw;
            }
        }

        public string CreatePassword(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public string response { get; set; }
        public string OpCode { get; set; }
        public string Value { get; set; }
        public string message { get; set; }
        public long Id { get; set; }
        public string transactionId { get; set; }
        public string filepath { get; set; }
        public static string RandomNumber()
        {
            Random random = new Random();
            return random.Next(10000000, 99999999).ToString() + random.Next(10000000, 99999999).ToString();
        }

        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string[] DatePart = InputDate.Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            string DateString;
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Year + "-" + Month + "-" + Day;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static string HITAPIFlightBooking(string APIurl, string body)
        {
            FintechResponse res = new FintechResponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {


                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var request = (HttpWebRequest)HttpWebRequest.Create(APIurl);
                if (request != null)
                {


                    request.Timeout = 50000;
                    request.Method = "POST";
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;

                    request.ContentType = "application/json";
                    using (Stream s = request.GetRequestStream())
                    {
                        using (StreamWriter sw = new StreamWriter(s))
                            sw.Write(body);
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
            return res.responseText;
        }

        public FintechResponse sendBBPSRequest(string url, string body)
        {
            FintechResponse res = new FintechResponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                if (request != null)
                {


                    request.Timeout = 50000;
                    request.Method = "POST";
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;

                    request.ContentType = "application/json";

                    request.Headers.Add("Authorization", "Bearer 6|oa1TR67v768gNYYiwtv1bvL2Izy1p11K5NpNqBYO");

                    using (Stream s = request.GetRequestStream())
                    {
                        using (StreamWriter sw = new StreamWriter(s))
                            sw.Write(body);
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

        public static string HITAPIBillPay(string APIurl, string body)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    body = body

                });

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
        public static string HITAPI(string APIurl, string body, string Header, string authkey)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Token", Header);
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    body = body

                });

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        } 
        public static string HITAPIFinque(string APIurl, string body, string Header, string authkey)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";
            string AuthToken = "Bearer " + Header;
            httpWebRequest.Headers.Add("Authorization", AuthToken);
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                
                streamWriter.Write(body);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
        public static string HITAPI(string APIurl)
        {
            var result = "";
            try
            {

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("TENANT", "MOBILEPE");
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                result = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            return result;
        }

        public static string HITNewBBPSAPI(string APIurl, string body)
        {
            var result = "";
            try
            {

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new

               StreamWriter(httpWebRequest.GetRequestStream()))
                {


                    streamWriter.Write(body);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                result = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            return result;
        }

        public static string HITNewBBPSAPI(string APIurl)
        {
            var result = "";
            try
            {

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                result = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            return result;
        }

        public static string HITFinequeAPI(string APIurl, string body, string token,string type)
        {
            var result = "";
            try
            {

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                if(type=="Login")
                {
                    httpWebRequest.Headers.Add("RequestHash", token);
                }
                else
                {
                    httpWebRequest.Headers.Add("Authorization", token);
                }
                using (var streamWriter = new

               StreamWriter(httpWebRequest.GetRequestStream()))
                {


                    streamWriter.Write(body);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                result = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            return result;
        }

        public static string HITFinequeAPI(string APIurl, string token)
        {
            var result = "";
            try
            {

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization", token);
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                result = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            return result;
        }

        public static string HITBusAPI(string APIurl, string body)
        {
            var result = "";
            try
            {

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new

               StreamWriter(httpWebRequest.GetRequestStream()))
                {


                    streamWriter.Write(body);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                result = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            return result;
        }
    }
    public class sort
    {
        public string Text { get; set; }
        public long Value { get; set; }
    }
    public class filter
    {
        public string Text { get; set; }
        public long Value { get; set; }
        public List<filterdata> data { get; set; }
    }
    public class filterdata
    {
        public string Text { get; set; }
        public long Value { get; set; }

    }

    public class InsuranceReponse
    {
        public string url { get; set; }
    }
    public class InsuranceRequest
    {
        public int serviceId { get; set; }
        public string partnerCode { get; set; }
        public string platform { get; set; }
        public string userCode { get; set; }
        public string authToken { get; set; }

    }
    public class PhysicalCardIssuranceRequest
    {
        public long memberId { get; set; }
        public string externalRequestId { get; set; }
        public string cardSchemeId { get; set; }
        public int pinMode { get; set; }
        public int cardIdentifier { get; set; }
        public string checksum { get; set; }
        public string PaymentId { get; set; }
        public List<CardDetailListPhysical> cardDetailList { get; set; }
    }
    public class CardDetailListPhysical
    {
        public string referenceNumber { get; set; }
        public object serialNumber { get; set; }
        public string customerName { get; set; }
        public string mobileNumber { get; set; }
        public object email { get; set; }
        public long amount { get; set; }
    }

    public class CardDetailResponseList
    {
        public string referenceNumber { get; set; }
        public string serialNumber { get; set; }
        public string customerName { get; set; }
        public long mobileNumber { get; set; }
        public string email { get; set; }
        public int amount { get; set; }
        public string cardLink { get; set; }
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
    }
    public class pinecard
    {
        public string cardtype { get; set; }
        public string CardReferenceNumber { get; set; }
        public string CardSerialNumber { get; set; }
        public string custstatus { get; set; }
        public string customerName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
    }
    public class ResultSet
    {
        public string TransactionId { get; set; }
        public long Id { get; set; }
        public int flag { get; set; }
        public string msg { get; set; }
        public string message { get; set; }
    }

    public class ResultCommon
    {

        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class Registrationresponse
    {
        public ResultSet CustomerRegistrationResponse { get; set; }
        public LoginResponse LoginResponse { get; set; }
    }
    public class RegistrationresponseV3
    {
        public ResultSet CustomerRegistrationResponse { get; set; }

    }
    public class PendingRechargeRequest
    {
        public long memberId { get; set; }
        public string Number { get; set; }
        public string PaymentId { get; set; }
        public string Type { get; set; }
        public string OperatorCode { get; set; }
        public decimal amount { get; set; }
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
    }

    public class CardBalanceDetailList
    {
        public int monthlyCreditAmount { get; set; }
        public int loadAmount { get; set; }
        public int redeemAmount { get; set; }
        public int availableAmount { get; set; }
        public int blockedAmount { get; set; }
        public object referenceNumber { get; set; }
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
    }

    public class PineCardBalanceResponse
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<CardBalanceDetailList> cardBalanceDetailList { get; set; }
    }

    public class PineCardTransactionDetailRequest
    {
        public string referenceNumber { get; set; }
        public long memberId { get; set; }
    }
    public class PineCardBalanceRequest
    {
        public List<string> cardBalanceRequestList { get; set; }
        public long memberId { get; set; }
    }
    public class TransactionDetailList
    {
        public int transactionAmount { get; set; }
        public int transactionIndicator { get; set; }
        public string transactionDate { get; set; }
        public string transactionType { get; set; }
        public string orderId { get; set; }
        public string link { get; set; }
        public object approvalCode { get; set; }
        public string cardNumber { get; set; }
        public string corporateName { get; set; }
        public object merchantName { get; set; }
        public object city { get; set; }
        public object transactionSummary { get; set; }
    }

    public class PineCardTransactionDetailResponse
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<TransactionDetailList> transactionDetailList { get; set; }
    }
    public class CommissionModel
    {
        public string Remark { get; set; }
        public long Id { get; set; }
        public string DeviceId { get; set; }
        public string Title { get; set; }
        public long memberId { get; set; }
    }
    public class Name_EmailModel
    {
        public string Name { get; set; }
        public string USEREMAILID { get; set; }
        public string ProductName { get; set; }
        public string FranchiseName { get; set; }
        public string FranchiseMobile { get; set; }

    }

    public class virtualaccountsRequest
    {
        public Receivers receivers { get; set; }
        public string description { get; set; }


    }

    public class Receivers
    {
        public List<string> types { get; set; }
        public Vpa vpa { get; set; }

    }

    public class Vpa
    {
        public string descriptor { get; set; }

    }

    public class virtualaccountsResponse
    {
        public string id { get; set; }
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

    public class Receiver
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string username { get; set; }
        public string handle { get; set; }
        public string address { get; set; }

    }
    public class SaveBookingResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }
}
