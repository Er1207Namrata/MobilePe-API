using System.Collections.Generic;

namespace MobileAPI_V2.Model.Travel
{
    public class FareRuleRequest
    {
        public string TraceId { get; set; }
        public string ResultIndex { get; set; }
        public string Commission { get; set; }
        public int FK_MemId { get; set; }
    }
    public class FareRuleResponse
    {
        public FareResponse Response { get; set; }
        //public string response { get; set; }
        //public string message { get; set; }
    }
    

    public class FareRuleRes
    {
        public string Airline { get; set; }
        public string Destination { get; set; }
        public string FareBasisCode { get; set; }
        public object FareRestriction { get; set; }
        public string FareRuleDetail { get; set; }
        public string Origin { get; set; }
    }

    public class FareResponse
    {
        public AuthenticateError Error { get; set; }
        public List<FareRuleRes> FareRules { get; set; }
        public int ResponseStatus { get; set; }
        public string TraceId { get; set; }
    }
}
