namespace MobileAPI_V2.Model.Affiliate
{
    public class AffiliateFinanceRegistrationDTO
    {
        public int Id { get; set; }
        public int AffiliateId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        public int CreatedBy { get; set; }
        
    }
    public class AffiliateFinanceRegistrationReportDTO
    {
        //public int Id { get; set; }
        //public int AffiliateId { get; set; }
        //public int UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        //public int CreatedBy { get; set; }
        //public int AffiliateProductId { get; set; }
        public string ProductName { get; set; }        
        //public string AffiliateName { get; set; }

    }
    public class AffiliateFinanceRegistrationFilterDTO
    {
        public int MemberId {  get; set; }
        public int AffiliateId { get; set; } = 0;

    }
}
