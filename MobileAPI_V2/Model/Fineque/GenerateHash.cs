namespace MobileAPI_V2.Model.Fineque
{
    public class GenerateHash
    {
        public string partnerKey { get; set; }
        public string partnerSecret { get; set; }
    }
    public class GenerateHashResponse
    {
        public string hashValue { get; set; }
    }
}
