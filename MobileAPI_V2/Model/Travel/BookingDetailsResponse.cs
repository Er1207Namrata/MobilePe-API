using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MobileAPI_V2.Model.Travel
{
    
    public class BookingListResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public BookingResult result { get; set; }
    }
    public class BookingResult
    {
        public List<BookingList> lstBooking { get; set; }
    }
    public class TrainBookingListResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public TrainBookingResult result { get; set; }
    }
    public class TrainBookingResult
    {
        public List<TrainBookingList> lstBooking { get; set; }
    }

    public class BookingList
    {
        public string TraceId { get; set; }
        public string TokenId { get; set; }
        public string PNRNo { get; set; }
        public string BookingId { get; set; }
        public string journeyDate { get; set; }
        public string Status { get; set; }
        public string paymentStatus { get; set; }
        public string paymentId { get; set; }
        public string paymentTime { get; set; }
        public bool IsCancelled { get; set; }
        public string JourneyType { get; set; }
        public string TicketDate { get; set; }
        public string Segment { get; set; }


    }
    public class TrainBookingList
    {
        public string ClientTransactionId { get; set; }
        public string pnrNumber { get; set; }
        public string trainName { get; set; }
        public string trainNo { get; set; }
        public string journeyDate { get; set; }
        public string departureTime { get; set; }
        public string destArrvDate { get; set; }
        public string arrivalTime { get; set; }
        public string journeyClass { get; set; }
        public string journeyQuota { get; set; }
        public string distance { get; set; }
        public string totalFare { get; set; }
        public string wpServiceTax { get; set; }
        public string wpServiceCharge { get; set; }
        public string Status { get; set; }


    }
    public class BookingDetailsRequest
    {
        public string TokenId { get; set; }
        public string PNR { get; set; }
        public string BookingId { get; set; }
        public string TraceId { get; set; }
        public string EndUserIp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class AirlineBookingDetails
    {
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string FlightNumber { get; set; }
        public string FareClass { get; set; }
        public string OperatingCarrier { get; set; }
    }

    public class AirportBookingDetails
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string Terminal { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class ChargeBUBookingDetails
    {
        public string key { get; set; }
        public int value { get; set; }
    }

    public class DestinationBookingDetails
    {
        public AirportBookingDetails Airport { get; set; }
        public DateTime ArrTime { get; set; }
    }



    public class FareBookingDetails
    {
        public string Currency { get; set; }
        public double BaseFare { get; set; }
        public double Tax { get; set; }
        public List<TaxBreakupBookingDetails> TaxBreakup { get; set; }
        public double YQTax { get; set; }
        public double AdditionalTxnFeeOfrd { get; set; }
        public double AdditionalTxnFeePub { get; set; }
        public double PGCharge { get; set; }
        public double OtherCharges { get; set; }
        public List<ChargeBUBookingDetails> ChargeBU { get; set; }
        public double Discount { get; set; }
        public double PublishedFare { get; set; }
        public double CommissionEarned { get; set; }
        public double PLBEarned { get; set; }
        public double IncentiveEarned { get; set; }
        public double OfferedFare { get; set; }
        public double TdsOnCommission { get; set; }
        public double TdsOnPLB { get; set; }
        public double TdsOnIncentive { get; set; }
        public double ServiceFee { get; set; }
        public double TotalBaggageCharges { get; set; }
        public double TotalMealCharges { get; set; }
        public double TotalSeatCharges { get; set; }
        public double TotalSpecialServiceCharges { get; set; }
    }

    public class FareRuleBookingDetails
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

    public class FlightItineraryBookingDetails
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
        public FareBookingDetails Fare { get; set; }
        public object CreditNoteCreatedOn { get; set; }
        public List<PassengerBookingDetails> Passenger { get; set; }
        public object CancellationCharges { get; set; }
        public List<SegmentBookingDetails> Segments { get; set; }
        public List<FareRuleBookingDetails> FareRules { get; set; }
        public List<MiniFareRuleBookingDetails> MiniFareRules { get; set; }
        public PenaltyChargesBookingDetails PenaltyCharges { get; set; }
        public int Status { get; set; }
        public List<InvoiceBookingDetails> Invoice { get; set; }
        public int InvoiceAmount { get; set; }
        public string InvoiceNo { get; set; }
        public int InvoiceStatus { get; set; }
        public DateTime InvoiceCreatedOn { get; set; }
        public string Remarks { get; set; }
        public bool IsWebCheckInAllowed { get; set; }
    }

    public class InvoiceBookingDetails
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

    public class MiniFareRuleBookingDetails
    {
        public string JourneyPoints { get; set; }
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Unit { get; set; }
        public string Details { get; set; }
    }

    public class OriginBookingDetails
    {
        public AirportBookingDetails Airport { get; set; }
        public DateTime DepTime { get; set; }
    }

    public class PassengerBookingDetails
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
        public FareBookingDetails Fare { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsLeadPax { get; set; }
        public object FFAirlineCode { get; set; }
        public object FFNumber { get; set; }
        public List<SsrBookingDetails> Ssr { get; set; }
        public TicketBookingDetails Ticket { get; set; }
        public List<SegmentAdditionalInfoBookingDetails> SegmentAdditionalInfo { get; set; }
    }

    public class PenaltyChargesBookingDetails
    {
        public string ReissueCharge { get; set; }
        public string CancellationCharge { get; set; }
    }

    public class BookingDetaResponse
    {
        public AuthenticateError Error { get; set; }
        public int ResponseStatus { get; set; }
        public string TraceId { get; set; }
        public FlightItineraryBookingDetails FlightItinerary { get; set; }
    }

    public class BookingDetailsReponse
    {
        public BookingDetaResponse Response { get; set; }
        //public string response { get; set; }
        //public string message { get; set; }
    }

    public class SegmentBookingDetails
    {
        public object Baggage { get; set; }
        public object CabinBaggage { get; set; }
        public int CabinClass { get; set; }
        public int TripIndicator { get; set; }
        public int SegmentIndicator { get; set; }
        public AirlineBookingDetails Airline { get; set; }
        public string AirlinePNR { get; set; }
        public OriginBookingDetails Origin { get; set; }
        public DestinationBookingDetails Destination { get; set; }
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

    public class SegmentAdditionalInfoBookingDetails
    {
        public string FareBasis { get; set; }
        public string NVA { get; set; }
        public string NVB { get; set; }
        public string Baggage { get; set; }
        public string Meal { get; set; }
        public string Seat { get; set; }
        public string SpecialService { get; set; }
    }

    public class SsrBookingDetails
    {
        public string Detail { get; set; }
        public string SsrCode { get; set; }
        public object SsrStatus { get; set; }
        public int Status { get; set; }
    }

    public class TaxBreakupBookingDetails
    {
        public string key { get; set; }
        public int value { get; set; }
    }

    public class TicketBookingDetails
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

   
    public class PassengersList
    {
        public string Title { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string email { get; set; }
        public string age { get; set; }
        public string DOB { get; set; }
        public string fk_memid { get; set; }

        public int pk_psid { get; set; }

        public int Type { get; set; }
        public string FoodPrefernce { get; set; }
        public bool SeniorCitizenConsession { get; set; }
        public string Nationality { get; set; }
        public string BirthPreference { get; set; }

        


    }
    public class PassengersListResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public List<PassengersList> lstpassenger { get; set; }
    }
    public class PassengerResponse
    {
        public string response { get; set; }
        public string msg { get; set; }
        public string flag { get; set; }
        
    }

}
