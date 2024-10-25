using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Downline
    {
        public long MemberId { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string Mobile { get; set; }
        public string JoiningDate { get; set; }
    }
    public class DownlineResponse 
    {
        public List<Downline> result { get; set; }
    }

}
