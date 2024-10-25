namespace MobileAPI_V2.Model.Thriwe
{
    public class AddCustomers
    {
        public string UserId { get; set; }        
        public string mobileNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string countryCode { get; set; }
        public string referenceId { get; set; }
       
    }
    public class CustomerResponse
    {
        public string createdAt { get; set; }
        public string objectId { get; set; }
        public string message { get; set; }
        
    }
    public class BenefitReq
    {
        public string referenceId { get; set; }
    }
    public class BenefitResponse
    {
        public string createdAt { get; set; }
        public string expiresAt { get; set; }
        public string objectId { get; set; }
        public string message { get; set; }
    }
}
