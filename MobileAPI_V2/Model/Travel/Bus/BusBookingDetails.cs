using System;

namespace MobileAPI_V2.Model.Travel.Bus
{
    public class BusBookingDetails
    {
        public string EndUserIp { get; set; }
        public string TraceId { get; set; }
        public string TokenId { get; set; }
        public int BusId { get; set; }
        public bool IsBaseCurrencyRequired { get; set; }
        public int SeatId { get; set; }
    }
    public class BusBookingDetailsResponse
    {
        public GetBookingDetailResult GetBookingDetailResult { get; set; }
    }
    public class GetBookingDetailResult
    {
        public BusError Error { get; set; }
        public Itinerary Itinerary { get; set; }
    }
    public class Itinerary
    {
        public bool IsDomestic { get; set; }
        public string TicketNo { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int BlockDuration { get; set; }
        public int BookingMode { get; set; }
        public int BusId { get; set; }
        public string BusType { get; set; }
        public DateTime DateOfJourney { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Destination { get; set; }
        public int DestinationId { get; set; }
        public int NoOfSeats { get; set; }
        public string Origin { get; set; }
        public Passenger Passenger { get; set; }
        public string RouteId { get; set; }
        public int SourceId { get; set; }
        public int Status { get; set; }
        public string TravelName { get; set; }
        public string TravelOperatorPNR { get; set; }
        public BoardingPointdetails BoardingPointdetails { get; set; }
        public CancelPolicy CancelPolicy { get; set; }
        public DropingPointdetails DropingPointdetails { get; set; }
        public Price Price { get; set; }
        public int ResponseStatus { get; set; }
        public string TraceId { get; set; }

    }
}
