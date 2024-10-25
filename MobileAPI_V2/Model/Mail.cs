using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Mail
    {
    }

    public class To
    {
        public string email { get; set; }
        public string name { get; set; }
        public string type { get; set; }

    }
    public class Message
    {
        public IList<To> to { get; set; }
        public List<Attachment> attachments { get; set; }
        public string html { get; set; }
        public string subject { get; set; }
        public string from_email { get; set; }
        public string from_name { get; set; }

    }
    public class Application
    {
        public Message message { get; set; }
        public string owner_id { get; set; }
        public string token { get; set; }
        public string smtp_user_name { get; set; }

    }

    public class ApplicationResponse
    {
        public string status { get; set; }
        public string message { get; set; }

    }
    public class Attachment
    {
        public string type { get; set; }
        public string name { get; set; }
        public string content { get; set; }

    }

    public class ICSMailService
    {
        public string username { get; set; }
        public string password { get; set; }
        public string emailTo { get; set; }
        public string emailcontent { get; set; }
        public string subject { get; set; }
        public string fromAddress { get; set; }
        public string attachment { get; set; }
    }
}
