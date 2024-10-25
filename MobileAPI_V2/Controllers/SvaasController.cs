using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model;
using MobileAPI_V2.Model.Svaas;
using MobileAPI_V2.Model.Travel;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileAPI_V2.Controllers
{
    [ApiController]
    [Route("api/Auth/")]
    public class SvaasController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public SvaasController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {

            _dataRepository = dataRepository;
        }
        [HttpPost("GetSvaasPinCode")]
        [Produces("application/json")]
        public async Task<object> GetSvaasPinCode(SvaasPinCode svaasPinCode)
        {
            PinCodeResult pinCodeResult = new PinCodeResult();
            SvaasPinCodeResponse objres = new SvaasPinCodeResponse();
            PinCodeData pinCodeData = new PinCodeData();
            var res = await this._dataRepository.GetSvaasAvailbility(svaasPinCode.PinCode);
            if (res != null && res.Count > 0)
            {
                objres.message = "Success";
                objres.response = "Success";
                pinCodeResult.lstPincode = res;
                objres.result = pinCodeResult;
            }
            else
            {
                objres.message = "error";
                objres.response = "Pincode not available";
            }

            return objres;
        }

        [HttpPost("SvaasPaymentReponse")]
        [Produces("application/json")]
        public async Task<CommonResponse<SvaasSaveResponse>>  SvaasPaymentReponse(SvaaspaymentResponse svaaspaymentResponse)
        {
            CommonResponse<SvaasSaveResponse> objres = new CommonResponse<SvaasSaveResponse>();
            // var res = _dataRepository.SvaasPaymentReponse(svaaspaymentResponse); 
            var res = await _dataRepository.SvaasPaymentReponse(svaaspaymentResponse);
            if (res != null && res.flag==1)
            {
                objres.message = "Success";
                objres.response = "1";
            }
            else
            {
                objres.message = "error";
                objres.response = "0";
            }

            return objres;



        }

        [HttpGet("GetOrders")]
        [Produces("application/json")]
        public async Task<object> GetOrders(string FK_MemId)
        {
            OrderResult orderResult = new OrderResult();
            SvaasOrdersResponse objres = new SvaasOrdersResponse();
            OrderData orderData = new OrderData();
            var res = await this._dataRepository.GetSvaasOrders(FK_MemId);
            if (res != null && res.Count > 0)
            {
                objres.message = "Success";
                objres.response = "Success";
                orderResult.lstOrders = res;
                objres.result = orderResult;
            }
            else
            {
                objres.message = "No Record Found";
                objres.response = "error";
            }

            return objres;
        }



    }
}
