using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Model.Travel;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;
using System.IO;
using System.Runtime.Serialization.Json;
using System;
using MobileAPI_V2.Model.Thriwe;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace MobileAPI_V2.Controllers
{
    [ApiController]
    [Route("api/Auth/")]
    public class ThriweController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        string BaseURL = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("ThriweBaseURL").Value;
        public ThriweController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {

            _dataRepository = dataRepository;

        }
        [HttpPost("AddCustomer")]
        [Produces("application/json")]
        public CustomerResponse AddCustomer(AddCustomers addCustomers)
        {
            
            CustomerResponse objres = new CustomerResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                
                string ApiUrl = BaseURL + "users";
                string result = ThriweCommon.HITAPI(ApiUrl, JsonConvert.SerializeObject(addCustomers));
                objres = JsonConvert.DeserializeObject<CustomerResponse>(result);


            }
            catch (Exception ex)
            {
                objres.message = ex.Message;
            }
            
            return objres;
        }
        [HttpPost("BenefitPacks")]
        [Produces("application/json")]
        public BenefitResponse BenefitPacks(BenefitReq benefitReq,string UserId)
        {

            BenefitResponse objres = new BenefitResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                string ApiUrl = BaseURL + "users/"+ UserId + "/benefitPacks";
                string result = ThriweCommon.HITAPI(ApiUrl, JsonConvert.SerializeObject(benefitReq));
                objres = JsonConvert.DeserializeObject<BenefitResponse>(result);


            }
            catch (Exception ex)
            {
                objres.message = ex.Message;
            }

            return objres;
        }


    }
}
