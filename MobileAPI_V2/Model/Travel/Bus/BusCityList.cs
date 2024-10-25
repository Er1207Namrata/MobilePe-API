using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Bus
{
    public class BusCityList
    {
        public string TokenId { get; set; }
        public string IpAddress { get; set; }
        public string ClientId { get; set; }
    }
    public class BusCityListResponse
    {
        public string TokenId { get; set; }
        public int Status { get; set; }
        public BusError Error { get; set; }
        public List<BusList> BusCities { get; set; }
    }
   
    public class BusList
    {
        public string CityId { get; set;}
        public string CityName { get; set;}
    }
}
