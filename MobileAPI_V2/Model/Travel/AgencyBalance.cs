namespace MobileAPI_V2.Model.Travel
{
    public class AgencyBalance
    {
        public string TokenId { get; set; }
    }
    public class AgencyBalanceResponse
    {
        public int AgencyType { get; set; }
        public double CashBalance { get; set; }
        public string CashBalanceInPrefCurrency { get; set; }
        public string CreditBalance { get; set; }
        public string CreditBalanceInPrefCurrency { get; set; }
        public bool DomHotelConfirmBookingLimitRequired { get; set; }
        public int DomHotelHoldBalanceLeft { get; set; }
        public string DomHotelHoldBalanceLeftInPrefCurrency { get; set; }
        public ErrorAgencyBalance Error { get; set; }
        public bool IntlHotelConfirmBookingLimitRequired { get; set; }
        public int IntlHotelHoldBalanceLeft { get; set; }
        public string IntlHotelHoldBalanceLeftInPrefCurrency { get; set; }
        public bool IsNonAirOverrideCreditLimit { get; set; }
        public bool IsOverrideCreditLimit { get; set; }
        public string PreferredCurrency { get; set; }
        public int Status { get; set; }
    }
    public class ErrorAgencyBalance
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
