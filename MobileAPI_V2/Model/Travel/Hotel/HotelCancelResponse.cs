namespace MobileAPI_V2.Model.Travel.Hotel
{
    public class HotelCancelResponse
    {
        public int ChangeRequestId { get; set; }
        public string TokenId { get; set; }
        public string EndUserIp { get; set; }
        public int FK_MemId { get; set; }
    }
    public class CancelResResponse
    {
        public HotelChangeRequestStatusResult HotelChangeRequestStatusResult { get; set; }
    }
    public class HotelChangeRequestStatusResult
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
