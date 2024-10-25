using Microsoft.AspNetCore.Mvc;
using MobileAPI_V2.Model;
using System.Threading.Tasks;
using System;
using MobileAPI_V2.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;
using System.IO;
using System.Runtime.Serialization.Json;
using MobileAPI_V2.Model.BillPayment;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MobileAPI_V2.Model.Ecommerce;
using MobileAPI_V2.Filter;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace MobileAPI_V2.Controllers
{
    //[ServiceFilter(typeof(CheckUser))]
    //[ApiVersion("1.1")]
    [Route("api/[controller]")]
    [ApiController]
    public class EcommerceController : ControllerBase
    {
        //private readonly IConfiguration _configuration;
        private readonly IDataRepositoryEcomm _dataRepository;
        string AESKEY = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
        private readonly IConfiguration _configuration;
        SendSMSEcomm objsms;
        PineCommonRequest PineCommonRequestobj;
        string PineCardbaseUrl = string.Empty;
        string PINECARDBALANCEURL = string.Empty;
        string PINECARDTOKENURL = string.Empty;
        public string INSURANCEURLCOMPAYCODE = string.Empty;
        public string INSURANCEURLENCRYPTIONKEY = string.Empty;
        public string INSURANCEURL = string.Empty;
        public string INSURANCEURLNEW = string.Empty;
        string PINECARDTransactionDetailURL = string.Empty;
        string IRCTCUserName = string.Empty;
        string IRCTCPassword = string.Empty;
        string BoardingPointChangeURL = string.Empty;
        string ClusterStationSearchEnquiryURL = string.Empty;
        string CountryListURL = string.Empty;
        string GetSMSeMailOTPURL = string.Empty;
        string HistorySearchByTransactionIDURL = string.Empty;
        JWTToken objJWT;
        IRCTCService iRCTCService;
        string IRCTCForgotDetailsURL = string.Empty;
        string IRCTCPINDetailsURL = string.Empty;
        string NewsAndAlertServiceURL = string.Empty;
        string OptVikalpURL = string.Empty;
        string PNREnquiryURL = string.Empty;
        string StationCodeURL = string.Empty;
        string TicketBookingDetailURL = string.Empty;
        string TicketRefundDetailURL = string.Empty;
        string TrainbetweenStationsURL = string.Empty;
        string TrainBoardingStationEnquiryURL = string.Empty;
        string TrainScheduleEnquiryURL = string.Empty;
        string UserStatusURL = string.Empty;
        string VikalpTrainListURL = string.Empty;
        string VerifySMSeMailOTPURL = string.Empty;
        public EcommerceController(IHostingEnvironment env, IDataRepositoryEcomm dataRepository, IConfiguration configuration)
        {

            _dataRepository = dataRepository;
            _configuration = configuration;
            PineCommonRequestobj = new PineCommonRequest(_configuration);
            PineCardbaseUrl = _configuration["PineCardbaseUrl"];
            PINECARDBALANCEURL = _configuration["PINECARDBALANCEURL"];
            PINECARDTOKENURL = _configuration["PINECARDTOKENURL"];
            INSURANCEURLCOMPAYCODE = _configuration["INSURANCEURLCOMPAYCODE"];
            INSURANCEURL = _configuration["INSURANCEURL"];
            INSURANCEURLNEW = _configuration["INSURANCEURLNEW"];
            INSURANCEURLENCRYPTIONKEY = _configuration["INSURANCEURLENCRYPTIONKEY"];
            PINECARDTransactionDetailURL = _configuration["PINECARDTransactionDetailURL"];
            objsms = new SendSMSEcomm(_configuration, _dataRepository);
            iRCTCService = new IRCTCService(_configuration);
            IRCTCUserName = _configuration["IRCTCUserName"];
            IRCTCPassword = _configuration["IRCTCPassword"];
            BoardingPointChangeURL = _configuration["BoardingPointChangeURL"];
            ClusterStationSearchEnquiryURL = _configuration["ClusterStationSearchEnquiryURL"];
            CountryListURL = _configuration["CountryListURL"];
            GetSMSeMailOTPURL = _configuration["GetSMSeMailOTPURL"];
            HistorySearchByTransactionIDURL = _configuration["HistorySearchByTransactionIDURL"];
            IRCTCForgotDetailsURL = _configuration["IRCTCForgotDetailsURL"];
            IRCTCPINDetailsURL = _configuration["IRCTCPINDetailsURL"];
            NewsAndAlertServiceURL = _configuration["NewsAndAlertServiceURL"];
            OptVikalpURL = _configuration["OptVikalpURL"];
            PNREnquiryURL = _configuration["PNREnquiryURL"];
            StationCodeURL = _configuration["StationCodeURL"];
            TicketBookingDetailURL = _configuration["TicketBookingDetailURL"];
            TicketRefundDetailURL = _configuration["TicketRefundDetailURL"];
            TrainbetweenStationsURL = _configuration["TrainbetweenStationsURL"];
            TrainBoardingStationEnquiryURL = _configuration["TrainBoardingStationEnquiryURL"];
            TrainScheduleEnquiryURL = _configuration["TrainScheduleEnquiryURL"];
            UserStatusURL = _configuration["UserStatusURL"];
            VikalpTrainListURL = _configuration["VikalpTrainListURL"];
            VerifySMSeMailOTPURL = _configuration["VerifySMSeMailOTPURL"];
        }

        [HttpPost("AutoLogin")]
        public async Task<ResponseModel> AutoLogin(RequestModel requestModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            CommonResponseEcomm<LoginResponseEcomm> objres = new CommonResponseEcomm<LoginResponseEcomm>();
            try
            {
                LoginEcomm objlogin = new LoginEcomm();
                EncryptedText = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                objlogin = JsonConvert.DeserializeObject<LoginEcomm>(EncryptedText);


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
                var res = await _dataRepository.AutoLogin(objlogin);
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

        [HttpGet("EcommerceDashboardV1")]
        [Produces("application/json")]
        public ResponseModel EcommerceDashboardV1(string memberId, string IsLocal, string appVersion, string appType)
        {

            string EncryptedText = "";
            CommonResponseEcomm<DashboardV5ResponseNew> res = new CommonResponseEcomm<DashboardV5ResponseNew>();
            ResponseModel returnResponse = new ResponseModel();
            DashboardV5ResponseNew dashboardV5ResponseNew = new DashboardV5ResponseNew();
            try
            {
                if (string.IsNullOrEmpty(appVersion))
                {
                    res.message = "Please pass appversion";
                    res.Status = 0;
                }
                else if (string.IsNullOrEmpty(appType))
                {
                    res.message = "Please pass appType";
                    res.Status = 0;
                }
                else if (string.IsNullOrEmpty(memberId))
                {
                    res.message = "Please pass memberId";
                    res.Status = 0;
                }
                else
                {
                    memberId = memberId.Replace(" ", "+");
                    memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);
                    IsLocal = IsLocal.Replace(" ", "+");
                    IsLocal = ApiEncrypt_Decrypt.DecryptString(AESKEY, IsLocal);
                    appVersion = appVersion.Replace(" ", "+");
                    appVersion = ApiEncrypt_Decrypt.DecryptString(AESKEY, appVersion);
                    appType = appType.Replace(" ", "+");
                    appType = ApiEncrypt_Decrypt.DecryptString(AESKEY, appType);

                    ENewDashboard dashboard = new ENewDashboard();
                    dashboard.appVersion = appVersion;
                    dashboard.appType = appType;
                    dashboard.memberId = long.Parse(memberId);
                    DataSet dataSet = dashboard.GetDashBoardData();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["Status"].ToString() == "1")
                            {
                                dashboardV5ResponseNew.IsPineCard = int.Parse(dataSet.Tables[0].Rows[0]["IsPineCard"].ToString());
                                dashboardV5ResponseNew.IsTopayCard = int.Parse(dataSet.Tables[0].Rows[0]["IsTopayCard"].ToString());

                                res.Status = 1;
                                DashboardCardResponseV5Ecomm dashboardCardResponseV5Ecomm = new DashboardCardResponseV5Ecomm();
                                List<CriteriaData> lstCriteria = new List<CriteriaData>();
                                List<Banner1> lstBanner1 = new List<Banner1>();
                                List<Banner1> lstBanner2 = new List<Banner1>();
                                List<ReferEranDash> lstReferEranDash = new List<ReferEranDash>();
                                dashboardCardResponseV5Ecomm.ScrolleText = dataSet.Tables[0].Rows[0]["ScrolleText"].ToString();
                                dashboardCardResponseV5Ecomm.ToPayToken = dataSet.Tables[0].Rows[0]["ToPayToken"].ToString();
                                dashboardCardResponseV5Ecomm.EntityId = dataSet.Tables[0].Rows[0]["EntityId"].ToString();
                                dashboardCardResponseV5Ecomm.JwtToken = dataSet.Tables[0].Rows[0]["JwtToken"].ToString();
                                dashboardCardResponseV5Ecomm.ThriweCoupon = int.Parse(dataSet.Tables[0].Rows[0]["ThriweCoupon"].ToString());
                                dashboardCardResponseV5Ecomm.IsChangePassword = bool.Parse(dataSet.Tables[0].Rows[0]["IsChangePassword"].ToString());
                                dashboardCardResponseV5Ecomm.IsLogout = bool.Parse(dataSet.Tables[0].Rows[0]["IsLogout"].ToString());
                                dashboardCardResponseV5Ecomm.IsCardApplied = bool.Parse(dataSet.Tables[0].Rows[0]["IsCardApplied"].ToString());
                                dashboardCardResponseV5Ecomm.IsCardUpgrade = bool.Parse(dataSet.Tables[0].Rows[0]["IsCardUpgrade"].ToString());
                                dashboardCardResponseV5Ecomm.IsWalletActive = bool.Parse(dataSet.Tables[0].Rows[0]["IsWalletActive"].ToString());
                                dashboardCardResponseV5Ecomm.IsPaymentGatewayActive = bool.Parse(dataSet.Tables[0].Rows[0]["IsPaymentGatewayActive"].ToString());
                                dashboardCardResponseV5Ecomm.cardAppliedFee = decimal.Parse(dataSet.Tables[0].Rows[0]["cardAppliedFee"].ToString());
                                dashboardCardResponseV5Ecomm.cardUpgradeFee = decimal.Parse(dataSet.Tables[0].Rows[0]["cardUpgradeFee"].ToString());
                                dashboardV5ResponseNew.card = dashboardCardResponseV5Ecomm;

                                for (int k = 0; k <= dataSet.Tables[1].Rows.Count - 1; k++)
                                {
                                    CriteriaData criteriaData = new CriteriaData();
                                    criteriaData.text = dataSet.Tables[1].Rows[k]["Text"].ToString();
                                    criteriaData.value = dataSet.Tables[1].Rows[k]["value"].ToString();
                                    criteriaData.Backcolor = dataSet.Tables[1].Rows[k]["Backcolor"].ToString();
                                    lstCriteria.Add(criteriaData);
                                }
                                dashboardV5ResponseNew.reward = lstCriteria;


                                for (int k = 0; k <= dataSet.Tables[2].Rows.Count - 1; k++)
                                {
                                    Banner1 banner1 = new Banner1();
                                    banner1.text = dataSet.Tables[2].Rows[k]["text"].ToString();
                                    banner1.link = dataSet.Tables[2].Rows[k]["link"].ToString();
                                    banner1.url = dataSet.Tables[2].Rows[k]["url"].ToString();
                                    lstBanner1.Add(banner1);
                                }
                                dashboardV5ResponseNew.banner1 = lstBanner1;

                                for (int k = 0; k <= dataSet.Tables[3].Rows.Count - 1; k++)
                                {
                                    Banner1 banner1 = new Banner1();
                                    banner1.text = dataSet.Tables[3].Rows[k]["text"].ToString();
                                    banner1.link = dataSet.Tables[3].Rows[k]["link"].ToString();
                                    banner1.url = dataSet.Tables[3].Rows[k]["url"].ToString();
                                    lstBanner2.Add(banner1);
                                }
                                dashboardV5ResponseNew.banner2 = lstBanner2;
                                for (int k = 0; k <= dataSet.Tables[7].Rows.Count - 1; k++)
                                {
                                    ReferEranDash referEranDash = new ReferEranDash();
                                    referEranDash.SrNo = dataSet.Tables[7].Rows[k]["SRNO"].ToString();
                                    referEranDash.Text = dataSet.Tables[7].Rows[k]["TEXT"].ToString();

                                    lstReferEranDash.Add(referEranDash);
                                }
                                dashboardV5ResponseNew.refertext = lstReferEranDash;




                                List<TopheaderNew> lst1 = new List<TopheaderNew>();
                                for (int i = 0; i <= dataSet.Tables[4].Rows.Count - 1; i++)
                                {
                                    TopheaderNew topheaderEcomm = new TopheaderNew();
                                    topheaderEcomm.header = dataSet.Tables[4].Rows[i]["ServiceName"].ToString();
                                    topheaderEcomm.ImgUrl = dataSet.Tables[4].Rows[i]["ImgUrl"].ToString();
                                    topheaderEcomm.Description = dataSet.Tables[4].Rows[i]["Description"].ToString();
                                    topheaderEcomm.Notes = dataSet.Tables[4].Rows[i]["Notes"].ToString();
                                    topheaderEcomm.IsNew = int.Parse(dataSet.Tables[4].Rows[i]["IsNew"].ToString());
                                    List<SubServices> lst = new List<SubServices>();
                                    for (int j = 0; j <= dataSet.Tables[5].Rows.Count - 1; j++)
                                    {

                                        if (dataSet.Tables[4].Rows[i]["ServiceId"].ToString() == dataSet.Tables[5].Rows[j]["ServiceId"].ToString())
                                        {
                                            SubServices subServices = new SubServices();
                                            subServices.link = dataSet.Tables[5].Rows[j]["link"].ToString();
                                            subServices.ImgUrl = dataSet.Tables[5].Rows[j]["ImgUrl"].ToString();
                                            subServices.IsActive = bool.Parse(dataSet.Tables[5].Rows[j]["IsActive"].ToString());
                                            subServices.link = dataSet.Tables[5].Rows[j]["link"].ToString();
                                            subServices.applink = dataSet.Tables[5].Rows[j]["applink"].ToString();
                                            subServices.IsNew = bool.Parse(dataSet.Tables[5].Rows[j]["IsNew"].ToString());
                                            subServices.value = dataSet.Tables[5].Rows[j]["value"].ToString();
                                            subServices.DisplayName = dataSet.Tables[5].Rows[j]["DisplayName"].ToString();
                                            subServices.billerCategoryID = int.Parse(dataSet.Tables[5].Rows[j]["billerCategoryID"].ToString());
                                            lst.Add(subServices);
                                        }


                                    }
                                    topheaderEcomm.data = lst;
                                    lst1.Add(topheaderEcomm);
                                    dashboardV5ResponseNew.maindata = lst1;
                                }


                                List<VocherNew> lst2 = new List<VocherNew>();
                                VocherNew vocherNew = new VocherNew();
                                vocherNew.header = "Brand Vouchers";

                                List<VoucherData> lst11 = new List<VoucherData>();
                                for (int j = 0; j <= dataSet.Tables[6].Rows.Count - 1; j++)
                                {

                                    VoucherData voucherData = new VoucherData();
                                    voucherData.Url = dataSet.Tables[6].Rows[j]["Url"].ToString();
                                    voucherData.Text = dataSet.Tables[6].Rows[j]["Text"].ToString();
                                    voucherData.Value = dataSet.Tables[6].Rows[j]["Value"].ToString();
                                    lst11.Add(voucherData);

                                    vocherNew.data = lst11;

                                }

                                lst2.Add(vocherNew);
                                dashboardV5ResponseNew.voucher = lst2;
                                res.result = dashboardV5ResponseNew;


                            }
                            else
                            {
                                res.message = dataSet.Tables[0].Rows[0]["message"].ToString();
                                res.Status = int.Parse(dataSet.Tables[0].Rows[0]["Status"].ToString());
                            }
                        }
                    }
                    //var result = await _dataRepository.GetDashBoardData(long.Parse(memberId), appVersion, appType);
                    //if (result != null)
                    //{
                    //    res.result = result;



                    //    res.response = "success";

                    //}
                }
            }
            catch (Exception ex)
            {
                res.Status = 0;
                res.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<DashboardV5ResponseNew>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }


        [HttpGet("GetProfile")]
        [Produces("application/json")]
        public async Task<ResponseModel> GetProfile(string memberId)
        {
            string EncryptedText = "";
            CommonResponseEcomm<LoginResponseEcomm> objUserProfile = new CommonResponseEcomm<LoginResponseEcomm>();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);
                var res = await _dataRepository.GetUserProfile(long.Parse(memberId));
                if (res != null)
                {
                    if (res.bankStatus == "2")
                    {
                        var bank = await _dataRepository.bankdetails(long.Parse(memberId));
                        if (!string.IsNullOrEmpty(bank.account_number))
                        {
                            res.bankDetails = bank;
                        }
                        else
                        {
                            res.bankDetails = new Bankdetails_accountResponseEcomm();
                        }
                    }
                    else
                    {
                    }
                    objUserProfile.result = res;
                    objUserProfile.response = "success";
                    objUserProfile.message = "success";
                    objUserProfile.Status = 1;
                }
                else
                {
                    objUserProfile.response = "error";
                    objUserProfile.message = "error";
                    objUserProfile.Status = 0;
                }

            }
            catch (Exception ex)
            {
                objUserProfile.response = "error";
                objUserProfile.message = ex.Message;
                objUserProfile.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<LoginResponseEcomm>));
            ms = new MemoryStream();
            js.WriteObject(ms, objUserProfile);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;


        }

        [HttpPost("ChangePassword")]
        [Produces("application/json")]
        public async Task<ResponseModel> ChangePassword(RequestModel requestModel)
        {
            string EncryptedText = "";
            CommonEcomm obj = new CommonEcomm();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ChangePasswordEcomm objrequest = new ChangePasswordEcomm();
                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                objrequest = JsonConvert.DeserializeObject<ChangePasswordEcomm>(dcdata);
                objrequest.DeviceId = objrequest.deviceInfo.deviceId;
                objrequest.DeviceType = objrequest.deviceInfo.deviceType;
                MD5 md5Hash = MD5.Create();
                string OldPassword = Md5Encyption.GetMd5Hash(md5Hash, objrequest.OldPassword);
                string NewPassword = Md5Encyption.GetMd5Hash(md5Hash, objrequest.NewPassword);
                objrequest.OldPassword = OldPassword;
                objrequest.NewPassword = NewPassword;
                var res = await _dataRepository.ChangePassword(objrequest);
                if (res != null && res.flag == 1)
                {
                    obj.response = "success";
                    obj.message = res.msg;

                }
                else if (res != null && res.Id == 2)
                {
                    obj.response = "error";
                    obj.message = res.msg;
                    obj.Id = res.Id;
                }
                else
                {
                    obj.response = "error";
                    obj.message = res.msg;
                    obj.Id = 0;
                }



            }

            catch (Exception ex)
            {
                obj.response = "error";
                obj.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonEcomm));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("SignOut")]
        [Produces("application/json")]
        public async Task<ResponseModel> SignOut(RequestModel requestModel)
        {
            string EncryptedText = "";
            CommonEcomm objresponse = new CommonEcomm();
            //var Token = Request.Headers["Tenant"].ToString();
            //model.Token = Token;
            SignOutRequestEcomm model = new SignOutRequestEcomm();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                model = JsonConvert.DeserializeObject<SignOutRequestEcomm>(dcdata);

                var res = await _dataRepository.SignOut(model);
                if (res != null && res.Id == 1)
                {
                    objresponse.response = "Success";
                    objresponse.message = res.msg;
                    objresponse.Id = res.Id;
                }
                else if (res != null && res.Id == 2)
                {
                    objresponse.response = "error";
                    objresponse.message = res.msg;
                    objresponse.Id = res.Id;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = res.msg;
                    objresponse.Id = 0;
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
        [HttpPost("EditProfile")]
        [Produces("application/json")]
        public async Task<ResponseModel> EditProfile(RequestModel requestModel)
        {
            CommonEcomm objres = new CommonEcomm();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            ProfileRequestEcomm model = new ProfileRequestEcomm();
            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                model = JsonConvert.DeserializeObject<ProfileRequestEcomm>(dcdata);
                model.DeviceId = model.deviceInfo.deviceId;
                model.DeviceType = model.deviceInfo.deviceType;
                var res = await _dataRepository.EditProfile(model);
                if (res != null && res.Id == 1)
                {
                    objres.response = "Success";
                    objres.message = res.msg;
                    objres.Id = res.Id;
                }
                else if (res != null && res.Id == 2)
                {
                    objres.response = "error";
                    objres.message = res.msg;
                    objres.Id = res.Id;
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.msg;
                    objres.Id = 0;
                }
            }
            catch (Exception ex)
            {
                objres.message = ex.Message;
                objres.response = "error";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonEcomm));
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
        [HttpPost("CalCulateCommission")]
        [Produces("application/json")]
        public async Task<ResponseModel> CalCulateCommission(RequestModel requestModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            CommonResponseEcomm<CommissionCalculationEcomm> objres = new CommonResponseEcomm<CommissionCalculationEcomm>();
            CommissionCalculationRequestEcomm model = new CommissionCalculationRequestEcomm();
            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                model = JsonConvert.DeserializeObject<CommissionCalculationRequestEcomm>(dcdata);
                if (model != null)
                {
                    objres.result = new CommissionCalculationEcomm();
                    if (model.CommType.ToLower() == "card")
                    {
                        objres.result.amount = (model.Number * model.Amount * 0.0091M * 0.75M * 0.08M) / 12;
                    }
                    else
                    {
                        objres.result.amount = (model.Number * model.Amount * 0.04M * 0.75M * 0.08M) / 12;
                    }
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {
                    objres.response = "error";
                    objres.message = "All Parameters are required";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<CommissionCalculationEcomm>));
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
        [HttpGet("CUGSize")]
        [Produces("application/json")]
        public async Task<ResponseModel> CUGSize(string MemberId)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();

            CommonResponseEcomm<BusinessLevelResultEcomm> objres = new CommonResponseEcomm<BusinessLevelResultEcomm>();
            try
            {

                if (!string.IsNullOrEmpty(MemberId))
                {
                    MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
                }
                var res = await _dataRepository.BusinessLevel(long.Parse(MemberId));
                if (objres != null)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "error";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<BusinessLevelResultEcomm>));
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

        [HttpGet("OperatorDetails")]
        [Produces("application/json")]
        public async Task<ResponseModel> OperatorDetails(string mobilenumber)
        {
            CommonResponseEcomm<operatordetailsEcomm> objres = new CommonResponseEcomm<operatordetailsEcomm>();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                mobilenumber = ApiEncrypt_Decrypt.DecryptString(AESKEY, mobilenumber);
                var res = await _dataRepository.OperatorDetails(mobilenumber);
                if (res != null && res.success == true)
                {
                    var url = await _dataRepository.GetOperatorsCode(2, "mobile recharge", res.data.Operator);
                    objres.result = res.data;
                    objres.result.imageUrl = url[0].ImageUrl;
                    objres.result.operatorCode = url[0].OperatorCode;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Please select operator manually";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<operatordetailsEcomm>));
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
        //[HttpGet("RechargePlan")]
        //[Produces("application/json")]
        //public async Task<ResponseModel> RechargePlan(string operator_id, string circle_id)
        //{
        //    CommonResponseEcomm<List<RechargePlanEcomm>> objres = new CommonResponseEcomm<List<RechargePlanEcomm>>();
        //    string EncryptedText = "";
        //    ResponseModel returnResponse = new ResponseModel();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(operator_id))
        //        {
        //            operator_id = ApiEncrypt_Decrypt.DecryptString(AESKEY, operator_id);
        //        }
        //        if (!string.IsNullOrEmpty(circle_id))
        //        {
        //            circle_id = ApiEncrypt_Decrypt.DecryptString(AESKEY, circle_id);
        //        }
        //        var res = await _dataRepository.RechargePlan(operator_id, circle_id);
        //        if (res.data.Count > 0)
        //        {
        //            objres.result = res.data;
        //            objres.response = "success";
        //            objres.message = "success";
        //        }
        //        else
        //        {

        //            objres.response = "error";
        //            objres.message = "Record Not Found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objres.response = "error";
        //        objres.message = ex.Message;
        //    }
        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    js = new DataContractJsonSerializer(typeof(CommonResponse<List<RechargePlan>>));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objres);
        //    ms.Position = 0;
        //    StreamReader sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
        //    returnResponse.Body = EncryptedText;
        //    return returnResponse;
        //}
        [HttpGet("RechargePlanV2")]
        [Produces("application/json")]
        public async Task<ResponseModel> RechargePlanV2(string operator_id, string circle_id)
        {
            CommonResponseEcomm<List<MobileRechargePlanEcomm>> objres = new CommonResponseEcomm<List<MobileRechargePlanEcomm>>();
            List<MobileRechargePlanEcomm> data = new List<MobileRechargePlanEcomm>();

            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();

            try
            {
                if (!string.IsNullOrEmpty(operator_id))
                {
                    operator_id = ApiEncrypt_Decrypt.DecryptString(AESKEY, operator_id);
                }
                if (!string.IsNullOrEmpty(circle_id))
                {
                    circle_id = ApiEncrypt_Decrypt.DecryptString(AESKEY, circle_id);
                }

                var res = await _dataRepository.RechargePlan(operator_id, circle_id);
                if (res.data.Count > 0)
                {
                    for (int i = 0; i < res.data.Count; i++)
                    {
                        var check = data.Where(m => m.recharge_type == res.data[i].recharge_type).ToList();
                        if (check.Count == 0)
                        {
                            MobileRechargePlanEcomm rechargetype = new MobileRechargePlanEcomm();
                            rechargetype.recharge_type = res.data[i].recharge_type;
                            data.Add(rechargetype);
                        }

                    }
                    for (int i = 0; i < data.Count; i++)
                    {

                        data[i].data = res.data.Where(x => x.recharge_type == data[i].recharge_type).ToList();



                    }
                    objres.result = data.Distinct().ToList();
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<List<MobileRechargePlanEcomm>>));
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
        [HttpGet("GETSupper30Expense")]
        [Produces("application/json")]
        public async Task<ResponseModel> GETSupper30Expense(string type)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            CommonResponseEcomm<List<ExpenseEcomm>> objres = new CommonResponseEcomm<List<ExpenseEcomm>>();
            try
            {
                if (!string.IsNullOrEmpty(type))
                {
                    type = ApiEncrypt_Decrypt.DecryptString(AESKEY, type);
                }
                var res = await _dataRepository.GETSupper30Expense(type);
                if (res != null && res.Count > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<List<ExpenseEcomm>>));
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
        [HttpGet("GetDirect")]
        [Produces("application/json")]
        public async Task<ResponseModel> GetDirect(string MemberId, string OldmemberId)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            CommonResponseEcomm<List<DirectEcomm>> objres = new CommonResponseEcomm<List<DirectEcomm>>();
            try
            {
                if (!string.IsNullOrEmpty(MemberId))
                {
                    MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
                }
                if (!string.IsNullOrEmpty(OldmemberId))
                {
                    OldmemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, OldmemberId);
                }
                var res = await _dataRepository.GetDirect(long.Parse(MemberId), long.Parse(OldmemberId));
                if (res != null && res.Count > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<List<DirectEcomm>>));
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

        [HttpGet("GetLevelWiseDetails")]
        [Produces("application/json")]
        public async Task<ResponseModel> GetLevelWiseDetails(string MemberId, string level, string type, string search, string page)
        {
            CommonResponseEcomm<List<DirectEcomm>> objres = new CommonResponseEcomm<List<DirectEcomm>>();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(MemberId))
                {
                    MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
                }
                if (!string.IsNullOrEmpty(level))
                {
                    level = ApiEncrypt_Decrypt.DecryptString(AESKEY, level);
                }
                else
                {
                    page = "1";
                }
                if (!string.IsNullOrEmpty(MemberId))
                {
                    type = ApiEncrypt_Decrypt.DecryptString(AESKEY, type);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    search = ApiEncrypt_Decrypt.DecryptString(AESKEY, search);
                }
                if (!string.IsNullOrEmpty(page))
                {
                    page = ApiEncrypt_Decrypt.DecryptString(AESKEY, page);
                }


                var res = await _dataRepository.GetLevelWiseDetail(long.Parse(MemberId), level, type, search, int.Parse(page));
                if (res != null && res.Count > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<List<DirectEcomm>>));
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

        [HttpGet("GetTeamStatus")]
        [Produces("application/json")]
        public async Task<ResponseModel> GetTeamStatus(string MemberId, string search)
        {
            CommonResponseEcomm<TeamStatusResponseV2Ecomm> objres = new CommonResponseEcomm<TeamStatusResponseV2Ecomm>();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();

            try
            {
                if (!string.IsNullOrEmpty(MemberId))
                {
                    MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    search = ApiEncrypt_Decrypt.DecryptString(AESKEY, search);
                }
                var res = await _dataRepository.GetTeamStatus(long.Parse(MemberId), search);
                if (res != null)
                {

                    objres.result = new TeamStatusResponseV2Ecomm();
                    objres.result.topHeader = res.header1;
                    objres.result.data = new CugItemRootEcomm();

                    objres.result.data.cugItems = new List<CugItemEcomm>();

                    for (int i = 0; i < res.header2.Count(); i++)
                    {
                        objres.result.data.cugItems.Add(new CugItemEcomm() { header = res.header2[i].text });
                        objres.result.data.cugItems[i].data = res.data.Where(m => m.levelId == res.header2[i].value).ToList();

                    }

                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<TeamStatusResponseV2Ecomm>));
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

        [HttpGet("GetRecentRecharge")]
        [Produces("application/json")]
        public async Task<ResponseModel> GetRecentRecharge(string MemberId, string type, string page)
        {
            CommonResponse<List<RecentRechargeEcomm>> objres = new CommonResponse<List<RecentRechargeEcomm>>();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                var settings = new JsonSerializerSettings();
                //settings.ContractResolver = new sLowercaseContractResolver();
                //var json = JsonConvert.SerializeObject(authority, Formatting.Indented, settings);
                if (!string.IsNullOrEmpty(MemberId))
                {
                    MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
                }
                if (!string.IsNullOrEmpty(type))
                {
                    type = ApiEncrypt_Decrypt.DecryptString(AESKEY, type);
                }
                else
                {
                    type = "";
                }
                if (!string.IsNullOrEmpty(page))
                {
                    page = ApiEncrypt_Decrypt.DecryptString(AESKEY, page);
                }
                else
                {
                    page = "1";
                }
                var res = await _dataRepository.GetRecentRecharge(long.Parse(MemberId), type, int.Parse(page));
                if (res != null && res.Count > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<RecentRechargeEcomm>>));
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
        [HttpGet("GetMiniStatement")]
        [Produces("application/json")]
        public async Task<ResponseModel> GetMiniStatement(string MemberId, string page)
        {
            CommonResponseEcomm<List<MinistatementEcomm>> objres = new CommonResponseEcomm<List<MinistatementEcomm>>();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(MemberId))
                {
                    MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
                }
                if (!string.IsNullOrEmpty(page))
                {
                    MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, page);
                }
                else
                {
                    page = "1";
                }
                var res = await _dataRepository.GetMiniStatement(long.Parse(MemberId), int.Parse(page));

                if (res != null && res.Count() > 0)
                {

                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<List<MinistatementEcomm>>));
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

        [HttpGet("GetFranchiseDetails")]
        [Produces("application/json")]
        public async Task<ResponseModel> GetFranchiseDetails(string DistrictId)
        {

            CommonResponse<List<FranchiseDetails>> objres = new CommonResponse<List<FranchiseDetails>>();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(DistrictId))
                {
                    DistrictId = ApiEncrypt_Decrypt.DecryptString(AESKEY, DistrictId);
                }
                else
                {
                    DistrictId = "0";
                }
                var res = await _dataRepository.GetFranchiseDetails(int.Parse(DistrictId));
                if (res.result.Count > 0)
                {
                    objres.result = res.result;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<FranchiseDetails>>));
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


        [HttpGet("GETOperatorCode")]
        public async Task<ResponseModel> GETOperatorCode(string type)
        {
            CommonResponse<List<OperatorCodeModel>> objres = new CommonResponse<List<OperatorCodeModel>>();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(type))
                {
                    type = ApiEncrypt_Decrypt.DecryptString(AESKEY, type);

                }
                type = type.Replace("+", "");
                var res = await _dataRepository.GetOperatorsCode(2, type, "");
                if (res != null && res.Count > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {
                    objres.response = "error";
                    objres.message = "No Operator found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<OperatorCodeModel>>));
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



        [HttpGet("GetMallCategory")]
        public async Task<ResponseModel> GetMallCategory(string Fk_MemId)
        {
            string EncryptedText = "";
            ResponseModel response = new ResponseModel();
            CommonResponseEcomm<List<MallProduct>> objres = new CommonResponseEcomm<List<MallProduct>>();
            try
            {
                if (!string.IsNullOrEmpty(Fk_MemId))
                {
                    Fk_MemId = ApiEncrypt_Decrypt.DecryptString(AESKEY, Fk_MemId);
                }
                var result = await _dataRepository.GetMallCategory(Fk_MemId);
                if (result != null)
                {
                    objres.Status = 1;
                    objres.message = "success";
                    objres.result = result;
                }
                else
                {

                    objres.Status = 0;
                    objres.response = "error";
                    objres.message = "No data found";
                }

            }
            catch (Exception ex)
            {
                objres.Status = 0;
                objres.response = "error";
                objres.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<List<MallProduct>>));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            response.Body = EncryptedText;
            return response;
        }

        [HttpPost("SaveSupportFAQ")]
        [Produces("application/json")]
        public async Task<ResponseModel> SaveSupportFAQ(RequestModel requestModel)
        {
            SupportFAQResponse supportfaqResult = new SupportFAQResponse();
            CommonEcomm objres = new CommonEcomm();
            ResponseModel returnResponse = new ResponseModel();
            SupportFAQList supportfaqmodel = new SupportFAQList();
            string EncryptedText = "";
            try
            {
                EncryptedText = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                supportfaqmodel = JsonConvert.DeserializeObject<SupportFAQList>(EncryptedText);
                var res = await _dataRepository.SaveSupportFAQ(supportfaqmodel);


                if (res != null && res.flag == "1")
                {
                    supportfaqResult.msg = res.msg;
                    supportfaqResult.response = "Success";
                }
                else
                {
                    supportfaqResult.msg = res.msg;
                    supportfaqResult.response = "Error";
                }
            }
            catch (Exception ex)
            {
                supportfaqResult.msg = ex.Message;
                supportfaqResult.response = "Error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SupportFAQResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, supportfaqResult);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpGet("BusinessDashBaord")]
        [Produces("application/json")]
        public ResponseModel BusinessDashBaord(string MemberId, string appVersion, string appType)
        {
            string EncryptedText = "";

            ResponseModel returnResponse = new ResponseModel();
            BusinessDashBaordResponse businessDashBaordResponse = new BusinessDashBaordResponse();
            try
            {
                MemberId = MemberId.Replace(" ", "+");
                MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
                appVersion = appVersion.Replace(" ", "+");
                appVersion = ApiEncrypt_Decrypt.DecryptString(AESKEY, appVersion);
                appType = appType.Replace(" ", "+");
                appType = ApiEncrypt_Decrypt.DecryptString(AESKEY, appType);
                ENewDashboard dashboard = new ENewDashboard();
                dashboard.appVersion = appVersion;
                dashboard.appType = appType;
                dashboard.memberId = long.Parse(MemberId);
                DataSet dataSet = dashboard.GetBusinessDashbaord();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Status"].ToString() == "1")
                        {
                            BusinessData businessData = new BusinessData();

                            businessDashBaordResponse.Status = 1;
                            businessData.Name = dataSet.Tables[0].Rows[0]["Name"].ToString();
                            businessData.Mobile = dataSet.Tables[0].Rows[0]["Mobile"].ToString();
                            businessData.UserType = dataSet.Tables[0].Rows[0]["UserType"].ToString();
                            businessData.VPA = dataSet.Tables[0].Rows[0]["VPA"].ToString();
                            businessData.IsSuperUser = bool.Parse(dataSet.Tables[0].Rows[0]["IsSuperUser"].ToString());

                            List<PersonalDetails> listPersonalDetails = new List<PersonalDetails>();
                            List<CUGSizeData> listCUGSizeData = new List<CUGSizeData>();
                            List<PersonalDetails> listCardClubs = new List<PersonalDetails>();
                            List<CardFee> listCardFee = new List<CardFee>();
                            List<CardFee> listDirectInsen = new List<CardFee>();
                            for (int i = 0; i <= dataSet.Tables[1].Rows.Count - 1; i++)
                            {
                                PersonalDetails personalDetails = new PersonalDetails();
                                personalDetails.Text = dataSet.Tables[1].Rows[i]["Text"].ToString();
                                personalDetails.Value = dataSet.Tables[1].Rows[i]["Value"].ToString();
                                personalDetails.OrderId = dataSet.Tables[1].Rows[i]["OrderId"].ToString();

                                listPersonalDetails.Add(personalDetails);
                            }
                            for (int i = 0; i <= dataSet.Tables[2].Rows.Count - 1; i++)
                            {
                                CUGSizeData cUGSizeData = new CUGSizeData();
                                cUGSizeData.LevelName = dataSet.Tables[2].Rows[i]["LevelName"].ToString();
                                

                                listCUGSizeData.Add(cUGSizeData);
                            }
                            for (int i = 0; i <= dataSet.Tables[3].Rows.Count - 1; i++)
                            {
                                PersonalDetails personal = new PersonalDetails();
                                personal.Text = dataSet.Tables[3].Rows[i]["Text"].ToString();
                                personal.Value = dataSet.Tables[3].Rows[i]["Value"].ToString();
                                personal.OrderId = dataSet.Tables[3].Rows[i]["OrderId"].ToString();

                                listCardClubs.Add(personal);
                            }
                            for (int i = 0; i <= dataSet.Tables[4].Rows.Count - 1; i++)
                            {
                                CardFee card = new CardFee();
                                card.Target = dataSet.Tables[4].Rows[i]["Target"].ToString();
                                card.Remaining = dataSet.Tables[4].Rows[i]["Remaining"].ToString();
                                card.Status = dataSet.Tables[4].Rows[i]["Status"].ToString();
                                card.Amount = dataSet.Tables[4].Rows[i]["amount"].ToString();

                                listCardFee.Add(card);
                            }
                            for (int i = 0; i <= dataSet.Tables[5].Rows.Count - 1; i++)
                            {
                                CardFee card = new CardFee();
                                card.Target = dataSet.Tables[5].Rows[i]["Target"].ToString();
                                card.Remaining = dataSet.Tables[5].Rows[i]["Remaining"].ToString();
                                card.Status = dataSet.Tables[5].Rows[i]["Status"].ToString();
                                card.Amount = dataSet.Tables[5].Rows[i]["amount"].ToString();

                                listDirectInsen.Add(card);
                            }
                            businessData.PersonalDetails = listPersonalDetails;
                            businessData.CUGSize = listCUGSizeData;
                            businessData.CardClubs = listCardClubs;
                            businessData.CardFee = listCardFee;
                            businessData.DirectInsecntive = listDirectInsen;


                            businessDashBaordResponse.Status = 1;
                            businessDashBaordResponse.Response = businessData;



                        }
                        else
                        {
                            businessDashBaordResponse.Message = dataSet.Tables[0].Rows[0]["message"].ToString();
                            businessDashBaordResponse.Status = int.Parse(dataSet.Tables[0].Rows[0]["Status"].ToString());
                        }
                    }
                }
                //var result = await _dataRepository.GetDashBoardData(long.Parse(memberId), appVersion, appType);
                //if (result != null)
                //{
                //    res.result = result;



                //    res.response = "success";

                //}
            }
            catch (Exception ex)
            {
                businessDashBaordResponse.Status = 0;
                businessDashBaordResponse.Message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusinessDashBaordResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, businessDashBaordResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpGet("BusinessTransaction")]
        public async Task<ResponseModel> BusinessTransaction(string MemberId)
        {
            string EncryptedText = "";
            MemberId = MemberId.Replace(" ", "+");
            MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<Dashboard> objres = new CommonResponse<Dashboard>();
            List<CompanyTransactionDetails> mlist = new List<CompanyTransactionDetails>();
            Dashboard das = new Dashboard();
            try
            {

                var dashboardResponse = await _dataRepository.BusinessTransaction(long.Parse(MemberId),0);
                if (dashboardResponse != null)
                {
                    string[] trans = new string[6] { "Transaction", "Card", "Club", "SuperUser", "Shopping", "Bank" };

                    for (int i = 0; i < trans.Length; i++)
                    {
                        CompanyTransactionDetails m = new CompanyTransactionDetails();

                        if (dashboardResponse.transactiondetails.Where(p => p.Type == trans[i]) != null &&
                            dashboardResponse.transactiondetails.Where(p => p.Type == trans[i]).ToList().Count() > 0)
                        {
                            m.Type = trans[i];
                            m.data = dashboardResponse.transactiondetails.Where(p => p.Type == trans[i]).ToList();
                            mlist.Add(m);
                        }



                    }
                    das.UserWallet = dashboardResponse.UserWallet;
                    das.CompanyTransactionDetails = mlist;
                    das.totalAmount = dashboardResponse.totalAmount;
                    objres.response = "success";
                    objres.message = "success";
                    objres.result = das;

                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<Dashboard>));
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



        [HttpGet("BusinessDashboardV3")]
        public async Task<ResponseModel> BusinessDashboardV3(string MemberId)
        {
            string EncryptedText = "";
            MemberId = MemberId.Replace(" ", "+");
            MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<BusinessDashboardV3> objres = new CommonResponse<BusinessDashboardV3>();
            try
            {
                var res = await _dataRepository.BusinessDashboardV3(long.Parse(MemberId));
                if (res != null)
                {
                    if (res.Second.data != null && res.Second.data.Count() > 1)
                    {
                        res.Second.Header = res.Second.data[0].LevelName;
                        res.Second.data.RemoveAt(0);

                    }
                    else
                    {
                        if (res.Second.data != null && res.Second.data.Count() > 0)
                        {
                            res.Second.data.RemoveAt(0);
                        }
                    }

                    if (res.Third.data != null && res.Third.data.Count() > 1)
                    {
                        res.Third.Header = res.Third.data[0].text;
                        res.Third.data.RemoveAt(0);

                    }
                    else
                    {
                        if (res.Third.data != null && res.Third.data.Count() > 0)
                        {
                            res.Third.data.RemoveAt(0);
                        }
                    }

                    objres.result = res;

                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<BusinessDashboardV3>));
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

        [HttpGet("Market")]
        public async Task<ResponseModel> Market(string memberId = "", string type = "")
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);

            if (!string.IsNullOrEmpty(type))
            {
                type = type.Replace(" ", "+");
                type = ApiEncrypt_Decrypt.DecryptString(AESKEY, type);
            }

            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<List<Market>> objres = new CommonResponse<List<Market>>();
            List<Market> mlist = new List<Market>();
            try
            {
                var AffiliateCategory = await _dataRepository.GetAffiliateCategory();
                //var AffiliateOffers = await _dataRepository.GetAffiliateOffers();
                if (AffiliateCategory != null && AffiliateCategory.Count > 0)
                {

                    var AffiliateProgram = await _dataRepository.GetAffiliateProgram(long.Parse(memberId), type);
                    //foreach (var item in AffiliateCategory)
                    //{

                    for (int i = 0; i < AffiliateCategory.Count(); i++)
                    {
                        Market m = new Market();
                        // objres.result.affilicate = new Market();
                        if (AffiliateProgram.Where(p => p.CategoryId == AffiliateCategory[i].CategoryId) != null
                         && AffiliateProgram.Where(p => p.CategoryId == AffiliateCategory[i].CategoryId).ToList().Count() > 0)
                        {
                            m.CategoryId = AffiliateCategory[i].CategoryId;
                            m.CategoryName = AffiliateCategory[i].CategoryName;
                            m.url = AffiliateCategory[i].url;
                            m.data = AffiliateProgram.Where(p => p.CategoryId == AffiliateCategory[i].CategoryId).ToList();
                            mlist.Add(m);
                        }
                    }
                    objres.result = mlist;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<Market>>));
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



        [HttpGet("MobilePeRegularEarnings")]
        public async Task<ResponseModel> MobilePeRegularEarnings(string MemberId)
        {
            string EncryptedText = "";
            MemberId = MemberId.Replace(" ", "+");
            MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<MobilePeRegularResult> objres = new CommonResponse<MobilePeRegularResult>();
            try
            {
                var res = await _dataRepository.MobilePeRegularEarnings(long.Parse(MemberId));
                if (res != null)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<MobilePeRegularResult>));
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

        [HttpGet("GetCartItem")]
        public async Task<ResponseModel> GetCartItem(string memberId)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);
            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<CartRespone> res = new CommonResponse<CartRespone>();
            CartRespone data = new CartRespone();
            List<CartList> cartlist = new List<CartList>();
            cartsummary summary = new cartsummary();
            decimal amount = 0;
            try
            {
                var result = await _dataRepository.GetCartItem(long.Parse(memberId));
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            amount = amount + Convert.ToDecimal(item.OfferPrice);
                        }
                    }
                    summary.discountAmount = 0.0M;
                    summary.shippingAmount = 0.0M;
                    summary.taxAmount = 0.0M;
                    summary.payableAmount = amount;
                    summary.totalAmount = amount + summary.shippingAmount + summary.taxAmount;
                    data.cartsummary = summary;
                    data.cartlist = result;
                    res.result = data;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<CartRespone>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("UpdateProductQuantity")]
        public async Task<ResponseModel> UpdateProductQuantity(string Pk_CartItemId, string type)
        {
            string EncryptedText = "";
            Pk_CartItemId = Pk_CartItemId.Replace(" ", "+");
            Pk_CartItemId = ApiEncrypt_Decrypt.DecryptString(AESKEY, Pk_CartItemId);
            type = type.Replace(" ", "+");
            type = ApiEncrypt_Decrypt.DecryptString(AESKEY, type);


            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<Common> res = new CommonResponse<Common>();
            try
            {
                var result = await _dataRepository.UpdateProductQuantity(long.Parse(Pk_CartItemId), type);
                if (result != null)
                {
                    res.result = result;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<Common>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("RemoveCartItem")]
        public async Task<ResponseModel> RemoveCartItem(string Pk_CartItemId)
        {
            string EncryptedText = "";
            Pk_CartItemId = Pk_CartItemId.Replace(" ", "+");
            Pk_CartItemId = ApiEncrypt_Decrypt.DecryptString(AESKEY, Pk_CartItemId);


            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<Common> res = new CommonResponse<Common>();
            try
            {
                var result = await _dataRepository.RemoveCartItem(long.Parse(Pk_CartItemId));
                if (result != null)
                {
                    res.result = result;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<Common>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("SelfCUGMore")]
        public async Task<ResponseModel> SelfCUGMore(string MemberId, string monthId, string year)
        {
            string EncryptedText = "";
            MemberId = MemberId.Replace(" ", "+");
            MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);

            monthId = monthId.Replace(" ", "+");
            monthId = ApiEncrypt_Decrypt.DecryptString(AESKEY, monthId);

            year = year.Replace(" ", "+");
            year = ApiEncrypt_Decrypt.DecryptString(AESKEY, year);


            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<SelfCUGMoreResult> objres = new CommonResponse<SelfCUGMoreResult>();
            try
            {
                var res = await _dataRepository.SelfCUGMore(long.Parse(MemberId), int.Parse(monthId), int.Parse(year));
                if (res != null)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<SelfCUGMoreResult>));
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



        [HttpGet("MobilePeClub")]
        [Produces("application/json")]
        public async Task<ResponseModel> MobilePeClub(string MemberId, string monthId, string year)
        {
            CommonResponseEcomm<List<MobilePeClubResult>> objres = new CommonResponseEcomm<List<MobilePeClubResult>>();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(MemberId))
                {
                    MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
                }
                if (!string.IsNullOrEmpty(monthId))
                {
                    monthId = ApiEncrypt_Decrypt.DecryptString(AESKEY, monthId);
                }
                if (!string.IsNullOrEmpty(year))
                {
                    year = ApiEncrypt_Decrypt.DecryptString(AESKEY, year);
                }
                var res = await _dataRepository.MobilePeClub(long.Parse(MemberId), int.Parse(monthId), int.Parse(year));
                if (res != null)
                {
                    objres.result = res.result;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<List<MobilePeClubResult>>));
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

        [HttpGet("EarningBankTrf")]
        [Produces("application/json")]
        public async Task<ResponseModel> EarningBankTrf(string MemberId, string monthId, string year)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            CommonResponseEcomm<List<EarningBankTrf>> objres = new CommonResponseEcomm<List<EarningBankTrf>>();
            try
            {
                if (!string.IsNullOrEmpty(MemberId))
                {
                    MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);
                }
                if (!string.IsNullOrEmpty(monthId))
                {
                    monthId = ApiEncrypt_Decrypt.DecryptString(AESKEY, monthId);
                }
                if (!string.IsNullOrEmpty(year))
                {
                    year = ApiEncrypt_Decrypt.DecryptString(AESKEY, year);
                }
                var res = await _dataRepository.EarningBankTrf(long.Parse(MemberId), int.Parse(monthId), int.Parse(year));
                if (res != null)
                {
                    objres.result = res.result;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponseEcomm<List<EarningBankTrf>>));
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
        [HttpPost("RedeemRewardPoints")]
        [Produces("application/json")]
        public async Task<ResponseModel> RedeemRewardPoints(RequestModel requestModel)
        {
            CommonResponse<ResultSet> objres = new CommonResponse<ResultSet>();
            RedeamRewardPoints model = new RedeamRewardPoints();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                model = JsonConvert.DeserializeObject<RedeamRewardPoints>(dcdata);
                var res = await _dataRepository.RedeemRewardPoints(model);
                if (res != null)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<ResultSet>));
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

        [HttpGet("GetCardDetails")]
        [Produces("application/json")]
        public async Task<ResponseModel> GetCardDetails(string memberId)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);


            ResponseModel returnResponse = new ResponseModel();


            CommonResponse<CardMasterResponse> objres = new CommonResponse<CardMasterResponse>();
            CommonResponse<PineCardBalanceResponse> cardbalance
                = new CommonResponse<PineCardBalanceResponse>();
            objres.result = new CardMasterResponse();
            try
            {
                var res = await _dataRepository.GetCardDetails(long.Parse(memberId));
                if (res != null && res.carddata != null)
                {
                    if (res.carddata.IsCardApplied)
                    {
                        PineCardBalanceRequest request = new PineCardBalanceRequest();
                        request.cardBalanceRequestList = new List<string>();
                        var reference = await _dataRepository.GetCardReference(long.Parse(memberId));
                        objres.result = res;
                        if (reference != null && !string.IsNullOrEmpty(reference.TransactionId))
                        {
                            request.cardBalanceRequestList.Add(reference.TransactionId);
                            request.memberId = long.Parse(memberId);

                            var newusercardstatus = await _dataRepository.CheckNewUserCardStatus(long.Parse(memberId));
                            if (newusercardstatus.flag == 1)
                            {
                                cardbalance = CardBalance_V1(request);
                            }
                            else
                            {
                                cardbalance = await CardBalance(request);
                            }

                            if (cardbalance.response == "success")
                            {
                                decimal balance = cardbalance.result.cardBalanceDetailList[0].availableAmount / 100M;
                                objres.result.carddata.cardbalance = balance;
                            }
                            else
                            {
                                decimal balance = 0.0M;
                                objres.result.carddata.cardbalance = balance;
                            }
                        }
                        else
                        {
                            decimal balance = 0.0M;
                        }

                    }
                    else
                    {
                        objres.result = res;
                    }
                    objres.result.viewstatement = new List<CommonResult>();
                    objres.result.viewstatement.Add(new CommonResult { Text = "Gift", Value = "Gift" });
                    objres.result.viewstatement.Add(new CommonResult { Text = "Reloadable", Value = "Reloadable" });
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {
                    objres.response = "error";
                    objres.message = "error";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<CardMasterResponse>));
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

        [HttpGet("CardBalance")]
        [Produces("application/json")]
        public async Task<CommonResponse<PineCardBalanceResponse>> CardBalance(PineCardBalanceRequest model)
        {
            CommonResponse<PineCardBalanceResponse> res = new CommonResponse<PineCardBalanceResponse>();
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string CustData = "";
            try
            {


                js = new DataContractJsonSerializer(typeof(PineCardBalanceRequest));
                ms = new MemoryStream();
                js.WriteObject(ms, model);
                ms.Position = 0;
                sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                string token = await PineToken();
                var response = PineCommonRequestobj.sendRequest(PineCardbaseUrl + "" + PINECARDBALANCEURL, "POST", CustData, token);
                var r = await _dataRepository.PineCardHistroy(model.memberId, "", CustData, response.responseText, token, "CardBalance");
                if (response.statuscode == 200)
                {
                    res.result = JsonConvert.DeserializeObject<PineCardBalanceResponse>(response.responseText);

                    if (res.result.cardBalanceDetailList[0].responseCode == 0)
                    {

                        res.response = "success";

                    }
                    else
                    {
                        res.response = "error";

                    }

                }
                else
                {
                    res.message = response.responseText;
                    res.response = "error";
                }
            }
            catch (Exception ex)
            {

                res.message = ex.Message;
                res.response = "error";
            }
            return res;
        }

        [HttpGet("PineToken")]
        [Produces("application/json")]
        public async Task<string> PineToken()
        {
            string token = "";
            try
            {
                var tokenresult = await _dataRepository.Getpinetoken(2, "", "");
                if (tokenresult.flag == 0)
                {
                    var res = PineCommonRequestobj.sendRequest(PineCardbaseUrl + "" + PINECARDTOKENURL, "GET", null);
                    if (res.statuscode == 200)
                    {
                        dynamic result = JsonConvert.DeserializeObject<object>(res.responseText);
                        _dataRepository.Getpinetoken(1, result.accessToken.ToString(), result.tokenExpiryTime.ToString());
                        return token = result.accessToken.ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return token = tokenresult.msg;
                }


            }
            catch (Exception ex)
            {
                return "";

            }

        }

        [HttpGet("CardBalance_V1")]
        [Produces("application/json")]
        public CommonResponse<PineCardBalanceResponse> CardBalance_V1(PineCardBalanceRequest model)
        {
            CommonResponse<PineCardBalanceResponse> res = new CommonResponse<PineCardBalanceResponse>();

            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string CustData = "";
            try
            {


                js = new DataContractJsonSerializer(typeof(PineCardBalanceRequest));
                ms = new MemoryStream();
                js.WriteObject(ms, model);
                ms.Position = 0;
                sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();



                var response = PineCommonRequestobj.sendRequestV1("https://mobilepefintech.com/api/card/CardBalance", "POST", CustData);

                if (response.statuscode == 200)
                {
                    res.result = JsonConvert.DeserializeObject<PineCardBalanceResponse>(response.responseText);

                    if (res.result != null &&
                        res.result.cardBalanceDetailList != null &&
                        res.result.cardBalanceDetailList.Count() > 0 &&
                        res.result.cardBalanceDetailList[0].responseCode == 0)
                    {

                        res.response = "success";


                    }
                    else
                    {
                        res.response = "error";

                    }
                }

                else
                {
                    res.response = "error";

                }


            }
            catch (Exception ex)
            {

                res.message = ex.Message;
                res.response = "error";
            }
            return res;
        }

        [HttpGet("GetLevelWiseLedger")]
        public async Task<ResponseModel> GetLevelWiseLedger(string memberId, string type, string monthId, string yearId, string comtype)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);

            type = type.Replace(" ", "+");
            type = ApiEncrypt_Decrypt.DecryptString(AESKEY, type);

            monthId = monthId.Replace(" ", "+");
            monthId = ApiEncrypt_Decrypt.DecryptString(AESKEY, monthId);

            yearId = yearId.Replace(" ", "+");
            yearId = ApiEncrypt_Decrypt.DecryptString(AESKEY, yearId);


            ResponseModel returnResponse = new ResponseModel();


            CommonResponse<AllLedgerResponse> objres = new CommonResponse<AllLedgerResponse>();
            objres.result = new AllLedgerResponse();
            try
            {
                var res = await _dataRepository.LevelWiseLedger(long.Parse(memberId), type, int.Parse(monthId), int.Parse(yearId));
                if (res != null && res != null)
                {

                    objres.result = res;

                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {
                    objres.response = "error";
                    objres.message = "error";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<AllLedgerResponse>));
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


        [HttpGet("AllLedgerV2")]
        public async Task<ResponseModel> AllLedgerV2(string memberId, string type, string monthId, string yearId, string level)
        {

            string EncryptedText = "";
            if(!string.IsNullOrEmpty(memberId))
            {
                memberId = memberId.Replace(" ", "+");
                memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);
            }

            if (!string.IsNullOrEmpty(type))
            {
                type = type.Replace(" ", "+");
                type = ApiEncrypt_Decrypt.DecryptString(AESKEY, type);
            }
            if (!string.IsNullOrEmpty(monthId))
            {
                monthId = monthId.Replace(" ", "+");
                monthId = ApiEncrypt_Decrypt.DecryptString(AESKEY, monthId);
            }
            if (!string.IsNullOrEmpty(yearId))
            {
                yearId = yearId.Replace(" ", "+");
                yearId = ApiEncrypt_Decrypt.DecryptString(AESKEY, yearId);
            }
            if (!string.IsNullOrEmpty(level))
            {
                level = level.Replace(" ", "+");
                level = ApiEncrypt_Decrypt.DecryptString(AESKEY, level);
            }

            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<AllLedgerResponse> objres = new CommonResponse<AllLedgerResponse>();
            objres.result = new AllLedgerResponse();
            try
            {
                var res = await _dataRepository.AllLedgerV2(long.Parse(memberId), type, int.Parse(monthId), int.Parse(yearId), level);
                if (res != null && res != null)
                {

                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {
                    objres.response = "error";
                    objres.message = "error";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<AllLedgerResponse>));
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

        [HttpGet("GetInsuranceUrl")]
        public ResponseModel GetInsuranceUrl(string serviceId, string memberId)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);

            serviceId = serviceId.Replace(" ", "+");
            serviceId = ApiEncrypt_Decrypt.DecryptString(AESKEY, serviceId);


            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<InsuranceReponse> res = new CommonResponse<InsuranceReponse>();
            InsuranceRequest objreq = new InsuranceRequest();
            try
            {
                res.result = new InsuranceReponse();
                if (int.Parse(serviceId) == -1)
                {
                    string CompanyCode = MD5Encryption.Encrypt(INSURANCEURLCOMPAYCODE, INSURANCEURLENCRYPTIONKEY);
                    string UniqueID = MD5Encryption.Encrypt(memberId.ToString(), INSURANCEURLENCRYPTIONKEY);
                    int UserType = 2;
                    INSURANCEURL += "CompanyCode=" + CompanyCode + "&UniqueID=" + UniqueID + "&UserType=" + UserType;
                    res.response = "success";
                    res.message = "success";
                    res.result.url = INSURANCEURL;
                }
                else
                {

                    objreq.serviceId = int.Parse(serviceId);
                    objreq.partnerCode = "43";
                    objreq.platform = "API";
                    objreq.userCode = memberId.ToString();
                    objreq.authToken = "7f39a67da4816370e36583c09f5e9e05";
                    DataContractJsonSerializer js;
                    MemoryStream ms;
                    StreamReader sr;
                    string CustData = "";
                    js = new DataContractJsonSerializer(typeof(InsuranceRequest));
                    ms = new MemoryStream();
                    js.WriteObject(ms, objreq);
                    ms.Position = 0;
                    sr = new StreamReader(ms);
                    CustData = sr.ReadToEnd();
                    sr.Close();
                    ms.Close();
                    dynamic result = JsonConvert.DeserializeObject<object>(CommonJsonPostRequest.CommonSendRequest(INSURANCEURLNEW, "POST", CustData));
                    res.response = "success";
                    res.message = "success";
                    res.result.url = result.data.url;
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }
            string CustData1 = "";
            DataContractJsonSerializer js1;
            MemoryStream ms1;
            js1 = new DataContractJsonSerializer(typeof(CommonResponse<InsuranceReponse>));
            ms1 = new MemoryStream();
            js1.WriteObject(ms1, res);
            ms1.Position = 0;
            StreamReader sr1 = new StreamReader(ms1);
            CustData1 = sr1.ReadToEnd();
            sr1.Close();
            ms1.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData1);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("CardLedger")]
        public async Task<ResponseModel> CardLedger(string memberId, string type)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);

            type = type.Replace(" ", "+");
            type = ApiEncrypt_Decrypt.DecryptString(AESKEY, type);

            if (string.IsNullOrEmpty(type))
            {
                type = "gift";
            }

            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<List<CardLedgerDetails>> objres = new CommonResponse<List<CardLedgerDetails>>();

            try
            {

                if (type.ToLower() == "reloadable")
                {
                    var rescardData = await _dataRepository.CardLedger(long.Parse(memberId), type);
                    var link = rescardData[rescardData.Count - 1].link;

                    PineCardTransactionDetailRequest model = new PineCardTransactionDetailRequest();
                    model.memberId = long.Parse(memberId);
                    var referenceNumber = await _dataRepository.GetCardReference(long.Parse(memberId));
                    if (referenceNumber != null && !string.IsNullOrEmpty(referenceNumber.TransactionId))
                    {
                        model.referenceNumber = referenceNumber.TransactionId;
                        var result = await CardTransactionDetail(model);


                        if (result.response == "success")
                        {
                            objres.result = new List<CardLedgerDetails>();
                            objres.response = result.response;

                            for (int i = 0; i < result.result.transactionDetailList.Count(); i++)
                            {
                                if (result.result.transactionDetailList[i].transactionType.ToLower() == "credit")
                                {
                                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
                                    CardLedgerDetails cardLedgerDetails = new CardLedgerDetails();
                                    cardLedgerDetails.creditAmount = "+ " + CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + Convert.ToString(result.result.transactionDetailList[i].transactionAmount / 100M);
                                    cardLedgerDetails.credit = Convert.ToDecimal(result.result.transactionDetailList[i].transactionAmount / 100M);
                                    cardLedgerDetails.debit = 0.0M;
                                    cardLedgerDetails.debitAmount = "";
                                    cardLedgerDetails.remark = "Amount Added in Card.";
                                    cardLedgerDetails.date = result.result.transactionDetailList[i].transactionDate;
                                    cardLedgerDetails.transactionId = result.result.transactionDetailList[i].orderId;
                                    cardLedgerDetails.link = link;
                                    objres.result.Add(cardLedgerDetails);
                                    //objres.result.Add(new CardLedgerDetails
                                    //{
                                    //    CreditAmount = "+ " + CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + Convert.ToString(result.result.transactionDetailList[i].transactionAmount / 100M)

                                    //    ,
                                    //    credit = Convert.ToDecimal(result.result.transactionDetailList[i].transactionAmount / 100M),
                                    //    debit = 0.0M,
                                    //    DebitAmount = ""
                                    //    ,
                                    //    Remark = "Amount Added in Card",
                                    //    date = result.result.transactionDetailList[i].transactionDate,
                                    //    transactionId = result.result.transactionDetailList[i].orderId,
                                    //    link = link
                                    //}); ;
                                }
                                else if (result.result.transactionDetailList[i].transactionType.ToLower() == "refund" || result.result.transactionDetailList[i].transactionType.ToLower() == "reversed")
                                {
                                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
                                    objres.result.Add(new CardLedgerDetails
                                    {
                                        creditAmount = "+ " + CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + Convert.ToString(result.result.transactionDetailList[i].transactionAmount / 100M)

                                        ,
                                        credit = Convert.ToDecimal(result.result.transactionDetailList[i].transactionAmount / 100M),
                                        debit = 0.0M,
                                        debitAmount = ""
                                        ,
                                        remark = "Amount refunded",
                                        date = result.result.transactionDetailList[i].transactionDate,
                                        transactionId = "",
                                        link = link
                                    }); ;
                                }
                                else if (result.result.transactionDetailList[i].transactionType.ToLower() == "debit")
                                {
                                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
                                    objres.result.Add(new CardLedgerDetails
                                    {

                                        debitAmount = "- " + CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + Convert.ToString(result.result.transactionDetailList[i].transactionAmount / 100M)
                                    ,
                                        debit = Convert.ToDecimal(result.result.transactionDetailList[i].transactionAmount / 100M),
                                        credit = 0.0M,
                                        creditAmount = "",
                                        remark = "Amount used at " + result.result.transactionDetailList[i].merchantName,
                                        date = result.result.transactionDetailList[i].transactionDate,
                                        transactionId = result.result.transactionDetailList[i].approvalCode.ToString(),
                                        link = ""
                                    }); ;

                                }

                            }
                            objres.result = objres.result.OrderByDescending(m => Convert.ToDateTime(m.date)).ToList();
                        }
                    }
                    else
                    {
                        objres.response = "error";
                        objres.message = "No Record found";

                    }

                }
                else
                {
                    var res = await _dataRepository.CardLedger(long.Parse(memberId), type);
                    if (res != null && res.Count > 0)
                    {
                        objres.response = "success";
                        objres.message = "success";
                        objres.result = res;

                    }
                    else
                    {
                        objres.response = "error";
                        objres.message = "No Record found";

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
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<CardLedgerDetails>>));
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
        [HttpGet("CardTransactionDetail")]
        public async Task<CommonResponse<PineCardTransactionDetailResponse>> CardTransactionDetail(PineCardTransactionDetailRequest model)
        {
            CommonResponse<PineCardTransactionDetailResponse> res = new CommonResponse<PineCardTransactionDetailResponse>();
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string CustData = "";
            try
            {


                var newusercardstatus = await _dataRepository.CheckNewUserCardStatus(model.memberId);
                if (newusercardstatus.flag == 1)
                {


                    var response = PineCommonRequestobj.sendRequestV1("https://mobilepefintech.com/api/card/CardTransactionDetail", "POST", CustData);
                }
                else
                {

                    js = new DataContractJsonSerializer(typeof(PineCardTransactionDetailRequest));
                    ms = new MemoryStream();
                    js.WriteObject(ms, model);
                    ms.Position = 0;
                    sr = new StreamReader(ms);
                    CustData = sr.ReadToEnd();
                    sr.Close();
                    ms.Close();
                    string token = await PineToken();

                    var response = PineCommonRequestobj.sendRequest(PineCardbaseUrl + "" + PINECARDTransactionDetailURL, "POST", CustData, token);
                    var r = await _dataRepository.PineCardHistroy(model.memberId, "", CustData, response.responseText, token, "CardTransactionDetail");
                    if (response.statuscode == 200)
                    {
                        res.result = JsonConvert.DeserializeObject<PineCardTransactionDetailResponse>(response.responseText);

                        if (res.result.responseCode == 0)
                        {

                            res.response = "success";



                        }
                        else
                        {
                            res.response = "error";

                        }

                    }
                    else
                    {
                        res.message = response.responseText;
                        res.response = "error";
                    }
                }
            }
            catch (Exception ex)
            {

                res.message = ex.Message;
                res.response = "error";
            }
            return res;
        }

        [HttpGet("Addedfunddetailsv2")]
        public async Task<ResponseModel> Addedfunddetailsv2(string memberId, string type)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);


            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<List<cardamountdetails>> objres = new CommonResponse<List<cardamountdetails>>();
            try
            {
                var res = await _dataRepository.Cardamountdetails(long.Parse(memberId), type);

                if (res != null && res.Count() > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                }
                else
                {
                    objres.response = "error";
                    objres.message = "No record found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<cardamountdetails>>));
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

        [HttpGet("GetNotification")]
        public async Task<ResponseModel> GetNotification(string MemberId)
        {
            string EncryptedText = "";
            MemberId = MemberId.Replace(" ", "+");
            MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);


            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<List<FCM>> objres = new CommonResponse<List<FCM>>();
            try
            {
                var res = await _dataRepository.GetNotification(long.Parse(MemberId));
                if (res != null)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<FCM>>));
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

        [HttpGet("GetInformationMessage")]
        public async Task<ResponseModel> GetInformationMessage(string memberId, string appType, string appVersion)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);

            appType = appType.Replace(" ", "+");
            appType = ApiEncrypt_Decrypt.DecryptString(AESKEY, appType);

            appVersion = appVersion.Replace(" ", "+");
            appVersion = ApiEncrypt_Decrypt.DecryptString(AESKEY, appVersion);


            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<List<Information>> res = new CommonResponse<List<Information>>();
            try
            {

                var result = await _dataRepository.GetInformationMessage(long.Parse(memberId), appType, appVersion);
                if (result != null && result.Count() > 0)
                {
                    res.result = result;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }


            }

            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<Information>>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("GetMiniStatement_V2")]
        public async Task<ResponseModel> GetMiniStatement_V2(string MemberId, string page, string transtype, string categorytype)
        {
            string EncryptedText = "";
            MemberId = MemberId.Replace(" ", "+");
            MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);

            page = page.Replace(" ", "+");
            page = ApiEncrypt_Decrypt.DecryptString(AESKEY, page);

            transtype = transtype.Replace(" ", "+");
            transtype = ApiEncrypt_Decrypt.DecryptString(AESKEY, transtype);

            categorytype = categorytype.Replace(" ", "+");
            categorytype = ApiEncrypt_Decrypt.DecryptString(AESKEY, categorytype);

            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<MinistatementV2ResponseEcomm> objres = new CommonResponse<MinistatementV2ResponseEcomm>();
            try
            {
                var res = await _dataRepository.GetMiniStatement_V2(long.Parse(MemberId), int.Parse(page), transtype, categorytype);
                if (res != null && res.data != null && res.data.Count() > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<MinistatementV2ResponseEcomm>));
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


        [HttpGet("GetCommissionTransfered")]
        public async Task<ResponseModel> GetCommissionTransfered(string memberId)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);


            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<List<CommissionTransferdetail>> objres = new CommonResponse<List<CommissionTransferdetail>>();
            try
            {
                var res = await _dataRepository.GetCommissionTransfer(long.Parse(memberId));
                if (res != null && res.Count() > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {
                    objres.response = "error";
                    objres.message = "No record found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<CommissionTransferdetail>>));
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

        [HttpGet("CommissionTransferedDetails")]
        public async Task<ResponseModel> CommissionTransferedDetails(string memberId, string amount)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);
            amount = amount.Replace(" ", "+");
            amount = ApiEncrypt_Decrypt.DecryptString(AESKEY, amount);
            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<CommissionDetailsResponseEcomm> objres = new CommonResponse<CommissionDetailsResponseEcomm>();
            try
            {

                var res = await _dataRepository.transferedCommissionDetails(long.Parse(memberId), decimal.Parse(amount));
                if (res != null && res.message == "success")
                {
                    objres.result = new CommissionDetailsResponseEcomm();
                    objres.response = "success";
                    objres.message = "success";
                    objres.result.data = res;
                    var bank = await _dataRepository.bankdetails(long.Parse(memberId));
                    objres.result.bankdetails = bank;
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.message;

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
            js = new DataContractJsonSerializer(typeof(CommonResponse<CommissionDetailsResponseEcomm>));
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


        [HttpGet("GetNotificationCount")]
        public async Task<ResponseModel> GetNotificationCount(string MemberId)
        {
            string EncryptedText = "";
            MemberId = MemberId.Replace(" ", "+");
            MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);


            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<NotificationCount> objres = new CommonResponse<NotificationCount>();
            try
            {
                var res = await _dataRepository.GetNoticationCount(long.Parse(MemberId));
                if (res != null)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<NotificationCount>));
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
        [HttpGet("VerifyPhysicalCard")]
        public async Task<ResponseModel> VerifyPhysicalCard(string Cardno, string MemberId)
        {
            string EncryptedText = "";
            MemberId = MemberId.Replace(" ", "+");
            MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);

            Cardno = Cardno.Replace(" ", "+");
            Cardno = ApiEncrypt_Decrypt.DecryptString(AESKEY, Cardno);


            ResponseModel returnResponse = new ResponseModel();
            Common objres = new Common();
            try
            {
                var res = await _dataRepository.VerifyPhysicalCard(Cardno, long.Parse(MemberId));
                if (res != null && res.flag == 0)
                {

                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = res.msg;
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

        [HttpGet("FAQ")]
        public async Task<ResponseModel> FAQ(string faqId)
        {
            string EncryptedText = "";
            
            faqId = faqId.Replace(" ", "+");
            faqId = ApiEncrypt_Decrypt.DecryptString(AESKEY, faqId);
            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<List<FAQResponse>> res = new CommonResponse<List<FAQResponse>>();
            try
            {
                List<FAQResponse> data = new List<FAQResponse>();

                var result = await _dataRepository.FAQ(long.Parse(faqId));

                if (result != null && result.Count > 0)
                {
                    if ((long.Parse(faqId) == 0))
                    {
                        foreach (var item in result)
                        {
                            FAQResponse objfaq = new FAQResponse();
                            List<SubFAQ> SubFAQlist = new List<SubFAQ>();
                            var subfaq = await _dataRepository.FAQ(item.FAQID);
                            objfaq.FAQID = -1;
                            objfaq.Question = item.Question;
                            objfaq.Answer = item.Answer;
                            objfaq.link = item.link;
                            objfaq.Videolink = item.Videolink;
                            foreach (var datares in subfaq)
                            {
                                SubFAQ obj = new SubFAQ();
                                obj.FAQID = datares.FAQID;
                                obj.Question = datares.Question;
                                SubFAQlist.Add(obj);
                            }
                            objfaq.SUBFAQ = SubFAQlist;
                            data.Add(objfaq);
                        }
                    }
                    else
                    {
                        foreach (var item in result)
                        {
                            FAQResponse objfaq = new FAQResponse();

                            objfaq.FAQID = item.FAQID;
                            objfaq.Question = item.Question;
                            objfaq.Answer = item.Answer;
                            objfaq.link = item.link;
                            objfaq.Videolink = item.Videolink;
                            data.Add(objfaq);
                        }
                    }
                    res.result = data;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "No record found";
                }


            }

            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<FAQResponse>>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("OrderDetail")]
        public async Task<ResponseModel> OrderDetail(string TransId)
        {
            string EncryptedText = "";
            TransId = TransId.Replace(" ", "+");
            TransId = ApiEncrypt_Decrypt.DecryptString(AESKEY, TransId);


            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<OrderDetailResponse> res = new CommonResponse<OrderDetailResponse>();
            try
            {
                var result = await _dataRepository.OrderDetail(long.Parse(TransId));
                if (result != null && result.orderhead != null
                    && result.orderhead.data != null && result.orderhead.data.Count() > 0)
                {
                    if (result.orderhead.data != null && result.orderhead.data.Count() > 0)
                    {
                        result.orderhead.header = result.orderhead.data[0].value.ToString();
                        result.orderhead.data.RemoveAt(0);
                    }
                    if (result.orderdetails.data != null && result.orderdetails.data.Count() > 0)
                    {
                        result.orderdetails.header = result.orderdetails.data[0].value.ToString();
                        result.orderdetails.data.RemoveAt(0);
                    }
                    if (result.orderstatus.data != null && result.orderstatus.data.Count() > 0)
                    {
                        result.orderstatus.header = result.orderstatus.data[0].value.ToString();
                        result.orderstatus.data.RemoveAt(0);
                    }
                    if (result.summarydetails.data != null && result.summarydetails.data.Count() > 0)
                    {
                        result.summarydetails.header = result.summarydetails.data[0].value.ToString();

                        result.summarydetails.data.RemoveAt(0);
                    }

                    if (result.payment.data != null && result.payment.data.Count() > 0)
                    {
                        result.payment.header = result.payment.data[0].value.ToString();
                        result.payment.data.RemoveAt(0);
                    }


                    res.result = result;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<OrderDetailResponse>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("ProductDetials")]
        public async Task<ResponseModel> ProductDetials(string productDetialsId)
        {
            string EncryptedText = "";
            productDetialsId = productDetialsId.Replace(" ", "+");
            productDetialsId = ApiEncrypt_Decrypt.DecryptString(AESKEY, productDetialsId);


            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<ProductDetailsResponse> res = new CommonResponse<ProductDetailsResponse>();
            List<avialableoffer> avialableoffers = new List<avialableoffer>();
            // avialableoffers.Add(new avialableoffer { offer = "" });
            try
            {
                var result = await _dataRepository.ProductDetail(long.Parse(productDetialsId));

                if (result != null)
                {
                    if (result.productdetails.productReviews.Count > 0)
                    {
                        for (int i = 0; i < result.productdetails.productReviews.Count; i++)
                        {
                            var reviewres = await _dataRepository.ReviewUser(result.productdetails.productReviews[i].user_id);
                            result.productdetails.productReviews[i].userName = reviewres.Name;
                            result.productdetails.productReviews[i].userImage = reviewres.ProfilePic;
                        }

                    }
                    result.productdetails.avialableoffer = avialableoffers;
                    res.result = result;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<ProductDetailsResponse>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("GetVoucherBrand")]
        public async Task<ResponseModel> GetVoucherBrand(string Brand)
        {
            string EncryptedText = "";
            Brand = Brand.Replace(" ", "+");
            Brand = ApiEncrypt_Decrypt.DecryptString(AESKEY, Brand);
            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<vocherresponse> res = new CommonResponse<vocherresponse>();
            try
            {

                var result = await _dataRepository.GetVoucherBrand(Brand);
                if (result != null)
                {
                    res.result = result;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<vocherresponse>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("GetUserAddress")]
        public async Task<ResponseModel> GetUserAddress(string memberId, string RequestType)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);

            RequestType = RequestType.Replace(" ", "+");
            RequestType = ApiEncrypt_Decrypt.DecryptString(AESKEY, RequestType);

            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<UserAddressresponse> res = new CommonResponse<UserAddressresponse>();

            UserAddressresponse result = new UserAddressresponse();

            try
            {
                if (string.IsNullOrEmpty(RequestType))
                {
                    res.response = "error";
                    res.message = "RequestType is mandatory";
                }
                else
                {
                    result = await _dataRepository.GetUserAddress(long.Parse(memberId), RequestType);
                    if (result != null)
                    {
                        cartsummary summary = new cartsummary();
                        decimal amount = 0;
                        if (result.purchasedetails.Count > 0)
                        {
                            foreach (var item in result.purchasedetails)
                            {
                                amount = amount + Convert.ToDecimal(item.ProductAmt);
                            }
                        }
                        summary.discountAmount = 0.0M;
                        summary.shippingAmount = 0.0M;
                        summary.taxAmount = 0.0M;
                        summary.payableAmount = amount;
                        summary.totalAmount = amount + summary.shippingAmount + summary.taxAmount;

                        res.result = result;
                        res.result.summary = summary;
                        res.response = "success";

                    }
                    else
                    {
                        res.response = "error";
                        res.message = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<UserAddressresponse>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpGet("CheckDeliveryAddress")]
        public async Task<ResponseModel> CheckDeliveryAddress(string ProductId, string pincode)
        {
            string EncryptedText = "";
            ProductId = ProductId.Replace(" ", "+");
            ProductId = ApiEncrypt_Decrypt.DecryptString(AESKEY, ProductId);

            pincode = pincode.Replace(" ", "+");
            pincode = ApiEncrypt_Decrypt.DecryptString(AESKEY, pincode);

            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<Common> res = new CommonResponse<Common>();
            try
            {
                var result = await _dataRepository.CheckDeliveryAddress(long.Parse(ProductId), pincode);
                if (result != null)
                {
                    res.result = result;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<Common>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("MallOrder")]
        public async Task<ResponseModel> MallOrder(string paymentId)
        {
            string EncryptedText = "";
            paymentId = paymentId.Replace(" ", "+");
            paymentId = ApiEncrypt_Decrypt.DecryptString(AESKEY, paymentId);

            ResponseModel returnResponse = new ResponseModel();

            CommonResponse<MallOrderDetail> res = new CommonResponse<MallOrderDetail>();
            customerordersummary customerordersummary = new customerordersummary();

            try
            {
                var result = await _dataRepository.MallOrderDetail(paymentId);
                var result2 = await _dataRepository.GetPaymentMethod(paymentId);
                if (result != null)
                {
                    if (result.customerOrderItem.Count > 0)
                    {
                        for (int i = 0; i < result.customerOrderItem.Count; i++)
                        {
                            List<productstatus> ls = new List<productstatus>();
                            ls = await _dataRepository.GetorderstatusV3(result.customerOrder.orderId, result.customerOrderItem[i].vendorId.ToString(), result.customerOrderItem[i].pK_ProductId);
                            result.customerOrderItem[i].status = ls;
                            customerordersummary.item = customerordersummary.item + result.customerOrderItem[i].offerPrice;
                        }
                    }
                    customerordersummary.discount = result.customerOrder.discount;
                    customerordersummary.total = customerordersummary.item - result.customerOrder.discount;
                    result.customerordersummary = customerordersummary;
                    result.paymentMethod = result2.paymentMethod;
                    result.helpdetails = result2.helpdetails;

                    res.result = result;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<MallOrderDetail>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpGet("GetReplacementItem")]
        public async Task<ResponseModel> GetReplacementItem(string pk_OrderItemId)
        {
            string EncryptedText = "";
            pk_OrderItemId = pk_OrderItemId.Replace(" ", "+");
            pk_OrderItemId = ApiEncrypt_Decrypt.DecryptString(AESKEY, pk_OrderItemId);

           
            ResponseModel returnResponse = new ResponseModel();


            CommonResponse<Replacement> res = new CommonResponse<Replacement>();

            try
            {

                var result = await _dataRepository.GetReplacementItem(long.Parse(pk_OrderItemId));

                if (result != null)
                {
                    res.result = result;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<Replacement>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("CancelOrder")]
        public async Task<ResponseModel> CancelOrder(string pk_OrderItemId, string memberId, string reason)
        {
            string EncryptedText = "";
            pk_OrderItemId = pk_OrderItemId.Replace(" ", "+");
            pk_OrderItemId = ApiEncrypt_Decrypt.DecryptString(AESKEY, pk_OrderItemId);

            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);

            reason = reason.Replace(" ", "+");
            reason = ApiEncrypt_Decrypt.DecryptString(AESKEY, reason);

            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<Common> res = new CommonResponse<Common>();
            try
            {
                var result = await _dataRepository.CancelOrder(long.Parse(pk_OrderItemId), long.Parse(memberId), reason);
                if (result != null && result.response == "success")
                {
                    if (result.RedeemPoint > 0)
                    {
                        var objres = _dataRepository.CreatePoint(long.Parse(memberId), result.RedeemPoint, result.Id);
                    }
                    res.result = result;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<Common>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("GetCartCount")]
        public async Task<ResponseModel> GetCartCount(string memberId)
        {
            string EncryptedText = "";
            memberId = memberId.Replace(" ", "+");
            memberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, memberId);

            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<CartCount> res = new CommonResponse<CartCount>();
            CartCount count = new CartCount();
            try
            {

                var result = await _dataRepository.GetCartCount(long.Parse(memberId));

                if (result != null)
                {
                    count.count = Convert.ToInt32(result.Id);
                    res.result = count;
                    res.response = "success";

                }
                else
                {
                    res.response = "error";
                    res.message = "error";
                }
            }
            catch (Exception ex)
            {
                res.response = "error";
                res.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<CartCount>));
            ms = new MemoryStream();
            js.WriteObject(ms, res);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("GetPaymentOption")]
        public async Task<ResponseModel> GetPaymentOption(RequestModel requestModel)
        {
            string EncryptedText = "";
            CardRequestModel model = new CardRequestModel();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
            model = JsonConvert.DeserializeObject<CardRequestModel>(dcdata);

            PaymentResult cardResult = new PaymentResult();
            PaymentData objcardinfo = new PaymentData();
            PaymentResponse commonResponse = new PaymentResponse();
            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<CardData> objres = new CommonResponse<CardData>();
            try
            {

                var cardData = await this._dataRepository.GetPaymentOption(model.Fk_memId, model.Type);

                //commonResponse.result = cardData;
                if (cardData != null && cardData.Count > 0)
                {
                    if (cardData[0].Flag == 1)
                    {
                        commonResponse.response = "success";
                        commonResponse.message = "";
                        cardResult.lstPaymentOption = cardData;
                        commonResponse.result = cardResult;
                        commonResponse.Flag = cardData[0].Flag;

                    }
                    else
                    {
                        commonResponse.response = "success";
                        commonResponse.message = cardData[0].Message;
                        commonResponse.AppStatus = cardData[0].AppStatus;
                        commonResponse.Title = cardData[0].Title;
                        commonResponse.Flag = cardData[0].Flag;
                    }


                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = "Data Not Found ";
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PaymentResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("GetBankDetails")]
        public async Task<ResponseModel> GetBankDetails(string MemberId)
        {
            string EncryptedText = "";
            MemberId = MemberId.Replace(" ", "+");
            MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);

            ResponseModel returnResponse = new ResponseModel();
            CommonResponse<BankDetailResponse> objres = new CommonResponse<BankDetailResponse>();
            try
            {
                var res = await _dataRepository.GetBank(long.Parse(MemberId));
                if (res != null)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<BankDetailResponse>));
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
        [HttpGet("ViewTicket")]
        public async Task<ResponseModel> ViewTicket(string MemberId)
        {
            string EncryptedText = "";
            MemberId = MemberId.Replace(" ", "+");
            MemberId = ApiEncrypt_Decrypt.DecryptString(AESKEY, MemberId);

            
            ResponseModel returnResponse = new ResponseModel();


            CommonResponse<List<ViewTicket>> objres = new CommonResponse<List<ViewTicket>>();
            try
            {
                var res = await _dataRepository.ViewTicket(long.Parse(MemberId));
                if (res != null && res.Count > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {

                    objres.response = "error";
                    objres.message = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<ViewTicket>>));
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

        [HttpGet("BoardingPointChange")]
        public ResponseModel BoardingPointChange(string PNRNo, string boardingStation)
        {
            string EncryptedText = "";
            PNRNo = PNRNo.Replace(" ", "+");
            PNRNo = ApiEncrypt_Decrypt.DecryptString(AESKEY, PNRNo);

            boardingStation = boardingStation.Replace(" ", "+");
            boardingStation = ApiEncrypt_Decrypt.DecryptString(AESKEY, boardingStation);

            ResponseModel returnResponse = new ResponseModel();

            IRCTCModel commonResponse = new IRCTCModel();
            try
            {

                var result = iRCTCService.sendRequest(BoardingPointChangeURL
                    .Replace("[PNRNo]", PNRNo)
                    .Replace("[boardingStation]", boardingStation)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }


            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpGet("ClusterStationSearchEnquiry")]
        public ResponseModel ClusterStationSearchEnquiry(string trainNo, string jrnyDate, string srcStation)
        {
            string EncryptedText = "";
            trainNo = trainNo.Replace(" ", "+");
            trainNo = ApiEncrypt_Decrypt.DecryptString(AESKEY, trainNo);

            jrnyDate = jrnyDate.Replace(" ", "+");
            jrnyDate = ApiEncrypt_Decrypt.DecryptString(AESKEY, jrnyDate);

            srcStation = srcStation.Replace(" ", "+");
            srcStation = ApiEncrypt_Decrypt.DecryptString(AESKEY, srcStation);

            ResponseModel returnResponse = new ResponseModel();

            IRCTCModel commonResponse = new IRCTCModel();
            try
            {

                var result = iRCTCService.sendRequest(ClusterStationSearchEnquiryURL
                    .Replace("[trainNo]", trainNo)
                    .Replace("[jrnyDate]", jrnyDate)
                    .Replace("[srcStation]", srcStation)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("CountryList")]
        public ResponseModel CountryList()
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();

            IRCTCModel commonResponse = new IRCTCModel();
            try
            {

                var result = iRCTCService.sendRequest(CountryListURL
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("GetSMSeMailOTP")]
        public ResponseModel GetSMSeMailOTP(string userLoginId, string otpType)
        {
            string EncryptedText = "";
            userLoginId = userLoginId.Replace(" ", "+");
            userLoginId = ApiEncrypt_Decrypt.DecryptString(AESKEY, userLoginId);

            otpType = otpType.Replace(" ", "+");
            otpType = ApiEncrypt_Decrypt.DecryptString(AESKEY, otpType);

            ResponseModel returnResponse = new ResponseModel();
            IRCTCModel commonResponse = new IRCTCModel();
            try
            {

                var result = iRCTCService.sendRequest(GetSMSeMailOTPURL
                    .Replace("[userLoginId]", userLoginId)
                    .Replace("[otpType]", otpType)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("HistorySearchByTransactionID")]
        public ResponseModel HistorySearchByTransactionID(string userId, string transactionId)
        {
            string EncryptedText = "";
            if(!string.IsNullOrEmpty(userId))
            {
                userId = userId.Replace(" ", "+");
                userId = ApiEncrypt_Decrypt.DecryptString(AESKEY, userId);
            }

            if (!string.IsNullOrEmpty(transactionId))
            {
                transactionId = transactionId.Replace(" ", "+");
                transactionId = ApiEncrypt_Decrypt.DecryptString(AESKEY, transactionId);
            }
            ResponseModel returnResponse = new ResponseModel();

            IRCTCModel commonResponse = new IRCTCModel();
            try
            {

                var result = iRCTCService.sendRequest(HistorySearchByTransactionIDURL
                    .Replace("[userId]", userId)
                    .Replace("[transactionId]", transactionId)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpGet("IRCTCForgotDetails")]
        public ResponseModel IRCTCForgotDetails(string requestType,
           string userLoginId, string email, string mobile, string otpType, string dob)
        {
            string EncryptedText = "";
            IRCTCModel commonResponse = new IRCTCModel();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(requestType))
                {
                    requestType = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestType);
                }
                if (!string.IsNullOrEmpty(userLoginId))
                {
                    userLoginId = ApiEncrypt_Decrypt.DecryptString(AESKEY, userLoginId);
                }
                if (!string.IsNullOrEmpty(email))
                {
                    email = ApiEncrypt_Decrypt.DecryptString(AESKEY, email);
                }
                if (!string.IsNullOrEmpty(mobile))
                {
                    mobile = ApiEncrypt_Decrypt.DecryptString(AESKEY, mobile);
                }
                if (!string.IsNullOrEmpty(otpType))
                {
                    otpType = ApiEncrypt_Decrypt.DecryptString(AESKEY, otpType);
                }
                if(!string.IsNullOrEmpty(dob))
                {
                    dob = ApiEncrypt_Decrypt.DecryptString(AESKEY, dob);
                }
                
                var result = iRCTCService.sendRequest(IRCTCForgotDetailsURL
                    .Replace("[requestType]", requestType)
                    .Replace("[userLoginId]", userLoginId)
                    .Replace("[email]", email)
                    .Replace("[mobile]", mobile)
                    .Replace("[otpType]", otpType)
                    .Replace("[dob]", dob)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpGet("IRCTCPINDetails")]
        public ResponseModel IRCTCPINDetails(string pincode)
        {
            string EncryptedText = "";
            IRCTCModel commonResponse = new IRCTCModel();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(pincode))
                {
                    pincode = ApiEncrypt_Decrypt.DecryptString(AESKEY, pincode);
                }
                var result = iRCTCService.sendRequest(IRCTCPINDetailsURL
                    .Replace("[pincode]", pincode)

                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpGet("NewsAndAlertService")]
        public ResponseModel NewsAndAlertService()
        {
            string EncryptedText = "";
            IRCTCModel commonResponse = new IRCTCModel();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                var result = iRCTCService.sendRequest(NewsAndAlertServiceURL
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);
                    

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("GetDropDown")]
        [Produces("application/json")]
        public async Task<ResponseModel> GetDropDown(RequestModel requestModel)
        {
            
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BindDropDown2Req modelReq = new BindDropDown2Req();
            CommonResponse<List<BindDropDown2>> objres = new CommonResponse<List<BindDropDown2>>();
            try
            {
                EncryptedText = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                modelReq = JsonConvert.DeserializeObject<BindDropDown2Req>(EncryptedText);
                
                var res = await _dataRepository.GetDropDown2(modelReq.ProcId, modelReq.data);
                if (res != null && res.Count() > 0)
                {
                    objres.response = "success";
                    objres.result = res;
                }
                else
                {
                    objres.response = "error";
                    objres.result = null;
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
                objres.result = null;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponse<List<BindDropDown2>>));
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

        [HttpGet("OptVikalp")]
        public ResponseModel OptVikalp(string TransId, string token, string specialTrainFlag)
        {
            string EncryptedText = "";
            IRCTCModel commonResponse = new IRCTCModel();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(TransId))
                {
                    TransId = ApiEncrypt_Decrypt.DecryptString(AESKEY, TransId);
                }
                if (!string.IsNullOrEmpty(token))
                {
                    token = ApiEncrypt_Decrypt.DecryptString(AESKEY, token);
                }
                if (!string.IsNullOrEmpty(specialTrainFlag))
                {
                    specialTrainFlag = ApiEncrypt_Decrypt.DecryptString(AESKEY, specialTrainFlag);
                }
                var result = iRCTCService.sendRequest(OptVikalpURL
                    .Replace("[TransId]", TransId)
                    .Replace("[token]", token)
                     .Replace("[specialTrainFlag]", specialTrainFlag)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("PNREnquiry")]
        public ResponseModel PNREnquiry(string PNRNo)
        {
            string EncryptedText = "";
            IRCTCModel commonResponse = new IRCTCModel();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(PNRNo))
                {
                    PNRNo = ApiEncrypt_Decrypt.DecryptString(AESKEY, PNRNo);
                }
                var result = iRCTCService.sendRequest(PNREnquiryURL.Replace("[PNRNo]", PNRNo)
                 , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("StationCode")]
        public ResponseModel StationCode()
        {
            IRCTCModel commonResponse = new IRCTCModel();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Item item = new Item();
                var list = new List<dynamic>();
                using (StreamReader r = new StreamReader("wwwroot/Statoincode.json"))
                {
                    string json = r.ReadToEnd();
                    string abc = "";
                    item = JsonConvert.DeserializeObject<Item>(json);
                    for (int i = 0; i <= item.StationCode.Count - 1; i++)
                    {
                        list.Add(new { Text = item.StationCode[i].Name, Value = item.StationCode[i].Name.Split("-")[1].Trim() });
                    }

                }
                //foreach (var v in item.StationCode)
                //{

                //    list.Add(new { Text = v, Value = v.ToString().Split("-")[1].Trim() });
                //}
                commonResponse.result = list;
                commonResponse.response = "success";
                commonResponse.message = "success";



            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;


        }


        [HttpGet("TicketBookingDetail")]
        public ResponseModel TicketBookingDetail(string agentTxnId)
        {

            IRCTCModel commonResponse = new IRCTCModel();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(agentTxnId))
                {
                    agentTxnId = ApiEncrypt_Decrypt.DecryptString(AESKEY, agentTxnId);
                }
                var result = iRCTCService.sendRequest(TicketBookingDetailURL
                    .Replace("[agentTxnId]", agentTxnId)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("TicketRefundDetail")]
        public ResponseModel TicketRefundDetail(string reservationId, string agentTxnId)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            IRCTCModel commonResponse = new IRCTCModel();
            try
            {
                if (!string.IsNullOrEmpty(reservationId))
                {
                    reservationId = ApiEncrypt_Decrypt.DecryptString(AESKEY, reservationId);
                }
                if (!string.IsNullOrEmpty(agentTxnId))
                {
                    agentTxnId = ApiEncrypt_Decrypt.DecryptString(AESKEY, agentTxnId);
                }
                var result = iRCTCService.sendRequest(TicketRefundDetailURL.Replace("[reservationId]", reservationId)
                    .Replace("[agentTxnId]", agentTxnId)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpGet("TrainbetweenStations")]
        public ResponseModel TrainbetweenStations(string FromStation, string ToStation, string JourneyDate)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            IRCTCModel commonResponse = new IRCTCModel();
            try
            {
                if (!string.IsNullOrEmpty(FromStation))
                {
                    FromStation = ApiEncrypt_Decrypt.DecryptString(AESKEY, FromStation);
                }
                if (!string.IsNullOrEmpty(ToStation))
                {
                    ToStation = ApiEncrypt_Decrypt.DecryptString(AESKEY, ToStation);
                }
                if (!string.IsNullOrEmpty(JourneyDate))
                {
                    JourneyDate = ApiEncrypt_Decrypt.DecryptString(AESKEY, JourneyDate);
                }
                var result = iRCTCService.sendRequest(TrainbetweenStationsURL.Replace("[FromStation]", FromStation)
                    .Replace("[ToStation]", ToStation)
                    .Replace("[JourneyDate]", JourneyDate), "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpGet("TrainBoardingStationEnquiry")]
        public ResponseModel TrainBoardingStationEnquiry(string TrainNumber,
         string JourneyDate, string FromStation,
         string ToStation, string JourneyClass)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            IRCTCModel commonResponse = new IRCTCModel();
            try
            {
                if(!string.IsNullOrEmpty(TrainNumber))
                {
                    TrainNumber = ApiEncrypt_Decrypt.DecryptString(AESKEY, TrainNumber);
                }
                if (!string.IsNullOrEmpty(JourneyDate))
                {
                    JourneyDate = ApiEncrypt_Decrypt.DecryptString(AESKEY, JourneyDate);
                }
                if (!string.IsNullOrEmpty(FromStation))
                {
                    FromStation = ApiEncrypt_Decrypt.DecryptString(AESKEY, FromStation);
                }
                if (!string.IsNullOrEmpty(ToStation))
                {
                    ToStation = ApiEncrypt_Decrypt.DecryptString(AESKEY, ToStation);
                }
                if (!string.IsNullOrEmpty(JourneyClass))
                {
                    JourneyClass = ApiEncrypt_Decrypt.DecryptString(AESKEY, JourneyClass);
                }
                var result = iRCTCService.sendRequest(TrainBoardingStationEnquiryURL
                    .Replace("[TrainNumber]", TrainNumber)
                    .Replace("[JourneyDate]", JourneyDate)
                    .Replace("[FromStation]", FromStation)
                    .Replace("[ToStation]", ToStation)
                    .Replace("[JourneyClass]", JourneyClass)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("TrainScheduleEnquiry")]
        public ResponseModel TrainScheduleEnquiry(string trainNo, string journeyDate, string startingStationCode)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            IRCTCModel commonResponse = new IRCTCModel();
            try
            {
                trainNo = ApiEncrypt_Decrypt.DecryptString(AESKEY, trainNo);
                journeyDate = ApiEncrypt_Decrypt.DecryptString(AESKEY, journeyDate);
                startingStationCode = ApiEncrypt_Decrypt.DecryptString(AESKEY, startingStationCode);
                var result = iRCTCService.sendRequest(TrainScheduleEnquiryURL
                    .Replace("[trainNo]", trainNo)
                    .Replace("[journeyDate]", journeyDate)
                    .Replace("[startingStationCode]", startingStationCode)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("UserStatus")]
        public ResponseModel UserStatus(string userLoginId)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            IRCTCModel commonResponse = new IRCTCModel();
            try
            {
                if(!string.IsNullOrEmpty(userLoginId))
                {
                    userLoginId = ApiEncrypt_Decrypt.DecryptString(AESKEY, userLoginId);
                }
                
                var result = iRCTCService.sendRequest(UserStatusURL
                    .Replace("[userLoginId]", userLoginId)

                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("VikalpTrainList")]
        public ResponseModel VikalpTrainList(string TransId)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            IRCTCModel commonResponse = new IRCTCModel();
            try
            {
                if(!string.IsNullOrEmpty(TransId))
                {
                    TransId = ApiEncrypt_Decrypt.DecryptString(AESKEY, TransId);
                }
                
                var result = iRCTCService.sendRequest(VikalpTrainListURL
                    .Replace("[TransId]", TransId)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("VerifySMSeMailOTP")]
        public ResponseModel VerifySMSeMailOTP(string userLoginId, string otpType, string emailCode, string smsCode)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            IRCTCModel commonResponse = new IRCTCModel();
            try
            {
                if(!string.IsNullOrEmpty(userLoginId))
                {
                    userLoginId = ApiEncrypt_Decrypt.DecryptString(AESKEY, userLoginId);
                }
                if (!string.IsNullOrEmpty(otpType))
                {
                    otpType = ApiEncrypt_Decrypt.DecryptString(AESKEY, otpType);
                }
                if (!string.IsNullOrEmpty(emailCode))
                {
                    emailCode = ApiEncrypt_Decrypt.DecryptString(AESKEY, emailCode);
                }
                if (!string.IsNullOrEmpty(smsCode))
                {
                    smsCode = ApiEncrypt_Decrypt.DecryptString(AESKEY, smsCode);
                }
                var result = iRCTCService.sendRequest(VerifySMSeMailOTPURL
                    .Replace("[userLoginId]", userLoginId)
                    .Replace("[otpType]", otpType)
                    .Replace("[emailCode]", emailCode)
                    .Replace("[smsCode]", smsCode)
                    , "GET", null, 0
                    );
                string responseText = result.responseText;
                if (result != null && result.statuscode == 200 && !string.IsNullOrEmpty(responseText))
                {
                    JObject jo = JObject.Parse(responseText);
                    commonResponse = iRCTCService.ToApiResponse(jo);

                }
                else
                {
                    commonResponse.response = "error";
                    commonResponse.message = responseText;
                }

            }
            catch (Exception ex)
            {
                commonResponse.response = "error";
                commonResponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(IRCTCModel));
            ms = new MemoryStream();
            js.WriteObject(ms, commonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


    }
}
