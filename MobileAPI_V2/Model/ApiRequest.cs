using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class ApiRequest
    {
        IConfiguration _configuration;
        public static string baseUrl = "";
        public ApiRequest(IConfiguration configuration)
        {
            _configuration = configuration;
            baseUrl = _configuration["BaseUrl"];
        }
        public static async Task<string> sendrequest(string url, string Method, object model)
        {
            string responseText = "";
            try
            {               
                string completeurl = baseUrl + url;
                var request = (HttpWebRequest)WebRequest.Create(completeurl);
                if (request != null)
                {
                    request.ContentType = "application/json";
                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    //request.Headers.Add("Authorization", "Bearer " + Token);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    //request.Headers.Add("Authorization", clientauth);
                    if (Method == "POST")
                    {
                        using (Stream s =await request.GetRequestStreamAsync())
                        {
                            using (StreamWriter sw = new StreamWriter(s))
                                sw.Write(JsonConvert.SerializeObject(model));
                        }
                    }

                    using (Stream s =request.GetResponse().GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                            responseText =await sr.ReadToEndAsync();
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
