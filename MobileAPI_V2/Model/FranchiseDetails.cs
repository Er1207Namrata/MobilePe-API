using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class FranchiseDetails
    {
        public string franchiseName { get; set; }
        public int numberOfCard { get; set; }
        public int FranchiseId { get; set; }
        public decimal perCardCharge { get; set; }
    }
    public class FranchiseDetailsResponse 
    {
        public List<FranchiseDetails> result { get; set; }
    }
    public class SubFranchiseCardRequest
    {
        public int numberofCard { get; set; }
        public decimal securityAmount { get; set; }
        public int fk_PaymentId { get; set; }
        public string transactionId { get; set; }
        public long createdBy { get; set; }
        public string paymentdate { get; set; }
        public string requestType { get; set; }
        public int fk_FranchiseId { get; set; }
    }
}
