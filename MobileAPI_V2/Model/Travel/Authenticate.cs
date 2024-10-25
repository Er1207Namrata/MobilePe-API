namespace MobileAPI_V2.Model.Travel
{
    public class Authenticate
    {
        public int Status { get; set; }
        public string TokenId { get; set; }
        public AuthenticateError Error { get; set; }
        public Member Member { get; set; }
    }
    public class AuthenticateError
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Member
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
