using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Direct
    {
        public string LevelName { get; set; }
        public string InviteCode { get; set; }
        public long MemberId { get; set; }
        public long TotalTeamCount { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string LoginId { get; set; }
        public string JoiningDate { get; set; }
    }
    public class DirectResponse 
    {
        public List<Direct> result { get; set; }
    }
}
