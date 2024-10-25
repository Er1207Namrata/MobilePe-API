using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Services;
using System;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;

namespace MobileAPI_V2.Controllers
{
    public class VerificationController : Controller
    {
        private readonly IDataRepositoryEcomm _dataRepository;
        string AESKEY = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
        public VerificationController(IDataRepositoryEcomm dataRepository)
        {
            _dataRepository = dataRepository;
        }
        public IActionResult Index()
        {
            return Ok();
        }

        [Route("/User/Account/Verification/step/2/v1/proceed/11.00.290.887.00/cyUYbbmbTYUvvcRDET")]
        public IActionResult AccountVerification(string _)
        {
            string key = ApiEncrypt_Decrypt.DecryptString(AESKEY, _);
            string[] Keys = key.Split('&');
            var _response = _dataRepository.UserAccountVerification(Convert.ToInt32(Keys[1]), Keys[0], Keys[2]).Result;
            string Message = _response.Item1 ? "Thank you.\n\nYour account verification process has been successfully completed." : _response.Item2;
            return Ok(Message);
        }
    }
}
