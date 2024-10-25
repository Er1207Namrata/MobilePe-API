using Nancy;
using Nancy.Json;
using System.IO;
using System.Net;

namespace MobileAPI_V2.Model.Thriwe
{
    public class ThriweCommon
    {
        public static string HITAPI(string APIurl, string body)
        {
            var result = "";
            try
            {


                
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("X-Thriwe-Client-Key", "CLIENT_API_KEY");
                httpWebRequest.Headers.Add("X-Thriwe-Application-Id", "CLIENT_APPLICATION_ID");
                httpWebRequest.Headers.Add("project-code", "MOBILEPE");
                using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    using (Stream s = httpWebRequest.GetRequestStream())
                    {
                        using (StreamWriter sw = new StreamWriter(s))
                            sw.Write(body);
                    }
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {

                            result = wex.Message;
                        }
                    }
                }
            }
            return result;
        }
    }
}
