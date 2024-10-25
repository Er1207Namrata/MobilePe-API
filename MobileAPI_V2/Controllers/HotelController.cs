using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.Model.Travel;
using MobileAPI_V2.Model.Travel.Hotel;
using MobileAPI_V2.Services;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.X86;

namespace MobileAPI_V2.Controllers
{
    [ApiController]
    [Route("api/Auth/")]
    public class HotelController : Controller
    {
        private readonly IDataRepository _dataRepository;
        public HotelController(IHostingEnvironment env, IDataRepository dataRepository, IConfiguration configuration)
        {

            _dataRepository = dataRepository;
        }
        [HttpPost("CountryList")]
        [Produces("application/json")]
        public CountryResponse CountryList()
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\",\"ClientId\":\"" + TravelCredentials.ClientId + "\"}";
            string Url = TravelCredentials.SharedBaseUrl + "/CountryList";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            CountryResponse deserializedProduct = JsonConvert.DeserializeObject<CountryResponse>(ResponseData);
            return deserializedProduct;
        }
        [HttpPost("GetDestination")]
        [Produces("application/json")]
        public DestinationResponse GetDestinationSearchStaticData(DestinationRequest destinationRequest)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\",\"CountryCode\":\"" + destinationRequest.CountryCode + "\",\"SearchType\":\"1\"}";
            string Url = TravelCredentials.StaticBaseUrl + "/GetDestinationSearchStaticData";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            DestinationResponse deserializedProduct = JsonConvert.DeserializeObject<DestinationResponse>(ResponseData);
            return deserializedProduct;
        }
        [HttpPost("TopDestinationList")]
        [Produces("application/json")]
        public TopDestinationResponse TopDestinationList(DestinationRequest destinationRequest)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string JsonData = "{\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\",\"CountryCode\":\"" + destinationRequest.CountryCode + "\",\"ClientId\":\"" + TravelCredentials.ClientId + "\"}";
            string Url = TravelCredentials.SharedBaseUrl + "/TopDestinationList";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            TopDestinationResponse deserializedProduct = JsonConvert.DeserializeObject<TopDestinationResponse>(ResponseData);
            return deserializedProduct;
        }
        [HttpPost("HotelSearch")]
        [Produces("application/json")]
        public HotelSearchResponse HotelSearch(HotelSearch hotelSearch)
        {
            string RoomGuests = "";
            string ChildAge = "";
            int index = 0;
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            for (int i = 0; i <= hotelSearch.RoomGuests.Count - 1; i++)
            {
                if (hotelSearch.RoomGuests[i].NoOfChild == 0)
                {
                    RoomGuests += "{\"NoOfAdults\":" + hotelSearch.RoomGuests[i].NoOfAdults + ",\"NoOfChild\":" + hotelSearch.RoomGuests[i].NoOfChild + ",\"ChildAge\":null},";
                }
                else
                {
                    for (int j = 0; j <= hotelSearch.RoomGuests[i].ChildAge.Count - 1; j++)
                    {
                        ChildAge += "" + hotelSearch.RoomGuests[i].ChildAge[j] + ",";
                    }
                    index = ChildAge.LastIndexOf(',');
                    ChildAge = ChildAge.Remove(index, 1);
                    ChildAge = "[" + ChildAge + "]";
                    RoomGuests += "{\"NoOfAdults\":" + hotelSearch.RoomGuests[i].NoOfAdults + ",\"NoOfChild\":" + hotelSearch.RoomGuests[i].NoOfChild + ",\"ChildAge\":" + ChildAge + "},";

                }


            }
            index = RoomGuests.LastIndexOf(',');
            RoomGuests = RoomGuests.Remove(index, 1);
            RoomGuests = "[" + RoomGuests + "]";
            string JsonData = "{\"CheckInDate\":\"" + hotelSearch.CheckInDate + "\",\"NoOfNights\":\"" + hotelSearch.NoOfNights + "\",\"CountryCode\":\"" + hotelSearch.CountryCode + "\",\"CityId\":\"" + hotelSearch.CityId + "\",\"ResultCount\":null,\"PreferredCurrency\":\"" + hotelSearch.PreferredCurrency + "\",\"GuestNationality\":\"" + hotelSearch.GuestNationality + "\",\"NoOfRooms\":\"" + hotelSearch.NoOfRooms + "\",\"RoomGuests\":" + RoomGuests + ",\"MaxRating\":" + hotelSearch.MaxRating + ",\"MinRating\":" + hotelSearch.MinRating + ",\"ReviewScore\":null,\"IsNearBySearchAllowed\":false,\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\"}";
            string Url = TravelCredentials.HotelBaseUrl + "/GetHotelResult";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            HotelSearchResponse deserializedProduct = JsonConvert.DeserializeObject<HotelSearchResponse>(ResponseData);

            return deserializedProduct;
        }

        [HttpPost("HotelInfo")]
        [Produces("application/json")]
        public HotelInfoResponse HotelInfo(HotelInfo hotelInfo)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string JsonData = "{\"ResultIndex\":\"" + hotelInfo.ResultIndex + "\",\"HotelCode\":\"" + hotelInfo.HotelCode + "\",\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\",\"TraceId\":\"" + hotelInfo.TraceId + "\"}";
            string Url = TravelCredentials.HotelBaseUrl + "/GetHotelInfo";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            HotelInfoResponse deserializedProduct = JsonConvert.DeserializeObject<HotelInfoResponse>(ResponseData);
            var HotelInfo = _dataRepository.SaveTravelRequest(JsonData, "HotelInfo", hotelInfo.FK_MemId);
            return deserializedProduct;
        }
        [HttpPost("GetHotelRoom")]
        [Produces("application/json")]
        public HotelRoomResonse GetHotelRoom(HotelInfo hotelInfo)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string JsonData = "{\"ResultIndex\":\"" + hotelInfo.ResultIndex + "\",\"HotelCode\":\"" + hotelInfo.HotelCode + "\",\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\",\"TraceId\":\"" + hotelInfo.TraceId + "\"}";
            string Url = TravelCredentials.HotelBaseUrl + "/GetHotelRoom";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            HotelRoomResonse deserializedProduct = JsonConvert.DeserializeObject<HotelRoomResonse>(ResponseData);
            var HotelRoom = _dataRepository.SaveTravelRequest(JsonData, "GetHotelRoom", hotelInfo.FK_MemId);
            return deserializedProduct;
        }
        [HttpPost("BlockRoom")]
        [Produces("application/json")]
        public BlockRoomResponse BlockRoom(BlockRoom blockRoom)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string HotelRoomsDetails = "";
            for (int i = 0; i <= blockRoom.HotelRoomsDetails.Count - 1; i++)
            {
                string Price = "{\"CurrencyCode\":\"" + blockRoom.HotelRoomsDetails[i].Price.CurrencyCode + "\",\"RoomPrice\":\"" + blockRoom.HotelRoomsDetails[i].Price.RoomPrice + "\",\"Tax\":\"" + blockRoom.HotelRoomsDetails[i].Price.Tax + "\",\"ExtraGuestCharge\":\"" + blockRoom.HotelRoomsDetails[i].Price.ExtraGuestCharge + "\",\"ChildCharge\":\"" + blockRoom.HotelRoomsDetails[i].Price.ChildCharge + "\",\"OtherCharges\":\"" + blockRoom.HotelRoomsDetails[i].Price.OtherCharges + "\",\"Discount\":\"" + blockRoom.HotelRoomsDetails[i].Price.Discount + "\",\"PublishedPrice\":\"" + blockRoom.HotelRoomsDetails[i].Price.PublishedPrice + "\",\"PublishedPriceRoundedOff\":\"" + blockRoom.HotelRoomsDetails[i].Price.PublishedPriceRoundedOff + "\",\"OfferedPrice\":\"" + blockRoom.HotelRoomsDetails[i].Price.OfferedPrice + "\",\"OfferedPriceRoundedOff\":\"" + blockRoom.HotelRoomsDetails[i].Price.OfferedPriceRoundedOff + "\",\"AgentCommission\":\"" + blockRoom.HotelRoomsDetails[i].Price.AgentCommission + "\",\"AgentMarkUp\":\"" + blockRoom.HotelRoomsDetails[i].Price.AgentMarkUp + "\",\"TDS\":\"" + blockRoom.HotelRoomsDetails[i].Price.TDS + "\",\"ServiceTax\":\"" + blockRoom.HotelRoomsDetails[i].Price.ServiceTax + "\"}";
                if (blockRoom.HotelRoomsDetails[i].BedTypeCode == null && blockRoom.HotelRoomsDetails[i].Supplements == null)
                {
                    HotelRoomsDetails += "{\"RoomIndex\":\"" + blockRoom.HotelRoomsDetails[i].RoomIndex + "\",\"RoomTypeCode\":\"" + blockRoom.HotelRoomsDetails[i].RoomTypeCode + "\",\"RoomTypeName\":\"" + blockRoom.HotelRoomsDetails[i].RoomTypeName + "\",\"RatePlanCode\":\"" + blockRoom.HotelRoomsDetails[i].RatePlanCode + "\",\"BedTypeCode\":null,\"SmokingPreference\":\"" + blockRoom.HotelRoomsDetails[i].SmokingPreference + "\",\"Supplements\":null,\"Price\":" + Price + "},";
                }
                else if (blockRoom.HotelRoomsDetails[i].BedTypeCode == null && blockRoom.HotelRoomsDetails[i].Supplements != null)
                {
                    HotelRoomsDetails += "{\"RoomIndex\":\"" + blockRoom.HotelRoomsDetails[i].RoomIndex + "\",\"RoomTypeCode\":\"" + blockRoom.HotelRoomsDetails[i].RoomTypeCode + "\",\"RoomTypeName\":\"" + blockRoom.HotelRoomsDetails[i].RoomTypeName + "\",\"RatePlanCode\":\"" + blockRoom.HotelRoomsDetails[i].RatePlanCode + "\",\"BedTypeCode\":null,\"SmokingPreference\":\"" + blockRoom.HotelRoomsDetails[i].SmokingPreference + "\",\"Supplements\":\"" + blockRoom.HotelRoomsDetails[i].Supplements + "\",\"Price\":" + Price + "},";
                }
                else if (blockRoom.HotelRoomsDetails[i].BedTypeCode != null && blockRoom.HotelRoomsDetails[i].Supplements == null)
                {
                    HotelRoomsDetails += "{\"RoomIndex\":\"" + blockRoom.HotelRoomsDetails[i].RoomIndex + "\",\"RoomTypeCode\":\"" + blockRoom.HotelRoomsDetails[i].RoomTypeCode + "\",\"RoomTypeName\":\"" + blockRoom.HotelRoomsDetails[i].RoomTypeName + "\",\"RatePlanCode\":\"" + blockRoom.HotelRoomsDetails[i].RatePlanCode + "\",\"BedTypeCode\":\"" + blockRoom.HotelRoomsDetails[i].BedTypeCode + "\",\"SmokingPreference\":\"" + blockRoom.HotelRoomsDetails[i].SmokingPreference + "\",\"Supplements\":null,\"Price\":" + Price + "},";
                }
                else
                {
                    HotelRoomsDetails += "{\"RoomIndex\":\"" + blockRoom.HotelRoomsDetails[i].RoomIndex + "\",\"RoomTypeCode\":\"" + blockRoom.HotelRoomsDetails[i].RoomTypeCode + "\",\"RoomTypeName\":\"" + blockRoom.HotelRoomsDetails[i].RoomTypeName + "\",\"RatePlanCode\":\"" + blockRoom.HotelRoomsDetails[i].RatePlanCode + "\",\"BedTypeCode\":\"" + blockRoom.HotelRoomsDetails[i].BedTypeCode + "\",\"SmokingPreference\":\"" + blockRoom.HotelRoomsDetails[i].SmokingPreference + "\",\"Supplements\":\"" + blockRoom.HotelRoomsDetails[i].Supplements + "\",\"Price\":" + Price + "},";
                }
            }
            int index = HotelRoomsDetails.LastIndexOf(',');
            HotelRoomsDetails = HotelRoomsDetails.Remove(index, 1);
            HotelRoomsDetails = "[" + HotelRoomsDetails + "]";
            string JsonData = "{\"ResultIndex\":\"" + blockRoom.ResultIndex + "\",\"HotelCode\":\"" + blockRoom.HotelCode + "\",\"HotelName\":\"" + blockRoom.HotelName + "\",\"GuestNationality\":\"" + blockRoom.GuestNationality + "\",\"NoOfRooms\":\"" + blockRoom.NoOfRooms + "\",\"ClientReferenceNo\":\"" + blockRoom.ClientReferenceNo + "\",\"IsVoucherBooking\":\"" + blockRoom.IsVoucherBooking + "\",\"HotelRoomsDetails\":" + HotelRoomsDetails + ",\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\",\"TraceId\":\"" + blockRoom.TraceId + "\"}";
            string Url = TravelCredentials.HotelBaseUrl + "/BlockRoom";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            BlockRoomResponse deserializedProduct = JsonConvert.DeserializeObject<BlockRoomResponse>(ResponseData);
            var HotelRoom = _dataRepository.SaveTravelRequest(JsonData, "BlockHotelRoom", blockRoom.FK_MemId);
            return deserializedProduct;
        }


        [HttpPost("BookRoom")]
        [Produces("application/json")]
        public BookRoomResponse BookRoom(BookRoom bookRoom)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;
            string HotelPassenger = "";
            string HotelRoomsDetails = "";
            for (int i = 0; i <= bookRoom.HotelRoomsDetails.Count - 1; i++)
            {
                for (int j = 0; j <= bookRoom.HotelRoomsDetails[i].HotelPassenger.Count - 1; j++)
                {
                    HotelPassenger = "{\"Title\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].Title + "\",\"FirstName\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].FirstName + "\",\"Middlename\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].Middlename + "\",\"LastName\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].LastName + "\",\"Phoneno\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].Phoneno + "\",\"Email\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].Email + "\",\"PaxType\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].PaxType + "\",\"Age\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].Age + "\",\"PassportNo\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].PassportNo + "\",\"PassportIssueDate\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].PassportIssueDate + "\",\"PassportExpDate\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].PassportExpDate + "\",\"PAN\":\"" + bookRoom.HotelRoomsDetails[i].HotelPassenger[j].PAN + "\"},";
                }
                int index1 = HotelPassenger.LastIndexOf(',');
                HotelPassenger = HotelPassenger.Remove(index1, 1);
                HotelPassenger = "[" + HotelPassenger + "]";

                string Price = "{\"CurrencyCode\":\"" + bookRoom.HotelRoomsDetails[i].Price.CurrencyCode + "\",\"RoomPrice\":\"" + bookRoom.HotelRoomsDetails[i].Price.RoomPrice + "\",\"Tax\":\"" + bookRoom.HotelRoomsDetails[i].Price.Tax + "\",\"ExtraGuestCharge\":\"" + bookRoom.HotelRoomsDetails[i].Price.ExtraGuestCharge + "\",\"ChildCharge\":\"" + bookRoom.HotelRoomsDetails[i].Price.ChildCharge + "\",\"OtherCharges\":\"" + bookRoom.HotelRoomsDetails[i].Price.OtherCharges + "\",\"Discount\":\"" + bookRoom.HotelRoomsDetails[i].Price.Discount + "\",\"PublishedPrice\":\"" + bookRoom.HotelRoomsDetails[i].Price.PublishedPrice + "\",\"PublishedPriceRoundedOff\":\"" + bookRoom.HotelRoomsDetails[i].Price.PublishedPriceRoundedOff + "\",\"OfferedPrice\":\"" + bookRoom.HotelRoomsDetails[i].Price.OfferedPrice + "\",\"OfferedPriceRoundedOff\":\"" + bookRoom.HotelRoomsDetails[i].Price.OfferedPriceRoundedOff + "\",\"AgentCommission\":\"" + bookRoom.HotelRoomsDetails[i].Price.AgentCommission + "\",\"AgentMarkUp\":\"" + bookRoom.HotelRoomsDetails[i].Price.AgentMarkUp + "\",\"TDS\":\"" + bookRoom.HotelRoomsDetails[i].Price.TDS + "\",\"ServiceTax\":\"" + bookRoom.HotelRoomsDetails[i].Price.ServiceTax + "\"}";
                if (bookRoom.HotelRoomsDetails[i].BedTypeCode == null && bookRoom.HotelRoomsDetails[i].Supplements == null)
                {
                    HotelRoomsDetails += "{\"RoomIndex\":\"" + bookRoom.HotelRoomsDetails[i].RoomIndex + "\",\"RoomTypeCode\":\"" + bookRoom.HotelRoomsDetails[i].RoomTypeCode + "\",\"RoomTypeName\":\"" + bookRoom.HotelRoomsDetails[i].RoomTypeName + "\",\"RatePlanCode\":\"" + bookRoom.HotelRoomsDetails[i].RatePlanCode + "\",\"BedTypeCode\":null,\"SmokingPreference\":\"" + bookRoom.HotelRoomsDetails[i].SmokingPreference + "\",\"Supplements\":null,\"Price\":" + Price + ",\"HotelPassenger\":" + HotelPassenger + "},";
                }
                else if (bookRoom.HotelRoomsDetails[i].BedTypeCode == null && bookRoom.HotelRoomsDetails[i].Supplements != null)
                {
                    HotelRoomsDetails += "{\"RoomIndex\":\"" + bookRoom.HotelRoomsDetails[i].RoomIndex + "\",\"RoomTypeCode\":\"" + bookRoom.HotelRoomsDetails[i].RoomTypeCode + "\",\"RoomTypeName\":\"" + bookRoom.HotelRoomsDetails[i].RoomTypeName + "\",\"RatePlanCode\":\"" + bookRoom.HotelRoomsDetails[i].RatePlanCode + "\",\"BedTypeCode\":null,\"SmokingPreference\":\"" + bookRoom.HotelRoomsDetails[i].SmokingPreference + "\",\"Supplements\":\"" + bookRoom.HotelRoomsDetails[i].Supplements + "\",\"Price\":" + Price + ",\"HotelPassenger\":" + HotelPassenger + "},";
                }
                else if (bookRoom.HotelRoomsDetails[i].BedTypeCode != null && bookRoom.HotelRoomsDetails[i].Supplements == null)
                {
                    HotelRoomsDetails += "{\"RoomIndex\":\"" + bookRoom.HotelRoomsDetails[i].RoomIndex + "\",\"RoomTypeCode\":\"" + bookRoom.HotelRoomsDetails[i].RoomTypeCode + "\",\"RoomTypeName\":\"" + bookRoom.HotelRoomsDetails[i].RoomTypeName + "\",\"RatePlanCode\":\"" + bookRoom.HotelRoomsDetails[i].RatePlanCode + "\",\"BedTypeCode\":\"" + bookRoom.HotelRoomsDetails[i].BedTypeCode + "\",\"SmokingPreference\":\"" + bookRoom.HotelRoomsDetails[i].SmokingPreference + "\",\"Supplements\":null,\"Price\":" + Price + ",\"HotelPassenger\":" + HotelPassenger + "},";
                }
                else
                {
                    HotelRoomsDetails += "{\"RoomIndex\":\"" + bookRoom.HotelRoomsDetails[i].RoomIndex + "\",\"RoomTypeCode\":\"" + bookRoom.HotelRoomsDetails[i].RoomTypeCode + "\",\"RoomTypeName\":\"" + bookRoom.HotelRoomsDetails[i].RoomTypeName + "\",\"RatePlanCode\":\"" + bookRoom.HotelRoomsDetails[i].RatePlanCode + "\",\"BedTypeCode\":\"" + bookRoom.HotelRoomsDetails[i].BedTypeCode + "\",\"SmokingPreference\":\"" + bookRoom.HotelRoomsDetails[i].SmokingPreference + "\",\"Supplements\":\"" + bookRoom.HotelRoomsDetails[i].Supplements + "\",\"Price\":" + Price + ",\"HotelPassenger\":" + HotelPassenger + "},";
                }
            }
            int index = HotelRoomsDetails.LastIndexOf(',');
            HotelRoomsDetails = HotelRoomsDetails.Remove(index, 1);
            HotelRoomsDetails = "[" + HotelRoomsDetails + "]";
            string JsonData = "{\"ResultIndex\":\"" + bookRoom.ResultIndex + "\",\"HotelCode\":\"" + bookRoom.HotelCode + "\",\"HotelName\":\"" + bookRoom.HotelName + "\",\"GuestNationality\":\"" + bookRoom.GuestNationality + "\",\"NoOfRooms\":\"" + bookRoom.NoOfRooms + "\",\"ClientReferenceNo\":\"" + bookRoom.ClientReferenceNo + "\",\"IsVoucherBooking\":\"" + bookRoom.IsVoucherBooking + "\",\"HotelRoomsDetails\":" + HotelRoomsDetails + ",\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\",\"TraceId\":\"" + bookRoom.TraceId + "\"}";
            string Url = TravelCredentials.HotelBaseUrl + "/Book";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            BookRoomResponse deserializedProduct = JsonConvert.DeserializeObject<BookRoomResponse>(ResponseData);
            var HotelRoom = _dataRepository.SaveTravelRequest(JsonData, "BookHotelRoom", bookRoom.FK_MemId);
            return deserializedProduct;
        }


        [HttpPost("HotelBookingDetail")]
        [Produces("application/json")]
        public HotelBookingResponse HotelBookingDetail(HotelBookingDetails modelReq)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;

            string JsonData = "{\"BookingId\":\"" + modelReq.BookingId + "\",\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\"}";
            string Url = TravelCredentials.HotelBaseUrl + "/GetBookingDetail";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            HotelBookingResponse deserializedProduct = JsonConvert.DeserializeObject<HotelBookingResponse>(ResponseData);
            var HotelRoom = _dataRepository.SaveTravelRequest(JsonData, "BookHotelRoomDetails", modelReq.FK_MemId);
            return deserializedProduct;
        }

        [HttpPost("CancelRequest")]
        [Produces("application/json")]
        public CancelReqResponse CancelRequest(HotelCancelrequest modelReq)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;

            string JsonData = "{\"BookingMode\":\"" + modelReq.BookingMode + "\",\"RequestType\":\"" + modelReq.RequestType + "\",\"Remarks\":\"" + modelReq.Remarks + "\",\"BookingId\":\""+ modelReq.BookingId + "\",\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\"}";
            string Url = TravelCredentials.HotelBaseUrl + "/SendChangeRequest";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            CancelReqResponse deserializedProduct = JsonConvert.DeserializeObject<CancelReqResponse>(ResponseData);
            var HotelRoom = _dataRepository.SaveTravelRequest(JsonData, "HotelCancelRequest", modelReq.FK_MemId);
            return deserializedProduct;
        }
        [HttpPost("CancelResponse")]
        [Produces("application/json")]
        public CancelResResponse CancelResponse(HotelCancelResponse modelReq)
        {
            // JourneyType=1 for Oneway and 2 for toway journey
            var res = _dataRepository.GetToken();
            string Token = res.Result.Token;

            string JsonData = "{\"ChangeRequestId\":\"" + modelReq.ChangeRequestId + "\",\"EndUserIp\":\"" + TravelCredentials.EndUserIP + "\",\"TokenId\":\"" + Token + "\"}";
            string Url = TravelCredentials.HotelBaseUrl + "/GetChangeRequestStatus/";
            string ResponseData = CommonTravel.GetResponse(JsonData, Url);
            CancelResResponse deserializedProduct = JsonConvert.DeserializeObject<CancelResResponse>(ResponseData);
            var HotelRoom = _dataRepository.SaveTravelRequest(JsonData, "HotelCancelResponse", modelReq.FK_MemId);
            return deserializedProduct;
        }
    }
}
