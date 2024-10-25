using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MobileAPI_V2.Model;
using MobileAPI_V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Filter
{
    public class Tenant : IActionFilter
    {

        JWTToken objjwt;

        private readonly IDataRepository _dataRepository;

        public Tenant(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            objjwt = new JWTToken(_dataRepository);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {


            Common objresponnse = new Common();
            string Token = context.HttpContext.Request.Headers["Tenant"];
            if (!string.IsNullOrEmpty(Token))
            {

                string tokenvalue = objjwt.ValidateToken(Token) == null ? "" : objjwt.ValidateToken(Token);
                if (!string.IsNullOrEmpty(tokenvalue))
                {

                }
                else
                {
                    objresponnse.response = "error";
                    objresponnse.message = "token expired";
                    
                }

            }
            else
            {
                objresponnse.response = "error";
                objresponnse.message = "missing headers";
               
            }

            if (objresponnse != null && objresponnse.response != null)
            {

                context.Result = new JsonResult(objresponnse)
                {
                    //  StatusCode = StatusCodes.Status400BadRequest // Status code here 
                    StatusCode = 201 // Status code here 
                };

            }
        }
    }
}
