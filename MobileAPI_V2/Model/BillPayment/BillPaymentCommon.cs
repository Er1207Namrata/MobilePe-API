using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Net;

namespace MobileAPI_V2.Model.BillPayment
{
    public class BillPaymentCommon
    {
        
        public static string HITMultiRechargeAPI(string APIurl)
        {
            string responseText = "";
            HttpWebRequest request = WebRequest.Create(APIurl) as HttpWebRequest;
           
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            WebHeaderCollection header = response.Headers;

            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
            }
            return responseText;
        }
        public class ApiEncrypt_Decrypt
        {
            public static string EncryptString(string key, string plainText)
            {
                byte[] iv = new byte[16];
                byte[] array;
                //string Aeskey = "";

                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 128;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                            {
                                streamWriter.Write(plainText);
                            }

                            array = memoryStream.ToArray();
                        }
                    }
                }

                return Convert.ToBase64String(array);
            }
            public static string DecryptString(string key, string cipherText)
            {
                cipherText = cipherText.Replace(" ", "+");
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);
                using (Aes aes = Aes.Create())
                {

                    aes.KeySize = 128;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }

        }

    }
    public class ResponseModel
    {
        public string Body { get; set; }
    }
    public class RequestModel
    {
        public string Body { get; set; }

    }
   
    public class Credentials
    {
        public static string AESKEY = "MOBILEP@#@132EYZ";
        public static string MobileNo = "9990132968";
        public static string PinNo = "2580";
        public static string ElectricityBillFetch = "https://api.multilinkrecharge.com/ReCharge/billfetchApi.asmx/ElectricityBillFetch?";
        public static string GasBillFetch = "https://api.multilinkrecharge.com/ReCharge/billfetchApi.asmx/GasBillFetch?";
        public static string WaterBillFetch = "https://api.multilinkrecharge.com/ReCharge/billfetchApi.asmx/WaterBillFetch?";
        public static string InsuranceBillFetch = "https://api.multilinkrecharge.com/ReCharge/billfetchApi.asmx/InsuranceBillFetch?";
        public static string LpgBillFetch = "https://api.multilinkrecharge.com/ReCharge/billfetchApi.asmx/LpgBillFetch?";
        public static string CableTvBillFetch = "https://api.multilinkrecharge.com/ReCharge/billfetchApi.asmx/CableTvBillFetch?";
        public static string BillPaymentWithPara = "https://api.multilinkrecharge.com/ReCharge/jsonrechargeapi.asmx/Recharge?";
        public static string CheckBillStatus = "https://api.multilinkrecharge.com/ReCharge/jsonrechargeapi.asmx/StatusCheckByRequestId?";
    }
   

}
