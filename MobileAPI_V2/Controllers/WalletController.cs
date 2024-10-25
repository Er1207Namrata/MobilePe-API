using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileAPI_V2.Model.BillPayment;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using MobileAPI_V2.Model;
using MobileAPI_V2.Model.BBPS;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Web;
using Nancy.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using MobileAPI_V2.Models;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using MobileAPI_V2.Services;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using OfficeOpenXml.Style;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Threading.Tasks;
using System.Text;
using static System.Net.WebRequestMethods;
using static MobileAPI_V2.Models.ForgotWalletPin;
using System.Xml.Linq;

using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Razorpay.Api;
using MobileAPI_V2.Model.Travel;
using Nancy;
using AutoMapper.Internal;

//using MobileAPI_V2.Model.Wallet;


namespace MobileAPI_V2.Controllers
{
    [ApiController]
    [Route("api/Auth/")]
    public class WalletController : ControllerBase
    {
        SendSMSEcomm objsms;
        private readonly IDataRepository _dataRepository;
        private readonly IConfiguration _configuration;
        private readonly IDataRepositoryEcomm _dataRepositoryEcom;
        string AESKEYMP = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
        public WalletController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {

            _dataRepository = dataRepository;
            _configuration = configuration;
            objsms = new SendSMSEcomm(_configuration, _dataRepositoryEcom);
        }
        string topaybaseurl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("TopayBaseURL").Value;
        [HttpGet("ValidateData")]
        [Produces("application/json")]
        public ResponseModel ValidateData(string Type, string Value, string Pin, string EntityId)
        {
            string EncryptedText = "";
            string Aeskey = "";
            ValidateResponse objres = new ValidateResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionData exceptionSendOTP = new ExceptionData();
                    exceptionSendOTP.errorCode = "0";
                    exceptionSendOTP.fieldErrors = "Please pass token in header.";
                    objres.exception = exceptionSendOTP;

                }
                else
                {
                    //ExceptionData exceptionSendOTP = new ExceptionData();
                    //exceptionSendOTP.errorCode = "0";
                    //exceptionSendOTP.fieldErrors = "Hold for some time";
                    //objres.exception = exceptionSendOTP;
                    Type = Type.Replace(" ", "+");
                    Value = Value.Replace(" ", "+");
                    EntityId = EntityId.Replace(" ", "+");
                    Pin = string.IsNullOrEmpty(Pin) ? "" : Pin.Replace(" ", "+");
                    string url = topaybaseurl + "ValidateData?Type=" + Type + "&Value=" + Value + "&Pin=" + Pin + "&EntityId=" + EntityId;
                    string result = WalletCommon.HITTOPAYAPI(url, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;

                }
            }
            catch (System.Exception ex)
            {
                ExceptionData exceptionSendOTP = new ExceptionData();
                exceptionSendOTP.errorCode = "0";
                exceptionSendOTP.fieldErrors = "Please pass token in header.";
                objres.exception = exceptionSendOTP;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ValidateResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpPost("SendWalletOTP")]
        [Produces("application/json")]
        public ResponseModel SendOTP(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            SendOTPResponse objres = new SendOTPResponse();
            ResponseModel returnResponse = new ResponseModel();

            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionData exceptionSendOTP = new ExceptionData();
                    exceptionSendOTP.errorCode = "0";
                    exceptionSendOTP.fieldErrors = "Please pass token in header.";
                    objres.exception = exceptionSendOTP;

                }
                else
                {
                    //    ExceptionData exceptionSendOTP = new ExceptionData();
                    //    exceptionSendOTP.errorCode = "0";
                    //    exceptionSendOTP.fieldErrors = "Hold for some time.";
                    //    objres.exception = exceptionSendOTP;

                    string url = topaybaseurl + "SendOTP";
                    string tokenVal = Request.Headers["Token"].ToString();
                    string result = WalletCommon.HITTOPAYAPI(url, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;
                }
            }
            catch (System.Exception ex)
            {
                ExceptionData exceptionSendOTP = new ExceptionData();
                exceptionSendOTP.errorCode = "0";
                exceptionSendOTP.fieldErrors = "Please pass token in header.";
                objres.exception = exceptionSendOTP;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SendOTPResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpPost("RegisterWallet")]
        [Produces("application/json")]
        public ResponseModel RegisterWallet(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            WalletCommon common = new WalletCommon();
            ReigterWalletResponse objres = new ReigterWalletResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionData exceptionSendOTP = new ExceptionData();
                    exceptionSendOTP.errorCode = "0";
                    exceptionSendOTP.fieldErrors = "Please pass token in header.";
                    objres.exception = exceptionSendOTP;

                }
                else
                {
                    string tokenVal = Request.Headers["Token"].ToString();
                    string[] split = tokenVal.Split("-");
                    Aeskey = split[1];

                    //ExceptionData exceptionSendOTP = new ExceptionData();
                    //exceptionSendOTP.errorCode = "0";
                    //exceptionSendOTP.fieldErrors = "Hold for some time";
                    //objres.exception = exceptionSendOTP;

                    string APIurl = topaybaseurl + "RegisterWallet";
                    RegisterWallet registerWallet = new RegisterWallet();

                    RegisterWalletRequest registerWalletR = new RegisterWalletRequest();
                    string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    registerWalletR = JsonConvert.DeserializeObject<RegisterWalletRequest>(dcdata1);
                    var GetAvailablrKirt = _dataRepository.GetAvailableKit(0, 0, 0, registerWalletR.entityId);
                    
                    string cardtype = "";
                    if (GetAvailablrKirt.Result.KitNo == "")
                    {
                        ExceptionData exceptionSendOTP = new ExceptionData();
                        exceptionSendOTP.errorCode = "0";
                        exceptionSendOTP.fieldErrors = "Kit not available";
                        objres.exception = exceptionSendOTP;

                    }
                    else
                    {

                        registerWalletR.kitInfo[0].kitNo = GetAvailablrKirt.Result.KitNo;
                        registerWalletR.IsLoungeCards = GetAvailablrKirt.Result.IsLoungeCards;
                        registerWalletR.kitInfo[0].cardType = GetAvailablrKirt.Result.KitType == "P" ? "PHYSICAL" : "VIRTUAL";
                        registerWalletR.TransactionId = GetAvailablrKirt.Result.PaymentId;
                        string request = ApiEncrypt_Decrypt.EncryptString(Aeskey, JsonConvert.SerializeObject(registerWalletR));
                        string result = WalletCommon.HITTOPAYAPI(APIurl, request, Request.Headers["Token"].ToString());
                        ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                        string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, objres1.Body);
                        objres = JsonConvert.DeserializeObject<ReigterWalletResponse>(dcdata);
                        ReigterWalletResponse reigterWalletResponse = JsonConvert.DeserializeObject<ReigterWalletResponse>(dcdata);
                        common.entityId = registerWalletR.entityId;
                        common.Request = JsonConvert.SerializeObject(registerWalletR);
                        common.Response = result.ToString();
                        common.Type = "RegisterWallet";
                        DataSet Req = common.SaveRequestResponse();
                        if (objres.exception == null)
                        {
                            //if(registerWalletR.kycType== "fullkyc")
                            //{
                            //    GenerateVCIP generateVCIP = new GenerateVCIP();

                            //    APIurl = topaybaseurl + "GenerateVCIP";
                            //    result = WalletCommon.HITTOPAYAPI(APIurl, requestModel.Body, Request.Headers["Token"].ToString());
                            //    ResponseModel kycresponse = JsonConvert.DeserializeObject<ResponseModel>(result);

                            //}
                            registerWallet.cardType = registerWalletR.kitInfo[0].cardType;
                            registerWallet.cardCategory = registerWalletR.kitInfo[0].cardCategory;
                            registerWallet.cardRegStatus = registerWalletR.kitInfo[0].cardRegStatus;
                            registerWallet.Paddress1 = registerWalletR.addressInfo[0].address1;
                            registerWallet.Paddress2 = registerWalletR.addressInfo[0].address2;
                            registerWallet.Paddress3 = registerWalletR.addressInfo[0].address3;
                            registerWallet.Pcity = registerWalletR.addressInfo[0].city;
                            registerWallet.Pstate = registerWalletR.addressInfo[0].state;
                            registerWallet.Pcountry = registerWalletR.addressInfo[0].country;
                            registerWallet.PpinCode = registerWalletR.addressInfo[0].pinCode;
                            registerWallet.Caddress1 = registerWalletR.addressInfo[1].address1;
                            registerWallet.Caddress2 = registerWalletR.addressInfo[1].address2;
                            registerWallet.Caddress3 = registerWalletR.addressInfo[1].address3;
                            registerWallet.Ccity = registerWalletR.addressInfo[1].city;
                            registerWallet.Cstate = registerWalletR.addressInfo[1].state;
                            registerWallet.Ccountry = registerWalletR.addressInfo[1].country;
                            registerWallet.CpinCode = registerWalletR.addressInfo[1].pinCode;
                            registerWallet.contactNo = registerWalletR.communicationInfo[0].contactNo;
                            registerWallet.notification = registerWalletR.communicationInfo[0].notification;
                            registerWallet.emailId = registerWalletR.communicationInfo[0].emailId;
                            registerWallet.documentType = registerWalletR.kycInfo[0].documentType;
                            registerWallet.documentNo = registerWalletR.kycInfo[0].documentNo;
                            registerWallet.documentExpiry = registerWalletR.kycInfo[0].documentExpiry;
                            registerWallet.dob = registerWalletR.dateInfo[0].date;
                            registerWallet.ismerchant = 0;
                            registerWallet.KITNO = objres.result.KitNo;
                            registerWallet.Merchant = "";
                            registerWallet.token = objres.result.token;
                            registerWallet.entityId = registerWalletR.entityId;
                            registerWallet.channelName = registerWalletR.channelName;
                            registerWallet.entityType = registerWalletR.entityType;
                            registerWallet.businessType = registerWalletR.businessType;
                            registerWallet.businessId = registerWalletR.businessId;
                            registerWallet.title = registerWalletR.title;
                            registerWallet.firstName = registerWalletR.firstName;
                            registerWallet.middleName = registerWalletR.middleName;
                            registerWallet.lastName = registerWalletR.lastName;
                            registerWallet.gender = registerWalletR.gender;
                            registerWallet.isNRICustomer = registerWalletR.isNRICustomer;
                            registerWallet.isMinor = registerWalletR.isMinor;
                            registerWallet.isDependant = registerWalletR.isDependant;
                            registerWallet.maritalStatus = registerWalletR.maritalStatus;
                            registerWallet.countryCode = registerWalletR.countryCode;
                            registerWallet.employmentIndustry = registerWalletR.employmentIndustry;
                            registerWallet.employmentType = registerWalletR.employmentType;
                            registerWallet.plasticCode = registerWalletR.plasticCode;
                            registerWallet.password = registerWalletR.password;
                            registerWallet.pin = registerWalletR.pin;
                            registerWallet.DeviceId = registerWallet.DeviceId;
                            registerWallet.FileBaseToken = registerWallet.FileBaseToken;
                            registerWallet.kycType = registerWalletR.kycType;

                            DataSet dataSet1 = registerWallet.SaveRegistration();
                        }
                    }


                }
            }
            catch (System.Exception ex)
            {
                ExceptionData exceptionSendOTP = new ExceptionData();
                exceptionSendOTP.errorCode = "0";
                exceptionSendOTP.fieldErrors = ex.Message;
                objres.exception = exceptionSendOTP;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ReigterWalletResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpPost("WalletTopupNew")]
        [Produces("application/json")]
        public ResponseModel WalletTopupNew(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            WalletTopupResponse objres = new WalletTopupResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionData exceptionSendOTP = new ExceptionData();
                    exceptionSendOTP.errorCode = "0";
                    exceptionSendOTP.fieldErrors = "Please pass token in header.";
                    objres.exception = exceptionSendOTP;

                }
                else
                {
                    string tokenVal = Request.Headers["Token"].ToString();
                    string[] split = tokenVal.Split("-");
                    Aeskey = split[1];

                    //ExceptionData exceptionSendOTP = new ExceptionData();
                    //exceptionSendOTP.errorCode = "0";
                    //exceptionSendOTP.fieldErrors = "Hold for some time";
                    //objres.exception = exceptionSendOTP;
                    WalletTopup walletTopup = new WalletTopup();
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    walletTopup = JsonConvert.DeserializeObject<WalletTopup>(dcdata);
                    string PaymentId = walletTopup.externalTransactionId;
                    walletTopup.externalTransactionId = walletTopup.externalTransactionId.Replace("pay_", "");
                    string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, JsonConvert.SerializeObject(walletTopup));

                    string APIurl = topaybaseurl + "WalletTopup";
                    string result = WalletCommon.HITTOPAYAPI(APIurl, Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    string dcdata2 = ApiEncrypt_Decrypt.DecryptString(Aeskey, objres1.Body);
                    objres = JsonConvert.DeserializeObject<WalletTopupResponse>(dcdata2);
                    if (objres.exception == null)
                    {
                        walletTopup.TxId = objres.result.txId;
                        walletTopup.PaymentId = PaymentId;
                        walletTopup.IsHold = 0;
                        DataSet dataSet11 = walletTopup.SaveWalletTopup();
                    }
                    else
                    {
                        if (objres.exception.errorCode == "Y3009" || objres.exception.errorCode == "Y3008" || objres.exception.errorCode == "Y1012" || objres.exception.errorCode == "Y3010" || objres.exception.errorCode == "S309" || objres.exception.errorCode == "S309\t\t")
                        {
                            walletTopup.IsHold = 1;
                            DataSet dataSet11 = walletTopup.SaveWalletTopup();
                        }
                    }
                    return objres1;


                }
            }
            catch (System.Exception ex)
            {
                ExceptionData exceptionSendOTP = new ExceptionData();
                exceptionSendOTP.errorCode = "0";
                exceptionSendOTP.fieldErrors = ex.Message;
                objres.exception = exceptionSendOTP;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(WalletTopupResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpPost("WalletDebitNew")]
        [Produces("application/json")]
        public async Task<ResponseModel> WalletDebitNew(RequestModel requestModel)
        {



            string EncryptedText = "";
            string Aeskey = "";
            WalletTopupResponse objres = new WalletTopupResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionData exceptionSendOTP = new ExceptionData();
                    exceptionSendOTP.errorCode = "0";
                    exceptionSendOTP.fieldErrors = "Please pass token in header.";
                    objres.exception = exceptionSendOTP;

                }
                else
                {
                    string tokenVal = Request.Headers["Token"].ToString();
                    string[] split = tokenVal.Split("-");
                    Aeskey = split[1];

                    //ExceptionData exceptionSendOTP = new ExceptionData();
                    //exceptionSendOTP.errorCode = "0";
                    //exceptionSendOTP.fieldErrors = "Hold for some time";
                    //objres.exception = exceptionSendOTP;
                    WalletTopup walletTopup = new WalletTopup();
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    walletTopup = JsonConvert.DeserializeObject<WalletTopup>(dcdata);
                    if (walletTopup.Type.ToLower() == "mobile recharge" || walletTopup.Type.ToLower() == "dth recharge")
                    {
                        walletTopup.amount = (walletTopup.amount - (walletTopup.amount * 1) / 100);
                    }
                    string BodyTopup = ApiEncrypt_Decrypt.EncryptString(Aeskey, JsonConvert.SerializeObject(walletTopup));
                    string APIurl = topaybaseurl + "WalletDebit";
                    string result = WalletCommon.HITTOPAYAPI(APIurl, BodyTopup, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    string dcdata111 = ApiEncrypt_Decrypt.DecryptString(Aeskey, objres1.Body);
                    objres = JsonConvert.DeserializeObject<WalletTopupResponse>(dcdata111);

                    if (objres.exception == null)
                    {
                        walletTopup.TxId = objres.result.txId;

                        DataSet dataSet1 = walletTopup.WalletDebit();
                        var GetAvailablrKirt = _dataRepository.GetAvailableKit(walletTopup.OldCardId, walletTopup.Fk_CardId, walletTopup.Fk_MemId, "");
                        if (walletTopup.Type.ToLower() == "addoncard" || walletTopup.Type.ToLower() == "addonphoto")
                        {
                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");

                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";

                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));


                        }
                        else if (walletTopup.Type.ToLower() == "topaycard" || walletTopup.Type.ToLower() == "topaycardphoto")
                        {


                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");
                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";


                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));
                            //DataSet dataSet11 = walletTopup.UpdateMemberCard(walletTopup.externalTransactionId.ToString(), walletTopup.externalTransactionId.ToString(), 1, "", 0, "topaycard");
                            ////var result21 = _dataRepository.UpdateMemberCard(walletTopup.externalTransactionId.ToString(), walletTopup.externalTransactionId.ToString(), 1, "", 0);
                            //string Remark = "Card purchased payment success";
                            //var transactiondata = _dataRepository.SaveTransactionData(walletTopup.Fk_MemId, walletTopup.externalTransactionId.ToString(), "Wallet Debit", Remark, "cardcost", walletTopup.amount, "", "", "", "");
                        }
                        else if (walletTopup.Type.ToLower() == "imagecard")
                        {

                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");

                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";


                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));
                        }
                        else if (walletTopup.Type.ToLower() == "upgradetopaycard" || walletTopup.Type.ToLower() == "upgradephoto")
                        {

                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");

                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";


                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));
                            //var result21 = _dataRepository.UpdateMemberCard(walletTopup.externalTransactionId.ToString(), walletTopup.externalTransactionId.ToString(), 2, GetAvailablrKirt.Result.KitNo, cardId);

                        }
                        else if (walletTopup.Type.ToLower() == "flightbooking")
                        {

                            DataTable dt = new DataTable();
                            dt.Columns.Add("JourneyType");
                            dt.Columns.Add("TripIndicator");
                            dt.Columns.Add("PNR");
                            dt.Columns.Add("Origin");
                            dt.Columns.Add("Destination");
                            dt.Columns.Add("AirlineCode");
                            dt.Columns.Add("LastTicketDate");
                            dt.Columns.Add("NonRefundable");


                            DataTable dtSegment = new DataTable();
                            dtSegment.Columns.Add("AirportCode");
                            dtSegment.Columns.Add("AirportName");
                            dtSegment.Columns.Add("Terminal");
                            dtSegment.Columns.Add("CityCode");
                            dtSegment.Columns.Add("CityName");
                            dtSegment.Columns.Add("CountryCode");
                            dtSegment.Columns.Add("CountryName");
                            dtSegment.Columns.Add("DepTime");
                            dtSegment.Columns.Add("Destination");
                            dtSegment.Columns.Add("ArrivalTime");


                            DataTable dtSegmentR = new DataTable();
                            dtSegmentR.Columns.Add("AirportCode");
                            dtSegmentR.Columns.Add("AirportName");
                            dtSegmentR.Columns.Add("Terminal");
                            dtSegmentR.Columns.Add("CityCode");
                            dtSegmentR.Columns.Add("CityName");
                            dtSegmentR.Columns.Add("CountryCode");
                            dtSegmentR.Columns.Add("CountryName");
                            dtSegmentR.Columns.Add("DepTime");

                            TravelSaveResponse travelSaveResponse = new TravelSaveResponse();
                            ResultTravel resultTravel = JsonConvert.DeserializeObject<ResultTravel>(walletTopup.request);
                            for (int i = 0; i <= resultTravel.data.Count - 1; i++)
                            {
                                string JourneyType = "";
                                if (resultTravel.data.Count == 2)
                                {
                                    JourneyType = "2";
                                }
                                else
                                {
                                    JourneyType = "1";
                                }


                                if (resultTravel.data[i].isLcc == true)
                                {

                                    ResultTravelForLCC resultTravelForLCC = JsonConvert.DeserializeObject<ResultTravelForLCC>(walletTopup.request);
                                    string ResultIndex = resultTravelForLCC.data[i].ResultIndex;

                                    travelSaveResponse.IsSuccess = 2;
                                    travelSaveResponse.Message = "Flight booking is pending";
                                    travelSaveResponse.TraceId = resultTravelForLCC.data[i].TraceId;
                                    travelSaveResponse.Fk_MemId = int.Parse(walletTopup.Fk_MemId.ToString());
                                    travelSaveResponse.IsLcc = "1";
                                    travelSaveResponse.JourneyDate = resultTravelForLCC.data[i].Flight[0].JourneyDate;
                                    travelSaveResponse.Class = resultTravelForLCC.data[i].Flight[0].Class;
                                    int BaggageCount = resultTravelForLCC.data[i].Passengers[0].Baggage.Count;
                                    string Origin = resultTravelForLCC.data[i].Flight[0].Origin;
                                    string Desitination = resultTravelForLCC.data[i].Flight[0].Destination;
                                    dt.Rows.Add(JourneyType, "", "", Origin, Desitination, "", "", "");
                                    travelSaveResponse.tblFlightItinerary = dt;

                                    for (int k = 0; k < resultTravelForLCC.data[i].Flight.Count; k++)
                                    {
                                        dtSegment.Rows.Add(resultTravelForLCC.data[i].Flight[k].AirlineCode, resultTravelForLCC.data[i].Flight[k].FlightNumber, "", resultTravelForLCC.data[i].Flight[k].Origin, resultTravelForLCC.data[i].Flight[k].Origin, "", "", resultTravelForLCC.data[i].Flight[k].DepartureTime, resultTravelForLCC.data[i].Flight[k].Destination,
                                            resultTravelForLCC.data[i].Flight[k].ArrivalTime);
                                        //dtSegmentR.Rows.Add(resultTravelForLCC.data[i].Flight[k].AirlineCode, resultTravelForLCC.data[i].Flight[k].FlightNumber, "", resultTravelForLCC.data[i].Flight[k].Destination, resultTravelForLCC.data[i].Flight[k].Destination, "", "", resultTravelForLCC.data[i].Flight[k].DepartureTime);

                                    }

                                    travelSaveResponse.dtSegment = dtSegment;
                                    travelSaveResponse.dtSegmentR = dtSegmentR;
                                    travelSaveResponse.OrderId = walletTopup.externalTransactionId;
                                    var SaveBookingResponse = _dataRepository.SaveBookingResponse(travelSaveResponse);





                                }
                                else
                                {
                                    ResultTravelForLCC resultTravelForLCC = JsonConvert.DeserializeObject<ResultTravelForLCC>(walletTopup.request);
                                    string ResultIndex = resultTravelForLCC.data[i].ResultIndex;
                                    travelSaveResponse.IsSuccess = 2;
                                    travelSaveResponse.Message = "Flight booking is pending";
                                    travelSaveResponse.TraceId = resultTravelForLCC.data[i].TraceId;
                                    travelSaveResponse.Fk_MemId = int.Parse(walletTopup.Fk_MemId.ToString());
                                    travelSaveResponse.IsLcc = "0";
                                    travelSaveResponse.JourneyDate = resultTravelForLCC.data[i].Flight[0].JourneyDate;
                                    travelSaveResponse.Class = resultTravelForLCC.data[i].Flight[0].Class;
                                    int BaggageCount = resultTravelForLCC.data[i].Passengers[0].Baggage.Count;
                                    string Origin = resultTravelForLCC.data[i].Passengers[0].Baggage[0].Origin;
                                    string Desitination = resultTravelForLCC.data[i].Passengers[BaggageCount].Baggage[0].Destination;
                                    dt.Rows.Add(JourneyType, "", "", Origin, Desitination, "", "", "");
                                    travelSaveResponse.tblFlightItinerary = dt;

                                    for (int k = 0; k < resultTravelForLCC.data[i].Flight.Count; k++)
                                    {
                                        dtSegment.Rows.Add(resultTravelForLCC.data[i].Flight[k].AirlineCode, resultTravelForLCC.data[i].Flight[k].FlightNumber, "", resultTravelForLCC.data[i].Flight[k].Origin, resultTravelForLCC.data[i].Flight[k].Origin, "", "", resultTravelForLCC.data[i].Flight[k].DepartureTime);
                                        //dtSegmentR.Rows.Add(resultTravelForLCC.data[i].Flight[k].AirlineCode, resultTravelForLCC.data[i].Flight[k].FlightNumber, "", resultTravelForLCC.data[i].Flight[k].Destination, resultTravelForLCC.data[i].Flight[k].Destination, "", "", resultTravelForLCC.data[i].Flight[k].DepartureTime);

                                    }

                                    travelSaveResponse.dtSegment = dtSegment;
                                    travelSaveResponse.dtSegmentR = dtSegmentR;
                                    travelSaveResponse.OrderId = walletTopup.externalTransactionId;
                                    var SaveBookingResponse = _dataRepository.SaveBookingResponse(travelSaveResponse);

                                }
                            }


                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");

                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";


                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));
                            //var result21 = _dataRepository.UpdateMemberCard(walletTopup.externalTransactionId.ToString(), walletTopup.externalTransactionId.ToString(), 2, GetAvailablrKirt.Result.KitNo, cardId);

                        }
                        else if (walletTopup.Type.ToLower() == "mobile recharge" || walletTopup.Type.ToLower() == "dth recharge")
                        {
                            
                            string PaymentId = walletTopup.externalTransactionId;
                            VenusRechargeResponse response = new VenusRechargeResponse();
                            string serviceType = "";
                            if (walletTopup.Type.ToLower() == "mobile" || walletTopup.Type.ToLower() == "mobile recharge")
                            {
                                serviceType = "MR";
                            }
                            else if (walletTopup.Type.ToLower() == "dth" || walletTopup.Type.ToLower() == "dth recharge")
                            {
                                serviceType = "DH";
                            }
                            RequestMobile requestMobile = JsonConvert.DeserializeObject<RequestMobile>(walletTopup.request);
                            string URL = "http://venusrecharge.co.in/Transaction.aspx?authkey=10036&authpass=MOBILEPE@613&mobile=[number]&amount=[recharge_amount]&opcode=[operator_code]&Merchantrefno=[transaction_id]&ServiceType=[ServiceType]";
                            URL = URL.Replace("[number]", requestMobile.Number).Replace("[recharge_amount]", requestMobile.Amount.ToString()).Replace("[operator_code]", requestMobile.OperatorCode).Replace("[transaction_id]", requestMobile.merchantInfoTxn).Replace("[ServiceType]", serviceType);
                            var result1 = CommonJsonPostRequest.CommonSendRequest(URL, "GET", null);
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(result1);
                            string json = JsonConvert.SerializeXmlNode(doc);
                            response = JsonConvert.DeserializeObject<VenusRechargeResponse>(json);
                            if (response != null && response.Response != null)
                            {
                                string Remark = "";
                                string status = "";
                                if (response.Response.ResponseStatus.ToLower() == "success")
                                {
                                    Remark = "Your recharge is SUCCESS";
                                    status = "SUCCESS";
                                }
                                else if (response.Response.ResponseStatus.ToLower() == "pending")
                                {
                                    Remark = "Mobile transaction pending";
                                    status = "Pending";
                                }
                                else if (response.Response.ResponseStatus.ToLower() == "failed")
                                {
                                    #region RefundAmount
                                    WalletTopup walletRecharge = new WalletTopup();
                                    walletRecharge.fromEntityId = "MOBILEPE01";
                                    walletRecharge.toEntityId = walletTopup.fromEntityId;
                                    walletRecharge.yapcode = "1234";
                                    walletRecharge.productId = "GENERAL";
                                    walletRecharge.description = "Wallet Refund against " + walletTopup.Type.ToLower();
                                    walletRecharge.amount = walletTopup.amount;
                                    walletRecharge.transactionType = "M2C";
                                    walletRecharge.business = "MOBILEPE";
                                    walletRecharge.businessEntityId = "MOBILEPE";
                                    walletRecharge.transactionOrigin = walletTopup.transactionOrigin;
                                    walletRecharge.externalTransactionId = "refnd_" + walletTopup.externalTransactionId;
                                    //string obj1 = "{\"CUSTOMERMOBILENO\":\"" + walletRecharge.CUSTOMERMOBILENO + "\",\"TOPUPAMOUNT\":\"" + objorder.Amount.ToString() + "\",\"TRANSACTIONID\":\"" + walletRecharge.TRANSACTIONID + "\",\"memberId\":\"" + walletRecharge.memberId + "\"}";

                                    //string result = Common.HITAPI("https://apiv2.mobilepe.co.in/api/auth/V2/WalletTopup", walletRecharge);

                                    string Body = ApiEncrypt_Decrypt.EncryptString("EE5A2360@C711@40", JsonConvert.SerializeObject(walletRecharge));

                                    URL = "https://apiv2.mobilepe.co.in/api/auth/" + "WalletTopupNew";

                                    result = WalletCommon.HITTOPAYAPI(URL, Body, "D99DD6FA-EE5A2360@C711@40-5B018B98");

                                    var data = _dataRepository.UpdateRefundPaymentStaus(PaymentId, walletRecharge.externalTransactionId, "Wallet", "Refunded");

                                    #endregion RefundAmount
                                }
                                var transactiondata = _dataRepository.SaveTransactionData(walletTopup.Fk_MemId, walletTopup.externalTransactionId.ToString(), walletTopup.externalTransactionId.ToString(), Remark, walletTopup.Type, walletTopup.amount, response.Response.OperatorTxnID, status, requestMobile.Number, requestMobile.OperatorCode, json);

                            }

                        }
                        else if (walletTopup.Type.ToLower() == "electricity" || walletTopup.Type.ToLower() == "gas" || walletTopup.Type.ToLower() == "water" || walletTopup.Type.ToLower() == "insurance" || walletTopup.Type.ToLower() == "lpg" || walletTopup.Type.ToLower() == "cabletv")
                        {

                            BillPaymentResponse billPay = JsonConvert.DeserializeObject<BillPaymentResponse>(walletTopup.request);
                            billPay.PaymentId = walletTopup.externalTransactionId;
                            billPay.Fk_MemId = walletTopup.Fk_MemId.ToString();
                            billPay.TransId = walletTopup.externalTransactionId;
                            billPay.RechargeType = walletTopup.Type;
                            MobileAPI_V2.Model.Common common = new MobileAPI_V2.Model.Common();
                            string Body = ApiEncrypt_Decrypt.EncryptString("MOBILEP@#@132EYZ", JsonConvert.SerializeObject(billPay));
                            string resultdata = MobileAPI_V2.Model.Common.HITAPIBillPay("https://apiv2.mobilepe.co.in/api/auth/BillPayment", Body);

                        }
                        else if (walletTopup.Type.ToLower() == "voucher")
                        {
                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");

                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";


                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));

                        }
                        else if (walletTopup.Type.ToLower() == "mobilepemall")
                        {
                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");

                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";


                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));

                        }
                        else if (walletTopup.Type.ToLower() == "addfund|reloadable")
                        {
                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");

                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";


                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));

                        }
                        else if (walletTopup.Type.ToLower() == "cardrequest")
                        {
                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");

                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";


                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));

                        }
                        else if (walletTopup.Type.ToLower() == "DDOCARDREQUEST")
                        {
                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");

                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";


                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));

                        }
                        else if (walletTopup.Type.Contains("BBPS_"))
                        {
                            WalletCommon common = new WalletCommon();
                            PineCardRequest pineCardRequest = new PineCardRequest();
                            pineCardRequest.contains = new List<string>();
                            pineCardRequest.payload = new Payload();
                            pineCardRequest.payload.payment = new PaymentWebHook();
                            pineCardRequest.payload.payment.entity = new WebHookEntity();
                            pineCardRequest.entity = "event";
                            pineCardRequest.account_id = "acc_Etrlio6flbzRY2";
                            pineCardRequest.@event = "payment.captured";
                            pineCardRequest.contains.Add("payment");

                            pineCardRequest.payload.payment.entity.id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.entity = "payment";
                            pineCardRequest.payload.payment.entity.amount = walletTopup.amount;
                            pineCardRequest.payload.payment.entity.currency = "INR";
                            pineCardRequest.payload.payment.entity.status = "captured";
                            pineCardRequest.payload.payment.entity.order_id = walletTopup.externalTransactionId;
                            pineCardRequest.payload.payment.entity.invoice_id = "";
                            pineCardRequest.payload.payment.entity.international = false;
                            pineCardRequest.payload.payment.entity.method = "Wallet";
                            pineCardRequest.payload.payment.entity.amount_refunded = 0;
                            pineCardRequest.payload.payment.entity.refund_status = "";
                            pineCardRequest.payload.payment.entity.captured = true;
                            pineCardRequest.payload.payment.entity.description = "";


                            var resultplastic = common.sendUpdateEntity("https://api.mobilepe.co.in/Webhook/Payment", JsonConvert.SerializeObject(pineCardRequest));

                        }

                        //DataSet dataSet1 = walletTopup.WalletDebit();
                    }
                    return objres1;



                }
            }
            catch (System.Exception ex)
            {
                ExceptionData exceptionSendOTP = new ExceptionData();
                exceptionSendOTP.errorCode = "0";
                exceptionSendOTP.fieldErrors = ex.Message;
                objres.exception = exceptionSendOTP;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(WalletTopupResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }
        [HttpGet("WalletTransactions")]
        [Produces("application/json")]
        public ResponseModel WalletTransactions(string EntityId, string pageSize)
        {
            string EncryptedText = "";
            string Aeskey = "";
            WalletTransactionResponse objres = new WalletTransactionResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionData exceptionSendOTP = new ExceptionData();
                    exceptionSendOTP.errorCode = "0";
                    exceptionSendOTP.fieldErrors = "Please pass token in header.";
                    objres.exception = exceptionSendOTP;

                }
                else
                {
                    string tokenVal = Request.Headers["Token"].ToString();
                    string[] split = tokenVal.Split("-");
                    Aeskey = split[1];
                    //EntityId = ApiEncrypt_Decrypt.DecryptString(Aeskey, EntityId);
                    //pageSize = ApiEncrypt_Decrypt.DecryptString(Aeskey, pageSize);
                    string APIurl = topaybaseurl + "WalletTransactions?EntityId=" + EntityId + "&pageSize=" + pageSize;
                    string result = WalletCommon.HITTOPAYAPI(APIurl, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;
                }
            }
            catch (System.Exception ex)
            {
                ExceptionData exceptionSendOTP = new ExceptionData();
                exceptionSendOTP.errorCode = "0";
                exceptionSendOTP.fieldErrors = ex.Message;
                objres.exception = exceptionSendOTP;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(WalletTransactionResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpGet("WalletBalance")]
        [Produces("application/json")]
        public ResponseModel WalletBalance(string EntityId)
        {
            string EncryptedText = "";
            string Aeskey = "";
            WalletBalResponse objres = new WalletBalResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionData exceptionSendOTP = new ExceptionData();
                    exceptionSendOTP.errorCode = "0";
                    exceptionSendOTP.fieldErrors = "Please pass token in header.";
                    objres.exception = exceptionSendOTP;

                }
                else
                {
                    string tokenVal = Request.Headers["Token"].ToString();
                    string[] split = tokenVal.Split("-");
                    Aeskey = split[1];
                    //EntityId = ApiEncrypt_Decrypt.DecryptString(Aeskey, EntityId);

                    string APIurl = topaybaseurl + "WalletBalance?EntityId=" + EntityId;
                    string result = WalletCommon.HITTOPAYAPI(APIurl, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;
                }
            }
            catch (System.Exception ex)
            {
                ExceptionData exceptionSendOTP = new ExceptionData();
                exceptionSendOTP.errorCode = "0";
                exceptionSendOTP.fieldErrors = ex.Message;
                objres.exception = exceptionSendOTP;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(WalletBalResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GenerateVCIP")]
        [Produces("application/json")]
        public ResponseModel GenerateVCIP(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            GenerateVCIPResponse objres = new GenerateVCIPResponse();
            ResponseModel returnResponse = new ResponseModel();

            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    objres.respcode = "0";
                    objres.respdesc = "Please pass token";

                }
                else
                {
                    string tokenVal = Request.Headers["Token"].ToString();
                    string[] split = tokenVal.Split("-");
                    Aeskey = split[1];
                    GenerateVCIP generateVCIP = new GenerateVCIP();
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    generateVCIP = JsonConvert.DeserializeObject<GenerateVCIP>(dcdata);
                    string APIurl = topaybaseurl + "GenerateVCIP";
                    string result = WalletCommon.HITTOPAYAPI(APIurl, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;


                }
            }
            catch (System.Exception ex)
            {
                objres.respcode = "0";
                objres.respdesc = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GenerateVCIPResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpPost("CustomerDashBoard")]
        [Produces("application/json")]
        public ResponseModel CustomerDashBoard(RequestModel requestModel)
        {
            WalletDashboard dashBoard = new WalletDashboard();
            string EncryptedText = "";
            string Aeskey = "";
            DashBoardResponse objres = new DashBoardResponse();
            ResponseModel returnResponse = new ResponseModel();

            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    objres.Status = 0;
                    objres.Message = "Please pass token";

                }
                else
                {
                    string tokenVal = Request.Headers["Token"].ToString();
                    string[] split = tokenVal.Split("-");
                    Aeskey = split[1];

                    string APIurl = topaybaseurl + "CustomerDashBoard";
                    string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    WalletDashboard dashboard1 = JsonConvert.DeserializeObject<WalletDashboard>(dcdata1);
                    string result = WalletCommon.HITTOPAYAPI(APIurl, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, objres1.Body);
                    DashBoardResponse dashBoardResponse = JsonConvert.DeserializeObject<DashBoardResponse>(dcdata);
                    dashBoard.entityId = dashboard1.entityId;
                    DataSet dataSet = dashBoard.CustomerDashBoard();
                    LstDashBoard dashBoardList1 = new LstDashBoard();

                    dashBoardResponse.Response.IsOldCard = int.Parse(dataSet.Tables[0].Rows[0]["IsOldCard"].ToString());
                    dashBoardResponse.Response.IsCardApply = int.Parse(dataSet.Tables[0].Rows[0]["IsCardApply"].ToString());
                    dashBoardResponse.Response.Isupgrade = int.Parse(dataSet.Tables[0].Rows[0]["Isupgrade"].ToString());
                    dashBoardResponse.Response.KYCPaymentStatus = dataSet.Tables[0].Rows[0]["KYCPaymentStatus"].ToString();
                    dashBoardResponse.Response.KYCText = dataSet.Tables[0].Rows[0]["KYCText"].ToString();
                    dashBoardResponse.Response.KYCPayment = decimal.Parse(dataSet.Tables[0].Rows[0]["KYCPayment"].ToString());

                    objres = dashBoardResponse;





                }
            }
            catch (System.Exception ex)
            {
                objres.Status = 0;
                objres.Message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DashBoardResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpPost("UpdateCustomerVCIPStatus")]
        [Produces("application/json")]
        public ResponseModel UpdateCustomerVCIPStatus(UpdateCustomerVCIPStatus updateCustomerVCIPStatus)
        {
            DataSet dataSet = updateCustomerVCIPStatus.UpdateVCIPStatus();

            return null;

        }
        [HttpPost("UpdateCustomerVCIPId")]
        [Produces("application/json")]
        public ResponseModel UpdateCustomerVCIPId(UpdateCustomerVCIPStatus updateCustomerVCIPStatus)
        {
            DataSet dataSet = updateCustomerVCIPStatus.UpdateVCIPStatus();

            return null;

        }

        [HttpPost("ChangeWalletPin")]
        [Produces("application/json")]
        public ResponseModel ChangeWalletPin(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            ChangePasswordResponse objres = new ChangePasswordResponse();

            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionChangePassword exceptionChangePassword = new ExceptionChangePassword();
                    exceptionChangePassword.errorCode = "0";
                    exceptionChangePassword.message = "Please pass token in header!";
                    objres.exception = exceptionChangePassword;
                }
                else
                {


                    string tokanVal = Request.Headers["Token"].ToString();
                    string[] split = tokanVal.Split("-");
                    Aeskey = split[1];
                    ChangePasswordWallet changePassword = new ChangePasswordWallet();
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    changePassword = JsonConvert.DeserializeObject<ChangePasswordWallet>(dcdata);
                    DataSet ds = changePassword.GetChangePin();
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            objres.response = "success";
                            objres.status = "1";
                            objres.message = "Pin changed successfully!";
                            string url = topaybaseurl + "ChangeWalletPin";
                            string tokenVal = Request.Headers["Token"].ToString();
                            string result = WalletCommon.HITTOPAYAPI(url, requestModel.Body, Request.Headers["Token"].ToString());
                            ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                            return objres1;
                        }
                        else
                        {
                            objres.response = "failed";
                            objres.status = "0";
                            objres.message = "Please enter correct pin";
                        }
                    }
                    else
                    {
                        objres.response = "failed";
                        objres.status = "0";
                        objres.message = "Please enter correct pin";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionChangePassword exception = new ExceptionChangePassword();
                exception.errorCode = "0";
                exception.message = ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ChangePasswordResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("CreateDigiLockerRequest")]
        [Produces("application/json")]
        public ResponseModel CreateDigiLockerRequest(string EntityId)
        {
            string EncryptedText = "";
            string Aeskey = "";
            GenerateVCIPResponse objres = new GenerateVCIPResponse();
            ResponseModel returnResponse = new ResponseModel();

            try
            {

                string APIurl = topaybaseurl + "CreateDigiLockerRequest?EntityId=" + EntityId;
                string result = WalletCommon.HITTOPAYAPI(APIurl, Request.Headers["Token"].ToString());
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                return objres1;



            }
            catch (System.Exception ex)
            {
                objres.respcode = "0";
                objres.respdesc = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GenerateVCIPResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpPost("OCR")]
        [Produces("application/json")]
        public ResponseModel OCR(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            OCRResponse objres = new OCRResponse();

            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    objres.respcode = "0";
                    objres.respdesc = "Please pass token";
                }
                else
                {

                    string url = topaybaseurl + "OCR";
                    string tokenVal = Request.Headers["Token"].ToString();
                    string result = WalletCommon.HITTOPAYAPI(url, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;


                }
            }
            catch (Exception ex)
            {
                objres.respcode = "0";
                objres.respdesc = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(OCRResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FetchTransactionStatus")]
        [Produces("application/json")]
        public ResponseModel FetchTransactionStatus(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            FetchTransResponse objres = new FetchTransResponse();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    objres.respcode = "0";
                    objres.respdesc = "Please pass token";
                }
                else
                {

                    string url = topaybaseurl + "FetchTransactionStatus";
                    string tokenVal = Request.Headers["Token"].ToString();
                    string result = WalletCommon.HITTOPAYAPI(url, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;


                }
            }
            catch (Exception ex)
            {
                objres.respcode = "0";
                objres.respdesc = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ChangePasswordResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }



        [HttpPost("UpdateEntity")]
        [Produces("application/json")]
        public ResponseModel UpdateEntity(UpdateEntity updateEntity)
        {
            string EncryptedText = "";
            string Aeskey = "";
            WalletCommon common = new WalletCommon();

            FetchTransResponse objres = new FetchTransResponse();
            ResponseModel returnResponse = new ResponseModel();
            DataContractJsonSerializer js;
            MemoryStream ms;
            try
            {
                string url = topaybaseurl + "UpdateEntity";
                string tokenVal = Request.Headers["Token"].ToString();
                string result = WalletCommon.HITAPISimple(url, JsonConvert.SerializeObject(updateEntity), "", "");
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);


                common.entityId = updateEntity.entityId;
                common.Request = JsonConvert.SerializeObject(updateEntity);
                common.Response = result.ToString();
                //common.EncryptedText = EncryptedText;
                common.Type = "UpdateEntity";
                DataSet Req = common.SaveRequestResponse();
                return objres1;

            }
            catch (System.Exception ex)
            {
                objres.respcode = "0";
                objres.respdesc = ex.Message;
            }
            string CustData = "";
            //DataContractJsonSerializer js;
            //MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FetchTransResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }


        [HttpPost("ApplyCard")]
        [Produces("application/json")]
        public ResponseModel ApplyCard(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            ApplyCardResponse objres = new ApplyCardResponse();
            ResponseModel returnResponse = new ResponseModel();


            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {

                    //ExceptionData exceptionSendOTP = new ExceptionData();
                    //exceptionSendOTP.errorCode = "0";
                    //exceptionSendOTP.fieldErrors = "Please pass token in header.";
                    //objres.exception = exceptionSendOTP;

                }
                else
                {
                    string url = topaybaseurl + "ApplyCard";
                    string tokenVal = Request.Headers["Token"].ToString();
                    string result = WalletCommon.HITTOPAYAPI(url, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;
                }
            }
            catch (System.Exception ex)
            {

            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(ApplyCardResponse));

            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms)
;
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("ValidatePin")]
        [Produces("application/json")]
        public ResponseModel ValidatePin(string Pin, string EntityId)
        {
            RequestModel requestModel = new RequestModel();
            string EncryptedText = "";
            string Aeskey = "";
            ValidateResponse objres = new ValidateResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionData exceptionSendOTP = new ExceptionData();
                    exceptionSendOTP.errorCode = "0";
                    exceptionSendOTP.fieldErrors = "Please pass token in header.";
                    objres.exception = exceptionSendOTP;

                }
                else
                {
                    //ExceptionData exceptionSendOTP = new ExceptionData();
                    //exceptionSendOTP.errorCode = "0";
                    //exceptionSendOTP.fieldErrors = "Hold for some time";
                    //objres.exception = exceptionSendOTP;

                    string tokenVal = Request.Headers["Token"].ToString();
                    string[] split = tokenVal.Split("-");
                    Aeskey = split[1];


                    EntityId = EntityId.Replace(" ", "+");
                    Pin = string.IsNullOrEmpty(Pin) ? "" : Pin.Replace(" ", "+");
                    string url = topaybaseurl + "ValidatePin?Pin=" + Pin + "&EntityId=" + EntityId;
                    string result = WalletCommon.HITTOPAYAPI(url, requestModel.Body = "", Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;

                }
            }
            catch (System.Exception ex)
            {
                ExceptionData exceptionSendOTP = new ExceptionData();
                exceptionSendOTP.errorCode = "0";
                exceptionSendOTP.fieldErrors = "Please pass token in header.";
                objres.exception = exceptionSendOTP;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ValidateResponse));

            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms)
;
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }


        [HttpPost("ForgetWalletPin")]
        [Produces("application/json")]
        public ResponseModel ForgetWalletPin(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            ForgotWalletPinResponse objres = new ForgotWalletPinResponse();

            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionChangePassword exceptionChangePassword = new ExceptionChangePassword();
                    exceptionChangePassword.errorCode = "0";
                    exceptionChangePassword.message = "Please pass token in header!";
                    objres.exception = exceptionChangePassword;
                }
                else
                {
                    string tokanVal = Request.Headers["Token"].ToString();
                    string[] split = tokanVal.Split("-");
                    Aeskey = split[1];
                    ForgotWalletPin forgotwalletpin = new ForgotWalletPin();
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    ForgotWalletPin walletpinreq = JsonConvert.DeserializeObject<ForgotWalletPin>(dcdata);
                    string url = topaybaseurl + "ForgetWalletPin";
                    string result = WalletCommon.HITTOPAYAPI(url, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, objres1.Body);
                    ForgotWalletPinResponse walletpinResponse = JsonConvert.DeserializeObject<ForgotWalletPinResponse>(dcdata1);
                    if (walletpinResponse != null && walletpinResponse.respcode == "1")
                    {

                        forgotwalletpin.EntityId = walletpinreq.EntityId.Replace(" ", "+"); ;
                        forgotwalletpin.WalletPin = walletpinreq.WalletPin.Replace(" ", "+");
                        DataSet ds = forgotwalletpin.GetForgotWalletPin();
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                                    {
                                        objres.respcode = ds.Tables[0].Rows[0]["flag"].ToString();
                                        objres.respdesc = ds.Tables[0].Rows[0]["msg"].ToString();
                                    }
                                    else
                                    {
                                        objres.respcode = "0";
                                        objres.respdesc = "Wallet Pin Not Updated, Try Again !!";
                                    }
                                }
                                else
                                {
                                    objres.respcode = "0";
                                    objres.respdesc = "Wallet Pin Not Updated, Try Again !!";
                                }
                            }
                        }
                    }

                    return objres1;
                    //var otpresponse = await _dataRepository.OTPProcess(otp);

                }
            }
            catch (Exception ex)
            {
                ExceptionChangePassword exception = new ExceptionChangePassword();
                exception.errorCode = "0";
                exception.message = ex.Message;
                objres.exception = exception;

            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;



            js = new DataContractJsonSerializer(typeof(ChangePasswordResponse));

            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms)

