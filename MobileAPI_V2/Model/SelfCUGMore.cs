using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class SelfCUGMore
    {
        public decimal Monthly { get; set; }
        public decimal Yearly { get; set; }
        public decimal TotalEarnings { get; set; }
    }
    public class SelfCUGMoreResult
    {
        public SelfCUGMore SelfCUGMore { get; set; }
        public SelfMotiveClub SelfMotiveClub { get; set; }
        public SelfHighValueOfYearlyClub SelfHighValueOfYearlyClub { get; set; }
        public GoldMemberShip GoldMemberShip { get; set; }
        public ExtraEarnings ExtraEarnings { get; set; }
    }
    public class SelfMotiveClub
    {
        public decimal TotalTransValueOfMonth { get; set; }
        public decimal NumberOfPoints { get; set; }
        public decimal PointsValue { get; set; }
    }
    public class SelfHighValueOfYearlyClub
    {
        public decimal TargetValue { get; set; }
        public decimal TransVAlue { get; set; }
        public decimal CommissionValue { get; set; }
    }
    public class GoldMemberShip
    {
        public decimal Qualification { get; set; }
        public decimal Benifits { get; set; }
    }
    public class ExtraEarnings
    {
        public decimal ExtraQualification { get; set; }
        public decimal ExtraBenifits { get; set; }
    }
}
