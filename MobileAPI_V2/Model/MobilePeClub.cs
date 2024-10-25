using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class MobilePeClub
    {
        public List<MobilePeClubResult> result { get; set; }
    }

    public class MobilePeClubResult
    {
        public string DOJ { get; set; }
        public string ClubName { get; set; }
        public decimal Target { get; set; }
        public int Days { get; set; }
        public decimal Benifit { get; set; }
        public decimal Achievement { get; set; }
        public string Status { get; set; }
    }

}
