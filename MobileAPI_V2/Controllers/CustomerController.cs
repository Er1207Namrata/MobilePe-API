using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Filter;
using MobileAPI_V2.Model;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;

namespace MobileAPI_V2.Controllers
{
    [ServiceFilter(typeof(ApiLog))]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IDataRepository _dataRepository;
        private readonly IConfiguration _configuration;
        string VenusRechargeURL = string.Empty;
        string VENUSPAYBILLURL = string.Empty;
        RechargeConn recConn = new RechargeConn();
        JWTToken objJWT;
        SendSMS objsms;

        public CustomerController(IConfiguration configuration, IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            _configuration = configuration;
            objJWT = new JWTToken(_dataRepository);
            VenusRechargeURL = _configuration["VenusRecharge"];
            VENUSPAYBILLURL = configuration["VENUSPAYBILLURL"];
            objsms = new SendSMS(_configuration, _dataRepository);
        }

        [HttpGet("GetMsg")]
        public string GetMsg()

        {
            objsms.SendEmailV2("vivek@mobilepesoftech.co.in", "Registration", "<h1 style=color:red;>Hello</h1>", "abhijeet", "https://www.mobilepe.co.in/assets/img/banner-image/banner1.png");
            return "mail sent";
        }

        [HttpGet("GetReferalDetails")]
        public async Task<CommonResponse<ReferralModelEcomm>> GetReferalDetails(string InviteCode)
        {

            CommonResponse<ReferralModelEcomm> objresponse = new CommonResponse<ReferralModelEcomm>();

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


            return objresponse;
        }

