using Microsoft.Extensions.Configuration;
using MobileAPI_V2.DataLayer;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace MobileAPI_V2.Model.Travel
{

    public class CommonTravel
    {

        public static string GetResponse(string requestData, string url)
        {
            string responseXML = string.Empty;
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(requestData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Accept-Encoding", "gzip");
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
                WebResponse webResponse = request.GetResponse();
                var rsp = webResponse.GetResponseStream();
                if (rsp == null)
                {
                    //throw exception
                }
                try
                {
                    using (StreamReader readStream = new StreamReader(new GZipStream(rsp, CompressionMode.Decompress)))
                    {
                        responseXML = readStream.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                return responseXML;
            }
            catch (WebException webEx)
            {
                //get the response stream
                WebResponse response = webEx.Response;
                Stream stream = response.GetResponseStream();
                String responseMessage = new StreamReader(stream).ReadToEnd();
                return responseMessage;
            }
        }


        public static string SimpleAPI(string APIurl, string json1)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = json1;

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }


    }
 
    public class TokenResponse
    {
        public string Token { get; set; }
    }
    public class CheckUserToken
    {

        public string Flag { get; set; }

        public DataSet UserToken(string Token, string Fk_MemId1)
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Token",Token),
                                      new SqlParameter("@Fk_MemId", Fk_MemId1)
                                  

                            };

                DataSet ds = Connection.ExecuteQuery("CheckUserToken", para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class TravelCredentials
    {
#if DEBUG
        public static string ClientId = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("ClientIdUAT").Value;
        public static string UserName = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("UserNameUAT").Value;
        public static string Password = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("TravelPasswordUAT").Value;
        public static string EndUserIP = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("EndUserIPUAT").Value;
        public static string TokenAgencyId = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("TokenAgencyIdUAT").Value;
        public static string TokenMemberId = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("TokenMemberIdUAT").Value;
        public static string SharedBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("SharedBaseUrlUAT").Value;
        public static string StaticBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("StaticBaseUrlUAT").Value;
        public static string HotelBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("HotelBaseUrlUAT").Value;
        public static string Authenticate = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AuthenticateUAT").Value;
        public static string GetBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("GetBaseUrlUAT").Value;
        public static string BookingBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("BookingBaseUrlUAT").Value;

#else
        public static string ClientId= new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("ClientId").Value;
        public static string UserName = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("UserName").Value;
        public static string Password = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("TravelPassword").Value;
        public static string EndUserIP = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("EndUserIP").Value;
        public static string TokenAgencyId = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("TokenAgencyId").Value;
        public static string TokenMemberId = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("TokenMemberId").Value;
        public static string SharedBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("SharedBaseUrl").Value;
        public static string StaticBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("StaticBaseUrl").Value;
        public static string HotelBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("HotelBaseUrl").Value;
        public static string Authenticate = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("Authenticate").Value;
        public static string GetBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("GetBaseUrl").Value;
        public static string BookingBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("BookingBaseUrl").Value;

#endif


        //public static string ClientId = "ApiIntegrationNew";
        //public static string UserName = "Partha1";
        //public static string Password = "Partha1@1234";
        //public static string EndUserIP = "192.168.10.10";
        //public static string TokenAgencyId = "57416";
        //public static string TokenMemberId = "57542";

        //public static string Authenticate = "http://api.tektravels.com/SharedServices/SharedData.svc/rest";
        //public static string GetBaseUrl = "http://api.tektravels.com/BookingEngineService_Air/AirService.svc/rest";
        //public static string BookingBaseUrl = "http://api.tektravels.com/BookingEngineService_Air/AirService.svc/rest";
        //public static string SharedBaseUrl = "http://api.tektravels.com/SharedServices/SharedData.svc/rest";
        //public static string StaticBaseUrl = "http://api.tektravels.com/SharedServices/StaticData.svc/rest";
        //public static string HotelBaseUrl = "http://api.tektravels.com/BookingEngineService_Hotel/hotelservice.svc/rest";
        //public static string ClientId = "tboprod";
        //public static string UserName = "DELM1250";
        //public static string Password = "Api@#Live$0102";
        //public static string EndUserIP = "52.172.198.236";
        //public static string TokenAgencyId = "57416";
        //public static string TokenMemberId = "57542";
        // public static string Authenticate = "https://api.travelboutiqueonline.com/SharedAPI/SharedData.svc/rest";
        // public static string GetBaseUrl = "https://tboapi.travelboutiqueonline.com/AirAPI_V10/AirService.svc/rest";
        // public static string BookingBaseUrl = "https://booking.travelboutiqueonline.com/AirAPI_V10/AirService.svc/rest";

    }
    public class ThriweCredentials
    {

    }
    public class CheckMobileTransfer
    {
        public string Msg { get; set; }
        public string Flag { get; set; }
    }

}
