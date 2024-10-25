using System;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Bus
{
    public class BusService
    {
        public DateTime DateOfJourney { get; set; }
        public int DestinationId { get; set; }
        public string EndUserIp { get; set; }
        public string OriginId { get; set; }
        public string TokenId { get; set; }
        public string PreferredCurrency { get; set; }
    }
    public class BusServiceResponse
    {
        public BusSerchResult BusSearchResult { get; set; }
    }
    public class BusSerchResult
    {
        public int ResponseStatus { get; set; }
        public BusError Error { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
        public string TraceId { get; set; }
        public BusResult BusResults { get; set; }
    }
   public class BusResult
    {
        public int ResultIndex { get; set; }
        public string ArrivalTime { get; set; }
        public string AvailableSeats { get; set; }
        public string DepartureTime { get; set; }
        public string RouteId { get; set; }
        public string BusType { get; set; }
        public string ServiceName { get; set; }
        public string TravelName { get; set; }
        public bool IdProofRequired { get; set; }
        public bool IsDropPointMandatory { get; set; }
        public bool LiveTrackingAvailable { get; set; }
        public bool MTicketEnabled { get; set; }
        public int MaxSeatsPerTicket { get; set; }
        public int OperatorId { get; set; }
        public bool PartialCancellationAllowed { get; set; }
        public List<BusBoardingPointsDetails> BoardingPointsDetails { get; set; }
        public List<BusDroppingPointsDetails> DroppingPointsDetails { get; set; }
        public BusPrice BusPrice { get; set; }
        public List<BusCancellationPolicies> CancellationPolicies { get; set; }
        public BusSeatlayoutRequest BusSeatlayoutRequest { get; set; }
    }
    public class BusBoardingPointsDetails
    {
        public int CityPointIndex { get; set; }
        public string CityPointLocation { get; set; }
        public string CityPointName { get; set; }
        public DateTime CityPointTime { get; set; }
    }
    public class BusDroppingPointsDetails
    {
        public int CityPointIndex { get; set; }
        public int CityPointLocation { get; set; }
        public int CityPointName { get; set; }
        public int CityPointTime { get; set; }
    }
    public class BusPrice
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
        public BusGST GST { get; set; }
    }
    public class BusGST
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

    public class BusCancellationPolicies
    {
        public decimal CancellationCharge { get; set; }
        public decimal CancellationChargeType { get; set; }
        public string PolicyString { get; set; }
        public string TimeBeforeDept { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class BusSeatlayoutRequest
    {
        public string EndUserIp { get; set; }
        public int ResultIndex { get; set; }
        public int AgencyId { get; set; }
        public string TraceId { get; set; }
        public string TokenId { get; set; }

    }
}
