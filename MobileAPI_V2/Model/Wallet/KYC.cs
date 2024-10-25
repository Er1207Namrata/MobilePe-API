using MobileAPI_V2.Model.BBPS;
using System.Data.SqlClient;
using System.Data;

namespace MobileAPI_V2.Model
{
    public class GenerateVCIPResponse
    {
        public string respcode { get; set; }
        public string respdesc { get; set; }
        public string vciplink { get; set; }
        public string qrimage { get; set; }
        public string vcipid { get; set; }
    }
    public class GenerateVCIP
    {
        public string customerid { get; set; }
        public string account_code { get; set; }
        public string vciptemplateid { get; set; }
        public string appid { get; set; }
        public string apptype { get; set; }
        public string fullname { get; set; }
        public string fathername { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string mobile { get; set; }
        public string curr_address { get; set; }
        public string per_address { get; set; }
        public string email { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public string ref4 { get; set; }
        public string ref5 { get; set; }
        public string is_kycdata_verified { get; set; }
        public string is_pandata_verified { get; set; }
        public string Vcipid { get; set; }
        public Kycdetails kycdetails { get; set; }
        public Pandetails pandetails { get; set; }

        
    }
    public class Kycdetails
    {
        public string formattype { get; set; }
        public string ovdtype { get; set; }
        public string ovdid { get; set; }
        public string ckycno { get; set; }
        public string fullname { get; set; }
        public string fathername { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string generationdate { get; set; }
        public string image { get; set; }
        public string curr_address { get; set; }
    }

    public class Pandetails
    {
        public string fullname { get; set; }
        public string dob { get; set; }
        public string pannumber { get; set; }
        public string image { get; set; }
        public string verificationtype { get; set; }
        public string ckycno { get; set; }
    }
}