        [HttpGet("GetToken")]
        public CommonResponse<TokenModel> GetToken(string MobileNo)
        {
            CommonResponse<TokenModel> objres = new CommonResponse<TokenModel>();
            try
            {
                var res = _dataRepository.CheckMobileNo(MobileNo);
                if (res.Result.flag == 1)
                {
                    objres.result = new TokenModel();
                    objres.result.token = objJWT.GenerateToken(MobileNo);
                    if (objres.result != null)
                    {
                        objres.response = "success";
                        objres.message = "success";

                    }
                    else
                    {
                        objres.response = "error";
                        objres.message = "An Error Occured";

                    }
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Result.msg;
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }
            return objres;
        }

        [HttpGet("GetAreaDetailByPincode")]
        public async Task<CommonResponse<PincodeModel>> GetAreaDetailByPincode(string pincode)
        {
            CommonResponse<PincodeModel> objres = new CommonResponse<PincodeModel>();
            try
            {

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


            return objres;

        }

        [HttpPost("Login")]
        public async Task<CommonResponse<LoginResponse>> Login(Login objlogin)
        {

            CommonResponse<LoginResponse> objres = new CommonResponse<LoginResponse>();

            try
            {

                MD5 md5Hash = MD5.Create();
                string hash = Md5Encyption.GetMd5Hash(md5Hash, objlogin.Password);
                objlogin.Password = hash;
                var res = await _dataRepository.Login(objlogin);
                if (res != null && res.flag == 1)
                {
                    if (res.BankStatus == "2")
                    {
                        var bank = await _dataRepository.bankdetails(res.MemberId);
                        res.bankdetails = bank;
                    }
                    //  objres.result = new LoginResponse();

                    if (!res.isActive)
                    {
                        string NotificationTitle, NotificationMessageUser, DeviceIdUser, DeviceIdSponsor, NotificationMessageSponsor;
                        NotificationTitle = "Welcome";
                        NotificationMessageUser = "Dear " + res.FirstName + ",\nWarm Welcome to MobilePe Parivar!\nYour unbeatable journey starts now for finanical freedom.\nwe hope to have a lasting relationship together.\nwe will succeed.\n\nThanking You\nMobilePe Admin";
                        DeviceIdUser = res.UserDeviceId;

                        NotificationMessageSponsor = "Dear " + res.SponsorName + ",\nwe are glad to inform that " + res.FirstName + " has joined your immediate CUG.\nKindly explain the process of MobilePe.\n\nThanking You\nMobilePe Admin";
                        DeviceIdSponsor = res.SponsorDeviceId;

                        if (!string.IsNullOrEmpty(res.UserDeviceId))
                        {
                            var messageuser = Notification.SendNotification(res.MemberId, DeviceIdUser, NotificationMessageUser, NotificationTitle);
                            var xmlnotificationuser = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageUser + "</Message><CustomerId>" + res.MemberId + "</CustomerId><DeviceId>" + DeviceIdUser + "</DeviceId></notifications></notification>";
                            var notificationresultuser = await _dataRepository.Insetnotification(xmlnotificationuser);
                        }
                        if (!string.IsNullOrEmpty(res.SponsorDeviceId))
                        {
                            var messagesponsor = Notification.SendNotification(res.SponsorId, DeviceIdSponsor, NotificationMessageSponsor, NotificationTitle);

                            var xmlnotificationsponsor = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageSponsor + "</Message><CustomerId>" + res.SponsorId + "</CustomerId><DeviceId>" + DeviceIdSponsor + "</DeviceId></notifications></notification>";

                            var notificationresultsponsor = await _dataRepository.Insetnotification(xmlnotificationsponsor);
                        }
                    }
                    objres.message = "Login successfully";
                    objres.response = "success";
                    objres.result = res;
                     //objres.result.token = objJWT.GenerateToken(objlogin.LoginID);
                    objres.result.token = objlogin.LoginID;

                }
                else
                {
                    objres.message = res.Msg;
                    objres.response = "error";
                }


            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }


            return objres;
        }
        //[HttpPost("Registration")]
        //public async Task<CommonResponse<LoginResponse>> Registration(Registration objregister)
        //{


        //    CommonResponse<LoginResponse> objres = new CommonResponse<LoginResponse>();
        //    try
        //    {
        //        if (objregister.Otp != "")
        //        {
        //            OTPRequest otp = new OTPRequest();
        //            otp.LoginId = objregister.MobileNo;
        //            otp.ProcId = 1;
        //            otp.Purpose = "Registration";
        //            otp.OTPNO = objregister.Otp;
        //            var otpresponse = await _dataRepository.OTPProcess(otp);
        //            if (otpresponse.Flag == 1)
        //            {
        //                MD5 md5Hash = MD5.Create();
        //                // string pwd = CreatePassword(8);
        //                string pwd = objregister.Password;
        //                string hash = Md5Encyption.GetMd5Hash(md5Hash, pwd);
        //                objregister.Password = hash;
        //                var res = await _dataRepository.Register(objregister).ConfigureAwait(false);
        //                if (res != null && res.flag == 1)
        //                {
        //                    string message = objsms.RegistrationSMS(objregister.FirstName, objregister.MobileNo, pwd);
        //                    objsms.SendSMSMessage(objregister.MobileNo, message, "REGISTRATION");

        //                    string msg = objsms.UserRegistrationMailMessage(objregister.FirstName, objregister.MobileNo, pwd);
        //                    var result = objsms.SendEmail(objregister.Email, "Registration", msg, objregister.FirstName);

        //                    var userId = Md5Encyption.GetMd5Hash(md5Hash, objregister.MobileNo);
        //                    var token = objJWT.GenerateToken(objregister.MobileNo);

        //                    var emailconfirmation = await _dataRepository.EmailConfirmationLink(token, "Insert", objregister.MobileNo, objregister.Email, Md5Encyption.GetMd5Hash(md5Hash, objregister.MobileNo)).ConfigureAwait(false);
        //                    if (emailconfirmation.response == "success")
        //                    {
        //                        string emailconfirmationmsg = "https://mobilepe.co.in/Home/EmailVerification?UserId=" + Md5Encyption.GetMd5Hash(md5Hash, objregister.MobileNo) + "&Token=" + token + "";
        //                        var emailconfirmationres = objsms.SendEmail(objregister.Email, "Email Verification", emailconfirmationmsg, objregister.FirstName);
        //                    }

        //                    if (result.flag == 1)
        //                    {
        //                        //objres.response = "success";
        //                        //objres.message = "Registration Successfully";
        //                        Login loginreq = new Login();
        //                        loginreq.LoginID = objregister.MobileNo;
        //                        loginreq.Password = objregister.Password;
        //                        loginreq.DeviceId = objregister.DeviceId;
        //                        loginreq.OSId = objregister.OSId;
        //                        loginreq.UserType = objregister.UserType;
        //                        var loginresponse = await _dataRepository.Login(loginreq).ConfigureAwait(false);
        //                        if (loginresponse.flag == 1)
        //                        {


        //                            if (!loginresponse.isActive)
        //                            {
        //                                string NotificationTitle, NotificationMessageUser, DeviceIdUser, DeviceIdSponsor, NotificationMessageSponsor;
        //                                NotificationTitle = "Welcome";
        //                                NotificationMessageUser = "Dear " + loginresponse.FirstName + ",\nWarm Welcome to MobilePe Parivar!\nYour unbeatable journey starts now for finanical freedom.\nwe hope to have a lasting relationship together.\nwe will succeed.\n\nThanking You\nMobilePe Admin";
        //                                DeviceIdUser = loginresponse.UserDeviceId;

        //                                NotificationMessageSponsor = "Dear " + loginresponse.SponsorName + ",\nwe are glad to inform that " + loginresponse.FirstName + " has joined your immediate CUG.\nKindly explain the process of MobilePe.\n\nThanking You\nMobilePe Admin";
        //                                DeviceIdSponsor = loginresponse.SponsorDeviceId;

        //                                if (!string.IsNullOrEmpty(loginresponse.UserDeviceId))
        //                                {
        //                                    var messageuser = Notification.SendNotification(loginresponse.MemberId, DeviceIdUser, NotificationMessageUser, NotificationTitle);
        //                                    var xmlnotificationuser = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageUser + "</Message><CustomerId>" + loginresponse.MemberId + "</CustomerId><DeviceId>" + DeviceIdUser + "</DeviceId></notifications></notification>";
        //                                    var notificationresultuser = await _dataRepository.Insetnotification(xmlnotificationuser).ConfigureAwait(false);
        //                                }
        //                                if (!string.IsNullOrEmpty(loginresponse.SponsorDeviceId))
        //                                {
        //                                    var messagesponsor = Notification.SendNotification(loginresponse.MemberId, DeviceIdSponsor, NotificationMessageSponsor, NotificationTitle);

        //                                    var xmlnotificationsponsor = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageSponsor + "</Message><CustomerId>" + loginresponse.SponsorId + "</CustomerId><DeviceId>" + DeviceIdSponsor + "</DeviceId></notifications></notification>";

        //                                    var notificationresultsponsor = await _dataRepository.Insetnotification(xmlnotificationsponsor).ConfigureAwait(false);
        //                                }
        //                            }
        //                            objres.message = "Login successfully";
        //                            objres.response = "success";
        //                            objres.result = loginresponse;
        //                            objres.result.token = objJWT.GenerateToken(loginresponse.MobileNo);

        //                        }
        //                        else
        //                        {
        //                            objres.message = "error";
        //                            objres.response = "error";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        objres.message = "Login Failed";
        //                        objres.response = "error";
        //                    }

        //                }
        //                else
        //                {
        //                    objres.message = res.msg;
        //                    objres.response = "error";
        //                }

        //            }
        //            else
        //            {
        //                objres.response = "error";
        //                objres.message = otpresponse.msg;
        //            }
        //        }
        //        else
        //        {
        //            objres.response = "error";
        //            objres.message = "Please enter otp";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objres.message = ex.Message;
        //        objres.response = "error";
        //    }
        //    return objres;
        //}

        [HttpPost("Registration")]
        public async Task<CommonResponse<LoginResponse>> Registration(Registration objregister)
        {


            CommonResponse<LoginResponse> objres = new CommonResponse<LoginResponse>();
            try
            {
                if (objregister.Otp != "")
                {
                    OTPRequest otp = new OTPRequest();
                    otp.LoginId = objregister.MobileNo;
                    otp.ProcId = 1;
                    otp.Purpose = "Registration";
                    otp.OTPNO = objregister.Otp;
                    var otpresponse = await _dataRepository.OTPProcess(otp);
                    if (otpresponse.Flag == 1)
                    {
                        MD5 md5Hash = MD5.Create();

                        string pwd = string.IsNullOrEmpty(objregister.Password)?"123456": objregister.Password;
                        string hash = Md5Encyption.GetMd5Hash(md5Hash, pwd);
                        objregister.Password = hash;
                        var res = await _dataRepository.CustomerRegistration(objregister);
                        if (res != null && res.CustomerRegistrationResponse.flag == 1)
                        {
                            string message = objsms.RegistrationSMS(objregister.FirstName, objregister.MobileNo, pwd);
                            objsms.SendSMSMessage(objregister.MobileNo, message, "REGISTRATION");

                            string msg = objsms.UserRegistrationMailMessage(objregister.FirstName, objregister.MobileNo, pwd);
                            var result = objsms.SendEmail(objregister.Email, "Registration", msg, objregister.FirstName);

                            var userId = Md5Encyption.GetMd5Hash(md5Hash, objregister.MobileNo);
                            var token = objJWT.GenerateToken(objregister.MobileNo);


                            if (result.flag == 1)
                            {

                                if (!res.LoginResponse.isActive)
                                {
                                    string NotificationTitle, NotificationMessageUser, DeviceIdUser, DeviceIdSponsor, NotificationMessageSponsor;
                                    NotificationTitle = "Welcome";
                                    NotificationMessageUser = "Dear " + res.LoginResponse.FirstName + ",\nWarm Welcome to MobilePe Parivar!\nYour unbeatable journey starts now for finanical freedom.\nwe hope to have a lasting relationship together.\nwe will succeed.\n\nThanking You\nMobilePe Admin";
                                    DeviceIdUser = res.LoginResponse.UserDeviceId;

                                    NotificationMessageSponsor = "Dear " + res.LoginResponse.SponsorName + ",\nwe are glad to inform that " + res.LoginResponse.FirstName + " has joined your immediate CUG.\nKindly explain the process of MobilePe.\n\nThanking You\nMobilePe Admin";
                                    DeviceIdSponsor = res.LoginResponse.SponsorDeviceId;

                                    if (!string.IsNullOrEmpty(res.LoginResponse.UserDeviceId))
                                    {
                                        var messageuser = Notification.SendNotification(res.LoginResponse.MemberId, DeviceIdUser, NotificationMessageUser, NotificationTitle);
                                        var xmlnotificationuser = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageUser + "</Message><CustomerId>" + res.LoginResponse.MemberId + "</CustomerId><DeviceId>" + DeviceIdUser + "</DeviceId></notifications></notification>";
                                        var notificationresultuser = await _dataRepository.Insetnotification(xmlnotificationuser);
                                    }
                                    if (!string.IsNullOrEmpty(res.LoginResponse.SponsorDeviceId))
                                    {
                                        var messagesponsor = Notification.SendNotification(res.LoginResponse.SponsorId, DeviceIdSponsor, NotificationMessageSponsor, NotificationTitle);

                                        var xmlnotificationsponsor = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageSponsor + "</Message><CustomerId>" + res.LoginResponse.SponsorId + "</CustomerId><DeviceId>" + DeviceIdSponsor + "</DeviceId></notifications></notification>";

                                        var notificationresultsponsor = await _dataRepository.Insetnotification(xmlnotificationsponsor);
                                    }
                                }

                                objres.result = res.LoginResponse;
                                objres.message = "success";
                                objres.response = "success";
                                objres.result.token = token;
                            }
                            else
                            {
                                objres.message = "Login Failed";
                                objres.response = "error";
                            }

                        }
                        else
                        {
                            objres.message = res.CustomerRegistrationResponse.msg;
                            objres.response = "error";
                        }

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
            catch (Exception ex)
            {
                objres.message = ex.Message;
                objres.response = "error";
            }
            return objres;
        }

        [HttpPost("ForgetPassword")]
        public async Task<Common> ForgetPassword(ForgotPassword objforgot)
        {

            Common objresponse = new Common();
            try
            {

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


            return objresponse;
        }


        [HttpPost("ForgetPasswordV2")]
        public async Task<Common> ForgetPasswordV2(ForgotPassword objforgot)
        {
            Common objresponse = new Common();
            try
            {
                if (objforgot.OTP != "")
                {
                    OTPRequest otp = new OTPRequest();
                    otp.LoginId = objforgot.LoginId;
                    otp.ProcId = 1;
                    otp.Purpose = "Registration";
                    otp.OTPNO = objforgot.OTP;
                    var otpresponse = await _dataRepository.OTPProcess(otp);
                    if (otpresponse.Flag == 1)
                    {
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
                }
            }
            catch (Exception ex)
            {
                objresponse.response = "error";
                objresponse.message = ex.Message;
            }


            return objresponse;
        }



        [HttpPost("OTP")]
        public async Task<CommonResponse<OTPResult>> OTP(OTPRequest objrequest)
        {
            CommonResponse<OTPResult> objresponse = new CommonResponse<OTPResult>();
            objresponse.result = new OTPResult();
            try
            {
                var res = await _dataRepository.OTPProcess(objrequest);
                if (res != null && res.Flag == 1)
                {
                    if (objrequest.ProcId == 2)
                    {
                        string messgae = objsms.OTPMemberMessage(res.Name, res.OTP, objrequest.Purpose, objrequest.mid);
                        if (!string.IsNullOrEmpty(messgae))
                        {
                            if (objrequest.Purpose != "Email Verification")
                            {
                                objsms.SendSMSMessage(res.Mobile, messgae, "OTP");
                            }

                            if (objrequest.Purpose == "Email Verification")
                            {
                                messgae = messgae.Replace("%23", "#");
                                if (!string.IsNullOrEmpty(objrequest.Email))
                                {
                                    var result = objsms.SendEmail(objrequest.Email, "OTP", messgae, res.Name);
                                }
                                else
                                {
                                    var result = objsms.SendEmail(res.Email, "OTP", messgae, res.Name);
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
                    objresponse.result = res;
                    objresponse.response = "error";
                    objresponse.message = res.msg;
                }

            }
            catch (Exception ex)
            {
                objresponse.response = "error";
                objresponse.message = ex.Message;
            }


            return objresponse;
        }

        [HttpGet("AutoUpdate")]
        public async Task<CommonResponse<AppVersionResponse>> AutoUpdate(string version, string ostype)
        {
            CommonResponse<AppVersionResponse> objres = new CommonResponse<AppVersionResponse>();
            try
            {
                var res = await _dataRepository.GetAppVersionDetail(ostype,version);
                if (res != null)
                {
                    if (ostype == "android" && version == res.version)
                    {
                        objres.response = "success";
                        objres.message = "success";
                        objres.result = res;
                    }
                    else if (ostype == "android" && version != res.version)
                    {
                        objres.response = "error";
                        objres.message = res.message;
                        objres.result = res;
                    }
                    else if (ostype == "ios" && version == res.version)
                    {
                        objres.response = "success";
                        objres.message = res.message;
                        objres.result = res;
                    }
                    else if (ostype == "ios" && version != res.version)
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


            return objres;

        }

        [HttpGet("CheckUserData")]
        public async Task<Common> CheckUserData(string data, string type)
        {
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

            return objres;

        }

        [HttpGet("UpdatePendingRecharge")]
        public async Task<Common> UpdatePendingRecharge()
        {
            Common objres = new Common();
            try
            {
                var res = await _dataRepository.GetPendingRecharge();
                if (res != null && res.Count > 0)
                {

                    foreach (var v in res)
                    {

                        if (v.Type.ToLower() == "electricity")
                        {
                            VenusBillPayRequest model = new VenusBillPayRequest();
                            model.TransactionNo = v.PaymentId;
                            model.Amount = v.amount;
                            model.Type = v.Type;
                            model.OpertorCode = v.OperatorCode;
                            model.MemberId = v.memberId;
                            model.CustomerId = v.CustomerId;
                            model.MobileNo = v.Number;
                            model.OrderId = v.OrderId;
                            var result = await BillPay(model);
                        }
                        else
                        {
                            RechargeDeskRequest model = new RechargeDeskRequest();
                            model.TransactionId = v.PaymentId;
                            model.amount = v.amount;
                            model.Type = v.Type;
                            model.OperatorCode = v.OperatorCode;
                            model.MemberId = v.memberId;
                            model.number = v.Number;
                            model.merchantInfoTxn = DateTime.UtcNow.Ticks.ToString().Substring(0, 14);
                            var result = await Recharge(model);
                        }
                    }
                    objres.response = "success";
                    objres.message = "success";
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }


            return objres;

        }

        [HttpPost("Recharge")]
        public async Task<CommonResponse<Common>> Recharge(RechargeDeskRequest model)
        {
            CommonResponse<Common> objres = new CommonResponse<Common>();
            VenusRechargeResponse response = new VenusRechargeResponse();
            string url = VenusRechargeURL;
            try
            {


                var check = await _dataRepository.CheckAmountByTransactionId(model.TransactionId);
                if (check.message != null && decimal.Parse(check.message) >= model.amount)
                {

                    // model.amount = Math.Round(model.amount * 1.01m);
                    model.ProcId = 1;
                    var res = await _dataRepository.OperatorRecharge_V2(model);
                    if (res != null && res.Id > 0)
                    {
                        model.merchantInfoTxn = res.TransactionId;
                        string serviceType = string.Empty;
                        if (model.Type.ToLower() == "mobile")
                        {
                            serviceType = "MR";
                        }
                        else if (model.Type.ToLower() == "dth")
                        {
                            serviceType = "DH";
                        }

                        url = url.Replace("[number]", model.number).Replace("[recharge_amount]", model.amount.ToString()).Replace("[operator_code]", model.OperatorCode).Replace("[transaction_id]", model.merchantInfoTxn).Replace("[ServiceType]", serviceType);
                        var result = CommonJsonPostRequest.CommonSendRequest(url, "GET", null);
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(result);

                        string json = JsonConvert.SerializeXmlNode(doc);
                        response = JsonConvert.DeserializeObject<VenusRechargeResponse>(json);
                        if (response != null && response.Response != null)
                        {
                            model.ProcId = 2;
                            model.Id = res.Id;

                            model.status = response.Response.ResponseStatus;
                            model.description = response.Response.Description;
                            if (response.Response.ResponseStatus.ToLower() == "success")
                            {
                                model.optr_txn_id = response.Response.OperatorTxnID;
                            }
                            model.response = result;

                            var res1 = _dataRepository.OperatorRecharge_V2(model);
                            if (res1 != null && res1.Id > 0)
                            {
                                objres.message = "RECHARGE " + response.Response.ResponseStatus.ToLower();
                                objres.response = response.Response.ResponseStatus.ToLower();
                            }
                            else
                            {
                                objres.message = "Error";
                                objres.response = "error";

                            }

                        }
                        else
                        {
                            objres.message = "Error";
                            objres.response = "error";

                        }
                    }
                    else
                    {
                        objres.message = res.msg;
                        objres.response = "error";

                    }

                }
                else
                {
                    objres.message = "Payment is pending";
                    objres.response = "error";

                }
            }
            catch (Exception ex)
            {
                objres.message = ex.Message;
                objres.response = "error";

            }
            return objres;
        }


        [HttpGet("CommissionNotification")]
        public async Task<Common> CommissionNotification()
        {
            Common objres = new Common();
            try
            {
                var res = await _dataRepository.GetGeneratedCommission(0);
                if (res != null && res.Count > 0)
                {

                    foreach (var v in res)
                    {
                        if (!string.IsNullOrEmpty(v.DeviceId))
                        {
                            var messageuser = Notification.SendNotification(v.memberId, v.DeviceId, v.Remark, v.Title);
                            var xmlnotificationuser = "<notification><notifications><type>" + v.Title + "</type><Message>" + v.Remark + "</Message><CustomerId>" + v.memberId + "</CustomerId><DeviceId>" + v.DeviceId + "</DeviceId></notifications></notification>";
                            var notificationresultuser = await _dataRepository.Insetnotification(xmlnotificationuser);
                            var r = await _dataRepository.GetGeneratedCommission(v.Id);
                        }
                        else
                        {
                            var r = await _dataRepository.GetGeneratedCommission(v.Id);
                        }

                    }
                    objres.response = "success";
                    objres.message = "success";
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }


            return objres;

        }

        [HttpPost("RegistrationV3")]
        public async Task<CommonResponse<LoginResponse>> RegistrationV3(RegistrationV3 objregister)
        {

            CommonResponse<LoginResponse> objres = new CommonResponse<LoginResponse>();
            try
            {

                if (objregister.ProcId == 1)
                {
                    if (objregister.Otp != "")
                    {
                        OTPRequest otp = new OTPRequest();
                        otp.LoginId = objregister.MobileNo;
                        otp.ProcId = 1;
                        otp.Purpose = "Mobile Verification";
                        otp.OTPNO = objregister.Otp;
                        var otpresponse = await _dataRepository.OTPProcess(otp);
                        if (otpresponse.Flag == 1)
                        {
                            MD5 md5Hash = MD5.Create();
                            Common common=new Common();
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
                        OTPRequest otp = new OTPRequest();
                        otp.LoginId = objregister.Email;
                        otp.ProcId = 1;
                        otp.Purpose = "Email Verification";
                        otp.OTPNO = objregister.Otp;
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
                        NotificationMessageUser = "Dear " + res.LoginResponse.FirstName + ",\nWarm Welcome to MobilePe Parivar!\nYour unbeatable journey starts now for finanical freedom.\nwe hope to have a lasting relationship together.\nwe will succeed.\n\nThanking You\nMobilePe Admin";
                        DeviceIdUser = res.LoginResponse.UserDeviceId;
                        NotificationMessageSponsor = "Dear " + res.LoginResponse.SponsorName + ",\nwe are glad to inform that " + res.LoginResponse.FirstName + " has joined your immediate CUG.\nKindly explain the process of MobilePe.\n\nThanking You\nMobilePe Admin";
                        DeviceIdSponsor = res.LoginResponse.SponsorDeviceId;
                        if (!string.IsNullOrEmpty(res.LoginResponse.UserDeviceId))
                        {
                            var messageuser = Notification.SendNotification(res.LoginResponse.MemberId, DeviceIdUser, NotificationMessageUser, NotificationTitle);
                            var xmlnotificationuser = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageUser + "</Message><CustomerId>" + res.LoginResponse.MemberId + "</CustomerId><DeviceId>" + DeviceIdUser + "</DeviceId></notifications></notification>";
                            var notificationresultuser = await _dataRepository.Insetnotification(xmlnotificationuser);
                        }
                        if (!string.IsNullOrEmpty(res.LoginResponse.SponsorDeviceId))
                        {
                            var messagesponsor = Notification.SendNotification(res.LoginResponse.SponsorId, DeviceIdSponsor, NotificationMessageSponsor, NotificationTitle);
                            var xmlnotificationsponsor = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageSponsor + "</Message><CustomerId>" + res.LoginResponse.SponsorId + "</CustomerId><DeviceId>" + DeviceIdSponsor + "</DeviceId></notifications></notification>";
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
                objres.message = ex.Message;
                objres.response = "error";
            }
            return objres;
        }
        [HttpPost]
        public async Task<CommonResponse<Common>> BillPay(VenusBillPayRequest model)
        {

            CommonResponse<Common> objres = new CommonResponse<Common>();
            VenusBillPayResponse response = new VenusBillPayResponse();
            try
            {
                var check = await _dataRepository.CheckAmountByTransactionId(model.TransactionNo);
                if (check.message != null && decimal.Parse(check.message) >= model.Amount)
                {
                    Random rand = new Random();
                    long randnum2 = (long)(rand.NextDouble() * 90000000000000) + 10000000000000;

                    model.ProcId = 1;
                    model.merchantInfoTxn = DateTime.UtcNow.Ticks.ToString().Substring(0, 14);


                    var res = await _dataRepository.BillRecharge(model);
                    if (res != null && res.Id > 0)
                    {
                        //model.merchantInfoTxn = res.TransactionId;
                        //VENUSPAYBILLURL = VENUSPAYBILLURL.Replace("[OPCODE]", model.OpertorCode).Replace("[TRANSNO]", model.merchantInfoTxn.ToString()).Replace("[CUSTID]", model.CustomerId).Replace("[MobileNo]", model.MobileNo).Replace("[Amount]", model.Amount.ToString()).Replace("[OrderId]", model.OrderId);
                        //var result = recConn.sendRequest(VENUSPAYBILLURL, "GET", "", 0);
                        //XmlDocument doc = new XmlDocument();
                        //doc.LoadXml(result);
                        //model.response = result;
                        //string json = JsonConvert.SerializeXmlNode(doc);
                        //response = JsonConvert.DeserializeObject<VenusBillPayResponse>(json);
                        //if (response != null && response.response != null)
                        //{
                        //    model.ProcId = 2;
                        //    model.Id = res.Id;

                        //    model.status = response.response.ResponseStatus;
                        //    model.description = response.response.Description;
                        //    if (response.response.ResponseStatus == "SUCCESS")
                        //    {
                        //        model.optr_txn_id = response.response.OperatorTxnId;
                        //    }

                        //    var res1 = await _dataRepository.BillRecharge(model);
                        //    if (res1 != null && res1.Id > 0)
                        //    {
                        //        objres.message = "Bill " + response.response.ResponseStatus;
                        //        objres.response = response.response.ResponseStatus;
                        //    }
                        //objres.message = "your bill is success";
                        //objres.response = "success";
                        // }
                        //else
                        //{
                        //    objres.message = "Error";
                        //    objres.response = "error";
                        //}
                        objres.message = "Your Bill payment is under process";
                        objres.response = "Success";
                    }
                    else
                    {
                        objres.message = res.message;
                        objres.response = "error";
                    }
                }
                else
                {
                    objres.message = "error";
                    objres.response = "error";
                }
            }
            catch (Exception ex)
            {
                objres.message = ex.Message;
                objres.response = "error";
            }
            return objres;
        }
        [HttpPost("WebViewLogin")]
        public async Task<CommonResponse<LoginResponse>> WebViewLogin(WebViewLoginRequest model)
        {

            CommonResponse<LoginResponse> objres = new CommonResponse<LoginResponse>();

            try
            {

                //var s = objJWT.GetPrincipal(model.token);
                //var res = await _dataRepository.WebViewLogin(s.Identity.Name);

                var res = await _dataRepository.WebViewLogin(model.token);
                if (res != null && res.flag == 1)
                {
                    if (res.BankStatus == "2")
                    {
                        var bank = await _dataRepository.bankdetails(res.MemberId);
                        res.bankdetails = bank;
                    }
                    //  objres.result = new LoginResponse();

                    if (!res.isActive)
                    {
                        string NotificationTitle, NotificationMessageUser, DeviceIdUser, DeviceIdSponsor, NotificationMessageSponsor;
                        NotificationTitle = "Welcome";
                        NotificationMessageUser = "Dear " + res.FirstName + ",\nWarm Welcome to MobilePe Parivar!\nYour unbeatable journey starts now for finanical freedom.\nwe hope to have a lasting relationship together.\nwe will succeed.\n\nThanking You\nMobilePe Admin";
                        DeviceIdUser = res.UserDeviceId;

                        NotificationMessageSponsor = "Dear " + res.SponsorName + ",\nwe are glad to inform that " + res.FirstName + " has joined your immediate CUG.\nKindly explain the process of MobilePe.\n\nThanking You\nMobilePe Admin";
                        DeviceIdSponsor = res.SponsorDeviceId;

                        if (!string.IsNullOrEmpty(res.UserDeviceId))
                        {
                            var messageuser = Notification.SendNotification(res.MemberId, DeviceIdUser, NotificationMessageUser, NotificationTitle);
                            var xmlnotificationuser = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageUser + "</Message><CustomerId>" + res.MemberId + "</CustomerId><DeviceId>" + DeviceIdUser + "</DeviceId></notifications></notification>";
                            var notificationresultuser = await _dataRepository.Insetnotification(xmlnotificationuser);
                        }
                        if (!string.IsNullOrEmpty(res.SponsorDeviceId))
                        {
                            var messagesponsor = Notification.SendNotification(res.SponsorId, DeviceIdSponsor, NotificationMessageSponsor, NotificationTitle);

                            var xmlnotificationsponsor = "<notification><notifications><type>" + NotificationTitle + "</type><Message>" + NotificationMessageSponsor + "</Message><CustomerId>" + res.SponsorId + "</CustomerId><DeviceId>" + DeviceIdSponsor + "</DeviceId></notifications></notification>";

                            var notificationresultsponsor = await _dataRepository.Insetnotification(xmlnotificationsponsor);
                        }
                    }
                    objres.message = "Login successfully";
                    objres.response = "success";
                    objres.result = res;
                    //objres.result.token = objJWT.GenerateToken(res.MobileNo);
                    objres.result.token = res.MobileNo;

                }
                else
                {
                    objres.message = res.Msg;
                    objres.response = "error";
                }


            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }


            return objres;
        }

        //public ActionResult Instantrefund(decimal Amount)
        //{
        //    Razorpay.Api.Refund objorder = null;
        //    string OrderId = "", Status = "";
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    RazorpayClient client = null;
        //    //client = new RazorpayClient("rzp_test_AFSSERe0Blac2S", "X6UBRaEzdUHdJFfXPQDmdLKb");
        //    client = new RazorpayClient("rzp_live_Z9C5IhxkanJaOi", "hZlF0j2K4AkJKgKEzsEcomXS");
        //    Dictionary<string, object> options = new Dictionary<string, object>();
        //    options.Add("amount", Amount * 100);
        //    options.Add("receipt", "");

        //    objorder = client.Refund.Create(options);

        //    return null;
        //}
        [HttpPost("ViewProfileEncrypt")]
        public async Task<ResponseModel> ViewProfile(RequestModel requestModel)
        {
            string EncrytedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<LoginResponse> objres = new CommonResponse<LoginResponse>();

            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    objres.result.flag = 0;
                    
                }
                else
                {
                    string tokenValue = Request.Headers["Token"].ToString();
                    string[] split = tokenValue.Split("-");
                    Aeskey = split[1];
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    ViewProfile viewProfile = JsonConvert.DeserializeObject<ViewProfile>(dcdata);
                    var res =await _dataRepository.ViewProfile(viewProfile);
                    objres.response = "success";
                    objres.result = res;
                }

            }
            catch (Exception ex)
            {
                objres.result.flag=0;
                objres.result.Msg = ex.Message;
                objres.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<LoginResponse>));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncrytedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncrytedText;
            return returnResponse;
        }
        [HttpPost("ViewProfile")]
        public async Task<CommonResponse<LoginResponse>> ViewProfile(ViewProfile objlogin)
        {

            CommonResponse<LoginResponse> objres = new CommonResponse<LoginResponse>();

            try
            {
                var res = await _dataRepository.ViewProfile(objlogin);
                if (res != null && res.flag == 1)
                {                   
                    objres.response = "success";
                    objres.result = res;                   
                }
                else
                {
                    objres.message = res.Msg;
                    objres.response = "error";
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }
            return objres;
        }
    }
}
