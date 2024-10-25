using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Models;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;

namespace MobileAPI_V2.Controllers
{
    [Route("api/BBPS")]
    [ApiController]
    public class NewBBPSController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public NewBBPSController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {

            _dataRepository = dataRepository;
        }
        string bbpsaseurl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("NewBBPSURL").Value;
        string Aeskey = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
        string SecretID = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("secretID").Value;
        string Password = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("password").Value;
        string CPID = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("cpid").Value;


        [HttpPost("BBPSCategoryList")]
        [Produces("application/json")]
        public ResponseModel BBPSCategoryList()
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            NewBBPSResponse bbpsres = new NewBBPSResponse();

            try
            {
                string body = "";
                string url = bbpsaseurl + "GetCategoryList";
                string result = Common.HITNewBBPSAPI(url, body);
                ResponseModel Objres = JsonConvert.DeserializeObject<ResponseModel>(result);
                NewBBPSResponse res = JsonConvert.DeserializeObject<NewBBPSResponse>(result);
                if (res != null)
                {
                    bbpsres.response_code = res.response_code;
                    bbpsres.response_message = res.response_message;
                    bbpsres.session_details = res.session_details;
                    bbpsres.Data = res.Data;
                }



            }
            catch (Exception ex)
            {
                ExceptionSendOTP exception = new ExceptionSendOTP();
                exception.errorCode = "error";
                exception.detailMessage = ex.Message;
                //objres.exception = exception;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(NewBBPSResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, bbpsres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("BBPSBillerList")]
        [Produces("application/json")]
        public ResponseModel BBPSBillerList(RequestModel requestModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BillerResponse bbpsres = new BillerResponse();
            try
            {
                BillerRequest bbpsreq = new BillerRequest();
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                bbpsreq = JsonConvert.DeserializeObject<BillerRequest>(dcdata1);
                string body = "";
                string url = bbpsaseurl + "GetBillerList?CategoryID=" + bbpsreq.CategoryID + "&LocationID=" + bbpsreq.LocationID;
                string result = Common.HITNewBBPSAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                BillerResponse res = JsonConvert.DeserializeObject<BillerResponse>(result);
                if (res != null)
                {
                    bbpsres.response_code = res.response_code;
                    bbpsres.response_message = res.response_message;
                    bbpsres.session_details = res.session_details;
                    bbpsres.Data = res.Data;
                    for(int i = 0;i<=res.Data.Count-1;i++)
                    {
                        DataSet dataSet = bbpsreq.SaveBillProviders(res.Data[i].billerID, res.Data[i].billerCategory, res.Data[i].billerName);
                    }

                }


            }
            catch (Exception ex)
            {
                throw;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BillerResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, bbpsres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("BBPSStateMaster")]
        [Produces("application/json")]
        public ResponseModel BBPSStateMaster(RequestModel requestModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BBPSStateMasterRes bbpsres = new BBPSStateMasterRes();
            try
            {
                BBPSStateMasterReq bbpsreq = new BBPSStateMasterReq();
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                bbpsreq = JsonConvert.DeserializeObject<BBPSStateMasterReq>(dcdata1);
                string body = "";
                string url = bbpsaseurl + "GetStateMaster?CategoryID=" + bbpsreq.CategoryID;
                string result = Common.HITNewBBPSAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                BBPSStateMasterRes res = JsonConvert.DeserializeObject<BBPSStateMasterRes>(result);
                if (res != null)
                {
                    bbpsres.response_code = res.response_code;
                    bbpsres.response_message = res.response_message;
                    bbpsres.session_details = res.session_details;
                    bbpsres.Data = res.Data;
                }


            }
            catch (Exception ex)
            {
                throw;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BBPSStateMasterRes));
            ms = new MemoryStream();
            js.WriteObject(ms, bbpsres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("BBPSBillFetch")]
        [Produces("application/json")]
        public ResponseModel BBPSBillFetch(RequestModel requestModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BillFetchres bbpsres = new BillFetchres();
            BillFetchReq bbpsreq = new BillFetchReq();
            try
            {
                string body = "";
                BillFetchReq bbpftchsreq = new BillFetchReq();
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                bbpftchsreq = JsonConvert.DeserializeObject<BillFetchReq>(dcdata1);

                bbpftchsreq.CPID = CPID;
                bbpftchsreq.SecretID = SecretID;
                bbpftchsreq.Password = Password;
                bbpftchsreq.EntityID = "MOBILEPE";
                bbpftchsreq.CpTransactionid = DateTime.Now.ToString("ddMMyyyyhhMMss");
                bbpftchsreq.textboxname2 = string.IsNullOrEmpty(bbpftchsreq.textboxname2) ? "NA" : bbpftchsreq.textboxname2;
                bbpftchsreq.textboxname3 = string.IsNullOrEmpty(bbpftchsreq.textboxname3) ? "NA" : bbpftchsreq.textboxname3;
                bbpftchsreq.textboxname4 = string.IsNullOrEmpty(bbpftchsreq.textboxname4) ? "NA" : bbpftchsreq.textboxname4;
                bbpftchsreq.textboxname5 = string.IsNullOrEmpty(bbpftchsreq.textboxname5) ? "NA" : bbpftchsreq.textboxname5;
                bbpftchsreq.textboxvalue2= string.IsNullOrEmpty(bbpftchsreq.textboxvalue2) ? "NA" : bbpftchsreq.textboxvalue2;
                bbpftchsreq.textboxvalue3= string.IsNullOrEmpty(bbpftchsreq.textboxvalue3) ? "NA" : bbpftchsreq.textboxvalue3;
                bbpftchsreq.textboxvalue4= string.IsNullOrEmpty(bbpftchsreq.textboxvalue4) ? "NA" : bbpftchsreq.textboxvalue4;
                bbpftchsreq.textboxvalue5= string.IsNullOrEmpty(bbpftchsreq.textboxvalue5) ? "NA" : bbpftchsreq.textboxvalue5;
               
                string url = bbpsaseurl + "billFetch?customer_mobile="+ bbpftchsreq.customer_mobile + "&billerid="+bbpftchsreq.billerid+"&textboxname1="+ bbpftchsreq.textboxname1 + "&textboxname2="+ bbpftchsreq.textboxname2 + "&textboxname3="+ bbpftchsreq.textboxname3 + "&textboxname4="+ bbpftchsreq.textboxname4 + "&textboxname5="+ bbpftchsreq.textboxname5 + "&textboxvalue1="+ bbpftchsreq.textboxvalue1 + "&textboxvalue2="+ bbpftchsreq.textboxvalue2 + "&textboxvalue3="+ bbpftchsreq.textboxvalue3 + "&textboxvalue4="+ bbpftchsreq.textboxvalue4 + "&textboxvalue5="+ bbpftchsreq.textboxvalue5 + "&nooftextbox="+ bbpftchsreq.nooftextbox + "&AgentMobileNo="+ bbpftchsreq.AgentMobileNo + "&CpTransactionid="+ bbpftchsreq.CpTransactionid + "&customername="+ bbpftchsreq.customername + "&emailId="+ bbpftchsreq.emailid + "&imei="+ bbpftchsreq.imei + "&ip="+ bbpftchsreq.ip + "&mac="+ bbpftchsreq.mac + "&CPID="+ bbpftchsreq.CPID + "&EntityID="+ bbpftchsreq.EntityID + "&SecretID="+ bbpftchsreq.SecretID + "&Password="+ bbpftchsreq.Password + "";
                string result = Common.HITNewBBPSAPI(url);
                bbpsres = JsonConvert.DeserializeObject<BillFetchres>(result);
                bbpsres.CpTransactionid = bbpftchsreq.CpTransactionid;


            }
            catch (Exception ex)
            {
                throw;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BillFetchres));
            ms = new MemoryStream();
            js.WriteObject(ms, bbpsres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("BBPSBillPay")]
        [Produces("application/json")]
        public async Task<ResponseModel> BBPSBillPay(RequestModel requestModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BBPSBillPayres bbpsres = new BBPSBillPayres();
            BBPSBillPay bbpsbillres = new BBPSBillPay();
            BillerSessionDetails bbpssession = new BillerSessionDetails();
          
            try
            {
                BBSPBillPayReq bbpsPayreq = new BBSPBillPayReq();
                
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                bbpsPayreq = JsonConvert.DeserializeObject<BBSPBillPayReq>(dcdata1);
                bbpsPayreq.CPID = CPID;
                bbpsPayreq.SecretID = SecretID;
                bbpsPayreq.Password = Password;
                bbpsPayreq.EntityID = "MOBILEPE";
                bbpsPayreq.textboxname2 = string.IsNullOrEmpty(bbpsPayreq.textboxname2) ? "NA" : bbpsPayreq.textboxname2;
                bbpsPayreq.textboxname3 = string.IsNullOrEmpty(bbpsPayreq.textboxname3) ? "NA" : bbpsPayreq.textboxname3;
                bbpsPayreq.textboxname4 = string.IsNullOrEmpty(bbpsPayreq.textboxname4) ? "NA" : bbpsPayreq.textboxname4;
                bbpsPayreq.textboxname5 = string.IsNullOrEmpty(bbpsPayreq.textboxname5) ? "NA" : bbpsPayreq.textboxname5;
                bbpsPayreq.textboxvalue2 = string.IsNullOrEmpty(bbpsPayreq.textboxvalue2) ? "NA" : bbpsPayreq.textboxvalue2;
                bbpsPayreq.textboxvalue3 = string.IsNullOrEmpty(bbpsPayreq.textboxvalue3) ? "NA" : bbpsPayreq.textboxvalue3;
                bbpsPayreq.textboxvalue4 = string.IsNullOrEmpty(bbpsPayreq.textboxvalue4) ? "NA" : bbpsPayreq.textboxvalue4;
                bbpsPayreq.textboxvalue5 = string.IsNullOrEmpty(bbpsPayreq.textboxvalue5) ? "NA" : bbpsPayreq.textboxvalue5;
                
                string body = JsonConvert.SerializeObject(bbpsPayreq);
                string url = bbpsaseurl + "billPay?transactionID="+ bbpsPayreq.transactionID + "&CustomerMobileNo="+ bbpsPayreq.customerMobileNo + "&CPID="+ bbpsPayreq.CPID + "&EntityID="+ bbpsPayreq.EntityID + "&biller_id="+ bbpsPayreq.biller_id + "&textboxname1="+ bbpsPayreq.textboxname1 + "&textboxname2="+ bbpsPayreq.textboxname2 + "&textboxname3="+ bbpsPayreq.textboxname3 + "&textboxname4="+ bbpsPayreq.textboxname4 + "&textboxname5="+ bbpsPayreq.textboxname5 + "&textboxvalue1="+ bbpsPayreq.textboxvalue1 + "&textboxvalue2="+ bbpsPayreq.textboxvalue2 + "&textboxvalue3="+ bbpsPayreq.textboxvalue3 + "&textboxvalue4="+ bbpsPayreq.textboxvalue4 + "&textboxvalue5="+ bbpsPayreq.textboxvalue5 + "&amount="+ bbpsPayreq.amount + "&duedate="+ bbpsPayreq.duedate + "&billdate="+ bbpsPayreq.billdate + "&billnumber="+ bbpsPayreq.billnumber + "&billperiod="+ bbpsPayreq.billperiod + "&CustomerName="+ bbpsPayreq.CustomerName + "&nooftextbox="+ bbpsPayreq.nooftextbox + "&SecretID="+ bbpsPayreq.SecretID + "&Password="+ bbpsPayreq.Password + "&ip="+ bbpsPayreq.ip + "&mac="+ bbpsPayreq.mac + "&imei="+ bbpsPayreq.imei + "";
                string result = Common.HITNewBBPSAPI(url);               
                BBPSBillPayres res = JsonConvert.DeserializeObject<BBPSBillPayres>(result);
                res.PaymentId = bbpsPayreq.PaymentId;
                res.OrderId = bbpsPayreq.OrderId;
                var saveresponse = _dataRepository.SaveBBPSBillPayResponse(res, bbpsPayreq.Fk_MemId, JsonConvert.SerializeObject(bbpsPayreq), result);
                //var SaveJonReq = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(bbpsPayreq), bbpsPayreq.Type,int.Parse(bbpsPayreq.Fk_MemId.ToString()));
               // var SaveJonRes = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(result), bbpsPayreq.Type, int.Parse(bbpsPayreq.Fk_MemId.ToString()));


                
            }
            catch (Exception ex)
            {
                throw;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BBPSBillPayres));
            ms = new MemoryStream();
            js.WriteObject(ms, bbpsres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("BBPSTransactionStatus")]
        [Produces("application/json")]
        public BillTraansStatus BBPSTransactionStatus(string CpTransactionID)
        {
            WalletCommon common = new WalletCommon();
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BillTraansStatus billTraansStatus=new BillTraansStatus();

            try
            {
                
                string url = bbpsaseurl + "txtStatusCheck?CpTransactionID="+ CpTransactionID;
                string result = Common.HITNewBBPSAPI(url);
                billTraansStatus = JsonConvert.DeserializeObject<BillTraansStatus>(result);
               // DataSet dataSet1 = common.UpdateWallet(json, EntityId, TransDateTime, objres.result.Count, j);


            }
            catch (Exception ex)
            {
                throw;
            }
           
            return billTraansStatus;
        }

    }
}
