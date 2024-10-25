using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model;
using MobileAPI_V2.Model.BBPS;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Model.Svaas;
using MobileAPI_V2.Model.Travel;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;

namespace MobileAPI_V2.Controllers
{
    [ApiController]
    [Route("api/Auth/")]
    public class BillPaymentController : Controller
    {
        private readonly IDataRepository _dataRepository;
        public BillPaymentController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {

            _dataRepository = dataRepository;
        }


        [HttpPost("GetBillPayProvider")]
        [Produces("application/json")]
        public ResponseModel GetBillPayProvider(RequestModel requestModel)
        {
            string EncryptedText = "";
            string AesKey = "";
            BillPayProviderResponse objres = new BillPayProviderResponse();
            objres.message = "Please update your app";
            objres.status = "0";
           
            ResponseModel returnResponse = new ResponseModel();
            //try
            //{
            //    GetBillPayProviderList getBillPayProviderList = new GetBillPayProviderList();


            //    AesKey = Credentials.AESKEY;
            //    BillPayProviderRequest billPayProviderRequest = new BillPayProviderRequest();
            //    string dcdata = ApiEncrypt_Decrypt.DecryptString(AesKey, requestModel.Body);
            //    billPayProviderRequest = JsonConvert.DeserializeObject<BillPayProviderRequest>(dcdata);
            //    var cardData = this._dataRepository.GetBilPayBillers(billPayProviderRequest.ebill_type);
            //    objres.status = "1";
            //    objres.message = "success";
            //    getBillPayProviderList.BillPayProviderList = cardData.Result;
            //    objres.Response = getBillPayProviderList;

            //}
            //catch (Exception ex)
            //{
            //    objres.message = ex.Message;
            //    objres.status = "0";
            //}
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BillPayProviderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AesKey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("BillFetch")]
        [Produces("application/json")]
        public ResponseModel BillFetch(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = Credentials.AESKEY;
            BillPayResponse objres = new BillPayResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                string url = "";

                BillPaymentFetch provider = new BillPaymentFetch();
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                provider = JsonConvert.DeserializeObject<BillPaymentFetch>(dcdata);

                if (provider.Type == "Electricity")
                {
                    url = Credentials.ElectricityBillFetch;
                }
                else if (provider.Type == "Gas")
                {
                    url = Credentials.GasBillFetch;
                }
                else if (provider.Type == "Water")
                {
                    url = Credentials.WaterBillFetch;
                }
                else if (provider.Type == "Insurance")
                {
                    url = Credentials.InsuranceBillFetch;
                }
                else if (provider.Type == "LPG")
                {
                    url = Credentials.LpgBillFetch;
                }
                else if (provider.Type == "CableTV")
                {
                    url = Credentials.CableTvBillFetch;
                }
                string ApiUrl = "";
                ApiUrl = url + "MobileNo=" + Credentials.MobileNo + "&PinNo=" + Credentials.PinNo + "&serviceName=" + provider.serviceName + "&customerNumber=" + provider.customerNumber + "&optional=" + provider.Optional + "&optional1=" + provider.Optional1;

                string result = BillPaymentCommon.HITMultiRechargeAPI(ApiUrl);
                objres = JsonConvert.DeserializeObject<BillPayResponse>(result);


            }
            catch (Exception ex)
            {
                objres.status = "0";
                objres.desc = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BillPayResponse));
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

