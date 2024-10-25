using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MobileAPI_V2.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MobileAPI_V2.Services
{
    public class RechargeSchedular : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

             string s=   UpdateRecharge();
                // var res=api.UpdatePendingRecharge();
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await ExecuteAsync(cancellationToken);
        }
        string  UpdateRecharge()
        {
            string responseText = "";
            try
            {
                string completeurl = "https://apiv2.mobilepe.co.in/api/Customer/UpdatePendingRecharge";
                var request = (HttpWebRequest)WebRequest.Create(completeurl);
                if (request != null)
                {
                    request.ContentType = "application/json";
                    request.Timeout = 50000;
                    request.Method = "GET";
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;
                    request.Credentials = CredentialCache.DefaultCredentials;

                    using (Stream s = request.GetResponse().GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                            responseText =  sr.ReadToEnd();
                    }
                }

            }
            catch (Exception e)
            {
                responseText = e.Message;
            }
            return responseText;
        }
    }
}
