using System.Collections.Generic;

namespace MobileAPI_V2.Model.Master
{
    public class AffiliateLinkDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteGoToLink { get; set; }
        public string Region { get; set; }
        public string IconName { get; set; }
        public string IconUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class AffiliateLinResponsekDTO
    {
        public string response { get; set; }
        public string message { get; set; }
        public List<AffiliateLinkDTO> result { get; set;}
}
}
