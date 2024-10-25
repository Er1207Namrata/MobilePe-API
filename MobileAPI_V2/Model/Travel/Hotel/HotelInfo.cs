using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Hotel
{
    public class HotelInfo
    {
        public int FK_MemId { get; set; }
        public string ResultIndex { get; set; }
        public string HotelCode { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
    }
    public class Attraction
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

  
    public class HotelDetails
    {
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public int StarRating { get; set; }
        public string HotelURL { get; set; }
        public string Description { get; set; }
        public List<Attraction> Attractions { get; set; }
        public List<string> HotelFacilities { get; set; }
        public string HotelPolicy { get; set; }
        public string SpecialInstructions { get; set; }
        public string HotelPicture { get; set; }
        public List<string> Images { get; set; }
        public string Address { get; set; }
        public string CountryName { get; set; }
        public string PinCode { get; set; }
        public string HotelContactNo { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string RoomData { get; set; }
        public List<string> RoomFacilities { get; set; }
        public string Services { get; set; }
    }

    public class HotelInfoResult
    {
        public int ResponseStatus { get; set; }
        public Error Error { get; set; }
        public string TraceId { get; set; }
        public HotelDetails HotelDetails { get; set; }
    }

    public class HotelInfoResponse
    {
        public HotelInfoResult HotelInfoResult { get; set; }
    }
}
