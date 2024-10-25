using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model;
using MobileAPI_V2.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MobileAPI_V2.Model.BBPS;

using System.Collections.Generic;

using System;
using System.Threading.Tasks;

namespace MobileAPI_V2.Controllers
{
    [ApiController]
    [Route("api/Auth/")]
    public class BBPSController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        string BBPSBillPayURL = string.Empty;
        string BillersURL = string.Empty;
        string BBPSBillValidateURL = string.Empty;
        string BBPSBillValidatePrePaidURL = string.Empty;
        string BBPSBillFetchURL = string.Empty;
        string BBPSTranctionStatusCheckURL = string.Empty;
       
        private readonly IConfiguration _configuration;
        Fintech fintech;
        public BBPSController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {
            _dataRepository = dataRepository;
            _configuration = configuration;
            BBPSBillPayURL = _configuration["BBPSBillPayURL"];
            BillersURL = _configuration["BillersURL"];
            BBPSBillValidateURL = _configuration["BBPSBillValidateURL"];
            BBPSBillValidatePrePaidURL = _configuration["BBPSBillValidatePrePaidURL"];
            BBPSBillFetchURL = _configuration["BBPSBillFetchURL"];
            BBPSTranctionStatusCheckURL = _configuration["BBPSTranctionStatusCheckURL"];
          
            fintech = new Fintech(_configuration);
        }
        [HttpGet("BillerCategory")]
        public async Task<CommonResponse<List<BillerModel>>> BillerCategory()
        {
            CommonResponse<List<BillerModel>> objres = new CommonResponse<List<BillerModel>>();

            try
            {
               
                var res = await _dataRepository.GetBiller();
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
            return objres;
        }

        [HttpGet("Billers")]
        public object Billers(string BillerType)
        {

            object commonResponse = new object();
            BillersURL = BillersURL + BillerType;
            var result = fintech.sendBBPSRequest(BillersURL, "POST", null, 0);
            string responseText = result.responseText;
            //  commonResponse = JsonConvert.DeserializeObject<WalletV2ResponseModel>(responseText);
            JObject jo = JObject.Parse(responseText);
            commonResponse = fintech.ToApiRequest(jo);
            
            return commonResponse;
        }
        [HttpPost("BBPSBillPay")]
        public object BBPSBillPay(BillPay model)
        {

            object commonResponse = new object();
            
            var result = fintech.sendBBPSRequest(BBPSBillPayURL, "POST", JsonConvert.SerializeObject(model), 0);
            string responseText = result.responseText;
            //  commonResponse = JsonConvert.DeserializeObject<WalletV2ResponseModel>(responseText);
            JObject jo = JObject.Parse(responseText);
            commonResponse = fintech.ToApiRequest(jo);
            var SaveResponse = _dataRepository.SaveBBPSRequest(JsonConvert.SerializeObject(model), responseText, "BBPSBillPay", model.Fk_MemId, model.OrderId);
            return commonResponse;
        }  
        [HttpPost("BBPSBillValidate")]
        public object BBPSBillValidate(BillValidate model)
        {

            object commonResponse = new object();

            var result = fintech.sendBBPSRequest(BBPSBillValidateURL, "POST", JsonConvert.SerializeObject(model), 0);
            string responseText = result.responseText;
            //  commonResponse = JsonConvert.DeserializeObject<WalletV2ResponseModel>(responseText);
            JObject jo = JObject.Parse(responseText);
            commonResponse = fintech.ToApiRequest(jo);

            return commonResponse;
        }
        
        [HttpPost("BBPSBillPayValidatePrePaid")]
        public object BBPSBillPayValidatePrePaid(BillValidatePrePaid model)
        {

            object commonResponse = new object();

            var result = fintech.sendBBPSRequest(BBPSBillValidatePrePaidURL, "POST", JsonConvert.SerializeObject(model), 0);
            string responseText = result.responseText;
            //  commonResponse = JsonConvert.DeserializeObject<WalletV2ResponseModel>(responseText);
            JObject jo = JObject.Parse(responseText);
            commonResponse = fintech.ToApiRequest(jo);

            return commonResponse;
        }

        [HttpPost("BBPSBillPayPrepaid")]
        public object BBPSBillPayPrepaid(BillPay model)
        {

            object commonResponse = new object();
           

            var result = fintech.sendBBPSRequest(BBPSBillPayURL, "POST", JsonConvert.SerializeObject(model), 0);
            string responseText = result.responseText;
            //  commonResponse = JsonConvert.DeserializeObject<WalletV2ResponseModel>(responseText);
            JObject jo = JObject.Parse(responseText);
            commonResponse = fintech.ToApiRequest(jo);

            var SaveResponse = _dataRepository.SaveBBPSRequest(JsonConvert.SerializeObject(model), responseText, "BBPSBillPayPrepaid", model.Fk_MemId,model.OrderId);

            return commonResponse;
        }
        [HttpPost("BBPSBillFetch")]
        public object BBPSBillFetch(BillFetch model)
        {
            object commonResponse = new object();

            var result = fintech.sendBBPSRequest(BBPSBillFetchURL, "POST", JsonConvert.SerializeObject(model), 0);
            string responseText = result.responseText;
            //  commonResponse = JsonConvert.DeserializeObject<WalletV2ResponseModel>(responseText);
            JObject jo = JObject.Parse(responseText);
            commonResponse = fintech.ToApiRequest(jo);

            return commonResponse;
        }

        [HttpPost("BBPSTranctionStatusCheck")]
        public TranctionStatusResponse BBPSTranctionStatusCheck(BBPSTranction model)
        {
            TranctionStatusResponse commonResponse = new TranctionStatusResponse();
            try
            {
                var result = fintech.sendBBPSRequest(BBPSTranctionStatusCheckURL, "POST", JsonConvert.SerializeObject(model), 0);
                string responseText = result.responseText;
                commonResponse = JsonConvert.DeserializeObject<TranctionStatusResponse>(responseText);
                //JObject jo = JObject.Parse(responseText);
                //commonResponse = fintech.ToApiRequest(jo);
            }
            catch (Exception ex)
            {
                commonResponse.status = "error";
                commonResponse.reason = ex.Message;
            }

            return commonResponse;
        }
    }
}



