using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Model;
using MobileAPI_V2.Models;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;
using System.IO;
using System.Runtime.Serialization.Json;
using System;
using System.Data;
using MobileAPI_V2.Model.Travel.Bus;
using System.Collections.Generic;
using MobileAPI_V2.Model.Fineque;

namespace MobileAPI_V2.Controllers
{
    [Route("api/Bus")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        string travelProtal = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("TravelProtal").Value;
        string busapiurl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("BusAPIURL").Value;
        string Aeskey = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
        string clientId = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("ClientId").Value;
        string userName = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("UserName").Value;
        string password = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("TravelPassword").Value;
        string endUserIP = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("EndUserIP").Value;
        string busurl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("BusSerchAPIURL").Value;
       
        public BusController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {

            _dataRepository = dataRepository;
        }

        [HttpPost("Authenticate")]
        [Produces("application/json")]
        public ResponseModel Authenticate()
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            AuthResponse authres = new AuthResponse();
            Authentication authreq = new Authentication();
            try
            {
                authreq.UserName = userName;
                authreq.Password=password;
                authreq.ClientId = clientId;
                authreq.EndUserIp = endUserIP;
                string body = JsonConvert.SerializeObject(authreq);
                string url = travelProtal + "Authenticate";
                string result = Common.HITBusAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                AuthResponse res = JsonConvert.DeserializeObject<AuthResponse>(result);
                if (res != null)
                {
                    if (res.Status == 1)
                    {
                        var saveres = _dataRepository.SaveBusToken(res.TokenId);
                        authres.Status = res.Status;
                        authres.TokenId = res.TokenId;
                        authres.Member = res.Member;
                    }
                    else
                    {
                        authres.Status = res.Status;
                        authres.Error = res.Error;
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AuthResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, authres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("BusCityList")]
        [Produces("application/json")]
        public ResponseModel BusCityList()
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BusCityListResponse objres = new BusCityListResponse();
            BusCityList objreq = new BusCityList();
            try
            {
                var tokenres = _dataRepository.GetBUSToken();
                
                objreq.ClientId = clientId;
                objreq.IpAddress = endUserIP;
                objreq.TokenId = tokenres.Result.Token;
                string body = JsonConvert.SerializeObject(objreq);
                string url = busapiurl + "GetBusCityList";
                string result = Common.HITBusAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                BusCityListResponse res = JsonConvert.DeserializeObject<BusCityListResponse>(result);
                if (res != null)
                {
                    if (res.Status == 1)
                    {
                        objres.Status = res.Status;
                        objres.TokenId = res.TokenId;
                        objres.BusCities = res.BusCities;
                    }
                    else
                    {
                        objres.Error=res.Error;
                        objres.Status=res.Status;
                        objres.TokenId=res.TokenId;
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusCityListResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("BusSearch")]
        [Produces("application/json")]
        public ResponseModel BusSearch(RequestModel reqModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BusServiceResponse objres = new BusServiceResponse();
            BusService objreq = new BusService();
            try
            {
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, reqModel.Body);
                objreq = JsonConvert.DeserializeObject<BusService>(dcdata1);
                var tokenres = _dataRepository.GetBUSToken();
                objreq.TokenId = tokenres.Result.Token;
                objreq.EndUserIp = endUserIP;
                string body = JsonConvert.SerializeObject(objreq);
                string url = busurl + "Search";
                string result = Common.HITBusAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                BusServiceResponse res = JsonConvert.DeserializeObject<BusServiceResponse>(result);
                if (res != null)
                {

                    objres.BusSearchResult = res.BusSearchResult;

                }
                else
                {
                    objres.BusSearchResult = res.BusSearchResult;

                }

            }
            catch (Exception ex)
            {
                throw;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusServiceResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("BusSeatLayout")]
        [Produces("application/json")]
        public ResponseModel BusSeatLayout(RequestModel reqModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BusSeatLayoutResponse objres = new BusSeatLayoutResponse();
            BusSeatLayout objreq = new BusSeatLayout();
            try
            {
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, reqModel.Body);
                objreq = JsonConvert.DeserializeObject<BusSeatLayout>(dcdata1);
                var tokenres = _dataRepository.GetBUSToken();
                objreq.TokenId = tokenres.Result.Token;
                objreq.EndUserIp = endUserIP;
                string body = JsonConvert.SerializeObject(objreq);
                string url = busurl + "GetBusSeatLayOut";
                string result = Common.HITBusAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                BusSeatLayoutResponse res = JsonConvert.DeserializeObject<BusSeatLayoutResponse>(result);
                if (res != null)
                {

                    objres.GetBusSeatLayOutResult = res.GetBusSeatLayOutResult;

                }
                else
                {
                    objres.GetBusSeatLayOutResult = res.GetBusSeatLayOutResult;

                }

            }
            catch (Exception ex)
            {
                throw;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusSeatLayoutResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("BusBoarding")]
        [Produces("application/json")]
        public ResponseModel BusBoarding(RequestModel reqModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BusBoardingResponse objres = new BusBoardingResponse();
            BusBoarding objreq = new BusBoarding();
            try
            {
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, reqModel.Body);
                objreq = JsonConvert.DeserializeObject<BusBoarding>(dcdata1);
                var tokenres = _dataRepository.GetBUSToken();
                objreq.TokenId = tokenres.Result.Token;
                objreq.EndUserIp = endUserIP;
                string body = JsonConvert.SerializeObject(objreq);
                string url = busurl + "GetBoardingPointDetails";
                string result = Common.HITBusAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                BusBoardingResponse res = JsonConvert.DeserializeObject<BusBoardingResponse>(result);
                if (res != null)
                {

                    objres.GetBusRouteDetailResult = res.GetBusRouteDetailResult;

                }
                else
                {
                    objres.GetBusRouteDetailResult = res.GetBusRouteDetailResult;

                }

            }
            catch (Exception ex)
            {
                throw;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusBoardingResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("BusBlock")]
        [Produces("application/json")]
        public ResponseModel BusBlock(RequestModel reqModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BusBlockResponse objres = new BusBlockResponse();
            BusBlock objreq = new BusBlock();
            try
            {
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, reqModel.Body);
                objreq = JsonConvert.DeserializeObject<BusBlock>(dcdata1);
                var tokenres = _dataRepository.GetBUSToken();
                objreq.TokenId = tokenres.Result.Token;
                objreq.EndUserIp = endUserIP;
                string body = JsonConvert.SerializeObject(objreq);
                string url = busurl + "Block";
                string result = Common.HITBusAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                BusBlockResponse res = JsonConvert.DeserializeObject<BusBlockResponse>(result);
                if (res != null)
                {

                    objres.BlockResult = res.BlockResult;

                }
                else
                {
                    objres.BlockResult = res.BlockResult;

                }

            }
            catch (Exception ex)
            {
                throw;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusBlockResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("BusBook")]
        [Produces("application/json")]
        public ResponseModel BusBook(RequestModel reqModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BusBookResponse objres = new BusBookResponse();
            BusBook objreq = new BusBook();
            try
            {
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, reqModel.Body);
                objreq = JsonConvert.DeserializeObject<BusBook>(dcdata1);
                var tokenres = _dataRepository.GetBUSToken();
                objreq.TokenId = tokenres.Result.Token;
                objreq.EndUserIp = endUserIP;
                string body = JsonConvert.SerializeObject(objreq);
                string url = busurl + "Book";
                string result = Common.HITBusAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                BusBookResponse res = JsonConvert.DeserializeObject<BusBookResponse>(result);
                if (res != null)
                {

                    objres.BookResult = res.BookResult;

                }
                else
                {
                    objres.BookResult = res.BookResult;

                }

            }
            catch (Exception ex)
            {
                throw;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusBookResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("BusBookDetails")]
        [Produces("application/json")]
        public ResponseModel BusBookDetails(RequestModel reqModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BusBookingDetailsResponse objres = new BusBookingDetailsResponse();
             BusBookingDetails objreq = new BusBookingDetails();
            try
            {
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, reqModel.Body);
                objreq = JsonConvert.DeserializeObject<BusBookingDetails>(dcdata1);
                var tokenres = _dataRepository.GetBUSToken();
                objreq.TokenId = tokenres.Result.Token;
                objreq.EndUserIp = endUserIP;
                string body = JsonConvert.SerializeObject(objreq);
                string url = busurl + "GetBookingDetail";
                string result = Common.HITBusAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                BusBookingDetailsResponse res = JsonConvert.DeserializeObject<BusBookingDetailsResponse>(result);
                if (res != null)
                {

                    objres.GetBookingDetailResult = res.GetBookingDetailResult;

                }
                else
                {
                    objres.GetBookingDetailResult = res.GetBookingDetailResult;

                }

            }
            catch (Exception ex)
            {
                throw;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusBookingDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("BusCancel")]
        [Produces("application/json")]
        public ResponseModel BusCancel(RequestModel reqModel)
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            BusCancelResponse objres = new BusCancelResponse();
            BusCancel objreq = new BusCancel();
            try
            {
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, reqModel.Body);
                objreq = JsonConvert.DeserializeObject<BusCancel>(dcdata1);
                var tokenres = _dataRepository.GetBUSToken();
                objreq.TokenId = tokenres.Result.Token;
                objreq.EndUserIp = endUserIP;
                string body = JsonConvert.SerializeObject(objreq);
                string url = busurl + "SendChangeRequest";
                string result = Common.HITBusAPI(url, body);
                ResponseModel objres1 = JsonConvert.DeserializeObject<ResponseModel>(result);
                BusCancelResponse res = JsonConvert.DeserializeObject<BusCancelResponse>(result);
                if (res != null)
                {

                    objres.SendChangeRequestResult = res.SendChangeRequestResult;

                }
                else
                {
                    objres.SendChangeRequestResult = res.SendChangeRequestResult;

                }

            }
            catch (Exception ex)
            {
                throw;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusCancelResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
    }
}
