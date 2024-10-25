using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MobileAPI_V2.Model.Travel
{
    public class BookTicket
    {
        public string ResultIndex { get; set; }
        public List<Passenger> Passengers { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public int Fk_MemId { get; set; }
    }
    public class Passenger
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PaxType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string PassportNo { get; set; }
        public DateTime PassportExpiry { get; set; }
        public DateTime PassportIssueDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public BookFare Fare { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string CellCountryCode { get; set; }
        public string ContactNo { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public bool? IsLeadPax { get; set; }
        public string FFAirlineCode { get; set; }
        public string FFNumber { get; set; }
        public string GSTCompanyAddress { get; set; }
        public string GSTCompanyContactNumber { get; set; }
        public string GSTCompanyName { get; set; }
        public string GSTNumber { get; set; }
        public string GSTCompanyEmail { get; set; }
    }
    public class BookFare
    {
        public string Currency { get; set; }
        public double BaseFare { get; set; }
        public double Tax { get; set; }
        public double YQTax { get; set; }
        public double AdditionalTxnFeePub { get; set; }
        public double AdditionalTxnFeeOfrd { get; set; }
        public double OtherCharges { get; set; }
        public double Discount { get; set; }
        public double PublishedFare { get; set; }
        public double OfferedFare { get; set; }
        public double TdsOnCommission { get; set; }
        public double TdsOnPLB { get; set; }
        public double TdsOnIncentive { get; set; }
        public double ServiceFee { get; set; }
    }
    public class BookTicketResponse
    {
        public BookResponse Response { get; set; }
        //public string response { get; set; }
        //public string message { get; set; }
    }
    public class BookResponse
    {
        public AuthenticateError Error { get; set; }
        public string TraceId { get; set; }
        public int ResponseStatus { get; set; }
        public BooksResponse Response { get; set; }
       
    }
    public class BooksResponse
    {
        public string PNR { get; set; }
        public int BookingId { get; set; }
        public bool SSRDenied { get; set; }
        public string SSRMessage { get; set; }
        public int Status { get; set; }
        public bool IsPriceChanged { get; set; }
        public bool IsTimeChanged { get; set; }
        public FlightItinerary FlightItinerary { get; set; }
    }
    public class FlightItinerary
    {
        public string CommentDetails { get; set; }
        public int JourneyType { get; set; }
        public int TripIndicator { get; set; }
        public bool BookingAllowedForRoamer { get; set; }
        public int BookingId { get; set; }
        public bool IsCouponAppilcable { get; set; }
        public bool IsManual { get; set; }
        public string PNR { get; set; }
        public bool IsDomestic { get; set; }
        public string ResultFareType { get; set; }
        public int Source { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string AirlineCode { get; set; }
        public DateTime LastTicketDate { get; set; }
        public string ValidatingAirlineCode { get; set; }
        public string AirlineRemark { get; set; }
        public bool IsLCC { get; set; }
        public bool NonRefundable { get; set; }
        public string FareType { get; set; }
        public string CreditNoteNo { get; set; }
        public BookingFare Fare { get; set; }
        public string CreditNoteCreatedOn { get; set; }
        public List<BookingPassenger> Passenger { get; set; }
        public string CancellationCharges { get; set; }
        public List<BookingSegment> Segments { get; set; }
        public List<BookingFareRule> FareRules { get; set; }
        public List<MiniFareRule> MiniFareRules { get; set; }
        public PenaltyCharges PenaltyCharges { get; set; }
        public int Status { get; set; }
        public bool IsWebCheckInAllowed { get; set; }
    }
    public class MiniFareRule
    {
        public string JourneyPoints { get; set; }
        public string Type { get; set; }
        public object From { get; set; }
        public object To { get; set; }
        public object Unit { get; set; }
        public string Details { get; set; }
    }
    public class BookingFareRule
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
    public class BookingSegment
    {
        public string Baggage { get; set; }
        public object CabinBaggage { get; set; }
        public int CabinClass { get; set; }
        public int TripIndicator { get; set; }
        public int SegmentIndicator { get; set; }
        public Airline Airline { get; set; }
        public string AirlinePNR { get; set; }
        public Origin Origin { get; set; }
        public Destination Destination { get; set; }
        public int AccumulatedDuration { get; set; }
        public int Duration { get; set; }
        public int GroundTime { get; set; }
        public int Mile { get; set; }
        public bool StopOver { get; set; }
        public string FlightInfoIndex { get; set; }
        public string StopPoint { get; set; }
        public DateTime StopPointArrivalTime { get; set; }
        public DateTime StopPointDepartureTime { get; set; }
        public string Craft { get; set; }
        public string Remark { get; set; }
        public bool IsETicketEligible { get; set; }
        public string FlightStatus { get; set; }
        public string Status { get; set; }
        public object FareClassification { get; set; }
    }
    public class BookingFare
    {
        public string Currency { get; set; }
        public int BaseFare { get; set; }
        public int Tax { get; set; }
        public List<TaxBreakup> TaxBreakup { get; set; }
        public int YQTax { get; set; }
        public int AdditionalTxnFeeOfrd { get; set; }
        public int AdditionalTxnFeePub { get; set; }
        public int PGCharge { get; set; }
        public int OtherCharges { get; set; }
        public List<ChargeBU> ChargeBU { get; set; }
        public int Discount { get; set; }
        public int PublishedFare { get; set; }
        public double CommissionEarned { get; set; }
        public double PLBEarned { get; set; }
        public double IncentiveEarned { get; set; }
        public double OfferedFare { get; set; }
        public double TdsOnCommission { get; set; }
        public double TdsOnPLB { get; set; }
        public double TdsOnIncentive { get; set; }
        public int ServiceFee { get; set; }
        public int TotalBaggageCharges { get; set; }
        public int TotalMealCharges { get; set; }
        public int TotalSeatCharges { get; set; }
        public int TotalSpecialServiceCharges { get; set; }
    }
    public class BookingPassenger
    {
        public object BarcodeDetails { get; set; }
        public object DocumentDetails { get; set; }
        public object GuardianDetails { get; set; }
        public int PaxId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PaxType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public bool IsPANRequired { get; set; }
        public bool IsPassportRequired { get; set; }
        public string PAN { get; set; }
        public string PassportNo { get; set; }
        public DateTime PassportExpiry { get; set; }
        public string AddressLine1 { get; set; }
        public BookingFare Fare { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsLeadPax { get; set; }
        public string FFAirlineCode { get; set; }
        public string FFNumber { get; set; }
        public List<object> Ssr { get; set; }
        public string GSTCompanyAddress { get; set; }
        public string GSTCompanyContactNumber { get; set; }
        public string GSTCompanyEmail { get; set; }
        public string GSTCompanyName { get; set; }
        public string GSTNumber { get; set; }
    }
    public class TravelSaveResponse
    {
        public int Fk_MemId { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public string PNRNo { get; set; }
        public int BookingId { get; set; }
        public string IsLcc { get; set; }
        public string JourneyDate { get; set; }
        public string SSRMessage { get; set; }
        public string Message { get; set; }
        public string OrderId { get; set; }
        public string Class { get; set; }
        public bool SSRDenied { get; set; }
        public bool IsPriceChanged { get; set; }
        public bool IsTimeChanged { get; set; }
        public int IsSuccess { get; set; }
        public DataTable tblFlightItinerary { get; set; }
        public DataTable dtSegment { get;  set; }
        public DataTable dtSegmentR { get;  set; }
    }

}
