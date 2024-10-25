using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class EarningBankTrf
    {
        public long FK_memId { get; set; }
        public int LevelCounts { get; set; }
        public string Level { get; set; }
        public decimal Amount { get; set; }
        public decimal Value { get; set; }
        public int Orders { get; set; }
    }
    public class EarningBankTrfResponse 
    {
        public List<EarningBankTrf> result { get; set; }
    }
}
