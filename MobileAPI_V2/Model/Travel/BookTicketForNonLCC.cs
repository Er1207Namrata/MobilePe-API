using System;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel
{
    public class BookTicketForNonLCC
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public List<Passport> Passport { get; set; }
        public string PNR { get; set; }
        public int BookingId { get; set; }
    }
    public class Passport
    {
        public int PaxId { get; set; }
        public string PassportNo { get; set; }
        public DateTime PassportExpiry { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AirlineNonLcc
    {
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string FlightNumber { get; set; }
        public string FareClass { get; set; }
        public string OperatingCarrier { get; set; }
    }

    public class AirportNonLcc
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string Terminal { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class ChargeBUNonLcc
    {
        public string key { get; set; }
        public int value { get; set; }
    }

    public class DestinationNonLcc
    {
        public AirportNonLcc Airport { get; set; }
        public DateTime ArrTime { get; set; }
    }

  
    public class FareNonLcc
    {
        public string Currency { get; set; }
        public int BaseFare { get; set; }
        public int Tax { get; set; }
        public List<TaxBreakupNonLcc> TaxBreakup { get; set; }
        public int YQTax { get; set; }
        public int AdditionalTxnFeeOfrd { get; set; }
        public int AdditionalTxnFeePub { get; set; }
        public int PGCharge { get; set; }
        public int OtherCharges { get; set; }
        public List<ChargeBUNonLcc> ChargeBU { get; set; }
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

    public class FareRuleNonLcc
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

    public class FlightItineraryNonLcc
    {
        public string AgentRemarks { get; set; }
        public object CommentDetails { get; set; }
        public bool IsAutoReissuanceAllowed { get; set; }
        public string IssuancePcc { get; set; }
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
        public object CreditNoteNo { get; set; }
        public FareNonLcc Fare { get; set; }
        public object CreditNoteCreatedOn { get; set; }
        public List<PassengerNonLcc> Passenger { get; set; }
        public object CancellationCharges { get; set; }
        public List<SegmentNonLcc> Segments { get; set; }
        public List<FareRuleNonLcc> FareRules { get; set; }
        public List<MiniFareRuleNonLcc> MiniFareRules { get; set; }
        public PenaltyChargesNonLcc PenaltyCharges { get; set; }
        public int Status { get; set; }
        public List<InvoiceNonLcc> Invoice { get; set; }
        public int InvoiceAmount { get; set; }
        public string InvoiceNo { get; set; }
        public int InvoiceStatus { get; set; }
        public DateTime InvoiceCreatedOn { get; set; }
        public string Remarks { get; set; }
        public bool IsWebCheckInAllowed { get; set; }
    }

    public class InvoiceNonLcc
    {
        public object CreditNoteGSTIN { get; set; }
        public object GSTIN { get; set; }
        public DateTime InvoiceCreatedOn { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public int InvoiceAmount { get; set; }
        public string Remarks { get; set; }
        public int InvoiceStatus { get; set; }
    }

    public class MiniFareRuleNonLcc
    {
        public string JourneyPoints { get; set; }
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Unit { get; set; }
        public string Details { get; set; }
    }

    public class OriginNonLcc
    {
        public AirportNonLcc Airport { get; set; }
        public DateTime DepTime { get; set; }
    }

    public class PassengerNonLcc
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
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public Fare Fare { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsLeadPax { get; set; }
        public object FFAirlineCode { get; set; }
        public object FFNumber { get; set; }
        public List<SsrNonLcc> Ssr { get; set; }
        public TicketNonLcc Ticket { get; set; }
        public List<SegmentAdditionalInfoNonLcc> SegmentAdditionalInfo { get; set; }
    }

    public class PenaltyChargesNonLcc
    {
        public string ReissueCharge { get; set; }
        public string CancellationCharge { get; set; }
    }

    public class BookTicketForNonLcc
    {
        public bool B2B2BStatus { get; set; }
        public AuthenticateError Error { get; set; }
        public int ResponseStatus { get; set; }
        public string TraceId { get; set; }
        public BookTicketForNonLcc Response { get; set; }
        public string PNR { get; set; }
        public int BookingId { get; set; }
        public bool SSRDenied { get; set; }
        public string SSRMessage { get; set; }
        public bool IsPriceChanged { get; set; }
        public bool IsTimeChanged { get; set; }
        public FlightItineraryNonLcc FlightItinerary { get; set; }
        public string Message { get; set; }
        public int TicketStatus { get; set; }
    }

    public class BookTicketResponseForNonLcc
    {
        public BookTicketForNonLcc Response { get; set; }
        //public string response { get; set; }
        //public string message { get; set; }
    }

    public class SegmentNonLcc
    {
        public object Baggage { get; set; }
        public object CabinBaggage { get; set; }
        public int CabinClass { get; set; }
        public int TripIndicator { get; set; }
        public int SegmentIndicator { get; set; }
        public AirlineNonLcc Airline { get; set; }
        public string AirlinePNR { get; set; }
        public OriginNonLcc Origin { get; set; }
        public DestinationNonLcc Destination { get; set; }
        public int AccumulatedDuration { get; set; }
        public int Duration { get; set; }
        public int GroundTime { get; set; }
        public int Mile { get; set; }
        public bool StopOver { get; set; }
        public string FlightInfoIndex { get; set; }
        public string StopPoint { get; set; }
        public object StopPointArrivalTime { get; set; }
        public object StopPointDepartureTime { get; set; }
        public string Craft { get; set; }
        public object Remark { get; set; }
        public bool IsETicketEligible { get; set; }
        public string FlightStatus { get; set; }
        public string Status { get; set; }
        public object FareClassification { get; set; }
    }

    public class SegmentAdditionalInfoNonLcc
    {
        public string FareBasis { get; set; }
        public string NVA { get; set; }
        public string NVB { get; set; }
        public string Baggage { get; set; }
        public string Meal { get; set; }
        public string Seat { get; set; }
        public string SpecialService { get; set; }
    }

    public class SsrNonLcc
    {
        public string Detail { get; set; }
        public string SsrCode { get; set; }
        public object SsrStatus { get; set; }
        public int Status { get; set; }
    }

    public class TaxBreakupNonLcc
    {
        public string key { get; set; }
        public int value { get; set; }
    }

    public class TicketNonLcc
    {
        public int TicketId { get; set; }
        public string TicketNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public string ValidatingAirline { get; set; }
        public string Remarks { get; set; }
        public string ServiceFeeDisplayType { get; set; }
        public string Status { get; set; }
        public string ConjunctionNumber { get; set; }
        public string TicketType { get; set; }
    }



}
