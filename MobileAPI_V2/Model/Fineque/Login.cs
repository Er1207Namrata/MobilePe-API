using System.Collections.Generic;

namespace MobileAPI_V2.Model.Fineque
{
    public class FinequeLogin
    {
        public string partnerKey { get; set; }
        public string merchantId { get; set; }
    }
    public class FinequeLoginRes: LoginErrorRes
    {
        public string token { get; set; }
        public string userName { get; set; }
        public string createdAt { get; set; }
        public string partnerId { get; set; }
        public string result { get; set; }
    }
    public class LoginHeader
    {
        public string RequestHash { get; set; }
    }
    public class LoginToken
    {
        public string Token { get; set; }
        public string Authorization { get; set; }
    }
    public class FinequeReqRes
    {
        public string flag { get; set; }
    }
    public class FinequeReq
    {
        public string Request { get; set; }
        public string Response { get; set; }
        public string Type { get; set; }
        public string Fk_MemId { get; set; }
    }

    public class LoginErrorRes
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        
        public string traceId { get; set; }
        public List<FinequeLogin> error { get; set; }
    }
}
