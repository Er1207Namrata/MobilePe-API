using System;

namespace MobileAPI_V2.Model.Travel.Hotel
{
    public class HotelBookingDetails
    {
        public string BookingId { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public int FK_MemId { get; set; }
    }

    public class HotelBookingResponse
    {
        public BookingDetailResult GetBookingDetailResult { get; set; }
    }
    public class BookingDetailResult
    {
        public bool VoucherStatus { get; set; }
        public int ResponseStatus { get; set; }
        public Error Error { get; set; }
        public string TraceId { get; set; }
        public int Status { get; set; }
        public string HotelBookingStatus { get; set; }
        public string ConfirmationNo { get; set; }
        public string BookingRefNo { get; set; }
        public int BookingId { get; set; }
        public bool IsPriceChanged { get; set; }
        public bool IsCancellationPolicyChanged { get; set; }
        public string CreditNoteGSTIN { get; set; }
        public string GSTIN { get; set; }
        public string GuestNationality { get; set; }
        public string HotelPolicyDetail { get; set; }
        public string IntHotelPassportDetails { get; set; }
        public string InvoiceCreatedOn { get; set; }
        public string InvoiceNo { get; set; }
        public bool IsCorporate { get; set; }
        public string HotelConfirmationNo { get; set; }
        public string HotelCode { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string TBOHotelCode { get; set; }
        public int StarRating { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CountryCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string City { get; set; }
        public int CityId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime InitialCheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime InitialCheckOutDate { get; set; }
        public DateTime LastCancellationDate { get; set; }
        public DateTime LastVoucherDate { get; set; }
        public int NoOfRooms { get; set; }
        public DateTime BookingDate { get; set; }
        public string SpecialRequest { get; set; }
        public bool IsDomestic { get; set; }
        public bool BookingAllowedForRoamer { get; set; }
    }
}
