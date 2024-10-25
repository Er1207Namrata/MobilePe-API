using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class OAuthorization
    {
        string FintechBaseUrl = string.Empty;
        string FintechConsumerkey = string.Empty;
        string FintechConsumerSecret = string.Empty;

        public OAuthorization(IConfiguration configuration)
        {
            FintechBaseUrl = configuration["FintechBaseUrl"];
            FintechConsumerkey = configuration["FintechConsumerkey"];
            FintechConsumerSecret = configuration["FintechConsumerSecret"];

        }
        #region Methods



        public string sendRequest(string url, string Method, string body, string access_token, string access_secret, long MemberId)
        {

            string responseText = "";
            try
            {
                string completeurl = FintechBaseUrl + url;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var client = new OAuthRequest()
                {
                    ConsumerKey = FintechConsumerkey,
                    ConsumerSecret = FintechConsumerSecret,
                    Token = access_token,
                    TokenSecret = access_secret,
                    Type = OAuthRequestType.ProtectedResource,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    RequestUrl = completeurl,
                    Version = "1.0",
                    Method = Method
                };

                string auth = client.GetAuthorizationQuery();
                var urlpath = client.RequestUrl + "?" + auth;
                var clientauth = client.GetAuthorizationHeader();

                var request = (HttpWebRequest)WebRequest.Create(urlpath);
                if (request != null)
                {
                    request.ContentType = "application/json";
                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.Credentials = CredentialCache.DefaultCredentials;
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
                            responseText = result.RESPONSEDESC.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return responseText;
        }


        private string StreamToString(Stream original)
        {
            StreamReader streamReader = new StreamReader(original, true);
            string streamString;
            try
            {

                streamString = streamReader.ReadToEnd();
            }
            finally
            {
                streamReader.Close();
                original.Close();
            }

            return streamString;
        }

        #endregion Methods
        public string encodeRequest(string requestData,string key,string checksum)
        {
            string encodedRequest = null;
            try
            {
                Request_Response objreq = new Request_Response();
                objreq.ENCVALUE = Encrypt(requestData, key);
                objreq.CHECKSUM = checksum;
               
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Request_Response));
                MemoryStream ms = new MemoryStream();
                js.WriteObject(ms, objreq);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                encodedRequest = sr.ReadToEnd();
                sr.Close();
                ms.Close();

            }
            catch (Exception e)
            {
                throw;
            }
            return encodedRequest;
        }

        public string decodeResponse(string ENCVALUE, string key)
        {

            return Decrypt(ENCVALUE,key);
        }
        public string Encrypt(string inputText, string EncryptionKey)
        {
            try
            {
                byte[] clearBytes = Encoding.ASCII.GetBytes(inputText);
                byte[] bENVVALUE;
                using (Aes encryptor = Aes.Create())
                {
                    byte[] bKey = ASCIIEncoding.UTF8.GetBytes(EncryptionKey);
                    encryptor.Key = bKey;
                    encryptor.Mode = CipherMode.ECB;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(),
                        CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        bENVVALUE = ms.ToArray();
                        inputText = Convert.ToBase64String(bENVVALUE);
                    }
                }
                return inputText;
            }
            catch
            {
                return null;
            }
        }
        public string Decrypt(string cipherText, string EncryptionKey)
        {
            try
            {
                byte[] bDECVALUE;
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    byte[] bKey = ASCIIEncoding.UTF8.GetBytes(EncryptionKey);
                    encryptor.Key = bKey;
                    encryptor.Mode = CipherMode.ECB;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(),
                       CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        bDECVALUE = ms.ToArray();
                        cipherText = Encoding.ASCII.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch
            {
                return null;
            }
        }
        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
