using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobileAPI_V2.Services
{
    public class CommonJsonPostRequest
    {

        string razorpayupikey = string.Empty;
        string razorpayupisecret = string.Empty;
        string razorpaydebitcardkey = string.Empty;
        string razorpaydebitcardsecret = string.Empty;
        string razorpaycreditcardkey = string.Empty;
        string razorpaycreditcardsecret = string.Empty;
        public CommonJsonPostRequest(IConfiguration configuration)
        {
            razorpayupikey = configuration["razorpayupikey"];
            razorpayupisecret = configuration["razorpayupisecret"];
            razorpaydebitcardkey = configuration["razorpaydebitcardkey"];
            razorpaydebitcardsecret = configuration["razorpaydebitcardsecret"];
            razorpaycreditcardkey = configuration["razorpaycreditcardkey"];
            razorpaycreditcardsecret = configuration["razorpaycreditcardsecret"];
        }
        public static string CommonSendRequest(string url, string Method, string body)
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

        public  razorpayresponse sendRequest(string url, string Method, string body, long MemberId)
        {
            razorpayresponse res=new razorpayresponse();
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
                            dynamic result = JsonConvert.DeserializeObject<object>(error);
                            res.responseText = result.error.description.ToString();
                        }
                    }
                }
            }
            return res;
        }
    }
}
