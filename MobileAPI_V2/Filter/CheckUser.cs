using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using System.Linq;
using System;
using Microsoft.Extensions.Primitives;
using System.Reflection.PortableExecutable;
using Nancy;
using System.Net;
using System.Data;
using MobileAPI_V2.Model.Travel;
using MobileAPI_V2.Model;
using AutoMapper.Execution;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;
using Microsoft.Extensions.Configuration;

namespace MobileAPI_V2.Filter
{
    public class CheckUser:IActionFilter
    {
        string _ConnectionString;
        IHttpContextAccessor _httpContextAccessor;
        private readonly IDataRepository _dataRepository;
        public CheckUser(IHttpContextAccessor httpContextAccessor, IDataRepository dataRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataRepository = dataRepository;
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            var result = new Common();
            CheckUserToken checkUserToken = new CheckUserToken();
            const string HeaderKeyName = "Authorization";
            var TokenHeader  = context.HttpContext.Request.Headers.TryGetValue(HeaderKeyName, out StringValues headerValue);
            string Token = headerValue.ToString();
            Token = Token.Replace("Bearer ","");
            if (string.IsNullOrEmpty(Token))
            {
                result.message = "Please pass token";
                result.response = System.Net.HttpStatusCode.BadRequest.ToString();
                ContentResult content = new ContentResult();
                content.ContentType = "application/json";
                content.Content = JsonConvert.SerializeObject(result);
                content.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                context.Result = content;
            }
            else
            {
                string Fk_MemId = Token.Split('-')[2];
                string Fk_MemId1 = Fk_MemId;


                if (string.IsNullOrEmpty(Token))
                {
                    result.message = "You are not authorized person";
                    result.response = System.Net.HttpStatusCode.Unauthorized.ToString();
                    ContentResult content = new ContentResult();
                    content.ContentType = "application/json";
                    content.Content = JsonConvert.SerializeObject(result);
                    content.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    context.Result = content;
                }

                else
                {
                    //string AESKEY = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
                    //Fk_MemId1 = ApiEncrypt_Decrypt.DecryptString(AESKEY, Fk_MemId1);
                    DataSet dataset = checkUserToken.UserToken(Token, Fk_MemId1);
                    if (dataset.Tables[0].Rows[0]["Flag"].ToString() == "0")
                    {

                        result.message = "Unauthorized User";
                        result.response = System.Net.HttpStatusCode.Unauthorized.ToString();
                        ContentResult content = new ContentResult();
                        content.ContentType = "application/json";
                        content.Content = JsonConvert.SerializeObject(result);
                        content.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                        context.Result = content;
                    }
                }
            }
           

            
        }

        public async void OnActionExecuted(ActionExecutedContext context)
        {
            const string HeaderKeyName = "Token";
            var TokenHeader = context.HttpContext.Request.Headers.TryGetValue(HeaderKeyName, out StringValues headerValue);
            string Token = headerValue.ToString();

            const string SHeaderKeyName = "Fk_MemId";
            var Fk_MemIdHeader = context.HttpContext.Request.Headers.TryGetValue(SHeaderKeyName, out StringValues Fk_MemId);
            string Fk_MemId1 = Fk_MemId.ToString();

           

        }
    }
}
