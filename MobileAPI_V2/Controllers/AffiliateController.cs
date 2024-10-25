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
using MobileAPI_V2.Model.Affiliate;

namespace MobileAPI_V2.Controllers
{
    [Route("api/Affiliate")]
    [ApiController]
    public class AffiliateController : ControllerBase
    {
        private readonly AffiliateService _AffiliateService;
        IHttpContextAccessor _httpContextAccessor;

        public AffiliateController(AffiliateService affiliateService, IHttpContextAccessor acc)
        {
            _AffiliateService = affiliateService;
            this._httpContextAccessor = acc;

        }
        string AESKEY = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;

        [HttpPost("AffiliateFinanceRegistration")]
        public IActionResult AffiliateFinanceRegistration(RequestModel reqModel)
        {

            string EncryptedText = "";
            ResponseModel objres = new ResponseModel();
            CommonResponseDTO<AffiliateFinanceRegistrationDTO> _CommonResponse = new CommonResponseDTO<AffiliateFinanceRegistrationDTO>();
            AffiliateFinanceRegistrationDTO objrequest = new AffiliateFinanceRegistrationDTO();

            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, reqModel.Body);
                objrequest = JsonConvert.DeserializeObject<AffiliateFinanceRegistrationDTO>(dcdata);
                var response = _AffiliateService.AffiliateFinanceRegistration(objrequest);
                if (response != null)
                {
                    if (response.Status)
                    {
                        _CommonResponse.flag = 1;
                        _CommonResponse.Status = true;
                        _CommonResponse.message = response.message;
                    }
                    else
                    {
                        _CommonResponse.flag = 0;
                        _CommonResponse.Status = false;
                        _CommonResponse.message = response.message;
                    }
                }
                else
                {
                    _CommonResponse.flag = 0;
                    _CommonResponse.Status = false;
                    _CommonResponse.message = "We are facing some technical issues please try after some time";
                }
            }
            catch (Exception ex)
            {
                _CommonResponse.flag = 0;
                _CommonResponse.Status = false;
                _CommonResponse.message = "We are facing some technical issues please try after some time. #001";
            }


            objres.Body = JsonConvert.SerializeObject(_CommonResponse);
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseDTO<AffiliateFinanceRegistrationDTO>));
            ms = new MemoryStream();
            js.WriteObject(ms, _CommonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            objres.Body = EncryptedText;
            return Ok(objres);
        }

        [HttpPost("GetAffiliateFinanceRegistration")]
        public IActionResult GetAffiliateFinanceRegistration(RequestModel reqModel)
        {
            string EncryptedText = "";
            ResponseModel objres = new ResponseModel();
            CommonResponseDTO<AffiliateFinanceRegistrationDTO> _CommonResponse = new CommonResponseDTO<AffiliateFinanceRegistrationDTO>();
            AffiliateFinanceRegistrationDTO objrequest = new AffiliateFinanceRegistrationDTO();

            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, reqModel.Body);
                objrequest = JsonConvert.DeserializeObject<AffiliateFinanceRegistrationDTO>(dcdata);
                var response = _AffiliateService.GetAffiliateFinanceRegistration(objrequest);
                if (response != null)
                {
                    _CommonResponse.flag = 1;
                    _CommonResponse.Status = true;
                    _CommonResponse.message = "Success";
                    _CommonResponse.result = response;
                }
                else
                {
                    _CommonResponse.flag = 0;
                    _CommonResponse.Status = false;
                    _CommonResponse.message = "We are facing some technical issues please try after some time";
                }
            }
            catch (Exception ex)
            {
                _CommonResponse.flag = 0;
                _CommonResponse.Status = false;
                _CommonResponse.message = "We are facing some technical issues please try after some time. #001";
            }

            objres.Body = JsonConvert.SerializeObject(_CommonResponse);
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseDTO<AffiliateFinanceRegistrationDTO>));
            ms = new MemoryStream();
            js.WriteObject(ms, _CommonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            objres.Body = EncryptedText;
            return Ok(objres);
        }

        [HttpPost("GetAllAffiliateFinanceDetailList")]
        public IActionResult GetAllAffiliateFinanceDetailList(RequestModel reqModel)
        {
            string EncryptedText = "";
            ResponseModel objres = new ResponseModel();
            CommonResponseDTO<List<AffiliateFinanceRegistrationReportDTO>> _CommonResponse = new CommonResponseDTO<List<AffiliateFinanceRegistrationReportDTO>>();
            AffiliateFinanceRegistrationFilterDTO objrequest = new AffiliateFinanceRegistrationFilterDTO();          
            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, reqModel.Body);
                objrequest = JsonConvert.DeserializeObject<AffiliateFinanceRegistrationFilterDTO>(dcdata);
                var response = _AffiliateService.GetAllAffiliateFinanceDetailList(objrequest);
                if (response != null)
                {
                    _CommonResponse.flag = 1;
                    _CommonResponse.Status = true;
                    _CommonResponse.message = "Success";
                    _CommonResponse.result = response;
                }
                else
                {
                    _CommonResponse.flag = 0;
                    _CommonResponse.Status = false;
                    _CommonResponse.message = "No data found.";
                    _CommonResponse.result = new List<AffiliateFinanceRegistrationReportDTO>();
                }
            }
            catch (Exception ex)
            {
                _CommonResponse.flag = 0;
                _CommonResponse.Status = false;
                _CommonResponse.message = "We are facing some technical issues please try after some time. #001";
                _CommonResponse.result = new List<AffiliateFinanceRegistrationReportDTO>();
            }
            objres.Body = JsonConvert.SerializeObject(_CommonResponse);
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommonResponseDTO<List<AffiliateFinanceRegistrationReportDTO>>));
            ms = new MemoryStream();
            js.WriteObject(ms, _CommonResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            objres.Body = EncryptedText;
            return Ok(objres);
        }
    }
}