        [HttpPost("BillPayment")]
        [Produces("application/json")]
        public ResponseModel BillPayment(RequestModel requestModel)
        {
            string EncryptedText = "";
            string Aeskey = Credentials.AESKEY;
            BillPaymentResponse objres = new BillPaymentResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                BillPayment billPayment = new BillPayment();
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                billPayment = JsonConvert.DeserializeObject<BillPayment>(dcdata);
                billPayment.RechargeNumber = billPayment.CustomerId;

                string ApiUrl = Credentials.BillPaymentWithPara + "MobileNo=" + Credentials.MobileNo + "&PinNumber=" + Credentials.PinNo + "&RechargeType=" + billPayment.RechargeType + "&ServiceName=" + billPayment.ServiceName + "&Amount=" + billPayment.Amount + "&RechargeNumber=" + billPayment.RechargeNumber
                          + "&TransId=" + billPayment.PaymentId + "&circle=" + billPayment.circle;
                //string result = BillPaymentCommon.HITMultiRechargeAPI(ApiUrl);
                //objres = JsonConvert.DeserializeObject<BillPaymentResponse>(result);



                objres.RechargeType = billPayment.RechargeType;
                objres.ServiceName = billPayment.OpertorCode;
                objres.Amount = billPayment.Amount;
                objres.RechargeNumber = billPayment.RechargeNumber;
                objres.TransId = billPayment.TransId;
                objres.circle = billPayment.circle;
                objres.Fk_MemId = billPayment.Fk_MemId;
                objres.PaymentId = billPayment.PaymentId;
                objres.OrderId = billPayment.OrderId;
                objres.Status = "0";
                var SaveBookingResponse = _dataRepository.SaveBillPaymentResponse(objres);



            }
            catch (Exception ex)
            {
                objres.Status = "0";
                objres.Message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BillPaymentResponse));
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


        [HttpGet("GetBillPaymentStatus")]
        [Produces("application/json")]
        public ResponseModel GetBillPaymentStatus(string accountId , string txid, string transtype)
        {
            var SaveBookingResponse = _dataRepository.UpdateBillpayemnt(accountId, transtype, txid);
            return null;
        }

        [HttpGet("CheckBillPaymentStatus")]
        [Produces("application/json")]
        public BillStatus CheckBillPaymentStatus(string RequestId)
        {


            BillStatus objres = new BillStatus();

            string mytxid = "mytxid";
            string ApiUrl = Credentials.CheckBillStatus + "MobileNo=" + Credentials.MobileNo + "&PinNumber=" + Credentials.PinNo + "&Command=" + mytxid + "&RequestId=" + RequestId;
            string result = BillPaymentCommon.HITMultiRechargeAPI(ApiUrl);
            objres = JsonConvert.DeserializeObject<BillStatus>(result);
            string transtype = objres.Status == "2" ? "S" : "F";

            if (objres.Message != "Your Transaction Id Invalid or You have not Made This Type of  Transcation Recharge.")
            {
                var SaveBookingResponse = _dataRepository.UpdateBillpayemnt(RequestId, transtype, objres.OperatorId);
                //var SaveBookingResponse = _dataRepository.UpdateBillpayemnt(RequestId, transtype);
            }


            return objres;
        }

        //[HttpGet("UpdatePendingRecharge")]
        //[Produces("application/json")]
        //public BillStatus UpdatePendingRecharge()
        //{
        //    BillStatus objres = new BillStatus();
        //    DataSet dataSet1 = objres.GetPendingRecharge();
        //    for(int i = 0; i <= dataSet1.Tables[0].Rows.Count-1;i++)
        //    {
        //        string Request = dataSet1.Tables[0].Rows[i]["Request"].ToString();
        //        PendinGRecharge objres1 = JsonConvert.DeserializeObject<PendinGRecharge>(Request);

        //        BillPaymentResponse objres11 = new BillPaymentResponse();
        //        objres11.RechargeType = "BP";
        //        objres11.ServiceName = objres1.ServiceName;
        //        objres11.Amount = objres1.Amount;
        //        objres11.RechargeNumber = objres1.RechargeNumber;
        //        objres11.TransId = dataSet1.Tables[0].Rows[i]["OrderId"].ToString();
        //        objres11.circle = "";
        //        objres11.Fk_MemId = objres1.Fk_MemId.ToString();
        //        objres11.PaymentId = dataSet1.Tables[0].Rows[i]["PaymentId"].ToString();
        //        objres11.OrderId = dataSet1.Tables[0].Rows[i]["OrderId"].ToString();
        //        objres11.Status = "0";
        //        var SaveBookingResponse = _dataRepository.SaveBillPaymentResponse(objres11);
        //    }


        //    return objres;
        //}

    }
}
