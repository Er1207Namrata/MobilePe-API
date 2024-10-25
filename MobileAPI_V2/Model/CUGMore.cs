using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class CUGMore
    {
        public long FK_memId { get; set; }
        public int LevelCounts { get; set; }
        public string Level { get; set; }
        public decimal Amount { get; set; }
        public decimal Value { get; set; }
        public decimal TotalAmount { get; set; }
        public int Orders { get; set; }

    }
    public class MonthlyCUGAchieverClub
    {
        public decimal TotalTransValueOfMonth { get; set; }
        public decimal NumberOfPoints { get; set; }
        public decimal PointsValue { get; set; }
        public decimal TotalEarnings { get; set; }

    }
    public class CUGExtraEarningFromCard
    {
        public decimal Qualification { get; set; }
        public decimal Earning { get; set; }
        public decimal CardActivationMore { get; set; }
        public string Benifits { get; set; }
    }
    public class CUGMoreRequest
    {
        public long memberId { get; set; }
        public int monthId { get; set; }
        public int year { get; set; }
    }
    public class CUGMoreDetails
    {
        public long Fk_CustomerId { get; set; }
        public decimal monthly { get; set; }
        public decimal yearly { get; set; }
    }
    public class CUGMoreResponse
    {

        public IEnumerable<CUGMore> items { get; set; }
        public MonthlyCUGAchieverClub MonthlyCUGAchieverClub { get; set; }
        public CUGExtraEarningFromCard CUGExtraEarningFromCard { get; set; }
    }
}
