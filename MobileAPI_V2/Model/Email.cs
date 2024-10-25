using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Email
    {
        public string HostURL { get; set; }
        public int PortNumber { get; set; }
        public bool EnableSsl { get; set; }
        public string DeliveryMethod { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }

    public class EmailLog
    {
        public string FromMailId { get; set; }
        public string ToMailId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
