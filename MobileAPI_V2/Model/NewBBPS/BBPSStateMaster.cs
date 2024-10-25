using System.Collections.Generic;

namespace MobileAPI_V2.Model
{
    public class BBPSStateMaster
    {
        public string state_ID { get; set; }
        public string state { get; set; }
    }
    public class BBPSStateMasterReq
    {
        public string CategoryID { get; set; }
    }
    
    public class BBPSStateMasterRes
    {
        public string response_code { get; set; }
        public string response_message { get; set; }
        public BillerSessionDetails session_details { get; set; }
        public List<BBPSStateMaster> Data { get; set; }
    }
}
