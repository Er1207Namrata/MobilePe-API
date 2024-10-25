using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Model;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System;
using MobileAPI_V2.Models;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;
using System.Data;
using System.Text;
using MobileAPI_V2.Model.Travel;

namespace MobileAPI_V2.Controllers
{
    [ApiController]
    [Route("api/Auth/")]
    public class CardController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public CardController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {

            _dataRepository = dataRepository;
        }
        string topaybaseurl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("TopayBaseURL").Value;
        [HttpPost("AddCardRequest")]
        [Produces("application/json")]
        public ResponseModel AddCardRequest(RequestModel requestModel)
        {
            string EncrytedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            RefundCustomerResponse objres = new RefundCustomerResponse();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    CardException exception = new CardException();
                    exception.errorCode = "0";
                    exception.message = "Please pass token in header";
                    objres.exception = exception;
                }
                else
                {
                    string tokenValue = Request.Headers["Token"].ToString();
                    string[] split = tokenValue.Split("-");
                    Aeskey = split[1];
                    CardRequest request = new CardRequest();
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    request = JsonConvert.DeserializeObject<CardRequest>(dcdata);
                    string ApiUrl = topaybaseurl + "AddCardRequest";
                    string result = WalletCommon.HITTOPAYAPI(ApiUrl, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;
                }
            }
            catch (Exception Ex)
            {
                CardException exception = new CardException();
                exception.errorCode = "0";
                exception.message = Ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RefundCustomerResponse));
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

        [HttpPost("ReplaceCard")]
        [Produces("application/json")]
        public ResponseModel ReplaceCard(RequestModel requestModel)
        {
            string EncrytedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            RefundCustomerResponse objres = new RefundCustomerResponse();
            CardLockUnlockResponse objres11 = new CardLockUnlockResponse();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    CardException exception = new CardException();
                    exception.errorCode = "0";
                    exception.message = "Please pass token in header";
                    objres.exception = exception;
                }
                else
                {
                    CardRequest request = new CardRequest();

                    string tokenValue = Request.Headers["Token"].ToString();
                    string[] split = tokenValue.Split("-");
                    Aeskey = split[1];

                    string ApiUrl = topaybaseurl + "CardLockUnlock";

                    string result = WalletCommon.HITTOPAYAPI(ApiUrl, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres111 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    objres11 = JsonConvert.DeserializeObject<CardLockUnlockResponse>(result);


                    //topaybaseurl = "http://localhost:63128/api/webapi/";
                    ApiUrl = topaybaseurl + "ReplaceCard";
                    string result1 = WalletCommon.HITTOPAYAPI(ApiUrl, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result1);
                    return objres1;
                }
            }
            catch (Exception Ex)
            {
                CardException exception = new CardException();
                exception.errorCode = "0";
                exception.message = Ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RefundCustomerResponse));
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

        [HttpPost("SetPreference")]
        [Produces("application/json")]
        public ResponseModel SetPreference(RequestModel requestModel)
        {
            string EncrytedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            SetPreferenceResponse objres = new SetPreferenceResponse();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    CardException exception = new CardException();
                    exception.errorCode = "0";
                    exception.message = "Please pass token in header";
                    objres.exception = exception;
                }
                else
                {
                    string tokenValue = Request.Headers["Token"].ToString();
                    string[] split = tokenValue.Split("-");
                    Aeskey = split[1];
                    CardRequest request = new CardRequest();
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    request = JsonConvert.DeserializeObject<CardRequest>(dcdata);
                    string ApiUrl = topaybaseurl + "SetPreference";
                    string result = WalletCommon.HITTOPAYAPI(ApiUrl, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;
                }
            }
            catch (Exception Ex)
            {
                CardException exception = new CardException();
                exception.errorCode = "0";
                exception.message = Ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SetPreferenceResponse));
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


        [HttpPost("CardLockUnlock")]
        [Produces("application/json")]
        public ResponseModel CardLockUnlock(RequestModel requestModel)
        {
            string EncrytedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            CardLockUnlockResponse objres = new CardLockUnlockResponse();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    CardException exception = new CardException();
                    exception.errorCode = "0";
                    exception.message = "Please pass token in header";
                    objres.exception = exception;
                }
                else
                {
                    string tokenValue = Request.Headers["Token"].ToString();
                    string[] split = tokenValue.Split("-");
                    Aeskey = split[1];
                    CardRequest request = new CardRequest();
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    request = JsonConvert.DeserializeObject<CardRequest>(dcdata);
                    string ApiUrl = topaybaseurl + "CardLockUnlock";
                    string result = WalletCommon.HITTOPAYAPI(ApiUrl, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, objres1.Body);
                    objres = JsonConvert.DeserializeObject<CardLockUnlockResponse>(dcdata1);
                    if (objres.result != null)
                    {
                        SaveCardLockUnlock saveCardLockUnlock = new SaveCardLockUnlock();
                        saveCardLockUnlock.entityId = request.entityId;
                        saveCardLockUnlock.kitNo = request.kitNo;
                        saveCardLockUnlock.reason = request.reason;
                        
                        if (request.flag == "L")
                        {
                            saveCardLockUnlock.IsBlock = "1";
                            saveCardLockUnlock.IsLock = "1";
                        }
                        else
                        {
                            saveCardLockUnlock.IsBlock = "0";
                            saveCardLockUnlock.IsLock = "0";
                        }
                        saveCardLockUnlock.OpCode = 1;
                        DataSet ds = saveCardLockUnlock.SaveResponse();
                    }
                    return objres1;
                }
            }
            catch (Exception Ex)
            {
                CardException exception = new CardException();
                exception.errorCode = "0";
                exception.message = Ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CardLockUnlockResponse));
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


        [HttpPost("FetchCardPreference")]
        [Produces("application/json")]
        public ResponseModel FetchCardPreference(RequestModel requestModel)
        {
            string EncrytedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            CardPreferenceResponse objres = new CardPreferenceResponse();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    CardException exception = new CardException();
                    exception.errorCode = "0";
                    exception.message = "Please pass token in header";
                    objres.exception = exception;
                }
                else
                {
                    string tokenValue = Request.Headers["Token"].ToString();
                    string[] split = tokenValue.Split("-");
                    Aeskey = split[1];
                    CardRequest request = new CardRequest();
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    request = JsonConvert.DeserializeObject<CardRequest>(dcdata);
                    string ApiUrl = topaybaseurl + "FetchCardPreference";
                    string result = WalletCommon.HITTOPAYAPI(ApiUrl, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;
                }
            }
            catch (Exception Ex)
            {
                CardException exception = new CardException();
                exception.errorCode = "0";
                exception.message = Ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CardPreferenceResponse));
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



        [HttpPost("GetCardList")]
        [Produces("application/json")]
        public ResponseModel GetCardList(RequestModel requestModel)
        {
            string EncrytedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            GetCardListResponse objres = new GetCardListResponse();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    CardException exception = new CardException();
                    exception.errorCode = "0";
                    exception.message = "Please pass token in header";
                    objres.exception = exception;
                }
                else
                {
                    string tokenValue = Request.Headers["Token"].ToString();
                    string[] split = tokenValue.Split("-");
                    Aeskey = split[1];
                    
                    string ApiUrl = topaybaseurl + "GetCardList";
                    string result = WalletCommon.HITTOPAYAPI(ApiUrl, requestModel.Body, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;
                }
            }
            catch (Exception Ex)
            {
                CardException exception = new CardException();
                exception.errorCode = "0";
                exception.message = Ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GetCardListResponse));
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

        [HttpPost("SetPin")]
        [Produces("application/json")]
        public ResponseModel SetPin(RequestModel requestModel)
        {
            string EncrytedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            GetCardListResponse objres = new GetCardListResponse();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    CardException exception = new CardException();
                    exception.errorCode = "0";
                    exception.message = "Please pass token in header";
                    objres.exception = exception;
                }
                else
                {
                    string tokenValue = Request.Headers["Token"].ToString();
                    string[] split = tokenValue.Split("-");
                    Aeskey = split[1];

                    string ApiUrl = topaybaseurl + "SetPin";    
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    SetPinRequest setPinRequest = JsonConvert.DeserializeObject<SetPinRequest>(dcdata);
                    setPinRequest.callbackUrl = "https://topay.live/";
                    setPinRequest.token = Common.RandomNumber();
                    EncrytedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, JsonConvert.SerializeObject(setPinRequest));

                    string result = WalletCommon.HITTOPAYAPI(ApiUrl, EncrytedText, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                    return objres1;
                }
            }
            catch (Exception Ex)
            {
                CardException exception = new CardException();
                exception.errorCode = "0";
                exception.message = Ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GetCardListResponse));
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

        [HttpPost("CardDetails")]
        [Produces("application/json")]
        public ResponseModel CardDetails(RequestModel requestModel)
        {
            string EncrytedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            GetCardListResponse objres = new GetCardListResponse();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    CardException exception = new CardException();
                    exception.errorCode = "0";
                    exception.message = "Please pass token in header";
                    objres.exception = exception;
                }
                else
                {
                    string tokenValue = Request.Headers["Token"].ToString();
                    string[] split = tokenValue.Split("-");
                    Aeskey = split[1];

                    string ApiUrl = topaybaseurl + "cardDetails";
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    SetPinRequest setPinRequest = JsonConvert.DeserializeObject<SetPinRequest>(dcdata);
                    setPinRequest.callbackUrl = "https://uat.wallet.topayfintech.com/iframe-redirection";
                    setPinRequest.token = Common.RandomNumber();
                    EncrytedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, JsonConvert.SerializeObject(setPinRequest));

                    string result = WalletCommon.HITTOPAYAPI(ApiUrl, EncrytedText, Request.Headers["Token"].ToString());
                    ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);


                    
                    return objres1;
                }
            }
            catch (Exception Ex)
            {
                CardException exception = new CardException();
                exception.errorCode = "0";
                exception.message = Ex.Message;
                objres.exception = exception;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GetCardListResponse));
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


        [HttpPost("RecivedCard")]
        [Produces("application/json")]
        public ResponseModel RecivedCard(RequestModel requestModel)
        {
            string EncrytedText = "";
            string Aeskey = "";
            ResponseModel returnResponse = new ResponseModel();
            CheckMobileTransfer objres = new CheckMobileTransfer();
            try
            {
                if (string.IsNullOrEmpty(Request.Headers["Token"]))
                {
                    objres.Flag = "0";
                }
                else
                {
                    string tokenValue = Request.Headers["Token"].ToString();
                    string[] split = tokenValue.Split("-");
                    Aeskey = split[1];

                   
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    RecivedCardRequest recivedCardRequest = JsonConvert.DeserializeObject<RecivedCardRequest>(dcdata);

                    var checkMobile = _dataRepository.RecievedCard(recivedCardRequest);
                    objres.Msg = checkMobile.Result.Msg;
                    objres.Flag= checkMobile.Result.Flag;

                }
            }
            catch (Exception Ex)
            {
                objres.Flag = "0";
                objres.Msg = Ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CheckMobileTransfer));
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

        [HttpPost("WalletTransactionsSimple")]
        [Produces("application/json")]
        public WalletTransactionResponse WalletTransactionsSimple(string EntityId, string pageSize)
        {

            WalletTransactionResponse objres = new WalletTransactionResponse();
            ResponseModel returnResponse = new ResponseModel();

            try
            {

                Common common = new Common();

                string APIurl = topaybaseurl + "WalletTransactionsSimple?EntityId=" + EntityId + "&pageSize=" + pageSize;
                
                string result = Common.HITAPI(APIurl);



                objres = JsonConvert.DeserializeObject<WalletTransactionResponse>(result);

            }
            catch (System.Exception ex)
            {
                ExceptionData exceptionSendOTP = new ExceptionData();
                exceptionSendOTP.errorCode = "0";
                exceptionSendOTP.fieldErrors = ex.Message;
                objres.exception = exceptionSendOTP;
            }

            return objres;

        }


    }
}
