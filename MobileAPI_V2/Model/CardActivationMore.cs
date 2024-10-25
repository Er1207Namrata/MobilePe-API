using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class CardActivationMore
    {
        public List<CardActivationEarning> CardActivationEarning { get; set; }
    }
    public class CardActivationEarningRequest
    {
        public long memberId { get; set; }
        public int monthId { get; set; }
        public int year { get; set; }
    }
    public class CardActivationEarning
    {
        public long FK_memId { get; set; }
        public int LevelCounts { get; set; }
        public string Level { get; set; }
        public decimal Amount { get; set; }
        public decimal Value { get; set; }
        public int Orders { get; set; }
    }

}
