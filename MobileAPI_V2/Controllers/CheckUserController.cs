using Microsoft.AspNetCore.Mvc;
using MobileAPI_V2.Model;
using System;
using System.Threading.Tasks;

namespace MobileAPI_V2.Controllers
{
    public class CheckUserController : ControllerBase
    {
        [HttpPost("CheckUser")]
        public async Task<Common> CheckUser(ChangePassword objrequest)
        {
            Common obj = new Common();
            try
            {
                
                    obj.response = "error";
                    obj.message = "";
               

            }

            catch (Exception ex)
            {
                obj.response = "error";
                obj.message = ex.Message;
            }

            return obj;
        }
    }
}
