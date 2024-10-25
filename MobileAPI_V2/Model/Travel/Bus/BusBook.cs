using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Bus
{
    public class BusBook
    {
        public string EndUserIp { get; set; }
        public string ResultIndex { get; set; }
        public string TraceId { get; set; }
        public string TokenId { get; set; }
        public int BoardingPointId { get; set; }
        public int DropingPointId { get; set; }
        public List<BusBookPassenger> Passenger { get; set; }
    }

    public class BusBookGST
    {
        public decimal CGSTAmount { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal CessAmount { get; set; }
        public decimal CessRate { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal TaxableAmount { get; set; }
    }

    public class BusBookPassenger
    {
        public bool LeadPassenger { get; set; }
        public int PassengerId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int Gender { get; set; }
        public object IdNumber { get; set; }
        public object IdType { get; set; }
        public string LastName { get; set; }
        public string Phoneno { get; set; }
        public BusBookSeat Seat { get; set; }
    }

    public class BusBookPrice
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
        public BusBookGST GST { get; set; }
    }

   

    public class BusBookSeat
    {
        public string ColumnNo { get; set; }
        public int Height { get; set; }
        public bool IsLadiesSeat { get; set; }
        public bool IsMalesSeat { get; set; }
        public bool IsUpper { get; set; }
        public string RowNo { get; set; }
        public string SeatIndex { get; set; }
        public string SeatName { get; set; }
        public bool SeatStatus { get; set; }
        public int SeatType { get; set; }
        public int Width { get; set; }
        public BusBookPrice Price { get; set; }
    }

    // bus book respone

    public class BusBookResponse
    {
        public BookResult BookResult { get; set; }
    }

    public class BookResult
    {
        public int ResponseStatus { get; set; }
        public BusError Error { get; set; }
        public string TraceId { get; set; }
        public string BusBookingStatus { get; set; }
        public double InvoiceAmount { get; set; }
        public string InvoiceNumber { get; set; }
        public int BusId { get; set; }
        public string TicketNo { get; set; }
        public string TravelOperatorPNR { get; set; }
    }

}
