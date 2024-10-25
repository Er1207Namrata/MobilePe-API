using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Model;
using MobileAPI_V2.Models;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System;
using MobileAPI_V2.Model.Fineque;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;
using System.Reflection.PortableExecutable;
using System.Data;
using System.Collections.Generic;
using System.Net;
using Nancy.Json;

namespace MobileAPI_V2.Controllers
{
    [Route("api/Fineque")]
    [ApiController]
    public class FinequeController : ControllerBase
    {
        private readonly FinqueService _finqueService;
        // private readonly IDataRepository _dataRepository;
        IHttpContextAccessor _httpContextAccessor;
        public FinequeController(FinqueService finqueService, IHttpContextAccessor acc, IDataRepository dataRepository, IConfiguration configuration)
        {

            //_dataRepository = dataRepository;
            _finqueService = finqueService;
            this._httpContextAccessor = acc;
        }
        //string finequeurl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("FinequeURL").Value;
        string Aeskey = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
        //string partnerKey = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("PartnerKey").Value;
        //string partnerSecret = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("PartnerSecret").Value;
        //string merchantId = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("merchantId").Value;

        [HttpGet("GenerateHash")]
        public IActionResult GenerateHash()
        {

            var response = _finqueService.GetRequestHashCode();

            return Ok(response);
        }
        [HttpPost("FinequeLogin")]
        public IActionResult FinequeLogin()
        {

            var response = _finqueService.GenerateToken();

            return Ok(response);
        }
        [HttpGet("ProductCode")]
        public IActionResult ProductCode()
        {
            ResponseModel objres = new ResponseModel();
            CommonListResponseDTO<FinequeProductCode> _commonResponse = new CommonListResponseDTO<FinequeProductCode>();
            var response = _finqueService.GetProductCode().Result;
            if (response != null)
            {
                _commonResponse.flag = 1;
                _commonResponse.Status = true;
                _commonResponse.result = response;
            }
            string Body = JsonConvert.SerializeObject(_commonResponse);

            string dcdata1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, Body);
            objres.Body = dcdata1;
            return Ok(objres);
        }

        [HttpPost("FinequeQdeUrl")]
        public IActionResult FinequeQdeUrl(RequestModel reqModel)
        {
            ResponseModel objres = new ResponseModel();
            var response = _finqueService.GetQdeUrl(reqModel);
            string Body = JsonConvert.SerializeObject(response);

            string dcdata1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, Body);
            objres.Body = dcdata1;
            return Ok(objres);
        }
        [HttpPost("FinequeAppId")]
        public IActionResult FinequeAppId(RequestModel reqModel)
        {
            ResponseModel objres = new ResponseModel();
            var response = _finqueService.GetAppId(reqModel);
            string Body = JsonConvert.SerializeObject(response);

            string dcdata1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, Body);
            objres.Body = dcdata1;
            return Ok(objres);
        }
        [HttpPost("FinequeLoanStatus")]
        public IActionResult FinequeLoanStatus(RequestModel reqModel)
        {
            ResponseModel objres = new ResponseModel();
            var response = _finqueService.GetLoanStatus(reqModel);
            string Body = JsonConvert.SerializeObject(response);
            string dcdata1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, Body);
            objres.Body = dcdata1;
            return Ok(objres);
        }

        [HttpGet("ShopType")]
        public IActionResult ShopType()
        {
            ResponseModel objres = new ResponseModel();

            CommonListResponseDTO<ShopTypeDTO> _commonResponse = new CommonListResponseDTO<ShopTypeDTO>();
            var response = _finqueService.GetShopType().Result;
            if (response != null)
            {
                _commonResponse.flag = 1;
                _commonResponse.Status = true;
                _commonResponse.result = response;
            }
            string Body = JsonConvert.SerializeObject(_commonResponse);

            string dcdata1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, Body);
            objres.Body = dcdata1;
            return Ok(objres);
        }
        [HttpGet("UserLoanApplications")]
        public IActionResult UserLoanApplications(RequestModel reqModel)
        {

            CommonListResponseDTO<QdeUrl> _commonResponse = new CommonListResponseDTO<QdeUrl>();
            ResponseModel objres = new ResponseModel();
            var response = _finqueService.GetLoanRegistradList(reqModel).Result;
            if (response != null)
            {
                _commonResponse.flag = 1;
                _commonResponse.Status = true;
                _commonResponse.result = response;
            }
            string Body = JsonConvert.SerializeObject(_commonResponse);

            string dcdata1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, Body);
            objres.Body = dcdata1;
            return Ok(objres);
        }
    }
}
