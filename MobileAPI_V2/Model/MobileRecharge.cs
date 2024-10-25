using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class MobileRechargePlan
    {
        public string recharge_type { get; set; }
        public List<RechargePlan> data { get; set; }
    }
    public class mobileoperator
    {
        public bool success { get; set; }
        public string message { get; set; }
        public operatordetails data { get; set; }
    }
    public class operatordetails
    {
        [JsonProperty("operator")]
        public string Operator { get; set; }
        public string circle { get; set; }
        public string imageurl { get; set; }
        public string OperatorCode { get; set; }
    }
    public class RechargePlan
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
    public class RechargePlanData
    {
        public List<RechargePlan> data { get; set; }
    }
}
