using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;

namespace MobileAPI_V2.Model
{
    public class FBH_Response
    {
        public int statuscode { get; set; }
        public string responseText { get; set; }
    }
    
    public class FBH_Repository
    {
        string FBHAuth_Url = "";

        public FBH_Repository(IConfiguration configuration)
        {
            FBHAuth_Url = configuration["FBHAuth_Url"];
        }

        public FBH_Response sendRequest(string url, string Method, string body, long MemberId)
        {


            FBH_Response res = new FBH_Response();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {
                url = FBHAuth_Url + url;

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

                   // request.Headers.Add("Authorization", "Bearer 1|kzC8wycMtL3SKg3USDZV6DleEDDAo9gDbUrkBbMW");
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
       
        public class FBH_Auth_Request
        {
            public string ClientId { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string EndUserIp { get; set; }
        }

        

    }
}
