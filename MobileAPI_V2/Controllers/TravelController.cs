using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Filter;
using MobileAPI_V2.Model;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Model.Travel;
using MobileAPI_V2.Services;
using Nancy.Diagnostics;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

using SelectPdf;

using System;

using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;

namespace MobileAPI_V2.Controllers
{

    [ApiController]
    [Route("api/Auth/")]
    public class TravelController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        string AESKEY = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
        public TravelController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {

            _dataRepository = dataRepository;
        }
        [HttpPost("Authenticate")]
        [Produces("application/json")]
        public Authenticate Authenticate()
        {
            string JsonData = "{\"ClientId\":\"" + TravelCredentials.ClientId + "\",\"UserName\":\"" + TravelCredentials.UserName + "\",\"Password\":\"" + TravelCredentials.Password + "\",\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\"}";
            var SaveRequest = _dataRepository.SaveTravelRequest(JsonData, "Authenicate", 0);
            string Url = TravelCredentials.Authenticate + "/Authenticate";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, "AuthenicateResponse", 0);
            Authenticate deserializedProduct = JsonConvert.DeserializeObject<Authenticate>(ResponseData);

            return deserializedProduct;
        }
        
        [HttpPost("GetAgencyBalance")]
        [Produces("application/json")]
        public AgencyBalanceResponse GetAgencyBalance()
        {
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string JsonData = "{\"ClientId\":\"" + TravelCredentials.ClientId + "\",\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenAgencyId\":\"" + TravelCredentials.TokenAgencyId + "\",\"TokenMemberId\":\"" + TravelCredentials.TokenMemberId + "\",\"TokenId\":\"" + Token + "\"}";
            string Url = TravelCredentials.Authenticate + "/GetAgencyBalance";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            AgencyBalanceResponse deserializedProduct = JsonConvert.DeserializeObject<AgencyBalanceResponse>(ResponseData);
            return deserializedProduct;
        }
        
        [HttpPost("FlightSearch")]
        [Produces("application/json")]
        public FlightSearchResponse FlightSearch(FlightSearch flightSearch)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;

            flightSearch.TokenId = Token;
            flightSearch.EndUserIp = TravelCredentials.EndUserIP;
            // var json = JsonConvert.SerializeObject(flightSearch);

            string Url = TravelCredentials.GetBaseUrl + "/Search";
            var SaveRequest = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(flightSearch), "FlightSearch", 0);
            string ResponseData = CommonTravel.GetResponse(JsonConvert.SerializeObject(flightSearch), Url);
            FlightSearchResponse deserializedProduct = JsonConvert.DeserializeObject<FlightSearchResponse>(ResponseData);
            var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, "FlightSearchResponse", 0);
            return deserializedProduct;
        }
        
        [HttpPost("FareRule")]
        [Produces("application/json")]
        public FareRuleResponse FareRule(FareRuleRequest fareRuleRequest)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\",\"TraceId\":\"" + fareRuleRequest.TraceId + "\",\"ResultIndex\":\"" + fareRuleRequest.ResultIndex + "\"}";
            var SaveFareRequest = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(fareRuleRequest), "FareRule", fareRuleRequest.FK_MemId);
            string Url = TravelCredentials.GetBaseUrl + "/FareRule";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            FareRuleResponse deserializedProduct = JsonConvert.DeserializeObject<FareRuleResponse>(ResponseData);

            var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, "FareRuleResponse", fareRuleRequest.FK_MemId);
            return deserializedProduct;
        }
        
        [HttpPost("FareQuote")]
        [Produces("application/json")]
        public FareQuoteResponse FareQuote(FareQuoteRequest fareQuoteRequest)
        {
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\",\"TraceId\":\"" + fareQuoteRequest.TraceId + "\",\"ResultIndex\":\"" + fareQuoteRequest.ResultIndex + "\"}";
            var SaveFareRequest = _dataRepository.SaveTravelRequest(JsonData, "FareQuote", fareQuoteRequest.FK_MemId);
            string Url = TravelCredentials.GetBaseUrl + "/FareQuote";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            FareQuoteResponse deserializedProduct = JsonConvert.DeserializeObject<FareQuoteResponse>(ResponseData);

            var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, "FareQuoteResponse", fareQuoteRequest.FK_MemId);
            return deserializedProduct;
        }
        
        [HttpPost("SSR")]
        [Produces("application/json")]
        public SSrResponse SSR(SSR sSR)
        {
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\",\"TraceId\":\"" + sSR.TraceId + "\",\"ResultIndex\":\"" + sSR.ResultIndex + "\"}";
            var SaveFareRequest = _dataRepository.SaveTravelRequest(JsonData, "SSR", sSR.FK_MemId);
            string Url = TravelCredentials.GetBaseUrl + "/SSR";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            SSrResponse deserializedProduct = JsonConvert.DeserializeObject<SSrResponse>(ResponseData);

            var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, "SSRResponse", sSR.FK_MemId);
            return deserializedProduct;
        }
        
        [HttpPost("HoldTicket")]
        [Produces("application/json")]
        public BookTicketResponse HoldTicket(BookTicket bookTicket)
        {
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;

            bookTicket.TokenId = Token;
            bookTicket.EndUserIp = TravelCredentials.EndUserIP;

            var SaveHoldRequest = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(bookTicket), "HoldTicket", bookTicket.Fk_MemId);
            string Url = TravelCredentials.BookingBaseUrl + "/Book";
            string ResponseData = CommonTravel.GetResponse(JsonConvert.SerializeObject(bookTicket), Url);
            BookTicketResponse deserializedProduct = JsonConvert.DeserializeObject<BookTicketResponse>(ResponseData);

            var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, "HoldTicketResponse", bookTicket.Fk_MemId);
            TravelSaveResponse travelSaveResponse = new TravelSaveResponse();
            if (deserializedProduct != null)
            {
                DataTable dtSegment = new DataTable();
                dtSegment.Columns.Add("AirportCode");
                dtSegment.Columns.Add("AirportName");
                dtSegment.Columns.Add("Terminal");
                dtSegment.Columns.Add("CityCode");
                dtSegment.Columns.Add("CityName");
                dtSegment.Columns.Add("CountryCode");
                dtSegment.Columns.Add("CountryName");
                dtSegment.Columns.Add("DepTime");


                DataTable dtSegmentR = new DataTable();
                dtSegmentR.Columns.Add("AirportCode");
                dtSegmentR.Columns.Add("AirportName");
                dtSegmentR.Columns.Add("Terminal");
                dtSegmentR.Columns.Add("CityCode");
                dtSegmentR.Columns.Add("CityName");
                dtSegmentR.Columns.Add("CountryCode");
                dtSegmentR.Columns.Add("CountryName");
                dtSegmentR.Columns.Add("DepTime");

                travelSaveResponse.Fk_MemId = bookTicket.Fk_MemId;

                if (deserializedProduct.Response.Error.ErrorCode == 0)
                {
                    travelSaveResponse.IsSuccess = 1;
                    travelSaveResponse.Message = "";
                    travelSaveResponse.TraceId = deserializedProduct.Response.TraceId;
                    travelSaveResponse.PNRNo = deserializedProduct.Response.Response.PNR;
                    travelSaveResponse.BookingId = deserializedProduct.Response.Response.BookingId;
                    travelSaveResponse.SSRDenied = deserializedProduct.Response.Response.SSRDenied;
                    travelSaveResponse.SSRMessage = deserializedProduct.Response.Response.SSRMessage;
                    travelSaveResponse.Status = deserializedProduct.Response.Response.Status;
                    travelSaveResponse.IsPriceChanged = deserializedProduct.Response.Response.IsPriceChanged;
                    travelSaveResponse.IsTimeChanged = deserializedProduct.Response.Response.IsTimeChanged;
                    travelSaveResponse.IsSuccess = 1;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("JourneyType");
                    dt.Columns.Add("TripIndicator");
                    dt.Columns.Add("PNR");
                    dt.Columns.Add("Origin");
                    dt.Columns.Add("Destination");
                    dt.Columns.Add("AirlineCode");
                    dt.Columns.Add("LastTicketDate");
                    dt.Columns.Add("NonRefundable");
                    dt.Rows.Add(deserializedProduct.Response.Response.FlightItinerary.JourneyType, deserializedProduct.Response.Response.FlightItinerary.TripIndicator, deserializedProduct.Response.Response.FlightItinerary.PNR, deserializedProduct.Response.Response.FlightItinerary.Origin, deserializedProduct.Response.Response.FlightItinerary.Destination, deserializedProduct.Response.Response.FlightItinerary.AirlineCode, deserializedProduct.Response.Response.FlightItinerary.LastTicketDate, deserializedProduct.Response.Response.FlightItinerary.NonRefundable);
                    travelSaveResponse.tblFlightItinerary = dt;
                    for (int k = 0; k < deserializedProduct.Response.Response.FlightItinerary.Segments.Count; k++)
                    {
                        dtSegment.Rows.Add(deserializedProduct.Response.Response.FlightItinerary.Segments[k].Origin.Airport.AirportCode, deserializedProduct.Response.Response.FlightItinerary.Segments[k].Origin.Airport.AirportName, deserializedProduct.Response.Response.FlightItinerary.Segments[k].Origin.Airport.Terminal, deserializedProduct.Response.Response.FlightItinerary.Segments[k].Origin.Airport.CityCode, deserializedProduct.Response.Response.FlightItinerary.Segments[k].Origin.Airport.CityName, deserializedProduct.Response.Response.FlightItinerary.Segments[k].Origin.Airport.CountryCode, deserializedProduct.Response.Response.FlightItinerary.Segments[k].Origin.Airport.CountryName, deserializedProduct.Response.Response.FlightItinerary.Segments[k].Origin.DepTime);
                        dtSegmentR.Rows.Add(deserializedProduct.Response.Response.FlightItinerary.Segments[k].Destination.Airport.AirportCode,
                            deserializedProduct.Response.Response.FlightItinerary.Segments[k].Destination.Airport.AirportName,
                            deserializedProduct.Response.Response.FlightItinerary.Segments[k].Destination.Airport.Terminal,
                            deserializedProduct.Response.Response.FlightItinerary.Segments[k].Destination.Airport.CityCode,
                            deserializedProduct.Response.Response.FlightItinerary.Segments[k].Destination.Airport.CityName,
                            deserializedProduct.Response.Response.FlightItinerary.Segments[k].Destination.Airport.CountryCode,
                            deserializedProduct.Response.Response.FlightItinerary.Segments[k].Destination.Airport.CountryName,
                            deserializedProduct.Response.Response.FlightItinerary.Segments[k].Destination.ArrTime);

                    }
                    travelSaveResponse.dtSegment = dtSegment;
                    travelSaveResponse.dtSegmentR = dtSegmentR;
                    travelSaveResponse.IsLcc = "0";
                    var SaveBookingResponse = _dataRepository.SaveBookingResponse(travelSaveResponse);


                }
                else
                {
                    travelSaveResponse.IsSuccess = 0;
                    travelSaveResponse.Message = deserializedProduct.Response.Error.ErrorMessage;
                    travelSaveResponse.TraceId = deserializedProduct.Response.TraceId;
                    travelSaveResponse.Fk_MemId = bookTicket.Fk_MemId;
                    travelSaveResponse.IsLcc = "0";
                    var SaveBookingResponse = _dataRepository.SaveErrorBookingResponse(travelSaveResponse);
                }


            }
            var SaveFareRequest = _dataRepository.SaveBookingResponse(travelSaveResponse);
            return deserializedProduct;
        }
        
        [HttpPost("BookTicketNonLCC")]
        [Produces("application/json")]
        public BookTicketResponseForNonLcc BookTicketNonLCC(BookTicketForNonLCC bookTicketForNonLCC)
        {
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string Passport = "";

            bookTicketForNonLCC.TokenId = Token;
            bookTicketForNonLCC.EndUserIp = TravelCredentials.EndUserIP;
            var SaveReq = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(bookTicketForNonLCC), "BookTicketNonLCCRequest", 0);
            // JsonData = JsonConvert.SerializeObject(JsonData);
            string Url = TravelCredentials.BookingBaseUrl + "/Ticket";
            string ResponseData = CommonTravel.GetResponse(JsonConvert.SerializeObject(bookTicketForNonLCC), Url);

            var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, "BookTicketNonLCCResponse", 0);
            BookTicketResponseForNonLcc deserializedProduct = JsonConvert.DeserializeObject<BookTicketResponseForNonLcc>(ResponseData);
            TravelSaveResponse travelSaveResponse = new TravelSaveResponse();
            if (deserializedProduct != null)
            {

                if (deserializedProduct.Response.Error.ErrorCode == 0)
                {
                    travelSaveResponse.TraceId = deserializedProduct.Response.TraceId;
                    travelSaveResponse.Message = "";
                    travelSaveResponse.IsSuccess = 1;
                    var SaveBookingResponse = _dataRepository.UpdateNonLCCBookingResponse(travelSaveResponse);
                }
                else
                {
                    travelSaveResponse.TraceId = deserializedProduct.Response.TraceId;
                    travelSaveResponse.Message = deserializedProduct.Response.Error.ErrorMessage;
                    travelSaveResponse.IsSuccess = 0;
                    var SaveBookingResponse = _dataRepository.UpdateNonLCCBookingResponse(travelSaveResponse);
                }
            }
            return deserializedProduct;
        }
        
        [HttpPost("BookTicketLCC")]
        [Produces("application/json")]
        public BookingResponseForLCC BookTicketLCC(BookTicketLCC bookTicket)
        {
            string ResponseData = "";
            string APIName = "";
            BookingResponseForLCC deserializedProduct = new BookingResponseForLCC();
            try
            {


                var res = _dataRepository.GetToken();
                string Token = res.Result.Token;
                bookTicket.TokenId = Token;
                bookTicket.EndUserIp = TravelCredentials.EndUserIP;

                var SaveReq = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(bookTicket), "BookTicketLCCRequest", bookTicket.Fk_MemId);
                string Url = TravelCredentials.BookingBaseUrl + "/Ticket";


                APIName = "BookTicketLCCResponse";
                ResponseData = CommonTravel.GetResponse(JsonConvert.SerializeObject(bookTicket), Url);


                var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, APIName, bookTicket.Fk_MemId);


                string JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + bookTicket.TokenId + "\",\"TraceId\":\"" + bookTicket.TraceId + "\"}";

                Url = "https://apiv2.mobilepe.co.in/api/auth/GetBookingDetails";
                string ResponseData1 = CommonTravel.SimpleAPI(Url, JsonConvert.SerializeObject(JsonData));



            }
            catch (System.Exception)
            {
                var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, APIName, bookTicket.Fk_MemId);

                throw;
            }
            return deserializedProduct;
        }
        
        [HttpGet("GetBooking")]
        [Produces("application/json")]
        public async Task<object> GetBooking(int Fk_MemId)
        {
            BookingResult bookingResult = new BookingResult();
            BookingListResponse objres = new BookingListResponse();
            BookingList bookingList = new BookingList();

            var res = await this._dataRepository.GetBookingList(Fk_MemId);
            if (res != null && res.Count > 0)
            {
                objres.message = "Success";
                objres.response = "Success";
                bookingResult.lstBooking = res;
                objres.result = bookingResult;
            }
            else
            {
                objres.message = "error";
                objres.response = "No Record Found";
            }

            return objres;
        }
        
        [HttpPost("GetBookingDetails")]
        [Produces("application/json")]
        public BookingDetailsReponse GetBookingDetails(BookingDetailsRequest bookingDetailsRequest)
        {
            string JsonData = "";
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            bookingDetailsRequest.TokenId = Token;
            if (!string.IsNullOrEmpty(bookingDetailsRequest.PNR))
            {
                JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + bookingDetailsRequest.TokenId + "\",\"PNR\":\"" + bookingDetailsRequest.PNR + "\",\"BookingId\":\"" + bookingDetailsRequest.BookingId + "\"}";
            }
            else
            {
                JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + bookingDetailsRequest.TokenId + "\",\"TraceId\":\"" + bookingDetailsRequest.TraceId + "\"}";
            }

           // string JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + bookingDetailsRequest.TokenId + "\",\"PNR\":\"" + bookingDetailsRequest.PNR + "\",\"BookingId\":\"" + bookingDetailsRequest.BookingId + "\"}";
            JsonData = JsonConvert.SerializeObject(JsonData);
            bookingDetailsRequest.EndUserIp = TravelCredentials.EndUserIP;
            var SaveReq = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(JsonData), "GetBookingDetailsRequest", 0);
            string Url = TravelCredentials.BookingBaseUrl + "/GetBookingDetails";
            //string Url = "https://apiv2.mobilepe.co.in/api/auth/GetBookingDetails";
            string ResponseData = CommonTravel.SimpleAPI(Url, JsonConvert.SerializeObject(bookingDetailsRequest));

            var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, "GetBookingDetailsResponse", 0);
            BookingDetailsReponse deserializedProduct = JsonConvert.DeserializeObject<BookingDetailsReponse>(ResponseData);
            TravelSaveResponse travelSaveResponse = new TravelSaveResponse();
            if (deserializedProduct != null)
            {
                DataTable dtSegment = new DataTable();
                dtSegment.Columns.Add("AirportCode");
                dtSegment.Columns.Add("AirportName");
                dtSegment.Columns.Add("Terminal");
                dtSegment.Columns.Add("CityCode");
                dtSegment.Columns.Add("CityName");
                dtSegment.Columns.Add("CountryCode");
                dtSegment.Columns.Add("CountryName");
                dtSegment.Columns.Add("DepTime");


                DataTable dtSegmentR = new DataTable();
                dtSegmentR.Columns.Add("AirportCode");
                dtSegmentR.Columns.Add("AirportName");
                dtSegmentR.Columns.Add("Terminal");
                dtSegmentR.Columns.Add("CityCode");
                dtSegmentR.Columns.Add("CityName");
                dtSegmentR.Columns.Add("CountryCode");
                dtSegmentR.Columns.Add("CountryName");
                dtSegmentR.Columns.Add("DepTime");

                DataTable dt = new DataTable();
                dt.Columns.Add("JourneyType");
                dt.Columns.Add("TripIndicator");
                dt.Columns.Add("PNR");
                dt.Columns.Add("Origin");
                dt.Columns.Add("Destination");
                dt.Columns.Add("AirlineCode");
                dt.Columns.Add("LastTicketDate");
                dt.Columns.Add("NonRefundable");

                if (deserializedProduct.Response.Error.ErrorCode == 0)
                {

                    travelSaveResponse.Message = "";
                    travelSaveResponse.TraceId = deserializedProduct.Response.TraceId;
                    travelSaveResponse.PNRNo = deserializedProduct.Response.FlightItinerary.PNR;
                    travelSaveResponse.BookingId = deserializedProduct.Response.FlightItinerary.BookingId;
                    travelSaveResponse.SSRDenied = false;
                    travelSaveResponse.SSRMessage = "";
                    travelSaveResponse.Status = 1;
                    travelSaveResponse.IsPriceChanged = false;
                    travelSaveResponse.IsTimeChanged = false;
                    travelSaveResponse.IsSuccess = 1;

                    dt.Rows.Add(deserializedProduct.Response.FlightItinerary.JourneyType, deserializedProduct.Response.FlightItinerary.TripIndicator, deserializedProduct.Response.FlightItinerary.PNR, deserializedProduct.Response.FlightItinerary.Origin, deserializedProduct.Response.FlightItinerary.Destination, deserializedProduct.Response.FlightItinerary.AirlineCode, deserializedProduct.Response.FlightItinerary.LastTicketDate, deserializedProduct.Response.FlightItinerary.NonRefundable);
                    travelSaveResponse.tblFlightItinerary = dt;

                    for (int k = 0; k < deserializedProduct.Response.FlightItinerary.Segments.Count; k++)
                    {
                        dtSegment.Rows.Add(deserializedProduct.Response.FlightItinerary.Segments[k].Origin.Airport.AirportCode, deserializedProduct.Response.FlightItinerary.Segments[k].Origin.Airport.AirportName, deserializedProduct.Response.FlightItinerary.Segments[k].Origin.Airport.Terminal, deserializedProduct.Response.FlightItinerary.Segments[k].Origin.Airport.CityCode, deserializedProduct.Response.FlightItinerary.Segments[k].Origin.Airport.CityName, deserializedProduct.Response.FlightItinerary.Segments[k].Origin.Airport.CountryCode, deserializedProduct.Response.FlightItinerary.Segments[k].Origin.Airport.CountryName, deserializedProduct.Response.FlightItinerary.Segments[k].Origin.DepTime);
                        dtSegmentR.Rows.Add(deserializedProduct.Response.FlightItinerary.Segments[k].Destination.Airport.AirportCode,
                            deserializedProduct.Response.FlightItinerary.Segments[k].Destination.Airport.AirportName,
                            deserializedProduct.Response.FlightItinerary.Segments[k].Destination.Airport.Terminal,
                            deserializedProduct.Response.FlightItinerary.Segments[k].Destination.Airport.CityCode,
                            deserializedProduct.Response.FlightItinerary.Segments[k].Destination.Airport.CityName,
                            deserializedProduct.Response.FlightItinerary.Segments[k].Destination.Airport.CountryCode,
                            deserializedProduct.Response.FlightItinerary.Segments[k].Destination.Airport.CountryName,
                            deserializedProduct.Response.FlightItinerary.Segments[k].Destination.ArrTime);

                    }
                    travelSaveResponse.dtSegment = dtSegment;
                    travelSaveResponse.dtSegmentR = dtSegmentR;
                    travelSaveResponse.IsLcc = deserializedProduct.Response.FlightItinerary.IsLCC == true ? "1" : "0";
                    var SaveBookingResponse = _dataRepository.SaveBookingResponseSuccess(travelSaveResponse);


                }
                else
                {
                    try
                    {
                        travelSaveResponse.IsSuccess = 2;
                        travelSaveResponse.Message = deserializedProduct.Response.Error.ErrorMessage;
                        travelSaveResponse.TraceId = deserializedProduct.Response.TraceId;
                        travelSaveResponse.Fk_MemId = 0;
                        travelSaveResponse.IsLcc = deserializedProduct.Response.FlightItinerary.IsLCC == true ? "1" : "0";
                        var SaveBookingResponse = _dataRepository.SaveErrorBookingResponse(travelSaveResponse);
                    }
                    catch
                    {

                    }
                }


            }
            return deserializedProduct;
        }

        
        [HttpPost("SendChangeRequest")]
        [Produces("application/json")]
        public CancelResponse SendChangeRequest(CancelTicket cancelTicket)
        {
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;

            cancelTicket.TokenId = Token;
            cancelTicket.EndUserIp = TravelCredentials.EndUserIP;

            var SaveReq = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(cancelTicket), "CancelRequest", 0);
            string Url = TravelCredentials.BookingBaseUrl + "/SendChangeRequest";
            string ResponseData = CommonTravel.GetResponse(JsonConvert.SerializeObject(cancelTicket), Url);

            var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, "CancelRequestResponse", 0);
            CancelResponse deserializedProduct = JsonConvert.DeserializeObject<CancelResponse>(ResponseData);

            return deserializedProduct;
        }

        
        [HttpGet("GetPassengers")]
        [Produces("application/json")]
        public async Task<ResponseModel> GetPassengers(string fk_memid)
        {
            PassengersListResponse objres = new PassengersListResponse();
            ResponseModel returnResponse = new ResponseModel();

            string EncryptedText = "";
            PassengersList passengersList = new PassengersList();
            try
            {

                if (!string.IsNullOrEmpty(fk_memid))
                {
                    fk_memid = ApiEncrypt_Decrypt.DecryptString(AESKEY, fk_memid);
                    var res = await this._dataRepository.GePassengersList(fk_memid);
                    if (res != null && res.Count > 0)
                    {
                        objres.message = "Success";
                        objres.response = "Success";
                        objres.lstpassenger = res;


                    }
                    else
                    {
                        objres.response = "error";
                        objres.message = "No Record Found";
                    }
                }
                else
                {
                    objres.response = "Please Mention the Member Id ";
                    objres.message = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                objres.message = ex.Message;
                objres.response = "Error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PassengersListResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        
        [HttpPost("SavePassengers")]
        [Produces("application/json")]
        public async Task<ResponseModel> SavePassengers(RequestModel requestModel)
        {
            PassengerResponse PassengerResult = new PassengerResponse();
            CommonEcomm objres = new CommonEcomm();
            ResponseModel returnResponse = new ResponseModel();
            PassengersList passengermodel = new PassengersList();
            string EncryptedText = "";
            try
            {
                EncryptedText = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                passengermodel = JsonConvert.DeserializeObject<PassengersList>(EncryptedText);
                var res = await _dataRepository.SavePassengersDetail(passengermodel);


                if (res != null && res.flag == "1")
                {
                    PassengerResult.msg = res.msg;
                    PassengerResult.response = "Success";
                }
                else
                {
                    PassengerResult.msg = res.msg;
                    PassengerResult.response = "Error";
                }
            }
            catch(Exception ex)
            {
                PassengerResult.msg = ex.Message;
                PassengerResult.response = "Error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PassengerResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, PassengerResult);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        
        [HttpPost("UpdatePassengers")]
        [Produces("application/json")]
        public async Task<ResponseModel> UpdatePassengers(RequestModel requestModel)
        {
            PassengerResponse PassengerResult = new PassengerResponse();
            CommonEcomm objres = new CommonEcomm();
            ResponseModel returnResponse = new ResponseModel();
            PassengersList passengermodel = new PassengersList();
            string EncryptedText = "";
            try
            {
                EncryptedText = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                passengermodel = JsonConvert.DeserializeObject<PassengersList>(EncryptedText);
                var res = await _dataRepository.UpdatePassengersDetail(passengermodel);


                if (res != null && res.flag == "1")
                {
                    PassengerResult.msg = res.msg;
                    PassengerResult.response = "Success";
                }
                else
                {
                    PassengerResult.msg = res.msg;
                    PassengerResult.response = "error";
                }
            }
            catch(Exception ex)
            {
                PassengerResult.msg = ex.Message;
                PassengerResult.response = "Error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PassengerResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, PassengerResult);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        
        [HttpPost("DeletePassengers")]
        [Produces("application/json")]
        public async Task<ResponseModel> DeletePassengers(RequestModel requestModel)
        {
            CommonEcomm objres = new CommonEcomm();
            PassengerResponse PassengerResult = new PassengerResponse();
            ResponseModel returnResponse = new ResponseModel();
            PassengersList passengermodel = new PassengersList();
            string EncryptedText = "";
            try
            {

                EncryptedText = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.Body);
                passengermodel = JsonConvert.DeserializeObject<PassengersList>(EncryptedText);
                var res = await _dataRepository.DeletePassengersDetail(passengermodel);


                if (res != null && res.flag == "1")
                {
                    PassengerResult.msg = res.msg;
                    PassengerResult.response = "Success";
                    PassengerResult.flag = res.flag;
                }
                else
                {
                    PassengerResult.msg = res.msg;
                    PassengerResult.response = "No Record Found";
                    PassengerResult.flag = res.flag;
                }
            }
            catch (Exception ex)
            {
                PassengerResult.msg = ex.Message;
                PassengerResult.response = "Error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PassengerResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, PassengerResult);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        
        [HttpPost("GetCancellationCharges")]
        [Produces("application/json")]
        public CancellationChargesResponse GetCancellationCharges(CancellationCharges cancellationCharges)
        {
            string ResponseData = "";
            string APIName = "";
            CancellationChargesResponse deserializedProduct = new CancellationChargesResponse();
            try
            {


                var res = _dataRepository.GetToken();
                string Token = res.Result.Token;
                cancellationCharges.TokenId = Token;
                cancellationCharges.EndUserIp = TravelCredentials.EndUserIP;

                var SaveReq = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(cancellationCharges), "GetCancellationCharges", cancellationCharges.Fk_MemId);
                string Url = TravelCredentials.BookingBaseUrl + "/GetCancellationCharges";


                APIName = "GetCancellationCharges";
                ResponseData = CommonTravel.SimpleAPI(Url, JsonConvert.SerializeObject(cancellationCharges));


                var SaveResponse = _dataRepository.SaveTravelRequest(ResponseData, APIName, cancellationCharges.Fk_MemId);

                deserializedProduct = JsonConvert.DeserializeObject<CancellationChargesResponse>(ResponseData);


            }
            catch (System.Exception)
            {

            }
            return deserializedProduct;
        }

        [HttpGet("PrintFlightTicket")]
        public async Task<IActionResult> PrintFlightTicket(string PNR,string BookingId)
        {
            var res = _dataRepository.GetToken();
            string TokenId = res.Result.Token;
            StringBuilder sb = new StringBuilder();
            string JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + TokenId + "\",\"PNR\":\"" + PNR + "\",\"BookingId\":\"" + BookingId + "\"}";
            var SaveReq = _dataRepository.SaveTravelRequest(JsonConvert.SerializeObject(JsonData), "GetBookingDetailsRequest", 0);
            string Url = TravelCredentials.BookingBaseUrl + "/GetBookingDetails";
            string ResponseData = CommonTravel.SimpleAPI(Url, JsonData);           
            BookingDetailsReponse deserializedProduct = JsonConvert.DeserializeObject<BookingDetailsReponse>(ResponseData);
            string InnvoiceDate = deserializedProduct.Response.FlightItinerary.InvoiceCreatedOn.ToString("dd-MMM-yyyy");
            string cabinClass = "";
            if (deserializedProduct.Response.FlightItinerary.Segments[0].CabinClass==2)
            {
                cabinClass = "Economy";
            }
            else if (deserializedProduct.Response.FlightItinerary.Segments[0].CabinClass == 3)
            {
                cabinClass = "Premium Economy";
            }
            else if (deserializedProduct.Response.FlightItinerary.Segments[0].CabinClass == 4)
            {
                cabinClass = "Business Class";
            }
            else if (deserializedProduct.Response.FlightItinerary.Segments[0].CabinClass == 5)
            {
                cabinClass = "Premium Business";
            }
            else if (deserializedProduct.Response.FlightItinerary.Segments[0].CabinClass == 6)
            {
                cabinClass = "First Class";
            }

            string stopOver = deserializedProduct.Response.FlightItinerary.Segments[0].StopOver==false?"Non-Stop":"";
            double passengercount = deserializedProduct.Response.FlightItinerary.Passenger.Count;
            double TotalTax = (deserializedProduct.Response.FlightItinerary.Fare.Tax) * (passengercount);
            double TotalFare = (deserializedProduct.Response.FlightItinerary.Fare.BaseFare) * (passengercount);
            double TotalAmount = (TotalTax) + (TotalFare);
            sb.Append("<html lang='en'>");
            sb.Append("<head>");
            sb.Append("<meta charset='UTF-8'>");
            sb.Append("<meta http-equiv='X-UA-Compatible' content='IE=edge'>");
            sb.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            sb.Append("<title>E-Ticket</title>");
            sb.Append("</head>");
            sb.Append("<body>");
            sb.Append("<div style='background:#fff; overflow:hidden; padding: 10px; width: 700px; border:1px solid #D6D8E7;font-size:12px; font-family:arial, sans-serif; margin:10px auto;'>");
            sb.Append("<span style='float:none; margin-right:8px; margin-bottom:20px; font-size:small; font-weight:bold; margin:250px;></span>");
            sb.Append("<div style='width:100%; float:left;'>");
            sb.Append("<div style='width:30%; float:left;'>");
            sb.Append("<span style='float:left; margin-right:8px; width:100%;'><img width='150' src='logo1.png' alt=''></span>");
            sb.Append("<span style='float: left; margin-right: 8px; width: 100%;'><b style='font-weight: bold;font-size: 12px;'></b></span>");
            sb.Append("<span style='float: left; width: 100%;'><br><br>Delhi<br>Phone:123456789</span>");
            sb.Append("</div>");
            sb.Append("<div style='width:40%; float:left; font-size:15px; text-align:center;'>");
            sb.Append("<b style='font-size:20px;'>E - Ticket</b><br>");
            sb.Append("<b style='font-size:20px;'>Ticketed</b><br>");
            sb.Append("</div>");
            sb.Append("<div style='float:right;font-size:13px;text-align:right;width:30%'>");
            sb.Append("<span style='float:left; margin-right:8px; width:100%;'><b style='font-weight:bold; font-size:12px;'></b></span>");
            sb.Append("<span>PNR : "+PNR+"</span><br>");
            sb.Append("<span>Innvoice Date : "+ InnvoiceDate + "</span><br>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div style='float: left; width: 100%; margin: 0px; padding: 10px 0;'>");
            sb.Append("<table border='0' cellpadding='0' cellspacing='0' style='width: 100%; font-family: Arial, Helvetica, sans-serif; font-size: 12px; border-collapse: collapse; border: 1px solid #D6D8E7; text-align: center;'>");
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("<th style='background: #004684; color: #fff; text-align: left; padding: 5px;'>  Passenger Name </th>");
            sb.Append("<th style='background: #004684; color: #fff; text-align: left; padding: 5px;'> Ticket Number </th>");
            sb.Append("<th style='background: #004684; color: #fff; text-align: left; padding: 5px;'> Frequent flyer no. </th>");
            sb.Append("</tr>");
            for(int i = 0;i<= deserializedProduct.Response.FlightItinerary.Passenger.Count-1;i++)
            {
                sb.Append("<tr>");
                sb.Append("<td style='width: 33.3%;text-align: left;padding: 5px;'>");
                sb.Append("<div>");
                sb.Append("<span style='margin-top: 5px; width: 100%; float: left;'>" + deserializedProduct.Response.FlightItinerary.Passenger[i].FirstName +" " + deserializedProduct.Response.FlightItinerary.Passenger[i].LastName +"</span><br></div></td>");
                sb.Append("<td style='width: 33.3%;text-align: left;padding: 5px;'> <div style='float: left; margin-right: 8px;'>");
                sb.Append("<span style='margin-top: 5px; width: 100%; float: left;'>" + deserializedProduct.Response.FlightItinerary.Passenger[i].Ticket.TicketNumber + "</span></div></td>");
                sb.Append("<td style='width: 33.3%;text-align: left;padding: 5px;'>");
                sb.Append("<div style='float: right; margin-right: 45px; text-align: left;'>");
                sb.Append("<span style='margin-top: 5px; width: 100%; float: left; text-align: left;'>-</span>");
                sb.Append("</div></td></tr>");
            }
            sb.Append("</tbody></table></div>");
            sb.Append("<div style='float: left; width: 100%; margin: 0px; padding: 10px 0;'>");
            sb.Append("<table border='0' cellpadding='0' cellspacing='0' style='width: 100%; font-family: Arial, Helvetica, sans-serif; font-size: 12px; border-collapse: collapse; border: 1px solid #D6D8E7; text-align: center;'>");
            sb.Append("<tbody><tr>");
            sb.Append("<th style='background: #004684; color: #fff; text-align: left; padding: 5px;'>  Flight </th>");
            sb.Append("<th style='background: #004684; color: #fff; text-align: left; padding: 5px;'> Departure </th>");
            sb.Append("<th style='background: #004684; color: #fff; text-align: left; padding: 5px;'> Arrival </th>");
            sb.Append("<th style='background: #004684; color: #fff; text-align: left; padding: 5px;'> Status </th>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='width: 25%;text-align: left;padding: 5px;'>");
            sb.Append("<div style='float: left; margin-right: 0;'>");
            sb.Append("<span style='margin-top: 5px;float: left;'>");
            sb.Append("<img id='airlineLogo' width='50' src='spicejet.png' alt='SG'>");
            sb.Append("</span>");
            sb.Append("<span style='margin-top: 5px;float: left;margin-left:5px;'>" + deserializedProduct.Response.FlightItinerary.Segments[0].Airline.AirlineName + " " + deserializedProduct.Response.FlightItinerary.Segments[0].Airline.AirlineCode + " " + deserializedProduct.Response.FlightItinerary.Segments[0].Airline.FlightNumber + "");
            sb.Append("<br> Cabin " + cabinClass + "");
            sb.Append("</span></div></td>");
            sb.Append("<td style='width: 25%; text-align: left;'>");
            sb.Append("<div style=' float: left; margin-right: 10px;'>");
            sb.Append("<span style='margin-top: 5px; width: 100%; float: left;'>" + deserializedProduct.Response.FlightItinerary.Segments[0].Origin.Airport.CityCode + " " + deserializedProduct.Response.FlightItinerary.Segments[0].Origin.Airport.AirportName + "</span>");
            sb.Append("<span style='margin-top: 5px;width: 100%; float: left;'>Terminal:"+ deserializedProduct.Response.FlightItinerary.Segments[0].Origin.Airport.Terminal + "</span>");
            sb.Append("<span style='margin-top: 5px; width: 100%; float: left;'>"+ deserializedProduct.Response.FlightItinerary.Segments[0].Origin.DepTime + "</span>");
            sb.Append("</div></td>");
            sb.Append("<td style='width: 25%; text-align: left;'>");
            sb.Append("<div style=' float: left; margin-right: 10px;'>");
            sb.Append("<span style='margin-top: 5px; width: 100%; float: left;'>" + deserializedProduct.Response.FlightItinerary.Segments[0].Destination.Airport.CityCode + " " + deserializedProduct.Response.FlightItinerary.Segments[0].Destination.Airport.AirportName + "</span>");
            sb.Append("<span style='margin-top: 5px;width: 100%; float: left;'>Terminal:" + deserializedProduct.Response.FlightItinerary.Segments[0].Destination.Airport.Terminal + "</span>");
            sb.Append("<span style='margin-top: 5px; width: 100%; float: left;'>" + deserializedProduct.Response.FlightItinerary.Segments[0].Destination.ArrTime + "</span>");
            sb.Append("</div></td>");
            sb.Append("<td style='width: 25%; text-align: left;'>");
            sb.Append("<div style='float: right; margin-right: 10px;'>");
            sb.Append("<span style='margin-top: 5px; width: 100%; float: left;'>" + deserializedProduct.Response.FlightItinerary.Segments[0].FlightStatus + "</span>");
            sb.Append("<span style='margin-top: 5px; width: 100%; float: left;'>Baggage(per person):" + deserializedProduct.Response.FlightItinerary.Passenger[0].SegmentAdditionalInfo[0].Baggage + "</span>");
            sb.Append("<span style='margin-top: 5px; width: 100%; float: left;'></span>");
            sb.Append("<span style='margin-top: 5px; width: 100%; float: left;'>"+ stopOver + "</span>");
            sb.Append("</div></td></tr></tbody></table></div>");
            sb.Append("<div style='float: left; width: 100%; margin: 0px; padding: 10px 0;'>");
            sb.Append("<table border='0' cellpadding='0' cellspacing='0' style='width: 100%; font-family: Arial, Helvetica, sans-serif; font-size: 12px; border-collapse: collapse; border: 1px solid #D6D8E7; text-align: center;'>");
            sb.Append("<tbody><tr><th colspan='2' style='background: #004684; color: #fff; text-align: left; padding: 5px;'> Payment Details </th>");
            sb.Append("</tr></tr><tr>");
            sb.Append("<td style='padding: 5px;'>");
            sb.Append("<div id='txnMsg' style=' width:100%; text-align:center; font-weight:bold; color:red; display:none'>");
            sb.Append("Txn fee/Discount amount will be equally divided on all the pax except infant and cancelled ticket.</div>");
            sb.Append("<div style='float:left; width:100%; text-align:left; font-weight:bold;'>");
            sb.Append("This is an electronic ticket. <br> Passengers must carry a valid photo ID card for check-in at the airport.");
            sb.Append("</div></td>");
            sb.Append("<td style='padding: 5px;'>");
            sb.Append("<div style='float:right; width:300px; margin-top:10px;' id='fareDetails'>");
            sb.Append("<div style='margin-top:5px; float:left; width:100%; '>");
            sb.Append("<div style='float:left; width:140px; text-align:right;'>Fare:</div>");
            sb.Append("<div style='width:85px; float:right; text-align:right;'>₹ "+ TotalFare + "</div>");
            sb.Append("</div>");
            sb.Append("<div style='margin-top:5px; float:left; width:100%; '>");
            sb.Append("<div style='float:left; width:140px; text-align:right;'> K3/GST:</div>");
            sb.Append("<div style='width:85px; float:right; text-align:right;'>₹ " + TotalTax + "</div>");
            sb.Append("</div>");
            sb.Append("<div style='margin-top:5px; float:left; width:100%; font-weight:bold;'>");
            sb.Append("<div style='float:left; width:140px; text-align:right'>Total Amount:</div>");
            sb.Append("<div style='width:85px; float:right; text-align:right;'>"+TotalAmount+ "</div>");
            sb.Append("</div></div></td></tr></tbody></table></div>");
            sb.Append("<div style='float: left; width: 100%; margin: 0px; padding: 10px 0;'>");
            sb.Append("<table border='0' cellpadding='0' cellspacing='0' style='width: 100%; font-family: Arial, Helvetica, sans-serif; font-size: 12px; border-collapse: collapse; border: 1px solid #D6D8E7; text-align: center;'>");
            sb.Append("<tbody><tr><th colspan='3' style='background: #004684; color: #fff; text-align: left; padding: 5px;'>Barcode Details</th></tr>");
            sb.Append("<tr><th style='border: 1px solid #D6D8E7; font-weight: bold; width: 40%; padding: 10px;'>Passenger Name</th>");
            sb.Append("<th style='border: 1px solid #D6D8E7; font-weight: bold; padding: 10px;'>Flight Details</th>");
            
            sb.Append("</tr>");
            for (int i = 0; i <= deserializedProduct.Response.FlightItinerary.Passenger.Count - 1; i++)
            {
                
                sb.Append("<tr>");
                sb.Append("<td rowspan='1' style='border: 1px solid #D6D8E7;'>" + deserializedProduct.Response.FlightItinerary.Passenger[i].FirstName + " " + deserializedProduct.Response.FlightItinerary.Passenger[i].LastName + "</td>");
                sb.Append("<td style='border: 1px solid #D6D8E7;'>"+ deserializedProduct.Response.FlightItinerary.Origin + " -  "+ deserializedProduct.Response.FlightItinerary.Destination + "</td>");
               
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("<div style='margin-top:20px; margin-left:5px; float:left; '></div>");
            sb.Append("<div style='float: left; width: 100%; margin-top:10px; padding-bottom:10px;'><div style='margin:0; padding:5px;'>");
            sb.Append("Carriage and other services provided by the carrier are subject to conditions of	 carriage which hereby incorporated by reference. These conditions may be obtained	 from the issuing carrier. If the passenger's journey involves an ultimate destination	 or stop in a country other than country of departure the Warsaw convention may be	 applicable and the convention governs and in most cases limits the liability of carriers for death or personal injury and in respect of loss of or damage to baggage.</div>");
            sb.Append("<p style=' margin:0; padding:15px 5px 5px 5px; color:red'>Don't Forget to purchase travel insurance for your Visit. Please Contact your travel agent to purchase travel insurance.</p></div></div>");
            sb.Append("</body>");
            sb.Append("</html>");
            HtmlToPdf ToPdf = new HtmlToPdf();
            PdfDocument Document = ToPdf.ConvertHtmlString(sb.ToString());
            byte[] pdf = Document.Save();
            Document.Close();

            return File(pdf, "application/pdf", "PrintTicket.pdf");
        }
    }
}
