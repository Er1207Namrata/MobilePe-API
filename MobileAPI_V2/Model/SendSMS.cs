using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;

namespace MobileAPI_V2.Model
{
    public class SendSMS
    {

        IConfiguration _configuration;     
        string SMSOTPMember = "";
        string SMSOTPMember1 = "";
        string SMSAPI = "";
        string SMSAPI1 = "";
        string SMSREGISTRATION = "";
        string SMSREGISTRATION1 = "";
        string MailUrl = "";
        string MailToken = "";
        string MailFrom = "";
        string MailFromName = "";
        string SMTPUserName = "";
        string MailOwnerId = "";
        string IsOTPNew = "0";
        private readonly IDataRepository _dataRepository;
        public SendSMS(IConfiguration configuration, IDataRepository dataRepository)
        {
            _configuration = configuration;
            _dataRepository = dataRepository;
            SMSOTPMember = configuration["SMS-OTP-Member"];
            SMSOTPMember1 = configuration["SMS-OTP-Member1"];
            SMSAPI = configuration["SMSAPI"];
            SMSAPI1 = configuration["SMSAPI1"];
            SMSREGISTRATION = configuration["SMS-REGISTRATION"];
            SMSREGISTRATION1 = configuration["SMS-REGISTRATION1"];
            MailUrl = configuration["MailUrl"];
            MailToken = configuration["MailToken"];
            MailFrom = configuration["MailFrom"];
            SMTPUserName = configuration["SMTPUserName"];
            MailOwnerId = configuration["MailOwnerId"];
            MailFromName = configuration["MailFromName"];
            IsOTPNew = configuration["IsOTPNew"];
        }
        public string OTPMemberMessage(string MemberName, string OTP, string purpose,string MID)
        {

            if (IsOTPNew == "1")
            {
                var message = SMSOTPMember1.Split("XXXX");
                SMSOTPMember1 = message[0] + MemberName;
                SMSOTPMember1 = SMSOTPMember1 + message[1] + OTP;
                SMSOTPMember1 = SMSOTPMember1 + message[2] + purpose + message[3];
                return SMSOTPMember1;
            }
            else
            {
                SMSOTPMember = SMSOTPMember.Replace("[MEMBER-NAME]", MemberName);
                SMSOTPMember = SMSOTPMember.Replace("[OTP]", OTP);
                SMSOTPMember = SMSOTPMember.Replace("[PURPOSE]", purpose);
                SMSOTPMember = SMSOTPMember.Replace("[MID]", MID);
                return SMSOTPMember;
            }

        }
        public string RegistrationSMS(string MemberName, string LoginId, string Password)
        {
            if (IsOTPNew == "1")
            {
                var message = SMSREGISTRATION1.Split("XXXX");
                SMSREGISTRATION1 = message[0] + MemberName;
                SMSREGISTRATION1 = SMSREGISTRATION1 + message[1] + LoginId;
                SMSREGISTRATION1 = SMSREGISTRATION1 + message[2] + Password + message[3];
                return SMSREGISTRATION1;
            }
            else
            {
                SMSREGISTRATION = SMSREGISTRATION.Replace("[Member-Name]", MemberName);
                SMSREGISTRATION = SMSREGISTRATION.Replace("[MobileNo]", LoginId);
                SMSREGISTRATION = SMSREGISTRATION.Replace("[Password]", Password);
                return SMSREGISTRATION;
            }
        }
        public void SendSMSMessage(string Mobile, string Message, string title = "")
        {
            string TempId = "";
            try
            {
                if (IsOTPNew == "1")
                {
                    if (title == "OTP")
                    {
                        TempId = "2493";
                    }
                    if (title == "REGISTRATION")
                    {
                        TempId = "2494";
                    }
                    if (title == "OFFICER REGISTRATION")
                    {
                        TempId = "2495";
                    }
                    SMSAPI1 = SMSAPI1.Replace("[AND]", "&");
                    SMSAPI1 = SMSAPI1.Replace("[MOBILE]", Mobile);
                    SMSAPI1 = SMSAPI1.Replace("[MESSAGE]", Message);
                    SMSAPI1 = SMSAPI1.Replace("[TEMID]", TempId);
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI1, false));
                    HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
                }
                else
                {
                    //if (title == "OTP")
                    //{
                    //    TempId = "1707160103122621078";
                    //}
                    if (title == "OTP")
                    {
                        TempId = "1707162186088085577";
                    }
                    if (title == "REGISTRATION")
                    {
                        TempId = "1707160103294043975";
                    }
                    if (title == "OFFICER REGISTRATION")
                    {
                        TempId = "1707160103318899491";
                    }
                    SMSAPI = SMSAPI.Replace("[AND]", "&");
                    SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile);
                    SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);
                    SMSAPI = SMSAPI.Replace("[TEMID]", TempId);
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI, false));
                    HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
                }
            }
            catch (Exception ex)
            {
            }
        }
        public string UserRegistrationMailMessage(string UserName, string LoginId, string Password)
        {

            string message = string.Empty;
            FileStream fileStream = new FileStream("wwwroot\\EmailTemplates\\UserRegistration.html", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                message = reader.ReadToEnd();
            }
            message = message.Replace("[UserName]", UserName);
            message = message.Replace("[LoginId]", LoginId);
            message = message.Replace("[Password]", Password);
            return message;
        }
        public string EmailVerificationMailMessage(string UserName, string link)
        {

            string message = string.Empty;
            FileStream fileStream = new FileStream("wwwroot\\EmailTemplates\\Email-Verification.html", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                message = reader.ReadToEnd();
            }
            message = message.Replace("[Username]", UserName);
            message = message.Replace("[EmailLink]", link);
            return message;
        }
        public ApplicationResponse MailService(string body)
        {
            ApplicationResponse objres = new ApplicationResponse();
            string responseText = "";
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(MailUrl);
                if (request != null)
                {

                   
                    request.Timeout = 50000;
                    request.Method = "POST";
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    request.ContentType = "application/json";
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

                if (!string.IsNullOrEmpty(responseText))
                {
                    objres = JsonConvert.DeserializeObject<ApplicationResponse>(responseText);
                }
            }

            catch (Exception ex)
            {
                objres.message = ex.Message;
                objres.status = "0";
            }
            return objres;

        }

        public ApplicationResponse MailServiceV2(string body)
        {
            ApplicationResponse objres = new ApplicationResponse();
            string responseText = "";
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create("https://mp.alert.mobilepe.co.in/ICSsmtp/mail.php");
                if (request != null)
                {

                    // New API logic here
                    NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);

                    Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
                    foreach (var v in dic)
                    {
                        outgoingQueryString.Add(v.Key, v.Value);
                    }
                    string postdata = outgoingQueryString.ToString();
                    body=postdata;
                    request.ContentType = "application/x-www-form-urlencoded";
                    //End login
                   
                    request.Timeout = 50000;
                    request.Method = "POST";
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    // request.ContentType = "application/json";
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

                if (!string.IsNullOrEmpty(responseText))
                {
                    objres = JsonConvert.DeserializeObject<ApplicationResponse>(responseText);
                }
            }

            catch (Exception ex)
            {
                objres.message = ex.Message;
                objres.status = "0";
            }
            return objres;

        }
        public string voucher(string valid, string VoucherCode, string Amount, string Brand, string url)
        {

            string message = string.Empty;
            FileStream fileStream = new FileStream("wwwroot\\EmailTemplates\\card.html", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                message = reader.ReadToEnd();
            }
            message = message.Replace("[valid]", valid);
            message = message.Replace("[VoucherCode]", VoucherCode);
            message = message.Replace("[Amount]", Amount);
            message = message.Replace("[Brand]", Brand);
            message = message.Replace("[BrandImg]", url);
            return message;
        }

        public ResultSet SendEmail(string ToEmailId, string Subject, string Message, string Name, string filedata = "", string filename = "")
        {
            ResultSet res = new ResultSet();
            try
            {
                ApplicationResponse objres = new ApplicationResponse();
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                StreamReader sr;
                Application objmail = new Application();
                objmail.message = new Message();
                objmail.message.to = new List<To>();
                objmail.message.attachments = new List<Attachment>();
                if (!string.IsNullOrEmpty(filedata))
                {
                   
                    objmail.message.attachments.Add(new Attachment { content = filedata, name = filename, type = "application/pdf" });
                }
                objmail.message.from_email = MailFrom;
                objmail.message.from_name = MailFromName;
                objmail.message.html = Message;
                objmail.message.subject = Subject;
                objmail.owner_id = MailOwnerId;
                objmail.token = MailToken;
                objmail.smtp_user_name = SMTPUserName;
                objmail.message.to.Add(new To { email = ToEmailId, name = Name, type = "to" });
                js = new DataContractJsonSerializer(typeof(Application));
                ms = new MemoryStream();
                js.WriteObject(ms, objmail);
                ms.Position = 0;
                sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                var result = MailService(CustData);
                if (result.status != "0" || result.status != "error")
                {
                    EmailLog log = new EmailLog() { FromMailId = MailFrom, ToMailId = ToEmailId, Subject = Subject, Message = Message };
                    _dataRepository.EmailLog(log);
                    res.flag = 1;
                }
                else
                {
                    res.flag = 0;
                }


            }
            catch 
            {
                throw;

            }
            return res;
        }

        public ResultSet SendEmailV2(string ToEmailId, string Subject, string Message, string Name, string filedata = "", string filename = "")
        {
            ResultSet res = new ResultSet();
            try
            {


                ApplicationResponse objres = new ApplicationResponse();
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                StreamReader sr;
                ICSMailService objmail = new ICSMailService();
             
                if (!string.IsNullOrEmpty(filedata))
                {

                    objmail.attachment= filedata;
                }
                objmail.fromAddress= "noreply@alert.mobilepe.co.in";           
                objmail.emailcontent= Message;
                objmail.subject = Subject;              
                objmail.username = "mobilepe";
                objmail.password = "MvTcBp2EJk";
                objmail.emailTo= ToEmailId;
                js = new DataContractJsonSerializer(typeof(ICSMailService));
                ms = new MemoryStream();
                js.WriteObject(ms, objmail);
                ms.Position = 0;
                sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                var result = MailServiceV2(CustData);
                if (result.status != "0" || result.status != "error")
                {
                    EmailLog log = new EmailLog() { FromMailId = MailFrom, ToMailId = ToEmailId, Subject = Subject, Message = Message };
                    _dataRepository.EmailLog(log);
                    res.flag = 1;
                }
                else
                {
                    res.flag = 0;
                }


            }
            catch
            {
                throw;

            }
            return res;
        }
    }
}
