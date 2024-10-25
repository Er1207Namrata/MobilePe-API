using System;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Hotel
{
    public class BlockRoom
    {
        public int FK_MemId { get; set; }
        public string ResultIndex { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string GuestNationality { get; set; }
        public string NoOfRooms { get; set; }
        public string ClientReferenceNo { get; set; }
        public string IsVoucherBooking { get; set; }
        public List<BlockRoomHotelRoomsDetail> HotelRoomsDetails { get; set; }
        public string TraceId { get; set; }
    }
    public class BlockRoomHotelRoomsDetail
    {
        public string RoomIndex { get; set; }
        public string RoomTypeCode { get; set; }
        public string RoomTypeName { get; set; }
        public string RatePlanCode { get; set; }
        public List<object> BedTypeCode { get; set; }
        public int SmokingPreference { get; set; }
        public List<string> Supplements { get; set; }
        public BlockRoomPrice Price { get; set; }
    }

    public class BlockRoomPrice
    {
        public string CurrencyCode { get; set; }
        public double RoomPrice { get; set; }
        public double Tax { get; set; }
        public double ExtraGuestCharge { get; set; }
        public double ChildCharge { get; set; }
        public double OtherCharges { get; set; }
        public double Discount { get; set; }
        public double PublishedPrice { get; set; }
        public double PublishedPriceRoundedOff { get; set; }
        public double OfferedPrice { get; set; }
        public double OfferedPriceRoundedOff { get; set; }
        public double AgentCommission { get; set; }
        public double AgentMarkUp { get; set; }
        public double TDS { get; set; }
        public double ServiceTax { get; set; }
    }

    public class CancellationPolicyBlockRoom
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ChargeType { get; set; }
        public string Currency { get; set; }
        public double Charge { get; set; }
        public bool? NoShowPolicy { get; set; }
    }

    
    public class GST
    {
        public double IGSTAmount { get; set; }
        public double SGSTAmount { get; set; }
        public double CGSTAmount { get; set; }
        public double CessAmount { get; set; }
        public double CessRate { get; set; }
        public double CGSTRate { get; set; }
        public double SGSTRate { get; set; }
        public double IGSTRate { get; set; }
        public double TaxableAmount { get; set; }
    }

    public class HotelRoomsDetailBlockRoom
    {
        public int RoomId { get; set; }
        public int ChildCount { get; set; }
        public bool RequireAllPaxDetails { get; set; }
        public int RoomStatus { get; set; }
        public string AvailabilityType { get; set; }
        public int RoomIndex { get; set; }
        public string RoomTypeCode { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomDescription { get; set; }
        public string RatePlanCode { get; set; }
        public string RatePlanName { get; set; }
        public int RatePlan { get; set; }
        public string InfoSource { get; set; }
        public string SequenceNo { get; set; }
        public bool IsPerStay { get; set; }
        public object SupplierPrice { get; set; }
        public BlockRoomPriceResponse Price { get; set; }
        public string RoomPromotion { get; set; }
        public List<string> Amenities { get; set; }
        public List<object> Amenity { get; set; }
        public string SmokingPreference { get; set; }
        public List<object> BedTypes { get; set; }
        public List<HotelSupplement> HotelSupplements { get; set; }
        public DateTime LastCancellationDate { get; set; }
        public List<CancellationPolicyBlockRoom> CancellationPolicies { get; set; }
        public DateTime LastVoucherDate { get; set; }
        public string CancellationPolicy { get; set; }
        public List<object> Inclusion { get; set; }
        public bool IsPassportMandatory { get; set; }
        public bool IsPANMandatory { get; set; }
    }

    public class HotelSupplement
    {
        public string SupplementId { get; set; }
        public string Name { get; set; }
        public int SupplementChargeType { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
    }

    public class BlockRoomPriceResponse
    {
        public string CurrencyCode { get; set; }
        public double RoomPrice { get; set; }
        public double Tax { get; set; }
        public double ExtraGuestCharge { get; set; }
        public double ChildCharge { get; set; }
        public double OtherCharges { get; set; }
        public double Discount { get; set; }
        public double PublishedPrice { get; set; }
        public double PublishedPriceRoundedOff { get; set; }
        public double OfferedPrice { get; set; }
        public double OfferedPriceRoundedOff { get; set; }
        public double AgentCommission { get; set; }
        public double AgentMarkUp { get; set; }
        public double ServiceTax { get; set; }
        public double TDS { get; set; }
        public double TCS { get; set; }
        public double ServiceCharge { get; set; }
        public double TotalGSTAmount { get; set; }
        public GST GST { get; set; }
    }

    public class BlockRoomResponse
    {
        public BlockRoomResult BlockRoomResult { get; set; }
        
    }
    public class BlockRoomResult
    {
        public string TraceId { get; set; }
        public int ResponseStatus { get; set; }
        public Error Error { get; set; }
        public bool IsPriceChanged { get; set; }
        public bool IsCancellationPolicyChanged { get; set; }
        public bool IsHotelPolicyChanged { get; set; }
        public bool IsPackageFare { get; set; }
        public bool IsPackageDetailsMandatory { get; set; }
        public bool IsDepartureDetailsMandatory { get; set; }
        public string AvailabilityType { get; set; }
        public bool GSTAllowed { get; set; }
        public string HotelNorms { get; set; }
        public string HotelName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int StarRating { get; set; }
        public string HotelPolicyDetail { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool BookingAllowedForRoamer { get; set; }
        public List<object> AncillaryServices { get; set; }
        public List<HotelRoomsDetailBlockRoom> HotelRoomsDetails { get; set; }
        public ValidationInfo ValidationInfo { get; set; }
    }

    public class ValidationAtConfirm
    {
        public bool IsPANMandatory { get; set; }
        public bool IsPassportMandatory { get; set; }
        public bool IsSamePANForAllAllowed { get; set; }
        public bool IsCrpPANMandatory { get; set; }
        public bool IsCrpPassportMandatory { get; set; }
        public bool IsCrpSamePANForAllAllowed { get; set; }
        public bool IsEmailMandatory { get; set; }
        public bool IsCorporateBookingAllowed { get; set; }
        public bool IsAgencyOwnPANAllowed { get; set; }
        public int NoOfPANRequired { get; set; }
    }

    public class ValidationAtVoucher
    {
        public bool IsPANMandatory { get; set; }
        public bool IsPassportMandatory { get; set; }
        public bool IsSamePANForAllAllowed { get; set; }
        public bool IsCrpPANMandatory { get; set; }
        public bool IsCrpPassportMandatory { get; set; }
        public bool IsCrpSamePANForAllAllowed { get; set; }
        public bool IsEmailMandatory { get; set; }
        public bool IsCorporateBookingAllowed { get; set; }
        public bool IsAgencyOwnPANAllowed { get; set; }
        public int NoOfPANRequired { get; set; }
    }

    public class ValidationInfo
    {
        public ValidationAtConfirm ValidationAtConfirm { get; set; }
        public ValidationAtVoucher ValidationAtVoucher { get; set; }
    }

}
