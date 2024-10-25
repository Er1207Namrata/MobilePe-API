using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel
{
    public class CancelTicket
    {
       
            public int BookingId { get; set; }
            public int RequestType { get; set; }
            public int CancellationType { get; set; }
            public List<Sector> Sectors { get; set; }
            public List<int> TicketId { get; set; }
            public string Remarks { get; set; }
            public string EndUserIp { get; set; }
            public string TokenId { get; set; }
        

     

    }
    public class Sector
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
    public class CancelResponse
    {
        public ResponseCancel Response { get; set; }
        //public string response { get; set; }
        //public string message { get; set; }
    }
    public class ResponseCancel
    {
        public List<TicketCRInfo> TicketCRInfo { get; set; }
        public int ResponseStatus { get; set; }
        public string TraceId { get; set; }
    }
    public class TicketCRInfo
    {
        public int ChangeRequestId { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
    }

}
