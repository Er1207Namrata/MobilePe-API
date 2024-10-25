using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Market: AffilicateCategory
    {
        public List<AffiliateModel> data { get; set; }
      

    }
    public class Marketv2
    {
        public List<Market> market { get; set; }
        public List<DashboradBanner> banner { get; set; }
    }
    public class MarketResponse
    {
        public Market affilicate { get; set; }
        public List<AffiliateOffer> offers { get; set; }
    }
    public class AffiliateOffer
    {
        public string link { get; set; }
        public string imgurl { get; set; }
        public string name { get; set; }
    }
}
