using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobileAPI_V2.Services
{
    public class PineCardresponse
    {
        public int statuscode { get; set; }
        public string responseText { get; set; }
    }
    public class PineCommonRequest
    {

        IConfiguration _configuration;
        string PineCardbaseUrl;
        string PineparkclientSecret;
        string PineCardUserName;
        public PineCommonRequest(IConfiguration configuration)
        {

            _configuration = configuration;
            PineCardbaseUrl = _configuration["PineCardbaseUrl"];

            PineCardUserName = _configuration["PineCardUserName"];
        }
        public PineCardresponse sendRequest(string url, string Method, string body, string token = "")
        {
            PineCardresponse res = new PineCardresponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {

                if (!string.IsNullOrEmpty(body))
                {
                    JObject jo = JObject.Parse(body);
                    if (jo.ContainsKey("PaymentId"))
                    {
                        jo.Property("PaymentId").Remove();
                    }
                    if (jo.ContainsKey("memberId"))
                    {
                        jo.Property("memberId").Remove();
                    }

                    body = jo.ToString();
                }
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                if (request != null)
                {

                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.ContentType = "application/json";
                    if (!string.IsNullOrEmpty(token))
                    {
                        request.Headers.Add("token", token);
                    }
                    request.Headers.Add("userName", PineCardUserName);
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
                            string error = reader.ReadToEnd();
                            dynamic result = JsonConvert.DeserializeObject<object>(error);
                            res.responseText = result.message.ToString();
                        }
                    }
                }
            }
            return res;
        }


        public PineCardresponse Checksum(string url, string externalRequestId, string cardschemeId, string amount, string quantity)
        {
            PineCardresponse res = new PineCardresponse();
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
                    request.Method = "GET";
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.ContentType = "application/json";
                    request.Headers.Add("externalRequestId", externalRequestId);
                    request.Headers.Add("amount", amount);
                    request.Headers.Add("quantity", quantity);
                    if (!string.IsNullOrEmpty(cardschemeId))
                    {
                        request.Headers.Add("cardSchemeId", cardschemeId);
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
                            string error = reader.ReadToEnd();
                            dynamic result = JsonConvert.DeserializeObject<object>(error);
                            res.responseText = result.message.ToString();
                        }
                    }
                }
            }
            return res;
        }


        public PineCardresponse sendRequestV1(string url, string Method, string body, string token = "")
        {
            PineCardresponse res = new PineCardresponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {

                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                if (request != null)
                {

                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.ContentType = "application/json";

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
                            string error = reader.ReadToEnd();
                            dynamic result = JsonConvert.DeserializeObject<object>(error);
                            res.responseText = result.message.ToString();
                        }
                    }
                }
            }
            return res;
        }
    }
}
