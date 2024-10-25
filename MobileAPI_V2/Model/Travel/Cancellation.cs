using MobileAPI_V2.Model.Travel.Hotel;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel
{
    public class CancellationCharges
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string RequestType { get; set; }
        public string BookingId { get; set; }
        public int Fk_MemId { get; set; }
    }
    public class CancellationChargesResponse
    {
        public ResponseCancellationCharges Response { get; set; }
        //public string response { get; set; }
        //public string message { get; set; }
    }
    public class ResponseCancellationCharges
    {
        public int ResponseStatus { get; set; }
        public string TraceId { get; set; }
        public int RefundAmount { get; set; }
        public int CancellationCharge { get; set; }
        public string Remarks { get; set; }
        public string Currency { get; set; }
        public GST GST { get; set; }
        public List<CancelChargeDetail> CancelChargeDetails { get; set; }
    }
    public class CancelChargeDetail
    {
        public object AdminFee { get; set; }
        public int CancellationCharge { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PaxType { get; set; }
        public double RefundAmount { get; set; }
        public object SupplierFee { get; set; }
        public object TicketNumber { get; set; }
        public string Title { get; set; }
    }
    public class GST
    {
        public int CGSTAmount { get; set; }
        public int CGSTRate { get; set; }
        public double CessAmount { get; set; }
        public int CessRate { get; set; }
        public double IGSTAmount { get; set; }
        public int IGSTRate { get; set; }
        public int SGSTAmount { get; set; }
        public int SGSTRate { get; set; }
        public double TaxableAmount { get; set; }
    }


}
