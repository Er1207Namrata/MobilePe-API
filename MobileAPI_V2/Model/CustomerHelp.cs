using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{

    public class AssistantDetails
    {
        public string url { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }
    public class CustomerDetail
    {

        public string Header { get; set; }
        public string Remarks { get; set; }
        public long RequestTypeId { get; set; }
        public bool IsTicketClosed { get; set; }
        public List<CommonOrderdata> data { get; set; }
    }


    public class CustomerHelpResponse
    {
        public AssistantDetails assistantDetails { get; set; }
        public CustomerDetail TopData { get; set; }
        public List<CustomerHelpReplyResponse> replydata { get; set; }
    }
    public class CustomerHelpReplyResponse
    {
        public string R_data { get; set; }
        public string L_data { get; set; }
    }
    public class HelpRequest
    {
        public string transId { get; set; }
        public int procId { get; set; }
        public string type { get; set; }
        public long imageId { get; set; }
        public long requestTypeId { get; set; }
        public long subsupportId { get; set; }
        public string message { get; set; }
        public long memberId { get; set; }
    }
    public class ViewTicketDetailMessages
    {
        public string Message { get; set; }
        public string IsShow { get; set; }
    }

    public class ViewTicket
    {
        public string status { get; set; }
        public string remark { get; set; }
        public string ticketnumber { get; set; }
        public string date { get; set; }
        public string transId { get; set; }

    }
    public class ViewTicketMessages
    {
        public ViewTicket topheader { get; set; }
        public List<ViewTicketDetailMessages> replydata { get; set; }
    }
    public class CustomerInfoDTO
    {
        public string Email { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
    }
}
