using System.Collections.Generic;

namespace MobileAPI_V2.Model
{
    public class BBPSList
    {
        public string billerCategoryID { get; set; }
        public string billerCategory { get; set; }
        public string status { get; set; }
        public string logopath { get; set; }
    }
    public class NewBBPSResponse
    {
        public string response_code { get;set; }
        public string response_message { get;set; }
        public List<BillerSessionDetails> session_details { get;set; }
        public List<BBPSList> Data { get; set; }
    }
    
}
