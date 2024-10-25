using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class MobileRechargePlanEcomm
    {
        public string recharge_type { get; set; }
        public List<RechargePlanEcomm> data { get; set; }
    }
    public class mobileoperatorEcomm
    {
        public bool success { get; set; }
        public string message { get; set; }
        public operatordetailsEcomm data { get; set; }
    }
    public class operatordetailsEcomm
    {

        public string Operator { get; set; }
        public string circle { get; set; }
        public string imageUrl { get; set; }
        public string operatorCode { get; set; }
    }
    public class RechargePlanEcomm
    {
        public string id { get; set; }
        public string operator_id { get; set; }
        public string circle_id { get; set; }
        public string recharge_amount { get; set; }
        public string recharge_talktime { get; set; }
        public string recharge_validity { get; set; }
        public string recharge_short_desc { get; set; }
        public string recharge_long_desc { get; set; }
        public string recharge_type { get; set; }
        public string updated_at { get; set; }
    }
    public class RechargePlanDataEcomm
    {
        public List<RechargePlanEcomm> data { get; set; }
    }
}
