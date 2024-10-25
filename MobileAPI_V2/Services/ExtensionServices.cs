using MobileAPI_V2.Model.BillPayment;
using Newtonsoft.Json;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;

namespace MobileAPI_V2.Services
{
    public static class StringExtensions
    {
        public static bool Null(this string str) => string.IsNullOrEmpty(str);
        public static string Encrypt(this string str, string Aeskey) => ApiEncrypt_Decrypt.EncryptString(Aeskey, str);
        public static string Decrypt(this string str, string Aeskey) => ApiEncrypt_Decrypt.DecryptString(Aeskey, str);
        public static bool HasValue(this string str) => !string.IsNullOrEmpty(str);
        public static ResponseModel Deserialize(this string str) => JsonConvert.DeserializeObject<ResponseModel>(str);


        public static T Deserialize<T>(this string str) => JsonConvert.DeserializeObject<T>(str);
    }
    public static class IntExtensions
    {
        public static bool IsGreater(this int number, int val) => number > val;
        //public static string Encrypt(this int number) => Crypto.Encrypt(number.ToString());
        //public static string Decrypt(this int number) => Crypto.Decrypt(number.ToString());
        public static bool Ok(this int number) => number > 0 ? true : false;
        public static bool NotOk(this int number) => number == 0 ? true : false;

    }
}
