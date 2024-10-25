using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class FAQ
    {
        public long FAQID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Videolink { get; set; }
        public string link { get; set; }
    }

    public class FAQResponse
    {
        public long FAQID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Videolink { get; set; }
        public string link { get; set; }
        public List<SubFAQ> SUBFAQ { get; set; }
    }
    public class SubFAQ
    {
        public long FAQID { get; set; }
        public string Question { get; set; }
    }
    public class FAQType
    {
        public int Pk_FAQId { get; set; }
        public string Type { get; set; }
        public List<FAQQuestion> Question { get; set; }
    }
    public class FAQQuestion
    {
        public int Pk_FAQQuestionId { get; set; }
        public int Fk_TypeId { get; set; }
        public string Question { get; set; }
        public List<FAQAnswer> Answer { get; set; }
    }
    public class FAQAnswer
    {
        public int Pk_FAQAnswerId { get; set; }
        public int Fk_TypeId { get; set; }
        public int Fk_QuestionId { get; set; }
        public string Answer { get; set; }
        public string Link { get; set; }
    }
    public class SupportTypeList
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    public class SupportFAQList
    {
        public string Title { get; set; }
        public string Discription { get; set; }
        public string Link { get; set; }
        public int Pk_Id { get; set; }
        
    }
    public class SupportListResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public List<SupportFAQList> lstsupport { get; set; }
    }
    public class SupportFAQResponse
    {
        public string response { get; set; }
        public string msg { get; set; }
        public string flag { get; set; }

    }
}
