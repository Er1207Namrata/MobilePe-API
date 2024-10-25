using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobilePeAPI.Model.RazorPay
{
    public class RazorpayConn
    {
        //UPI/Rupay Card key
        string razorpayupikey = string.Empty;
        string razorpayupisecret = string.Empty;
        string razorpaydebitcardkey = string.Empty;
        string razorpaydebitcardsecret = string.Empty;
        string razorpaycreditcardkey = string.Empty;
        string razorpaycreditcardsecret = string.Empty;
        public RazorpayConn(IConfiguration configuration)
        {
            razorpayupikey = configuration["razorpayupikey"];
            razorpayupisecret = configuration["razorpayupisecret"];
            razorpaydebitcardkey = configuration["razorpaydebitcardkey"];
            razorpaydebitcardsecret = configuration["razorpaydebitcardsecret"];
            razorpaycreditcardkey = configuration["razorpaycreditcardkey"];
            razorpaycreditcardsecret = configuration["razorpaycreditcardsecret"];
        }
        public razorpayresponse sendRequest(string url, string Method, string body, long MemberId)
        {
            razorpayresponse res = new razorpayresponse();
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
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.ContentType = "application/json";
                    String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(razorpayupikey + ":" + razorpayupisecret));
                    request.Headers.Add("Authorization", "Basic " + encoded);
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
                            dynamic result= JsonConvert.DeserializeObject<object>(error);
                            res.responseText = result.error.description.ToString();
                        }
                    }
                }
            }
            return res;
        }
        public razorpayresponse RefundRequest(string url, string Method, string body, string PaymentId, long MemberId, string type)
        {
            razorpayresponse res = new razorpayresponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                if (request != null)
                {
                    String encoded = "";
                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.ContentType = "application/json";
                    if (type.ToLower() == "upi"|| type.ToLower() == "rupay" || type.ToLower() == "rupaycard")
                    {
                        encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(razorpayupikey + ":" + razorpayupisecret));
                    }                  
                    else if (type.ToLower() == "debitcard")
                    {
                        encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(razorpaydebitcardkey + ":" + razorpaydebitcardsecret));
                    }
                    else
                    {
                        encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(razorpaycreditcardkey + ":" + razorpaycreditcardsecret));
                    }
                    request.Headers.Add("Authorization", "Basic " + encoded);
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
                            res.responseText = result.error.description.ToString();

                           
                        }
                    }
                }

            }
            return res;
        }
        public razorpayresponse GetRefundStatus(string url, string Method, string body)
        {
            razorpayresponse res = new razorpayresponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                if (request != null)
                {
                    String encoded = "";
                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.ContentType = "application/json";
                   
                    request.Headers.Add("Authorization", "Basic " + encoded);
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
                            res.responseText = result.error.description.ToString();


                        }
                    }
                }

            }
            return res;
        }

        public string sendRequest1(string url)
        {
           
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
                    String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(razorpayupikey + ":" + razorpayupisecret));
                    request.Headers.Add("Authorization", "Basic " + encoded);
                    
                    response = (HttpWebResponse)request.GetResponse();
                    using (Stream s = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                            responseText = sr.ReadToEnd();
                       
                    }


                }

            }
            catch (Exception e)
            {
                throw;

            }
            return responseText;
        }

        public string INRDealRequest(string url, string Method, object model)
        {

            string responseText = "";
            try
            {

                string completeurl = url;





                var request = (HttpWebRequest)WebRequest.Create(completeurl);
                if (request != null)
                {
                    request.ContentType = "application/json";
                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    // request.Headers.Add("Authorization", "Bearer " + Token);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    //request.Headers.Add("Authorization", clientauth);
                    if (Method == "POST")
                    {
                        using (Stream s = request.GetRequestStream())
                        {
                            using (StreamWriter sw = new StreamWriter(s))
                                sw.Write(JsonConvert.SerializeObject(model));
                        }
                    }

                    using (Stream s = request.GetResponse().GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                            responseText = sr.ReadToEnd();
                    }
                }
                return responseText;
            }
            catch (Exception e)
            {
                return responseText;
            }

        }

        public string GetPendingRechargeStatus(string url, string Method)
        {

            string responseText = "";
            try
            {

               
                var request = (HttpWebRequest)WebRequest.Create(url);
                if (request != null)
                {
                    request.ContentType = "application/json";
                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;                   
                    request.Credentials = CredentialCache.DefaultCredentials;                   

                    using (Stream s = request.GetResponse().GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                            responseText = sr.ReadToEnd();
                    }
                }
                return responseText;
            }
            catch (Exception e)
            {
                return responseText;
            }

        }

        public static string InsuranceRequest(string url, string Method, string body)
        {

            string responseText = "";
            try
            {

                string completeurl = url;
                var request = (HttpWebRequest)WebRequest.Create(completeurl);
                if (request != null)
                {

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

                    using (Stream s = request.GetResponse().GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                            responseText = sr.ReadToEnd();
                    }
                }
                return responseText;
            }
            catch (Exception e)
            {
                return responseText;
            }

        }
    }
}
