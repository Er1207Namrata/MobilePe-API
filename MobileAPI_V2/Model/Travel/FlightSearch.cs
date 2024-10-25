using System;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel
{
    public class FlightSearch
    {
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
        public string DirectFlight { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string OneStopFlight { get; set; }
        public int JourneyType { get; set; }
        public string PreferredAirlines { get; set; }
        public List<Segment> Segments { get; set; }

        public List<string> Sources { get; set; }
    }
    public class Segment
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string FlightCabinClass { get; set; }
        public string PreferredDepartureTime { get; set; }
        public string PreferredArrivalTime { get; set; }
    }
    public class FlightSearchResponse
    {
        public ResponseFlightSearch Response { get; set; }
        //public string response { get; set; }
        //public string message { get; set; }
    }
    public class ResponseFlightSearch
    {
        public int ResponseStatus { get; set; }
        public AuthenticateError Error { get; set; }
        public string TraceId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public List<List<ResultsFlightSearch>> Results { get; set; }
    }
    public class ResultsFlightSearch
    {
        public bool IsHoldAllowedWithSSR { get; set; }
        public bool IsUpsellAllowed { get; set; }
        public string ResultIndex { get; set; }
        public int Source { get; set; }
        public bool IsLCC { get; set; }
        public bool IsRefundable { get; set; }
        public bool IsPanRequiredAtBook { get; set; }
        public bool IsPanRequiredAtTicket { get; set; }
        public bool IsPassportRequiredAtBook { get; set; }
        public bool IsPassportRequiredAtTicket { get; set; }
        public bool GSTAllowed { get; set; }
        public bool IsCouponAppilcable { get; set; }
        public bool IsGSTMandatory { get; set; }
        public string AirlineRemark { get; set; }
        public string ResultFareType { get; set; }
        public Fare Fare { get; set; }
        public List<FareBreakdown> FareBreakdown { get; set; }
        public List<List<Segments>> Segments { get; set; }
        public string LastTicketDate { get; set; }
        public string TicketAdvisory { get; set; }
        public List<FareRule> FareRules { get; set; }
        public PenaltyCharges PenaltyCharges { get; set; }
        public string AirlineCode { get; set; }
        public List<List<MiniFareRules>> MiniFareRules { get; set; }
        public string ValidatingAirline { get; set; }
        public FareClassification FareClassification { get; set; }
    }
    public class Segments
    {
        public string Baggage { get; set; }
        public string CabinBaggage { get; set; }
        public int CabinClass { get; set; }
        public int TripIndicator { get; set; }
        public int SegmentIndicator { get; set; }
        public Airline Airline { get; set; }
        public int NoOfSeatAvailable { get; set; }
        public Origin Origin { get; set; }
        public Destination Destination { get; set; }
        public int AccumulatedDuration { get; set; }
        public int Duration { get; set; }
        public int GroundTime { get; set; }
        public int Mile { get; set; }
        public bool StopOver { get; set; }
        public string FlightInfoIndex { get; set; }
        public string StopPoint { get; set; }
        public string StopPointArrivalTime { get; set; }
        public string StopPointDepartureTime { get; set; }
        public string Craft { get; set; }
        public string Remark { get; set; }
        public bool IsETicketEligible { get; set; }
        public string FlightStatus { get; set; }
        public string Status { get; set; }
        public FareClassification FareClassification { get; set; }
    }
    public class Airline
    {
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string FlightNumber { get; set; }
        public string FareClass { get; set; }
        public string OperatingCarrier { get; set; }
    }

    public class Airport
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string Terminal { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class Destination
    {
        public Airport Airport { get; set; }
        public string ArrTime { get; set; }
    }

  
    public class Origin
    {
        public Airport Airport { get; set; }
        public string DepTime { get; set; }
    }
    public class MiniFareRules
    {
        public string JourneyPoints { get; set; }
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Unit { get; set; }
        public string Details { get; set; }
    }
   

   
    public class ChargeBU
    {
        public string key { get; set; }
        public decimal value { get; set; }
    }

    public class Fare
    {
        public string Currency { get; set; }
        public int BaseFare { get; set; }
        public int Tax { get; set; }
        public List<TaxBreakup> TaxBreakup { get; set; }
        public int YQTax { get; set; }
        public int AdditionalTxnFeeOfrd { get; set; }
        public int AdditionalTxnFeePub { get; set; }
        public int PGCharge { get; set; }
        public decimal OtherCharges { get; set; }
        public List<ChargeBU> ChargeBU { get; set; }
        public int Discount { get; set; }
        public decimal PublishedFare { get; set; }
        public double CommissionEarned { get; set; }
        public double PLBEarned { get; set; }
        public double IncentiveEarned { get; set; }
        public double OfferedFare { get; set; }
        public double TdsOnCommission { get; set; }
        public double TdsOnPLB { get; set; }
        public double TdsOnIncentive { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal TotalBaggageCharges { get; set; }
        public decimal TotalMealCharges { get; set; }
        public decimal TotalSeatCharges { get; set; }
        public decimal TotalSpecialServiceCharges { get; set; }
    }
    public class TaxBreakup
    {
        public string key { get; set; }
        public int value { get; set; }
    }
    public class FareBreakdown
    {
        public string Currency { get; set; }
        public int PassengerType { get; set; }
        public int PassengerCount { get; set; }
        public int BaseFare { get; set; }
        public int Tax { get; set; }
        public List<TaxBreakup> TaxBreakUp { get; set; }
        public int YQTax { get; set; }
        public int AdditionalTxnFeeOfrd { get; set; }
        public int AdditionalTxnFeePub { get; set; }
        public int PGCharge { get; set; }
        public int SupplierReissueCharges { get; set; }
    }

    public class FareClassification
    {
        public string Color { get; set; }
        public string Type { get; set; }
    }

    public class FareRule
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Airline { get; set; }
        public string FareBasisCode { get; set; }
        public string FareRuleDetail { get; set; }
        public string FareRestriction { get; set; }
        public string FareFamilyCode { get; set; }
        public string FareRuleIndex { get; set; }
    }

    public class PenaltyCharges
    {
        public string ReissueCharge { get; set; }
    }

}
