namespace MobileAPI_V2.Model.Travel.Bus
{
    public class Authentication
    {
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EndUserIp { get; set; }
    }
    public class AuthResponse
    {
        public int Status { get; set; }
        public string TokenId { get; set; }
        public BusError Error { get; set; }
        public Authmember Member { get; set; }
    }

    public class BusError
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class Authmember
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int MemberId { get; set; }
        public int AgencyId { get; set; }
        public string LoginName { get; set; }
        public string LoginDetails { get; set; }
        public bool isPrimaryAgent { get; set; }
    }
}
