using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class AffiliateModel
    {

        public string Name { get; set; }
        public string SiteGoToLink { get; set; }
        public string Region { get; set; }
        public string ImgUrl { get; set; }
        public string Commission { get; set; }
        public int CategoryId { get; set; }
    }
    public class AffilicateCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string url { get; set; }
    }
}
