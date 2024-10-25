using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Hotel
{
    public class HotelSearch
    {
        public string CheckInDate { get; set; }
       
        public string NoOfNights { get; set; }
        public string CountryCode { get; set; }
        public string CityId { get; set; }
        public object ResultCount { get; set; }
        public string PreferredCurrency { get; set; }
        public string GuestNationality { get; set; }
        public string NoOfRooms { get; set; }
        public List<RoomGuest> RoomGuests { get; set; }
        public int MaxRating { get; set; }
        public int MinRating { get; set; }       
        public bool IsNearBySearchAllowed { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
    }
    public class RoomGuest
    {
        public int NoOfAdults { get; set; }
        public int NoOfChild { get; set; }
        public List<int> ChildAge { get; set; }
    }
    public class HotelSearchResponse
    {
        public HotelSearchResult HotelSearchResult { get; set; }
    }
    public class HotelSearchResult
    {
        public int ResponseStatus { get; set; }
        public Error Error { get; set; }
        public string TraceId { get; set; }
        public string CityId { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string PreferredCurrency { get; set; }
        public int NoOfRooms { get; set; }
        public List<RoomGuest> RoomGuests { get; set; }
        public List<HotelResult> HotelResults { get; set; }
    }

    public class HotelResult
    {
        public int ResultIndex { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string HotelCategory { get; set; }
        public int StarRating { get; set; }
        public string HotelDescription { get; set; }
        public string HotelPromotion { get; set; }
        public string HotelPolicy { get; set; }
        public Price Price { get; set; }
        public string HotelPicture { get; set; }
        public string HotelAddress { get; set; }
        public string HotelContactNo { get; set; }
        public object HotelMap { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string HotelLocation { get; set; }
        public TripAdvisor TripAdvisor { get; set; }
        public List<object> RoomDetails { get; set; }
    }
    public class Price
    {
        public string CurrencyCode { get; set; }
        public double RoomPrice { get; set; }
        public double Tax { get; set; }
        public double ExtraGuestCharge { get; set; }
        public double ChildCharge { get; set; }
        public double OtherCharges { get; set; }
        public double Discount { get; set; }
        public double PublishedPrice { get; set; }
        public double PublishedPriceRoundedOff { get; set; }
        public double OfferedPrice { get; set; }
        public double OfferedPriceRoundedOff { get; set; }
        public double AgentCommission { get; set; }
        public double AgentMarkUp { get; set; }
        public double ServiceTax { get; set; }
        public double TDS { get; set; }
    }
    public class TripAdvisor
    {
        public double Rating { get; set; }
        public string ReviewURL { get; set; }
    }


}