;
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }



        [HttpGet("EmploymentTypeList")]
        [Produces("application/json")]
        public ResponseModel EmploymentTypeList()
        {
            string EncryptedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            EmployementTypeResponse objres = new EmployementTypeResponse();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionData exceptionSendOTP = new ExceptionData();
                    exceptionSendOTP.errorCode = "0";
                    exceptionSendOTP.fieldErrors = "Please pass token in header.";
                }
                else
                {
                    string tokanVal = Request.Headers["Token"].ToString();
                    string[] split = tokanVal.Split("-");
                    Aeskey = split[1];
                    //string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey);
                    string url = topaybaseurl + "GetEmploymentList";
                    string result = WalletCommon.HITTOPAYAPI(url, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    EmplTypelist EmpList = new EmplTypelist();
                    return objres1;
                }
            }
            catch (Exception ex)
            {
                objres.Status = 0;
                objres.Message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(EmployementTypeResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("OTPVerifiyWallet")]
        [Produces("application/json")]
        public ResponseModel OTPVerifiyWallet(RequestModel requestModel)
        {

            ResponseModel returnResponse = new ResponseModel();
            ChangePasswordResponse objres = new ChangePasswordResponse();
            string EncryptedText = "";
            try
            {

                WalletOtpVerify verifyOtp = new WalletOtpVerify();
                string dcdata = ApiEncrypt_Decrypt.DecryptString("EE5A2360@C711@40", requestModel.Body);
                verifyOtp = JsonConvert.DeserializeObject<WalletOtpVerify>(dcdata);
                verifyOtp.OTPNO = verifyOtp.OTPNO;
                verifyOtp.LoginId = verifyOtp.LoginId;
                DataSet ds = verifyOtp.GetVerifyOtpWallet();
                //var otpresponse = await _dataRepository.OTPProcess(otp);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        objres.status = "1";
                        objres.response = "success";
                        objres.message = "OTP Match successfully!";

                    }
                    else
                    {
                        objres.status = "0";
                        objres.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                else
                {
                    objres.status = "0";
                    objres.message = ds.Tables[0].Rows[0]["Message"].ToString();
                }

            }
            catch (Exception ex)
            {
                ExceptionChangePassword exception = new ExceptionChangePassword();
                exception.errorCode = "0";
                exception.message = ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ChangePasswordResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms)

;
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString("EE5A2360@C711@40", CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("VerifiyMobEmail")]
        [Produces("application/json")]
        public ResponseModel VerifiyMobEmail(RequestModel requestModel)
        {

            ResponseModel returnResponse = new ResponseModel();
            MobileEmailVfyResponse objres = new MobileEmailVfyResponse();
            string EncryptedText = "";
            try
            {

                VerifyMobileEmail verifymodel = new VerifyMobileEmail();
                string dcdata = ApiEncrypt_Decrypt.DecryptString("EE5A2360@C711@40", requestModel.Body);
                verifymodel = JsonConvert.DeserializeObject<VerifyMobileEmail>(dcdata);
                verifymodel.Email = verifymodel.Email;
                verifymodel.LoginId = verifymodel.LoginId;
                DataSet ds = verifymodel.GeVerification();
                //var otpresponse = await _dataRepository.OTPProcess(otp);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        VerifyMobileEmail infoData = new VerifyMobileEmail()
                        {
                            CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString(),


                        };
                        objres.status = "1";
                        objres.message = "success";
                        objres.response = "success";
                        objres.Data = infoData;

                    }
                    else
                    {

                        if (ds.Tables[0].Rows[0]["Flag"].ToString() == "3")
                        {
                            try
                            {
                                string msg = "<body style='margin: 0;font-family: 'Poppins', sans-serif;background: #E9E9E9;font-size: 14px;'>";
                                msg += "<table style='width: 100%; max-width: 600px; background: #fff; margin:20px auto; border-radius: 25px; box-shadow: 3px 7px 20px 7px rgb(205 205 205 / 50%);' border='0' cellpadding='0' cellspacing='0'>";
                                msg += "<tbody><tr><td style='text-align: center;'><img alt='Logo' src='https://web.mobilepe.co.in/Adminassets/img/logo.png'height='80px' /></td></tr>";
                                msg += "<tr><td style='text-align: center; background: #345aad; padding: 35px 50px; font-size: 18px; color: #fff;'><h1 style='margin: 0px; padding: 0px'>Welcome to MobilePe</h1>";
                                msg += "<p style='margin: 0px; padding: 0px'>MOBILEPE sparked India's Digital Revolution.</p></td></tr>";
                                msg += "<tr><td style='text-align: left; background: #fff; padding: 30px 50px 50px;'><h3 style='text-align: left;'>User Details</h3>";
                                msg += "<table style='width:100%' border='0' cellpadding='5' cellspacing='0' ><tr><td style='width: 150px;'><strong>Mobile No</strong></td><td style='width: 50px;'>:</td>";
                                msg += "<td>" + verifymodel.LoginId + "</td></tr>";


                                msg += "</table></td></tr><tr><td style='text-align: center; background: #e6ebf1; padding: 10px 50px;border-radius:0 0 25px 25px; font-size: 12px;'>Copyright © 2023-24 MOBILEPE. All Rights Reserved.</td></tr></tbody></table></body>";
                                var result = objsms.SendEmailV2("ali@mobilepe.co.in", "Unsuspected User on " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), msg, "");
                            }
                            catch { }
                        }
                        objres.status = ds.Tables[0].Rows[0]["Flag"].ToString();
                        objres.response = "error";
                        objres.message = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                else
                {
                    objres.status = "0";
                    objres.response = "error";
                    objres.message = "Please enter Mobile OR Email";
                }

            }
            catch (Exception ex)
            {
                ExceptionChangePassword exception = new ExceptionChangePassword();
                exception.errorCode = "0";
                exception.message = ex.Message;

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MobileEmailVfyResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;

            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString("EE5A2360@C711@40", CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("KYCGuidLine")]
        [Produces("application/json")]
        public ResponseModel KYCGuidLine(RequestModel requestModel)
        {

            ResponseModel returnResponse = new ResponseModel();
            KYCGuideLineReturnRes objres = new KYCGuideLineReturnRes();
            string EncryptedText = "";
            try
            {

                KYCGuideLine kycmodel = new KYCGuideLine();
                string dcdata = ApiEncrypt_Decrypt.DecryptString("EE5A2360@C711@40", requestModel.Body);
                kycmodel = JsonConvert.DeserializeObject<KYCGuideLine>(dcdata);
                kycmodel.FK_MemId = kycmodel.FK_MemId;

                DataSet ds = kycmodel.GetKYCGuideLine();
                //var otpresponse = await _dataRepository.OTPProcess(otp);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                    {
                        int count = ds.Tables[0].Rows.Count;
                        string[] dataar = new string[count];
                        for (int i = 0; i < count; i++)
                        {
                            dataar[i] = ds.Tables[0].Rows[i]["Name"].ToString();
                        }
                        KYCGUideData KYCGuidData = new KYCGUideData()
                        {
                            KycData = dataar
                        };
                        objres.Data = KYCGuidData;
                        objres.respcode = "1";
                        objres.respdesc = "Success, KYC GuidLine List!";


                    }
                    else
                    {
                        //objres.respcode = "0";
                        //objres.respdesc = "Record Not Found !!";
                    }
                }
                else
                {
                    objres.respcode = "0";
                    objres.respdesc = "Record Not Found !!";
                }

            }
            catch (Exception ex)
            {
                ExceptionChangePassword exception = new ExceptionChangePassword();
                exception.errorCode = "0";
                exception.message = ex.Message;

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(KYCGuideLineReturnRes));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms)

;
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString("EE5A2360@C711@40", CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("WalletInfoV2")]
        [Produces("application/json")]
        public ResponseModel WalletInfoV2(RequestModel requestModel)
        {

            ResponseModel returnResponse = new ResponseModel();
            WalletInfoRes objres = new WalletInfoRes();

            string EncryptedText = "";
            try
            {

                WalletInfo walletmodel = new WalletInfo();
                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEYMP, requestModel.Body);
                walletmodel = JsonConvert.DeserializeObject<WalletInfo>(dcdata);
                walletmodel.mobileNo = walletmodel.mobileNo;

                DataSet ds = walletmodel.GetWalletInfo();
                //var otpresponse = await _dataRepository.OTPProcess(otp);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                    {

                        WalletInfoList infoData = new WalletInfoList()
                        {
                            Name = ds.Tables[0].Rows[0]["Name"].ToString(),
                            contactNo = ds.Tables[0].Rows[0]["contactNo"].ToString(),
                            IsFULLKYC = ds.Tables[0].Rows[0]["IsFULLKYC"].ToString(),
                        };
                        objres.status = "1";
                        objres.response = "success";
                        objres.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        objres.Data = infoData;

                    }
                    else
                    {

                        WalletInfoList infoData = new WalletInfoList()
                        {
                            Name = ds.Tables[0].Rows[0]["Name"].ToString(),
                            contactNo = ds.Tables[0].Rows[0]["contactNo"].ToString(),
                            IsFULLKYC = ds.Tables[0].Rows[0]["IsFULLKYC"].ToString(),
                        };
                        objres.message = ds.Tables[0].Rows[0]["Message"].ToString();
                        objres.status = "0";
                        objres.response = "Record Not Found !!";
                        objres.Data = infoData;
                    }
                }
                else
                {
                    objres.status = "0";
                    objres.response = "Record Not Found !!";
                }

            }
            catch (Exception ex)
            {
                ExceptionChangePassword exception = new ExceptionChangePassword();
                exception.errorCode = "0";
                exception.message = ex.Message;

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(WalletInfoRes));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEYMP, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("V2/Mobile2MobileTransfer2")]
        public ResponseModel Mobile2MobileTransfer2(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            WalletTransferRes objres = new WalletTransferRes();
            ResponseModel returnResponse = new ResponseModel();

            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    //objres.status = "0";
                    //objres.message = "Please pass token";
                }
                else
                {
                    string tokanVal = Request.Headers["Token"].ToString();
                    string[] split = tokanVal.Split("-");
                    Aeskey = split[1];
                    WalletToWalletTran wallettans = new WalletToWalletTran();
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);

                    WalletToWalletTran walletTansreq = JsonConvert.DeserializeObject<WalletToWalletTran>(dcdata);

                    walletTansreq.toEntityId = "MobilePe_" + walletTansreq.toEntityId;
                    walletTansreq.fromEntityId = "MobilePe_" + walletTansreq.fromEntityId;
                    walletTansreq.TxId = 0;
                    DataSet dataSet = walletTansreq.validate_user2user();

                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        string CustData1 = "";
                        DataContractJsonSerializer js1;
                        MemoryStream ms1;
                        js1 = new DataContractJsonSerializer(typeof(WalletToWalletTran));
                        ms1 = new MemoryStream();
                        js1.WriteObject(ms1, walletTansreq);
                        ms1.Position = 0;
                        StreamReader sr1 = new StreamReader(ms1);
                        CustData1 = sr1.ReadToEnd();
                        sr1.Close();
                        ms1.Close();
                        EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEYMP, CustData1);
                        requestModel.Body = EncryptedText;

                        string url = topaybaseurl + "user2userTransfer";
                        string tokenVal = Request.Headers["Token"].ToString();
                        string result = WalletCommon.HITTOPAYAPI(url, requestModel.Body, Request.Headers["Token"].ToString());
                        ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                        string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, objres1.Body);
                        WalletTransferRes wallettransfer = JsonConvert.DeserializeObject<WalletTransferRes>(dcdata1);
                        if (wallettransfer != null && wallettransfer.exception == null)
                        {

                            wallettans.toEntityId = walletTansreq.toEntityId;
                            wallettans.fromEntityId = walletTansreq.fromEntityId;
                            wallettans.amount = walletTansreq.amount;
                            wallettans.externalTransactionId = walletTansreq.externalTransactionId;
                            wallettans.description = walletTansreq.description;
                            wallettans.TxId = walletTansreq.TxId;
                            DataSet ds = wallettans.GetWalletToWallettranAmount();
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                                        {

                                            ExceptionSendOTP exception = new ExceptionSendOTP();
                                            exception.errorCode = ds.Tables[0].Rows[0]["flag"].ToString();
                                            exception.detailMessage = ds.Tables[0].Rows[0]["MEssage"].ToString();
                                            objres.exception = exception;


                                        }
                                        else
                                        {
                                            ExceptionSendOTP exception = new ExceptionSendOTP();
                                            exception.errorCode = "0";
                                            exception.detailMessage = ds.Tables[0].Rows[0]["MEssage"].ToString();
                                            objres.exception = exception;
                                        }
                                    }
                                    else
                                    {
                                        ExceptionSendOTP exception = new ExceptionSendOTP();
                                        exception.errorCode = "0";
                                        exception.detailMessage = ds.Tables[0].Rows[0]["MEssage"].ToString();
                                        objres.exception = exception;
                                    }
                                }
                            }
                        }
                        return objres1;
                    }
                    else
                    {
                        ExceptionSendOTP exception = new ExceptionSendOTP();
                        exception.errorCode = "0";
                        exception.detailMessage = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        objres.exception = exception;
                    }


                }

            }
            catch (Exception ex)
            {
                ExceptionSendOTP exception = new ExceptionSendOTP();
                exception.errorCode = "error";
                exception.detailMessage = ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(WalletTransferRes));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }


        [HttpPost("CheckMobileEmail")]
        [Produces("application/json")]
        public ResponseModel CheckMobileEmail(RequestModel requestModel)
        {

            ResponseModel returnResponse = new ResponseModel();
            CHeckMobileEmailResponse objres = new CHeckMobileEmailResponse();
            string EncryptedText = "";
            try
            {

                CheckMobileEmail modelReq = new CheckMobileEmail();
                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEYMP, requestModel.Body);
                modelReq = JsonConvert.DeserializeObject<CheckMobileEmail>(dcdata);
                modelReq.Email = modelReq.Email;
                modelReq.Mobile = modelReq.Mobile;
                modelReq.Action = modelReq.Action;
                DataSet ds = modelReq.GetCheckMobileEmail();
                //var otpresponse = await _dataRepository.OTPProcess(otp);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {

                        objres.message = ds.Tables[0].Rows[0]["MSG"].ToString();
                        objres.respcode = "1";
                        objres.respdesc = "success";

                    }
                    else
                    {
                        objres.respcode = "0";
                        objres.respdesc = ds.Tables[0].Rows[0]["MSG"].ToString();
                    }
                }
                else
                {
                    objres.respcode = "failed";
                    objres.respdesc = "Please enter Mobile OR Email";
                }

            }
            catch (Exception ex)
            {
                ExceptionChangePassword exception = new ExceptionChangePassword();
                exception.errorCode = "0";
                exception.message = ex.Message;

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CHeckMobileEmailResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;

            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEYMP, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("Wallet2WalletTranHistorylist")]
        [Produces("application/json")]
        public ResponseModel Wallet2WalletTranHistorylist(RequestModel requestModel)
        {
            string EncryptedText = "";

            wallettowallethist_res objres = new wallettowallethist_res();
            WalletTopup objreq = new WalletTopup();
            ResponseModel returnResponse = new ResponseModel();
            DataContractJsonSerializer js;
            MemoryStream ms;
            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEYMP, requestModel.Body);
                objreq = JsonConvert.DeserializeObject<WalletTopup>(dcdata);
                objreq.EntityId = objreq.EntityId;

                DataSet ds = objreq.Getuser2user_tranHistory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                    {
                        //int count = ds.Tables[0].Rows.Count;
                        //string[] dataar = new string[count];
                        //for (int i = 0; i < count; i++)
                        //{
                        //    dataar[i] = ds.Tables[0].Rows[i]["Name"].ToString();
                        //}
                        List<wallettowallethistlist> list = new List<wallettowallethistlist>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            list.Add(new wallettowallethistlist()
                            {
                                TransactionDate = row["TransactionDate"].ToString(),
                                TransactionNo = row["TransactionNo"].ToString(),
                                Narration = row["Narration"].ToString(),
                                FromName = row["FromName"].ToString(),
                                ToName = row["ToName"].ToString(),
                                Amount = row["Amount"].ToString(),
                                Status = row["Status"].ToString(),

                            });
                        }
                        wallettowalletDataRes walletData = new wallettowalletDataRes()
                        {
                            Data = list
                        };
                        objres.ListData = walletData;
                        objres.status = "1";
                        objres.message = "Wallet to wallet transfer  List !";


                    }
                    else
                    {
                        objres.status = "0";
                        objres.message = "Record Not Found !!";
                    }
                }
                else
                {
                    objres.status = "0";
                    objres.message = "Record Not Found !!";
                }



            }
            catch (System.Exception ex)
            {
                objres.status = "0";
                objres.message = ex.Message;
            }
            string CustData = "";
            js = new DataContractJsonSerializer(typeof(wallettowallethist_res));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEYMP, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }


        [HttpGet("UpdateWallet")]
        [Produces("application/json")]
        public WalletTransactionResponse UpdateWallet(string EntityId)
        {
            WalletTransactionResponse objres = new WalletTransactionResponse();
            WalletCommon common = new WalletCommon();
            DataSet dataSet = common.GetWalletData(EntityId);

            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                EntityId = dataSet.Tables[0].Rows[i]["entityId"].ToString();
                string APIurl = "https://topay.live/api/webAPI/WalletTransactionsSimple?EntityId=" + EntityId + "&pageSize=1000000";

                string result = WalletCommon.HITTOPAYAPI(APIurl,"");
                objres = JsonConvert.DeserializeObject<WalletTransactionResponse>(result);
                if (objres.result != null)
                {

                    for (int j = 0; j <= objres.result.Count - 1; j++)
                    {
                        string EntityId1 = EntityId;
                        double timestmp = double.Parse(objres.result[j].transaction.time);
                        var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(timestmp / 1000d)).ToLocalTime();
                        string TransDateTime = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + " " + dt.TimeOfDay;

                        string json = JsonConvert.SerializeObject(objres.result[j].transaction);
                        DataSet dataSet1 = common.UpdateWallet(json, EntityId, TransDateTime, objres.result.Count, j);



                    }
                }
            }

            return objres;
        }


        [HttpPost("AddBankDetails")]
        [Produces("application/json")]
        public ResponseModel AddBankDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = "";
            SendOTPResponse objres = new SendOTPResponse();
            ResponseModel returnResponse = new ResponseModel();

            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    ExceptionData exceptionSendOTP = new ExceptionData();
                    exceptionSendOTP.errorCode = "0";
                    exceptionSendOTP.fieldErrors = "Please pass token in header.";
                    objres.exception = exceptionSendOTP;

                }
                else
                {
                    string url = topaybaseurl + "BankDetails";
                    string tokenVal = Request.Headers["Token"].ToString();
                    string result = WalletCommon.HITTOPAYAPI(url, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;
                }
            }
            catch (System.Exception ex)
            {
                ExceptionData exceptionSendOTP = new ExceptionData();
                exceptionSendOTP.errorCode = "0";
                exceptionSendOTP.fieldErrors = "Please pass token in header.";
                objres.exception = exceptionSendOTP;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SendOTPResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }




    }

}
