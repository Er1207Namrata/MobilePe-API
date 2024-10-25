using System.Collections.Generic;
using System.Xml.Serialization;

namespace MobileAPI_V2.Model.Travel.Hotel
{

    public class CountryResponse
    {
        public string CountryList { get; set; }
        public Error Error { get; set; }
        public int Status { get; set; }
        public string TokenId { get; set; }
    }
}
