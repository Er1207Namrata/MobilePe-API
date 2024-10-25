using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileAPI_V2.Filter;
using MobileAPI_V2.Model;
using MobileAPI_V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Razorpay.Api;
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using System.Xml;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Globalization;
using System.Threading;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Routing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System.Security.Claims;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Reflection;
using System.Security.Permissions;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
using static MobileAPI_V2.Model.FBH_Repository;

namespace MobileAPI_V2.Controllers
{
    [ServiceFilter(typeof(ApiLog))]
    [Route("api/[controller]")]
    [ApiController]
    

    public class FBHController : ControllerBase
    {
        private IHostingEnvironment _env;
        private readonly IDataRepository _dataRepository;
        private readonly IConfiguration _configuration;
        public string razorpayupikey = string.Empty;
        public string razorpayupisecret = string.Empty;
        public string razorpaydebitcardkey = string.Empty;
        public string razorpaydebitcardsecret = string.Empty;
        public string razorpaycreditcardkey = string.Empty;
        public string razorpaycreditcardsecret = string.Empty;
        public string VenusRechargeURL = string.Empty;
        public string INSURANCEURLNEW = string.Empty;
        public string INSURANCEURLCOMPAYCODE = string.Empty;
        public string INSURANCEURLENCRYPTIONKEY = string.Empty;
        public string INSURANCEURL = string.Empty;
        string razorpaycontactsUrl = string.Empty;
        string razorpayfundAccountUrl = string.Empty;
        string razorpaypayoutUrl = string.Empty;
        string VOUCHERURL = string.Empty;
        RechargeConn recConn = new RechargeConn();
        CommonJsonPostRequest CommonJsonPostRequestobj;
        PineCommonRequest PineCommonRequestobj;
        ApiRequest request;
        JWTToken objJWT;
        SendSMS objsms;
        OAuthorization oAuthorization;
        string PineCardbaseUrl = string.Empty;
        string PINECARDTOKENURL = string.Empty;
        string WorkingKey = string.Empty;
        // Flight , Bus & Hotel Booking
        FBH_Repository FBH_Repository;
        string FBH_Authenticate = string.Empty;
        
        public FBHController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {
            _env = env;
            _dataRepository = dataRepository;
            _configuration = configuration;
            oAuthorization = new OAuthorization(_configuration);
            objJWT = new JWTToken(_dataRepository);
            objsms = new SendSMS(_configuration, _dataRepository);
            CommonJsonPostRequestobj = new CommonJsonPostRequest(_configuration);
            PineCommonRequestobj = new PineCommonRequest(_configuration);
            razorpayupikey = _configuration["razorpayupikey"];
            razorpayupisecret = _configuration["razorpayupisecret"];
            razorpaydebitcardkey = _configuration["razorpaydebitcardkey"];
            razorpaydebitcardsecret = _configuration["razorpaydebitcardsecret"];
            razorpaycreditcardkey = _configuration["razorpaycreditcardkey"];
            razorpaycreditcardsecret = _configuration["razorpaycreditcardsecret"];
            VenusRechargeURL = _configuration["VenusRecharge"];
            INSURANCEURLNEW = _configuration["INSURANCEURLNEW"];
            INSURANCEURLCOMPAYCODE = _configuration["INSURANCEURLCOMPAYCODE"];
            INSURANCEURLENCRYPTIONKEY = _configuration["INSURANCEURLENCRYPTIONKEY"];
            INSURANCEURL = _configuration["INSURANCEURL"];
            razorpaycontactsUrl = _configuration["razorpaycontactsUrl"];
            razorpayfundAccountUrl = _configuration["razorpayfundAccountUrl"];
            razorpaypayoutUrl = _configuration["razorpaypayoutUrl"];
            request = new ApiRequest(_configuration);
            PineCardbaseUrl = _configuration["PineCardbaseUrl"];
            PINECARDTOKENURL = _configuration["PINECARDTOKENURL"];
            WorkingKey = _configuration["WorkingKey"];

            //
            FBH_Repository = new FBH_Repository(_configuration);

            FBH_Authenticate = _configuration["FBH_Authenticate"];
        }

      

        private readonly Dictionary<string, byte[]> _mimeTypes = new Dictionary<string, byte[]>
    {
        {"image/jpeg", new byte[] {255, 216, 255}},
        {"image/jpg", new byte[] {255, 216, 255}},
        //{"image/pjpeg", new byte[] {255, 216, 255}},
        //{"image/apng", new byte[] {137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82}},
        {"image/png", new byte[] {137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82}},
       // {"image/bmp", new byte[] {66, 77}},
        {"image/gif", new byte[] {71, 73, 70, 56}},
    };


        private bool ValidateMimeType(byte[] file, string contentType)
        {
            var imageType = _mimeTypes.SingleOrDefault(x => x.Key.Equals(contentType));

            return file.Take(imageType.Value.Length).SequenceEqual(imageType.Value);
        }

        [HttpPost("FBH_Auth")]
        public FBHResponseModel FBHAuthentication(FBH_Auth_Request model)
        {

            FBHResponseModel commonResponse = new FBHResponseModel();
           
            try
            {
               

                var result = FBH_Repository.sendRequest(FBH_Authenticate, "POST", JsonConvert.SerializeObject(model), 0);
                string responseText = result.responseText;
                commonResponse = JsonConvert.DeserializeObject<FBHResponseModel>(responseText);

            }
            catch (Exception ex)
            {
                //commonResponse.response = "error";
                //commonResponse.message = ex.Message;

            }
            return commonResponse;
        }


    }
}
