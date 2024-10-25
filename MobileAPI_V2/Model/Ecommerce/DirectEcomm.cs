using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class DirectEcomm
    {
        public string levelName { get; set; }
        public string inviteCode { get; set; }
        public long memberId { get; set; }
        public long totalTeamCount { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string loginId { get; set; }
        public string joiningDate { get; set; }
    }
    public class DirectResponseEcomm 
    {
        public List<Direct> result { get; set; }
    }
}
