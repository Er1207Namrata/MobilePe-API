using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class RedeamRewardPoints
    {
        public long memberId { get; set; }
        public int numberOfPoints { get; set; }
        public string remark { get; set; }
        public string type { get; set; }
    }

    public class UpdateKycStatus
    {
        public long memberId { get; set; }
        public int status { get; set; }
    }
}
