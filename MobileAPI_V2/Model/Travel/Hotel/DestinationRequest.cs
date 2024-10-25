using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Hotel
{
    public class DestinationRequest
    {
        public string CountryCode { get; set; }
    }
    public class DestinationResponse
    {
        public string TraceId { get; set; }
        public string TokenId { get; set; }
        public int Status { get; set; }
        public Error Error { get; set; }
        public List<DestinationCity> Destinations { get; set; }
    }
    public class DestinationCity
    {
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public int DestinationId { get; set; }
        public string StateProvince { get; set; }
        public int Type { get; set; }
    }
    public class TopDestinationResponse
    {
        public Error Error { get; set; }
        public int Status { get; set; }
        public string TokenId { get; set; }
        public string TopDestination { get; set; }
    }

}
