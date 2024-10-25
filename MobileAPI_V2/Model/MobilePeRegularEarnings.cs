using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class MobilePeRegularEarnings
    {
       
    }
    public class MobilePeRegularResult
    {
        public List<ResultCommon> summarydata { get; set; }
        public IEnumerable<MobilePeRegularEarningsDetails> Income { get; set; }
    }
    public class MobilePeCommission
    {
        public decimal CommissionAmount { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal TransferToBankOrCard { get; set; }

        public static implicit operator MobilePeCommission(Task<MobilePeCommission> v)
        {
            throw new NotImplementedException();
        }
    }
    public class MobilePeRegularEarningsDetails
    {
        public int Pk_IncomeTypeId { get; set; }
        public string IncomeType { get; set; }
        public string RedirectionUrl { get; set; }
        public decimal IncomeAmount { get; set; }
        public decimal IncomeValue { get; set; }
        public int Orders { get; set; }
        public string ApiName { get; set; }

    }
}
