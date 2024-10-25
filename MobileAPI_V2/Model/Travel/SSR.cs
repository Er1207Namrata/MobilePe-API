using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel
{
    public class SSR
    {
        public string TraceId { get; set; }
        public string ResultIndex { get; set; }
        public int FK_MemId { get; set; }
    }
    public class SSrResponse
    {
        public SResponse Response { get; set; }
        //public string response { get; set; }
        //public string message { get; set; }
    }
    public class SResponse
    {
        public List<Meal> Meal { get; set; }
        public int ResponseStatus { get; set; }
        public AuthenticateError Error { get; set; }
        public string TraceId { get; set; }
        public List<List<Baggage>> Baggage { get; set; }
        public List<List<MealDynamic>> MealDynamic { get; set; }
        public List<SeatDynamic> SeatDynamic { get; set; }
       
    }
    public class Meal
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class SeatDynamic
    {
        public List<SegmentSeat> SegmentSeat { get; set; }
    }

    public class SegmentSeat
    {
        public List<RowSeat> RowSeats { get; set; }
    }
    public class RowSeat
    {
        public List<Seat> Seats { get; set; }
    }
    public class Seat
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public string CraftType { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int AvailablityType { get; set; }
        public int Description { get; set; }
        public string Code { get; set; }
        public string RowNo { get; set; }
        public string SeatNo { get; set; }
        public int SeatType { get; set; }
        public int SeatWayType { get; set; }
        public int Compartment { get; set; }
        public int Deck { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public string Text { get; set; }
    }

}
