using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Support
    {
    }
    public class SupportRequest
    {
        public long Fk_SupportImageId { get; set; }
        public int Fk_SupportTypeId { get; set; }
        public string IssueMessage { get; set; }
        public string RepliedBy { get; set; }
        public long CreatedBy { get; set; }
    }
    public class SupportRequestdata
    {
        public string name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public string TicketNumber { get; set; }
        public string ImageUrl { get; set; }
        public string RequestDate { get; set; }
        public string Type { get; set; }

        public long sn { get; set; }
        public long totaluser { get; set; }

    }
    public class SupportRequestdataresponse 
    {
        public List<SupportRequestdata> result { get; set; }
    }
    public class SupportResponse :Common
    {
        public string imageurl { get; set; }
        public long SupportId { get; set; }
    }
    public class SupportMessageDetails
    {
        public string Type { get; set; }
        public string MESSAGE { get; set; }
        public string RepliedBy { get; set; }
        public string messagedate { get; set; }
        public long Pk_TicketId { get; set; }
        public bool Status { get; set; }
    }
    public class SupportMessageDetailsresponse 
    {
        public List<SupportMessageDetails> result { get; set; }
    }
    public class ReplyMessage
    {
        public long Fk_SupportImageId { get; set; }
        public long Pk_TicketId { get; set; }
        public string RepliedMessage { get; set; }
        public string RepliedBy { get; set; }
        public long CreatedBy { get; set; }
    }
}
