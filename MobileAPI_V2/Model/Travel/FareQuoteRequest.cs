using System;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel
{
    public class FareQuoteRequest
    {
        public string TraceId { get; set; }
        public string ResultIndex { get; set; }
        public int FK_MemId { get; set; }
    }
    public class FareQuoteResponse
    {
        public QuoteResponse Response { get; set; }
        //public string response { get; set; }
        //public string message { get; set; }
    }
    public class QuoteChargeBU
    {
        public string key { get; set; }
        public double value { get; set; }
    }

   
    public class QuoteFare
    {
        public string Currency { get; set; }
        public int BaseFare { get; set; }
        public int Tax { get; set; }
        public int YQTax { get; set; }
        public int AdditionalTxnFeeOfrd { get; set; }
        public int AdditionalTxnFeePub { get; set; }
        public double OtherCharges { get; set; }
        public List<QuoteChargeBU> ChargeBU { get; set; }
        public int Discount { get; set; }
        public double PublishedFare { get; set; }
        public decimal CommissionEarned { get; set; }
        public double PLBEarned { get; set; }
        public double IncentiveEarned { get; set; }
        public double OfferedFare { get; set; }
        public double TdsOnCommission { get; set; }
        public double TdsOnPLB { get; set; }
        public double TdsOnIncentive { get; set; }
        public decimal ServiceFee { get; set; }
    }

    public class QuoteFareBreakdown
    {
        public string Currency { get; set; }
        public int PassengerType { get; set; }
        public int PassengerCount { get; set; }
        public decimal BaseFare { get; set; }
        public decimal Tax { get; set; }
        public decimal YQTax { get; set; }
        public decimal AdditionalTxnFeeOfrd { get; set; }
        public decimal AdditionalTxnFeePub { get; set; }
        public decimal? AdditionalTxnFee { get; set; }
    }

    public class QuoteFareRule
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Airline { get; set; }
        public string FareBasisCode { get; set; }
        public string FareRuleDetail { get; set; }
        public string FareRestriction { get; set; }
    }

    public class QuoteResponse
    {
        public AuthenticateError Error { get; set; }
        public bool IsPriceChanged { get; set; }
        public int ResponseStatus { get; set; }
        public QuoteResults Results { get; set; }
        public string TraceId { get; set; }
    }

    public class QuoteResults
    {
        public string ResultIndex { get; set; }
        public int Source { get; set; }
        public bool IsLCC { get; set; }
        public bool IsRefundable { get; set; }
        public string AirlineRemark { get; set; }
        public QuoteFare Fare { get; set; }
        public List<QuoteFareBreakdown> FareBreakdown { get; set; }
        public List<List<QuoteSegments>> Segments { get; set; }
        public string LastTicketDate { get; set; }
        public string TicketAdvisory { get; set; }
        public List<QuoteFareRule> FareRules { get; set; }
        public string AirlineCode { get; set; }
        public string ValidatingAirline { get; set; }
    }
    public class QuoteSegments
    {
        public int TripIndicator { get; set; }
        public int SegmentIndicator { get; set; }
        public QuoteAirline Airline { get; set; }
        public QuoteOrigin Origin { get; set; }
        public QuoteDestination Destination { get; set; }
        public int Duration { get; set; }
        public int GroundTime { get; set; }
        public int Mile { get; set; }
        public bool StopOver { get; set; }
        public string StopPoint { get; set; }
        public string StopPointArrivalTime { get; set; }
        public string StopPointDepartureTime { get; set; }
        public string Craft { get; set; }
        public bool IsETicketEligible { get; set; }
        public string FlightStatus { get; set; }
        public string Status { get; set; }
    }
    public class QuoteAirline
    {
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string FlightNumber { get; set; }
        public string FareClass { get; set; }
        public string OperatingCarrier { get; set; }
    }

    public class QuoteAirport
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string Terminal { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class QuoteDestination
    {
        public Airport Airport { get; set; }
        public DateTime ArrTime { get; set; }
    }

    public class QuoteOrigin
    {
        public Airport Airport { get; set; }
        public DateTime DepTime { get; set; }
    }
}
