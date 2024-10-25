using System.Collections.Generic;

namespace MobileAPI_V2.Model.Ecommerce
{
    public class BusinessDashBaordResponse
    {
        public BusinessData Response { get; set; }
        public int? Status { get; set; }
        public string Message { get; set; }
    }
    public class BusinessData
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; }
        public string VPA { get; set; }
        public bool IsSuperUser { get; set; }
        public List<PersonalDetails> PersonalDetails { get; set; }
        public List<PersonalDetails> CardClubs { get; set; }
        public List<CUGSizeData> CUGSize { get; set; }
        public List<CardFee> CardFee { get; set; }
        public List<CardFee> DirectInsecntive { get; set; }
    }
    public class PersonalDetails
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string OrderId { get; set; }
       
    }
    public class CUGSizeData
    {
        public string LevelName { get; set; }
        public List<SizeData> Data { get; set; }

    }
    public class SizeData
    {
        public string LevelName { get; set; }
        public string Level { get; set; }
        public string CompleteLevel { get; set; }
        public string OrderId { get; set; }
    }
    public class CardFee
    {
        public string Target { get; set; }
        public string Remaining { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
    }
}
