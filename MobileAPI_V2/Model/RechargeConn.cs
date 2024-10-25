using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class RechargeConn
    {
        public string sendRequest(string url, string Method, string body, long MemberId)
        {
            string responseText = "";
            try
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(url);
                if (request != null)
                {

                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    if (Method == "POST")
                    {
                        //request.ContentType = "application/json";
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
            }
            catch (Exception e)
            {
                throw;
            }
            return responseText;
        }

    }
}
