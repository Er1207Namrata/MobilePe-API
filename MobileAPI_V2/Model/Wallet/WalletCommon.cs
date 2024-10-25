using Microsoft.Extensions.Primitives;
using Nancy;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace MobileAPI_V2.Model
{
    public class WalletCommon
    {
        public ExceptionData exception { get; set; }
        public string entityId { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string FK_MemId { get; set; }
        public string Type { get; set; }
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
        public DataSet GetDashBoardData()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_MemId",FK_MemId),

            };

            DataSet ds = Connection.ExecuteQuery("GetWalletDashboard", para);
            return ds;
        }
        public DataSet SaveRequestResponse()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@ProfilerId",entityId),
                new SqlParameter("@Request",Request),
                new SqlParameter("@Response",Response),
                new SqlParameter("@Type",Type)

            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.SaveRequestResponse, para);
            return ds;
        }
        public FintechResponse sendUpdateEntity(string url, string body)
        {
            FintechResponse res = new FintechResponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {


                var request = (HttpWebRequest)HttpWebRequest.Create(url);
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
            return res;
        }
        public static string HITTOPAYAPI(string APIurl, string body, string token)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Token", token);
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
        public static string HITAPISimple(string APIurl, string json, string Header, string authkey)
        {
            var result = "";
            try
            {
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
                httpWebRequest.ContentType = "application/json";
                if (Header == "UserAuth")
                {
                    httpWebRequest.Headers.Add("apikey", "0");
                }
                else if (Header == "VCIPStatus")
                {
                    httpWebRequest.Headers.Add("apikey", "0");
                    httpWebRequest.Headers.Add("authkey", authkey);
                }
                else if (Header == "GenerateVCIP")
                {
                    httpWebRequest.Headers.Add("apikey", "0");
                    httpWebRequest.Headers.Add("authkey", authkey);
                }
                else if (Header == "Createdigilockerrequest")
                {
                    httpWebRequest.Headers.Add("apikey", "0");
                    httpWebRequest.Headers.Add("authkey", authkey);
                }
                else if (Header == "OCR")
                {
                    httpWebRequest.Headers.Add("authkey", authkey);
                    httpWebRequest.Headers.Add("apikey", "0");
                }
                else if (Header == "CreatedigilockerrequestAuth")
                {
                    httpWebRequest.Headers.Add("apikey", "0");

                }
                else if (Header == "SendOTP")
                {
                    httpWebRequest.Headers.Add("TENANT", "MOBILEPE");
                    httpWebRequest.Headers.Add("partnerId", "MOBILEPE");
                    // httpWebRequest.Headers.Add("partnerToken", "Basic TFFLQVJCT04==");
                    httpWebRequest.Headers.Add("partnerToken", "Basic TU9CSUxFUEU==");
                }
                else if (Header == "UpdateEntity")
                {
                    httpWebRequest.Headers.Add("TENANT", "MOBILEPE");
                    // httpWebRequest.Headers.Add("partnerToken", "Basic TFFLQVJCT04==");
                    httpWebRequest.Headers.Add("Authorization", "Basic TU9CSUxFUEU==");
                }
                else
                {
                    httpWebRequest.Headers.Add("TENANT", Header);
                }
                //httpWebRequest.Headers.Add("Content-Type", "application/json");
                httpWebRequest.Method = "POST";
                using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
                {


                    streamWriter.Write(json);
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
        public static string HITTOPAYAPI(string APIurl, string token)
        {
            var result = "";
            try
            {



                var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("Token", token);
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

                result =ex.Message;
            }
            return result;
        }
        public static string IRCTCAPI(string APIurl)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "text/html";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public DataSet GetWalletData(string EntityId)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@EntityId",EntityId),

            };
            DataSet ds = Connection.ExecuteQuery("GetWalletData", para);
            return ds;
        }
        public DataSet UpdateWallet(string json, string EntityId, string TransDateTime, int totalcount, int count)
        {

            SqlParameter[] para =
          {
                new SqlParameter("@Json",json),
                new SqlParameter("@entityId",EntityId),
                new SqlParameter("@TransDateTime",TransDateTime),
                new SqlParameter("@totalcount",totalcount),
                new SqlParameter("@count",count),

            };
            DataSet ds = Connection.ExecuteQuery("UpdateWalletData", para);
            return ds;
        }
        public DataSet UpdateBBPSStatus(string json, string EntityId, string TransDateTime, int totalcount, int count)
        {

            SqlParameter[] para =
          {
                new SqlParameter("@Json",json),
                new SqlParameter("@entityId",EntityId),
                new SqlParameter("@TransDateTime",TransDateTime),
                new SqlParameter("@totalcount",totalcount),
                new SqlParameter("@count",count),

            };
            DataSet ds = Connection.ExecuteQuery("UpdateBBPSStatus", para);
            return ds;
        }
    }
    public class RequestMobile
    {
        public string TransactionId { get; set; }
        public string Number { get; set; }
        public string OperatorCode { get; set; }
        public string Type { get; set; }
        public string merchantInfoTxn { get; set; }
        public int Amount { get; set; }
        public int MemberId { get; set; }
    }
    public class ReplaceResponse
    {
        public string result { get; set; }
        public string KitNo { get; set; }
    }
    public class ProcedureName
    {
        public static string SaveRequestResponse = "SaveRequestResponse";
        public static string SaveRegistration = "Registration";
        public static string WalletTopup = "WalletTopup";
        public static string UpdateVCIPStatus = "UpdateVCIPStatus";
        public static string GetWalletDashboard = "GetWalletDashboard";
        public static string ChangeWalletPin = "ChangeWalletPin";
        public static string WalletDebit = "WalletDebit";
        public static string GetCardLockUnlock = "GetCardLockUnlock";
        public static string MemberCard = "MemberCard";

        public static string GetCustomerDetails = "GetCustomerDetails";

        public static string ForgotWalletPin = "ForgotWalletPin";

        public static string GetPhotoCardData = "GetPhotoCardData";

        public static string GetVefMob_Email = "GetVefMob_Email";
        public static string GetKYCGuideLine = "GetKYCGuideLine";
        public static string VerifyOTP_Wallet = "VerifyOTP_Wallet";
        public static string WalletInfo = "WalletInfo";
        public static string GetWalletToWalletTra = "GetWalletToWalletTra";
        public static string CheckEmailMobile = "CheckEmailMobile";

        public static string validate_user2user = "validate_user2user";

        public static string UpdateVCIPId = "UpdateVCIPId";

        public static string wallet_to_walletTran_Hist = "wallet_to_walletTran_Hist";



    }
    public class ValidateResponse
    {
        public int Success { get; set; }
        public string Name { get; set; }
        public string UPIId { get; set; }
        public string KYCStatus { get; set; }

        public ExceptionData exception { get; set; }

    }

}
