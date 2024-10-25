using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Model;
using Newtonsoft.Json;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Services;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;
using MobileAPI_V2.Model.Travel;
using Nancy.Diagnostics;
using Nancy.Json;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace MobileAPI_V2.Controllers
{
    //[ApiVersion("1.1")]
    [Route("api/Ecommerce")]
    [ApiController]

    public class AcountsController : ControllerBase
    {
        private readonly IDataRepositoryEcomm _dataRepository;
        string AESKEY = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
        private readonly IConfiguration _configuration;
        SendSMSEcomm objsms;
        PineCommonRequest PineCommonRequestobj;
        string PineCardbaseUrl = string.Empty;
        string PINECARDBALANCEURL = string.Empty;
        string PINECARDTOKENURL = string.Empty;
        JWTToken objJWT;

        private readonly EmailTemplateService _emailTemplateService;
        public AcountsController(IHostingEnvironment env, IDataRepositoryEcomm dataRepository, IConfiguration configuration, EmailTemplateService emailTemplateService)
        {

            _dataRepository = dataRepository;
            _configuration = configuration;
            PineCommonRequestobj = new PineCommonRequest(_configuration);
            PineCardbaseUrl = _configuration["PineCardbaseUrl"];
            PINECARDBALANCEURL = _configuration["PINECARDBALANCEURL"];
            PINECARDTOKENURL = _configuration["PINECARDTOKENURL"];
            objsms = new SendSMSEcomm(_configuration, _dataRepository);
            _emailTemplateService = emailTemplateService;
        }




        [HttpPost("OTP")]
        [Produces("application/json")]
        public async Task<ResponseModel> OTP(RequestModel requestModel)
        {
            string EncryptedText = "";
            CommonResponseEcomm<OTPResultEcomm> objresponse = new CommonResponseEcomm<OTPResultEcomm>();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                OTPRequestEcomm oTPRequest = new OTPRequestEcomm();
                EncryptedText = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);



                oTPRequest = JsonConvert.DeserializeObject<OTPRequestEcomm>(EncryptedText);
                if (oTPRequest.deviceInfo != null)
                {
                    oTPRequest.DeviceId = oTPRequest.deviceInfo.deviceId;
                    oTPRequest.DeviceType = oTPRequest.deviceInfo.deviceType;
                }

                var res = await _dataRepository.OTPProcess(oTPRequest);

                if (res != null && res.Flag == 1)
                {
                    if (oTPRequest.ProcId == 2)
                    {
                        string messgae = objsms.OTPMemberMessage(res.Name, res.OTP, oTPRequest.Purpose, oTPRequest.mid);
                        if (!string.IsNullOrEmpty(messgae))
                        {
                            if (oTPRequest.Purpose != "Email Verification")
                            {
                                objsms.SendSMSMessage(res.Mobile, messgae, "OTP");
                            }

                            if (oTPRequest.Purpose == "Email Verification")
                            {
                                messgae = messgae.Replace("%23", "#");
                                if (!string.IsNullOrEmpty(oTPRequest.Email))
                                {
                                    var result = objsms.SendEmailV2(oTPRequest.Email, "OTP", messgae, res.Name);
                                }
                                else
                                {
                                    var result = objsms.SendEmailV2(res.Email, "OTP", messgae, res.Name);
                                }
                            }

                        }
                        objresponse.response = "success";
                        objresponse.message = res.msg;
                    }
                    else
                    {
                        objresponse.response = "success";
                        objresponse.message = res.msg;
                    }

                    objresponse.result = res;
                    objresponse.result.OTP = "";
                }
                else
                {
                    if(res.Flag==3)
                    {
                        string msg = "<body style='margin: 0;font-family: 'Poppins', sans-serif;background: #E9E9E9;font-size: 14px;'>";
                        msg += "<table style='width: 100%; max-width: 600px; background: #fff; margin:20px auto; border-radius: 25px; box-shadow: 3px 7px 20px 7px rgb(205 205 205 / 50%);' border='0' cellpadding='0' cellspacing='0'>";
                        msg += "<tbody><tr><td style='text-align: center;'><img alt='Logo' src='https://web.mobilepe.co.in/Adminassets/img/logo.png'height='80px' /></td></tr>";
                        msg += "<tr><td style='text-align: center; background: #345aad; padding: 35px 50px; font-size: 18px; color: #fff;'><h1 style='margin: 0px; padding: 0px'>Welcome to MobilePe</h1>";
                        msg += "<p style='margin: 0px; padding: 0px'>MOBILEPE sparked India's Digital Revolution.</p></td></tr>";
                        msg += "<tr><td style='text-align: left; background: #fff; padding: 30px 50px 50px;'><h3 style='text-align: left;'>User Details</h3>";
                        msg += "<table style='width:100%' border='0' cellpadding='5' cellspacing='0' ><tr><td style='width: 150px;'><strong>Mobile No</strong></td><td style='width: 50px;'>:</td>";
                        msg += "<td>" + oTPRequest.LoginId + "</td></tr>";


                        msg += "</table></td></tr><tr><td style='text-align: center; background: #e6ebf1; padding: 10px 50px;border-radius:0 0 25px 25px; font-size: 12px;'>Copyright © 2023-24 MOBILEPE. All Rights Reserved.</td></tr></tbody></table></body>";
                        var result = objsms.SendEmailV2("ali@mobilepe.co.in", "Unsuspected User on " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), msg, "");

                    }
                    objresponse.response = "error";
                    objresponse.message = res.msg;
                }
            }
            catch (Exception ex)
            {
                objresponse.response = "error";
                objresponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<OTPResultEcomm>));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("Registration")]
        public async Task<ResponseModel> Registration(RequestModel requestModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            CommonResponseEcomm<LoginResponseEcomm> objres = new CommonResponseEcomm<LoginResponseEcomm>();
            try
            {
                RegistrationV3Ecomm objregister = new RegistrationV3Ecomm();
                EncryptedText = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);


                objregister = JsonConvert.DeserializeObject<RegistrationV3Ecomm>(EncryptedText);
                objregister.DeviceId = objregister.deviceInfo.deviceId;
                objregister.DeviceType = objregister.deviceInfo.deviceType;

                if (objregister.ProcId == 1)
                {
                    if (objregister.Otp != "")
                    {
                        OTPRequestEcomm otp = new OTPRequestEcomm();
                        otp.LoginId = objregister.MobileNo;
                        otp.ProcId = 1;
                        otp.Purpose = "Mobile Verification";
                        otp.OTPNO = objregister.Otp;

                        otp.deviceInfo = objregister.deviceInfo;
                        otp.DeviceType = objregister.deviceInfo.deviceType;
                        otp.DeviceId = objregister.deviceInfo.deviceId;

                        var otpresponse = await _dataRepository.OTPProcess(otp);
                        if (otpresponse.Flag == 1)
                        {
                            MD5 md5Hash = MD5.Create();
                            Common common = new Common();
                            string pwd = common.CreatePassword(8);
                            string hash = Md5Encyption.GetMd5Hash(md5Hash, pwd);
                            objregister.Password = hash;
                            var res = await _dataRepository.CustomerRegistrationV3(objregister);
                            objres.Id = res.CustomerRegistrationResponse.Id;
                            objres.response = "success";
                        }
                        else
                        {
                            objres.response = "error";
                            objres.message = otpresponse.msg;
                        }
                    }
                    else
                    {
                        objres.response = "error";
                        objres.message = "Please enter otp";
                    }
                }
                else if (objregister.ProcId == 2)
                {
                    if (objregister.Otp != "")
                    {
                        OTPRequestEcomm otp = new OTPRequestEcomm();
                        otp.LoginId = objregister.Email;
                        otp.ProcId = 1;
                        otp.Purpose = "Email Verification";
                        otp.OTPNO = objregister.Otp;

                        otp.deviceInfo = objregister.deviceInfo;
                        otp.DeviceType = objregister.deviceInfo.deviceType;
                        otp.DeviceId = objregister.deviceInfo.deviceId;

                        var otpresponse = await _dataRepository.OTPProcess(otp);
                        if (otpresponse.Flag == 1)
                        {
                            var res = await _dataRepository.CustomerRegistrationV3(objregister);
                            objres.Id = res.CustomerRegistrationResponse.Id;
                            objres.response = "success";
                        }
                        else
                        {
                            objres.response = "error";
                            objres.message = otpresponse.msg;
                        }
                    }
                    else
                    {
                        objres.response = "error";
                        objres.message = "Please enter otp";
                    }
                }
                else if (objregister.ProcId == 3)
                {

                    MD5 md5Hash = MD5.Create();
                    string pwd = objregister.Password;
                    string hash = Md5Encyption.GetMd5Hash(md5Hash, pwd);
                    objregister.Password = hash;
                    var res = await _dataRepository.CustomerRegistrationV3(objregister);
                    if (!res.LoginResponse.isActive)
                    {
                        string NotificationTitle, NotificationMessageUser, DeviceIdUser, DeviceIdSponsor, NotificationMessageSponsor;
                        NotificationTitle = "Welcome";
                        NotificationMessageUser = "Dear " + res.LoginResponse.firstName + ",\nWarm Welcome to MobilePe Parivar!\nYour unbeatable journey starts now for finanical freedom.\nwe hope to have a lasting relationship together.\nwe will succeed.\n\nThanking You\nMobilePe Admin";
                        DeviceIdUser = res.LoginResponse.userDeviceId;
                        NotificationMessageSponsor = "Dear " + res.LoginResponse.sponsorName + ",\nwe are glad to inform that " + res.LoginResponse.firstName + " has joined your immediate CUG.\nKindly explain the process of MobilePe.\n\nThanking You\nMobilePe Admin";
                        DeviceIdSponsor = res.LoginResponse.sponsorDeviceId;
                        if (!string.IsNullOrEmpty(res.LoginResponse.userDeviceId))
                        {
                            var messageuser = Notification.SendNotification(res.LoginResponse.memberId, DeviceIdUser, NotificationMessageUser, NotificationTitle);
                            var xmlnotificationuser = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageUser + "</Message><CustomerId>" + res.LoginResponse.memberId + "</CustomerId><DeviceId>" + DeviceIdUser + "</DeviceId></notifications></notification>";
                            var notificationresultuser = await _dataRepository.Insetnotification(xmlnotificationuser);
                        }
                        if (!string.IsNullOrEmpty(res.LoginResponse.sponsorDeviceId))
                        {
                            var messagesponsor = Notification.SendNotification(res.LoginResponse.sponsorId, DeviceIdSponsor, NotificationMessageSponsor, NotificationTitle);
                            var xmlnotificationsponsor = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageSponsor + "</Message><CustomerId>" + res.LoginResponse.sponsorId + "</CustomerId><DeviceId>" + DeviceIdSponsor + "</DeviceId></notifications></notification>";
                            var notificationresultsponsor = await _dataRepository.Insetnotification(xmlnotificationsponsor);
                        }
                        string message = objsms.RegistrationSMS(objregister.FullName, objregister.MobileNo, pwd);
                        objsms.SendSMSMessage(objregister.MobileNo, message, "REGISTRATION");
                        string msg = objsms.UserRegistrationMailMessage(objregister.FullName, objregister.MobileNo, pwd);
                        var result = objsms.SendEmail(objregister.Email, "Registration", msg, objregister.FullName);
                    }
                    objres.result = res.LoginResponse;
                    objres.Id = res.CustomerRegistrationResponse.Id;
                    objres.response = "success";
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<LoginResponseEcomm>));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("Registration_v2")]
        public async Task<ResponseModel> Registration_v2(RequestModel requestModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            CommonResponseEcomm<LoginResponseEcomm> objres = new CommonResponseEcomm<LoginResponseEcomm>();
            try
            {
                RegistrationV3Ecomm objregister = new RegistrationV3Ecomm();
                EncryptedText = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);


                objregister = JsonConvert.DeserializeObject<RegistrationV3Ecomm>(EncryptedText);
                objregister.DeviceId = objregister.deviceInfo.deviceId;
                objregister.DeviceType = objregister.deviceInfo.deviceType;

                if (objregister.ProcId == 1)
                {
                    if (objregister.Otp != "")
                    {
                        OTPRequestEcomm otp = new OTPRequestEcomm();
                        otp.LoginId = objregister.MobileNo;
                        otp.ProcId = 1;
                        otp.Purpose = "Mobile Verification";
                        otp.OTPNO = objregister.Otp;

                        otp.deviceInfo = objregister.deviceInfo;
                        otp.DeviceType = objregister.deviceInfo.deviceType;
                        otp.DeviceId = objregister.deviceInfo.deviceId;

                        var otpresponse = await _dataRepository.OTPProcess(otp);
                        if (otpresponse.Flag == 1)
                        {
                            MD5 md5Hash = MD5.Create();
                            Common common = new Common();
                            string pwd = common.CreatePassword(8);
                            string hash = Md5Encyption.GetMd5Hash(md5Hash, pwd);
                            objregister.Password = hash;
                            var res = await _dataRepository.CustomerRegistration_V2(objregister);
                            objres.Id = res.CustomerRegistrationResponse.Id;
                            objres.response = "success";

                            //Added by maqsood 29-12-2023
                            string key = string.Empty;
                            key = objregister.Email + "&" + objres.Id.ToString() + "&" + DateTime.Now.ToString("ddMMyyyy");
                            key = ApiEncrypt_Decrypt.EncryptString(AESKEY, key);
                            string messgae = _emailTemplateService.AccountVerificationTemplate(objregister.FullName, key);

                            var result = objsms.SendEmailV2(objregister.Email, "Account verification", messgae, objregister.FullName);

                        }
                        else
                        {
                            objres.response = "error";
                            objres.message = otpresponse.msg;
                        }
                    }
                    else
                    {
                        objres.response = "error";
                        objres.message = "Please enter otp";
                    }
                }
                else if (objregister.ProcId == 2)
                {
                    if (objregister.Otp != "")
                    {
                        OTPRequestEcomm otp = new OTPRequestEcomm();
                        otp.LoginId = objregister.Email;
                        otp.ProcId = 1;
                        otp.Purpose = "Email Verification";
                        otp.OTPNO = objregister.Otp;

                        otp.deviceInfo = objregister.deviceInfo;
                        otp.DeviceType = objregister.deviceInfo.deviceType;
                        otp.DeviceId = objregister.deviceInfo.deviceId;

                        var otpresponse = await _dataRepository.OTPProcess(otp);
                        if (otpresponse.Flag == 1)
                        {
                            var res = await _dataRepository.CustomerRegistrationV3(objregister);
                            objres.Id = res.CustomerRegistrationResponse.Id;
                            objres.response = "success";
                        }
                        else
                        {
                            objres.response = "error";
                            objres.message = otpresponse.msg;
                        }
                    }
                    else
                    {
                        objres.response = "error";
                        objres.message = "Please enter otp";
                    }
                }
                else if (objregister.ProcId == 3)
                {

                    MD5 md5Hash = MD5.Create();
                    string pwd = objregister.Password;
                    string hash = Md5Encyption.GetMd5Hash(md5Hash, pwd);
                    objregister.Password = hash;
                    var res = await _dataRepository.CustomerRegistrationV3(objregister);
                    if (!res.LoginResponse.isActive)
                    {
                        string NotificationTitle, NotificationMessageUser, DeviceIdUser, DeviceIdSponsor, NotificationMessageSponsor;
                        NotificationTitle = "Welcome";
                        NotificationMessageUser = "Dear " + res.LoginResponse.firstName + ",\nWarm Welcome to MobilePe Parivar!\nYour unbeatable journey starts now for finanical freedom.\nwe hope to have a lasting relationship together.\nwe will succeed.\n\nThanking You\nMobilePe Admin";
                        DeviceIdUser = res.LoginResponse.userDeviceId;
                        NotificationMessageSponsor = "Dear " + res.LoginResponse.sponsorName + ",\nwe are glad to inform that " + res.LoginResponse.firstName + " has joined your immediate CUG.\nKindly explain the process of MobilePe.\n\nThanking You\nMobilePe Admin";
                        DeviceIdSponsor = res.LoginResponse.sponsorDeviceId;
                        if (!string.IsNullOrEmpty(res.LoginResponse.userDeviceId))
                        {
                            var messageuser = Notification.SendNotification(res.LoginResponse.memberId, DeviceIdUser, NotificationMessageUser, NotificationTitle);
                            var xmlnotificationuser = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageUser + "</Message><CustomerId>" + res.LoginResponse.memberId + "</CustomerId><DeviceId>" + DeviceIdUser + "</DeviceId></notifications></notification>";
                            var notificationresultuser = await _dataRepository.Insetnotification(xmlnotificationuser);
                        }
                        if (!string.IsNullOrEmpty(res.LoginResponse.sponsorDeviceId))
                        {
                            var messagesponsor = Notification.SendNotification(res.LoginResponse.sponsorId, DeviceIdSponsor, NotificationMessageSponsor, NotificationTitle);
                            var xmlnotificationsponsor = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageSponsor + "</Message><CustomerId>" + res.LoginResponse.sponsorId + "</CustomerId><DeviceId>" + DeviceIdSponsor + "</DeviceId></notifications></notification>";
                            var notificationresultsponsor = await _dataRepository.Insetnotification(xmlnotificationsponsor);
                        }
                        string message = objsms.RegistrationSMS(objregister.FullName, objregister.MobileNo, pwd);
                        objsms.SendSMSMessage(objregister.MobileNo, message, "REGISTRATION");
                        string msg = objsms.UserRegistrationMailMessage(objregister.FullName, objregister.MobileNo, pwd);
                        var result = objsms.SendEmail(objregister.Email, "Registration", msg, objregister.FullName);
                    }
                    objres.result = res.LoginResponse;
                    objres.Id = res.CustomerRegistrationResponse.Id;
                    objres.response = "success";
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<LoginResponseEcomm>));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("Login")]
        public async Task<ResponseModel> Login(RequestModel requestModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            CommonResponseEcomm<LoginResponseEcomm> objres = new CommonResponseEcomm<LoginResponseEcomm>();
            try
            {
                LoginEcomm objlogin = new LoginEcomm();
                EncryptedText = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                objlogin = JsonConvert.DeserializeObject<LoginEcomm>(EncryptedText);

                MD5 md5Hash = MD5.Create();
                string hash = Md5Encyption.GetMd5Hash(md5Hash, objlogin.Password);
                objlogin.Password = hash;

                //objlogin.DeviceId = objlogin.deviceInfo.deviceId;
                //objlogin.OSId = objlogin.deviceInfo.os;
                //objlogin.DeviceType = objlogin.deviceInfo.deviceType;


                if (objlogin.deviceInfo != null)
                {
                    objlogin.DeviceId = objlogin.deviceInfo.deviceId;
                    objlogin.DeviceType = objlogin.deviceInfo.deviceType;
                    objlogin.appVersion = objlogin.deviceInfo.appVersion;
                    objlogin.telecom = objlogin.deviceInfo.telecom;
                    objlogin.geoCode = objlogin.deviceInfo.geoCode;
                    objlogin.appId = objlogin.deviceInfo.appId;
                    objlogin.ipAddress = objlogin.deviceInfo.ipAddress;
                    objlogin.location = objlogin.deviceInfo.location;
                    objlogin.mobile = objlogin.deviceInfo.mobile;
                    objlogin.OSId = objlogin.deviceInfo.os;

                }
                else
                {
                    objlogin.DeviceId = "";
                    objlogin.DeviceType = "";
                    objlogin.appVersion = "";
                    objlogin.telecom = "";
                    objlogin.geoCode = "";
                    objlogin.appId = "";
                    objlogin.ipAddress = "";
                    objlogin.location = "";
                    objlogin.mobile = "";
                    objlogin.OSId = "";
                }

                var res = await _dataRepository.Login(objlogin);
                if (res != null && res.flag == 1)
                {
                    if (res.bankStatus == "2")
                    {
                        var bank = await _dataRepository.bankdetails(res.memberId);
                        res.bankDetails = bank;
                    }
                    //  objres.result = new LoginResponse();

                    if (!res.isActive)
                    {
                        string NotificationTitle, NotificationMessageUser, DeviceIdUser, DeviceIdSponsor, NotificationMessageSponsor;
                        NotificationTitle = "Welcome";
                        NotificationMessageUser = "Dear " + res.firstName + ",\nWarm Welcome to MobilePe Parivar!\nYour unbeatable journey starts now for finanical freedom.\nwe hope to have a lasting relationship together.\nwe will succeed.\n\nThanking You\nMobilePe Admin";
                        DeviceIdUser = res.userDeviceId;

                        NotificationMessageSponsor = "Dear " + res.sponsorName + ",\nwe are glad to inform that " + res.firstName + " has joined your immediate CUG.\nKindly explain the process of MobilePe.\n\nThanking You\nMobilePe Admin";
                        DeviceIdSponsor = res.sponsorDeviceId;

                        if (!string.IsNullOrEmpty(res.userDeviceId))
                        {
                            var messageuser = Notification.SendNotification(res.memberId, DeviceIdUser, NotificationMessageUser, NotificationTitle);
                            var xmlnotificationuser = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageUser + "</Message><CustomerId>" + res.memberId + "</CustomerId><DeviceId>" + DeviceIdUser + "</DeviceId></notifications></notification>";
                            var notificationresultuser = await _dataRepository.Insetnotification(xmlnotificationuser);
                        }
                        if (!string.IsNullOrEmpty(res.sponsorDeviceId))
                        {
                            var messagesponsor = Notification.SendNotification(res.sponsorId, DeviceIdSponsor, NotificationMessageSponsor, NotificationTitle);

                            var xmlnotificationsponsor = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageSponsor + "</Message><CustomerId>" + res.sponsorId + "</CustomerId><DeviceId>" + DeviceIdSponsor + "</DeviceId></notifications></notification>";

                            var notificationresultsponsor = await _dataRepository.Insetnotification(xmlnotificationsponsor);
                        }
                    }
                    objres.message = "Login successfully";
                    objres.response = "success";
                    objres.result = res;
                    objres.result.token = res.token;

                    var SaveResponse = _dataRepository.SaveLoginLog(JsonConvert.SerializeObject(objlogin), JsonConvert.SerializeObject(objres), objlogin.LoginID);
                    //var Response = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(objres), "Login Response", 102030);

                    //objres.result.token = objlogin.LoginID;
                }
                else
                {
                    objres.message = res.msg;
                    objres.response = "error";
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<LoginResponseEcomm>));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("ForgetPassword")]
        public async Task<ResponseModel> ForgetPassword(RequestModel requestModel)
        {
            string EncryptedText = "";
            CommonEcomm objresponse = new CommonEcomm();
            ResponseModel returnResponse = new ResponseModel();
            ForgotPasswordEcomm objforgot = new ForgotPasswordEcomm();
            try
            {

                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                objforgot = JsonConvert.DeserializeObject<ForgotPasswordEcomm>(dcdata);
                MD5 md5Hash = MD5.Create();
                string NewPassword = Md5Encyption.GetMd5Hash(md5Hash, objforgot.NewPassword);
                objforgot.NewPassword = NewPassword;
                var res = await _dataRepository.ForgetPassword(objforgot);

                if (res != null && res.Id == 1)
                {

                    objresponse.response = "success";
                    objresponse.message = res.msg;

                }
                else
                {

                    objresponse.response = "error";
                    objresponse.message = res.msg;
                }

            }
            catch (Exception ex)
            {
                objresponse.response = "error";
                objresponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonEcomm));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }
        [HttpGet("CheckUserData")]
        public async Task<ResponseModel> CheckUserData(string data, string type)
        {
            string EncryptedText = "";
            data = data.Replace(" ", "+");
            data = ApiEncrypt_Decrypt.DecryptString(AESKEY, data);
            ResponseModel returnResponse = new ResponseModel();
            Common objres = new Common();
            try
            {
                var res = await _dataRepository.CheckEmail_Mobile(data);
                if (res != null && res.flag == 0)
                {
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    if (type.ToLower() == "email")
                    {
                        objres.response = "error";
                        objres.message = "Email'Id already exists";

                    }

                    else
                    {
                        objres.response = "error";
                        objres.message = "mobile no. already exists";
                    }
                }


            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Common));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }
        [HttpGet("AutoUpdate")]
        public async Task<ResponseModel> AutoUpdate(string version, string ostype)
        {
            string EncryptedText = "";
            version = version.Replace(" ", "+");
            version = ApiEncrypt_Decrypt.DecryptString(AESKEY, version);
            ostype = ostype.Replace(" ", "+");
            ostype = ApiEncrypt_Decrypt.DecryptString(AESKEY, ostype);
            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<AppVersionResponse> objres = new CommonResponse<AppVersionResponse>();
            try
            {
                var res = await _dataRepository.GetAppVersionDetail(ostype, version);
                if (res != null)
                {

                    if (res.IsForced == false)
                    {
                        objres.response = "error";
                        objres.message = res.message;
                        objres.result = res;
                    }
                    else
                    {
                        objres.response = "success";
                        objres.message = "success";
                        objres.result = res;
                    }
                }


            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }


            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<AppVersionResponse>));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }
        [HttpGet("GetReferalDetails")]
        public async Task<ResponseModel> GetReferalDetails(string InviteCode)
        {
            ResponseModel returnResponse = new ResponseModel();
            CommonResponseEcomm<ReferralModelEcomm> objresponse = new CommonResponseEcomm<ReferralModelEcomm>();
            string EncryptedText = "";
            if (!string.IsNullOrEmpty(InviteCode))
            {
                InviteCode = ApiEncrypt_Decrypt.DecryptString(AESKEY, InviteCode);
            }
            var res = await _dataRepository.GetReferralName(InviteCode);
            try
            {
                if (res != null && res.Name != null)
                {

                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse.result = res;

                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "Invalid Referral Code";
                }
            }
            catch (Exception ex)
            {
                objresponse.response = "error";
                objresponse.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<ReferralModelEcomm>));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("GetAreaDetailByPincode")]
        public async Task<ResponseModel> GetAreaDetailByPincode(string pincode)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            CommonResponseEcomm<PincodeModel> objres = new CommonResponseEcomm<PincodeModel>();
            try
            {
                if (!string.IsNullOrEmpty(pincode))
                {
                    pincode = ApiEncrypt_Decrypt.DecryptString(AESKEY, pincode);
                }
                var res = await _dataRepository.GetAreaDetailByPincode(pincode);
                if (res != null)
                {

                    objres.response = "success";
                    objres.message = "success";
                    objres.result = res;
                }
                else
                {
                    objres.response = "error";
                    objres.message = "Invalid Pincode";
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<PincodeModel>));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpGet("SendEmailVerificationLink")]
        public ResponseModel SendEmailVerificationLink(string mobileno, string email, string Name)
        {
            string EncryptedText = "";
            mobileno = mobileno.Replace(" ", "+");
            mobileno = ApiEncrypt_Decrypt.DecryptString(AESKEY, mobileno);


            email = email.Replace(" ", "+");
            email = ApiEncrypt_Decrypt.DecryptString(AESKEY, email);


            Name = Name.Replace(" ", "+");
            Name = ApiEncrypt_Decrypt.DecryptString(AESKEY, Name);


            ResponseModel returnResponse = new ResponseModel();
            Common objres = new Common();
            try
            {
                MD5 md5Hash = MD5.Create();
                var token = objJWT.GenerateToken(mobileno);

                var emailconfirmation = _dataRepository.EmailConfirmationLink(token, "Insert", mobileno, email, Md5Encyption.GetMd5Hash(md5Hash, mobileno));
                if (emailconfirmation.Result.response == "success")
                {
                    string emailconfirmationmsg = "https://mobilepe.co.in/Home/EmailVerification?UserId=" + Md5Encyption.GetMd5Hash(md5Hash, mobileno) + "&Token=" + token + "";
                    string msg = objsms.EmailVerificationMailMessage(Name, emailconfirmationmsg);
                    var emailconfirmationres = objsms.SendEmail(email, "Email Verification", msg, Name);
                }

                objres.response = "success";
                objres.message = "Email Verification link has been sent  to your registered Email";


            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Common));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpGet("SendEmailVerification")]
        public IActionResult SendEmailVerification()
        {
            string key = string.Empty;
            key = "maqsood@thesileo.com" + "&1&" + DateTime.Now.ToString("ddMMyyyy");
            key = ApiEncrypt_Decrypt.EncryptString(AESKEY, key);
            string messgae = _emailTemplateService.AccountVerificationTemplate("MOHD MAQSOOD KHAN", key);

            var result = objsms.SendEmailV2("maqsood@thesileo.com", "Account verification", messgae, "Mohd Maqsood Khan");
            return Ok(result);

        }
    }
}
