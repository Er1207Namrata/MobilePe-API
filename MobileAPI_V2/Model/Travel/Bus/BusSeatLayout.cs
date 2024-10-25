using MobileAPI_V2.Model.Travel.Hotel;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Bus
{
    public class BusSeatLayout
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public int ResultIndex { get; set; }
    }
    public class BusSeatLayoutResponse
    {
        public BusSeatLayOutResult GetBusSeatLayOutResult { get; set; }
    }
    public class BusSeatLayOutResult
    {
        public int ResponseStatus { get; set; }
        public BusError Error { get; set; }
        public string TraceId { get; set; }
        public BusSeatLayoutDetails SeatLayoutDetails { get; set; }
    }
    public class BusSeatLayoutDetails
    {
        public string AvailableSeats { get; set; }
        public string HTMLLayout { get; set; }
        public SeatLayout SeatLayout { get; set; }
    }
    public class SeatLayout
    {
        public int NoOfColumns { get; set; }
        public int NoOfRows { get; set; }
        public List<SeatDetails> SeatDetails { get; set; }
    }
    public class SeatDetails
    {
        public string ColumnNo { get; set; }
        public string Height { get; set; }
        public bool IsLadiesSeat { get; set; }
        public bool IsMalesSeat { get; set; }
        public string RowNo { get; set; }
        public string SeatFare { get; set; }
        public string SeatIndex { get; set; }
        public string SeatName { get; set; }
        public bool SeatStatus { get; set; }
        public int SeatType { get; set; }
        public int Width { get; set; }
        public SeatPrice Price { get; set; }
    }
    public class SeatPrice
    {
        public string CurrencyCode { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Tax { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal Discount { get; set; }
        public decimal PublishedPrice { get; set; }
        public decimal PublishedPriceRoundedOff { get; set; }
        public decimal OfferedPrice { get; set; }
        public decimal OfferedPriceRoundedOff { get; set; }
        public decimal AgentCommission { get; set; }
        public decimal AgentMarkUp { get; set; }
        public decimal TDS { get; set; }
        public PriceGST GST { get; set; }
    }
    public class PriceGST
    {
        public decimal CGSTAmount { get; set;}
        public decimal CGSTRate { get; set;}
        public decimal CessAmount { get; set;}
        public decimal CessRate { get; set;}
        public decimal IGSTAmount { get; set;}
        public decimal IGSTRate { get; set;}
        public decimal SGSTAmount { get; set;}
        public decimal SGSTRate { get; set;}
        public decimal TaxableAmount { get; set;}
    }
}
