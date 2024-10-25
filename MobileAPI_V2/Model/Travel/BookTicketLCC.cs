using System;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel
{
    public class BookTicketLCC
    {
        //public string PreferredCurrency { get; set; }
        public string ResultIndex { get; set; }

        public List<PassengerLCC> Passengers { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public int Fk_MemId { get; set; }
    }
    public class PassengerLCC
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PaxType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string PassportNo { get; set; }
        public DateTime PassportExpiry { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public FareLCC Fare { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsLeadPax { get; set; }
        public string FFAirlineCode { get; set; }
        public string FFNumber { get; set; }
        public List<Baggage> Baggage { get; set; }
        public List<MealDynamic> MealDynamic { get; set; }
        public List<SeatDynamicLCC> SeatDynamic { get; set; }
        public string GSTCompanyAddress { get; set; }
        public string GSTCompanyContactNumber { get; set; }
        public string GSTCompanyName { get; set; }
        public string GSTNumber { get; set; }
        public string GSTCompanyEmail { get; set; }
    }
    public class MealDynamic
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public int WayType { get; set; }
        public string Code { get; set; }
        public int Description { get; set; }
        public string AirlineDescription { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
    public class SeatDynamicLCC
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
    }


    public class Baggage
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public int WayType { get; set; }
        public string Code { get; set; }
        public int Description { get; set; }
        public int Weight { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
    public class FareLCC
    {
        public double BaseFare { get; set; }
        public double Tax { get; set; }
        public double YQTax { get; set; }
        public double AdditionalTxnFeePub { get; set; }
        public double AdditionalTxnFeeOfrd { get; set; }
        public double OtherCharges { get; set; }
    }

    #region Response
    public class AirlineForLCC
    {
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string FlightNumber { get; set; }
        public string FareClass { get; set; }
        public string OperatingCarrier { get; set; }
    }

    public class AirportForLCC
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string Terminal { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class BaggageForLCC
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public int WayType { get; set; }
        public string Code { get; set; }
        public int Description { get; set; }
        public int Weight { get; set; }
        public string Currency { get; set; }
        public int Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

    public class BarcodeForLCC
    {
        public int Index { get; set; }
        public string Format { get; set; }
        public string Content { get; set; }
        public object BarCodeInBase64 { get; set; }
        public int JourneyWayType { get; set; }
    }

    public class BarcodeDetailsForLCC
    {
        public int Id { get; set; }
        public List<BarcodeForLCC> Barcode { get; set; }
    }

    public class ChargeBUForLCC
    {
        public string key { get; set; }
        public double value { get; set; }
    }

    public class DestinationForLCC
    {
        public AirportForLCC Airport { get; set; }
        public DateTime ArrTime { get; set; }
    }


    public class FareForLCC
    {
        public string Currency { get; set; }
        public dynamic BaseFare { get; set; }
        public double Tax { get; set; }
        public List<TaxBreakupForLCC> TaxBreakup { get; set; }
        public double YQTax { get; set; }
        public double AdditionalTxnFeeOfrd { get; set; }
        public double AdditionalTxnFeePub { get; set; }
        public double PGCharge { get; set; }
        public double OtherCharges { get; set; }
        public List<ChargeBUForLCC> ChargeBU { get; set; }
        public double Discount { get; set; }
        public double PublishedFare { get; set; }
        public dynamic CommissionEarned { get; set; }
        public dynamic PLBEarned { get; set; }
        public dynamic IncentiveEarned { get; set; }
        public double OfferedFare { get; set; }
        public dynamic TdsOnCommission { get; set; }
        public dynamic TdsOnPLB { get; set; }
        public dynamic TdsOnIncentive { get; set; }
        public dynamic ServiceFee { get; set; }
        public dynamic TotalBaggageCharges { get; set; }
        public dynamic TotalMealCharges { get; set; }
        public dynamic TotalSeatCharges { get; set; }
        public dynamic TotalSpecialServiceCharges { get; set; }
    }

    public class FareRuleForLCC
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Airline { get; set; }
        public string FareBasisCode { get; set; }
        public string FareRuleDetail { get; set; }
        public object FareRestriction { get; set; }
    }

    public class FlightItineraryForLCC
    {
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
        public FareForLCC Fare { get; set; }
        public object CreditNoteCreatedOn { get; set; }
        public List<PassengerForLCC> Passenger { get; set; }
        public object CancellationCharges { get; set; }
        public List<SegmentForLCC> Segments { get; set; }
        public List<FareRuleForLCC> FareRules { get; set; }
        public List<MiniFareRuleForLCC> MiniFareRules { get; set; }
        public PenaltyChargesForLCC PenaltyCharges { get; set; }
        public int Status { get; set; }
        public List<InvoiceForLCC> Invoice { get; set; }
        public double InvoiceAmount { get; set; }
        public string InvoiceNo { get; set; }
        public int InvoiceStatus { get; set; }
        public DateTime InvoiceCreatedOn { get; set; }
        public string Remarks { get; set; }
        public bool IsWebCheckInAllowed { get; set; }
    }

    public class InvoiceForLCC
    {
        public object CreditNoteGSTIN { get; set; }
        public object GSTIN { get; set; }
        public DateTime InvoiceCreatedOn { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public double InvoiceAmount { get; set; }
        public string Remarks { get; set; }
        public int InvoiceStatus { get; set; }
    }

    public class MealDynamicForLCC
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public int WayType { get; set; }
        public string Code { get; set; }
        public int Description { get; set; }
        public string AirlineDescription { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; }
        public int Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

    public class MiniFareRuleForLCC
    {
        public string JourneyPoints { get; set; }
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Unit { get; set; }
        public string Details { get; set; }
    }

    public class OriginForLCC
    {
        public AirportForLCC Airport { get; set; }
        public DateTime DepTime { get; set; }
    }

    public class PassengerForLCC
    {
        public BarcodeDetailsForLCC BarcodeDetails { get; set; }
        public object DocumentDetails { get; set; }
        public object GuardianDetails { get; set; }
        public int PaxId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PaxType { get; set; }
        public string DateOfBirth { get; set; }
        public int Gender { get; set; }
        public bool IsPANRequired { get; set; }
        public bool IsPassportRequired { get; set; }
        public string PAN { get; set; }
        public string PassportNo { get; set; }
        public string PassportExpiry { get; set; }
        public string AddressLine1 { get; set; }
        public FareForLCC Fare { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsLeadPax { get; set; }
        public object FFAirlineCode { get; set; }
        public string FFNumber { get; set; }
        public List<BaggageForLCC> Baggage { get; set; }
        public List<MealDynamicForLCC> MealDynamic { get; set; }
        public List<SeatDynamicForLCC> SeatDynamic { get; set; }
       
        public TicketForLCC Ticket { get; set; }
        public string GSTCompanyAddress { get; set; }
        public string GSTCompanyContactNumber { get; set; }
        public string GSTCompanyEmail { get; set; }
        public string GSTCompanyName { get; set; }
        public string GSTNumber { get; set; }
        public List<SegmentAdditionalInfoForLCC> SegmentAdditionalInfo { get; set; }
    }

    public class PenaltyChargesForLCC
    {
    }

    public class ResponseForLCC
    {
        public bool B2B2BStatus { get; set; }
        public AuthenticateError Error { get; set; }
        public int ResponseStatus { get; set; }
        public string TraceId { get; set; }
        public ResponseForLCC Response { get; set; }
        public string PNR { get; set; }
        public int BookingId { get; set; }
        public bool SSRDenied { get; set; }
        public string SSRMessage { get; set; }
        public int Status { get; set; }
        public bool IsPriceChanged { get; set; }
        public bool IsTimeChanged { get; set; }
        public FlightItineraryForLCC FlightItinerary { get; set; }
        public int TicketStatus { get; set; }
    }

    public class BookingResponseForLCC
    {
        public ResponseForLCC Response { get; set; }
    }

    public class SeatDynamicForLCC
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
        public object SeatNo { get; set; }
        public int SeatType { get; set; }
        public int SeatWayType { get; set; }
        public int Compartment { get; set; }
        public int Deck { get; set; }
        public string Currency { get; set; }
        public int Price { get; set; }
    }

    public class SegmentForLCC
    {
        public string Baggage { get; set; }
        public string CabinBaggage { get; set; }
        public int CabinClass { get; set; }
        public int TripIndicator { get; set; }
        public int SegmentIndicator { get; set; }
        public AirlineForLCC Airline { get; set; }
        public string AirlinePNR { get; set; }
        public OriginForLCC Origin { get; set; }
        public DestinationForLCC Destination { get; set; }
        public int Duration { get; set; }
        public int GroundTime { get; set; }
        public int Mile { get; set; }
        public bool StopOver { get; set; }
        public string FlightInfoIndex { get; set; }
        public string StopPoint { get; set; }
        public DateTime StopPointArrivalTime { get; set; }
        public DateTime StopPointDepartureTime { get; set; }
        public string Craft { get; set; }
        public object Remark { get; set; }
        public bool IsETicketEligible { get; set; }
        public string FlightStatus { get; set; }
        public string Status { get; set; }
        public object FareClassification { get; set; }
    }

    public class SegmentAdditionalInfoForLCC
    {
        public string FareBasis { get; set; }
        public string NVA { get; set; }
        public string NVB { get; set; }
        public string Baggage { get; set; }
        public string Meal { get; set; }
        public string Seat { get; set; }
        public string SpecialService { get; set; }
    }

    public class TaxBreakupForLCC
    {
        public string key { get; set; }
        public double value { get; set; }
    }

    public class TicketForLCC
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
    #endregion Response
    public class ResultTravel
    {
        public string Request { get; set; }
        public List<DatumNonLCC> data { get; set; }
    }
    public class ResultTravelForLCC
    {
        public List<DatumLCC> data { get; set; }
    }
    public class DatumLCC
    {
        public string ResultIndex { get; set; }
        public int Fk_MemId { get; set; }
        public List<PassengerForLCC> Passengers { get; set; }
        public List<FlightDetailsLCC> Flight { get; set; }
        public string TraceId { get; set; }
        public string Price { get; set; }
        public bool isLcc { get; set; }
    }
    public class FlightDetailsLCC
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
       
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string JourneyDate { get; set; }
        public string Class { get; set; }
    }
    public class DatumNonLCC
    {
        public string TraceId { get; set; }
        public string Price { get; set; }
        public string PNR { get; set; }
        public int BookingId { get; set; }
        public bool isLcc { get; set; }

        public List<FlightDetailsLCC> Flight { get; set; }
    }

}
