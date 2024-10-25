using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MobileAPI_V2.Filter
{
    public class ApiLog : IActionFilter
    {
        string _ConnectionString;
        IHttpContextAccessor _httpContextAccessor;
        private readonly IDataRepository _dataRepository;
        public ApiLog(IHttpContextAccessor httpContextAccessor, IDataRepository dataRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataRepository = dataRepository;
        }


        public async void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {

                string data = "";
                var objResult = context.Result as ObjectResult;
                if (objResult != null && objResult.Value != null)
                {
                    data = JsonConvert.SerializeObject(objResult.Value);
                }

               
                var controllername = context.RouteData.Values["controller"].ToString();
                var actionname = context.RouteData.Values["action"].ToString();
                var url = controllername + '/' + actionname;
                string IPAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var response = context.HttpContext.Response.Body;


                var res = await _dataRepository.AddActivityDetails(IPAddress, "", controllername, actionname, url, data);

            }
            catch (Exception e)
            {

            }


        }
        public async void OnActionExecuting(ActionExecutingContext context)
        {
            string data = "";
            var controllername = context.RouteData.Values["controller"].ToString();
            var actionname = context.RouteData.Values["action"].ToString();
            var url = controllername + '/' + actionname;
            string IPAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (!String.IsNullOrEmpty(context.HttpContext.Request.QueryString.Value))
            {
                data = context.HttpContext.Request.QueryString.ToString();
            }
            else
            {
                var userdata = context.ActionArguments.FirstOrDefault();
                data = JsonConvert.SerializeObject(userdata);
            }
            var res = await _dataRepository.AddActivityDetails(IPAddress, data, controllername, actionname, url, "");
        }
    }
}
