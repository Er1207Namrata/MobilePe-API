namespace MobileAPI_V2.Model.Travel.Bus
{
    public class BusCancel
    {
        public string EndUserIp { get; set; }
        public string AgencyId { get; set; }
        public string TraceId { get; set; }
        public string TokenId { get; set; }
        public string BusId { get; set; }
        public string RequestType { get; set; }
        public string Remarks { get; set; }
        public string BookingMode { get; set; }
    }
    public class BusCancelResponse
    {
        public SendChangeRequestResult SendChangeRequestResult { get; set; }
    }
    public class SendChangeRequestResult
    {
        public int ResponseStatus { get; set; }
        public BusError Error { get; set; }
        public string TraceId { get; set; }
        public BusCRInfo BusCRInfo { get; set; }

    }
    public class BusCRInfo
    {
        public int ChangeRequestId { get; set; }
        public string CreditNoteNo { get; set; }
        public int ChangeRequestStatus { get; set; }
        public string CreditNoteCreatedOn { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal RefundedAmount { get; set; }
        public decimal CancellationCharge { get; set; }
        public decimal TotalServiceCharge { get; set; }
        public decimal TotalGSTAmount { get; set; }
        public GST GST { get; set; }
    }
}
