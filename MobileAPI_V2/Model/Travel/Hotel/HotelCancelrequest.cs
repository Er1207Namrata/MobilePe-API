namespace MobileAPI_V2.Model.Travel.Hotel
{
    public class HotelCancelrequest
    {
        public int BookingMode { get;set; }
        public int RequestType { get;set; }
        public string Remarks { get;set; }
        public int BookingId { get;set; }
        public string EndUserIp { get;set; }
        public string TokenId { get;set; }
        public int FK_MemId { get;set; }

    }
    public class CancelReqResponse
    {
        public HotelChangeRequestResult HotelChangeRequestResult { get; set; }
    }
    public class HotelChangeRequestResult
    {
        public bool B2B2BStatus { get; set; }
        public string CancellationChargeBreakUp { get; set; }
        public int TotalServiceCharge { get; set; }
        public int ResponseStatus { get; set; }
        public Error Error { get; set; }
        public string TraceId { get; set; }
        public int ChangeRequestId { get; set; }
        public int ChangeRequestStatus { get; set; }
    }
}
