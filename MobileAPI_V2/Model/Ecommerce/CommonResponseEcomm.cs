using MobileAPI_V2.Model.BillPayment;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class CommonResponseEcomm<T> : CommonEcomm
    {
        public T result { get; set; }

    }
    public class CommonEcomm
    {

        public string response { get; set; }
        public string OpCode { get; set; }
        public string Value { get; set; }
        public string message { get; set; }
        public int Status { get; set; }
        public long Id { get; set; }
        public string transactionId { get; set; }
        public string filepath { get; set; }
        public static string RandomNumber()
        {
            Random random = new Random();
            return random.Next(10000000, 99999999).ToString() + random.Next(10000000, 99999999).ToString();
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
    }
    public class sortEcomm
    {
        public string Text { get; set; }
        public long Value { get; set; }
    }
    public class filterEcomm
    {
        public string Text { get; set; }
        public long Value { get; set; }
        public List<filterdataEcomm> data { get; set; }
    }
    public class filterdataEcomm
    {
        public string Text { get; set; }
        public long Value { get; set; }

    }

    public class InsuranceReponseEcomm
    {
        public string url { get; set; }
    }
    public class InsuranceRequestEcomm
    {
        public int serviceId { get; set; }
        public string partnerCode { get; set; }
        public string platform { get; set; }
        public string userCode { get; set; }
        public string authToken { get; set; }

    }
    public class ResultSetEcomm
    {
        public string TransactionId { get; set; }
        public long Id { get; set; }
        public int flag { get; set; }
        public string msg { get; set; }
        public string message { get; set; }
    }

    public class ResultCommonEcomm
    {

        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class RegistrationresponseEcomm
    {
        public ResultSetEcomm CustomerRegistrationResponse { get; set; }
        public LoginResponseEcomm LoginResponse { get; set; }
    }
    public class RegistrationresponseV3Ecomm
    {
        public ResultSetEcomm CustomerRegistrationResponse { get; set; }

    }
    public class PendingRechargeRequestEcomm
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

    public class CardBalanceDetailListEcomm
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

    public class PineCardBalanceResponseEcomm
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<CardBalanceDetailListEcomm> cardBalanceDetailList { get; set; }
    }

    public class PineCardTransactionDetailRequestEcomm
    {
        public string referenceNumber { get; set; }
        public long memberId { get; set; }
    }
    public class PineCardBalanceRequestEcomm
    {
        public List<string> cardBalanceRequestList { get; set; }
        public long memberId { get; set; }
    }
    public class TransactionDetailListEcomm
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

    public class PineCardTransactionDetailResponseEcomm
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<TransactionDetailListEcomm> transactionDetailList { get; set; }
    }
    public class CommissionModelEcomm
    {
        public string Remark { get; set; }
        public long Id { get; set; }
        public string DeviceId { get; set; }
        public string Title { get; set; }
        public long memberId { get; set; }
    }
    public class Name_EmailModelEcomm
    {
        public string Name { get; set; }
        public string USEREMAILID { get; set; }
        public string ProductName { get; set; }
        public string FranchiseName { get; set; }
        public string FranchiseMobile { get; set; }

    }

    public class virtualaccountsRequestEcomm
    {
        public ReceiversEcomm receivers { get; set; }
        public string description { get; set; }


    }

    public class ReceiversEcomm
    {
        public List<string> types { get; set; }
        public VpaEcomm vpa { get; set; }

    }

    public class VpaEcomm
    {
        public string descriptor { get; set; }

    }

    public class virtualaccountsResponseEcomm
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
        public List<ReceiverEcomm> receivers { get; set; }
        public int? close_by { get; set; }
        public object closed_at { get; set; }
        public int created_at { get; set; }

    }

    public class ReceiverEcomm
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string username { get; set; }
        public string handle { get; set; }
        public string address { get; set; }

    }
    public class SaveBookingResponseEcomm
    {
        public int Flag { get; set; }
        public string Message { get; set; }
    }
}
