using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel.Hotel
{
    public class BookRoom
    {
        public string ResultIndex { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string GuestNationality { get; set; }
        public string NoOfRooms { get; set; }
        public string ClientReferenceNo { get; set; }
        public string IsVoucherBooking { get; set; }
        public string TraceId { get; set; }
        public int FK_MemId { get; set; }
        public List<BookHotelRoomsDetail> HotelRoomsDetails { get; set; }
    
    }
    public class BookHotelRoomsDetail
    {
        public string RoomIndex { get; set; }
        public string RoomTypeCode { get; set; }
        public string RoomTypeName { get; set; }
        public string RatePlanCode { get; set; }
        public object BedTypeCode { get; set; }
        public int SmokingPreference { get; set; }
        public object Supplements { get; set; }
        public BookPrice Price { get; set; }
        public List<HotelPassenger> HotelPassenger { get; set; }
    }
    public class BookPrice
    {
        public string CurrencyCode { get; set; }
        public string RoomPrice { get; set; }
        public string Tax { get; set; }
        public string ExtraGuestCharge { get; set; }
        public string ChildCharge { get; set; }
        public string OtherCharges { get; set; }
        public string Discount { get; set; }
        public string PublishedPrice { get; set; }
        public string PublishedPriceRoundedOff { get; set; }
        public string OfferedPrice { get; set; }
        public string OfferedPriceRoundedOff { get; set; }
        public string AgentCommission { get; set; }
        public string AgentMarkUp { get; set; }
        public string TDS { get; set; }
        public string ServiceTax { get; set; }
    }
    public class HotelPassenger
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public object Middlename { get; set; }
        public string LastName { get; set; }
        public object Phoneno { get; set; }
        public object Email { get; set; }
        public int PaxType { get; set; }
        public bool LeadPassenger { get; set; }
        public int Age { get; set; }
        public object PassportNo { get; set; }
        public string PassportIssueDate { get; set; }
        public string PassportExpDate { get; set; }
        public string PAN { get; set; }
    }
    public class BookRoomResponse
    {

    }
}
