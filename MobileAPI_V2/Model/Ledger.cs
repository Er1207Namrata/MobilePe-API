using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Ledger
    {
    }
    public class LedgerResponse
    {
        public LedgerResult result { get; set; }
    }
    public class LedgerResult
    {
        public LedgerBalance balance { get; set; }
        public List<LedgerDetails> balancedetails { get; set; }
    }


    public class LedgerResultV2
    {
        public LedgerBalance balance { get; set; }
        public List<LedgerDetails> balancedetails { get; set; }
        public List<CommonOrderdata> transtype { get; set; }
        public List<CommonOrderdata> categorytype { get; set; }
    }


    public class LedgerBalance
    {
        public decimal TotalAmount { get; set; }
        public decimal TransferedAmount { get; set; }
        public decimal AvailableAmount { get; set; }
    }
    public class LedgerDetails
    {
        public string CreditAmount { get; set; }
        public string DebitAmount { get; set; }
        public decimal credit { get; set; }
        public decimal debit { get; set; }
        public string Remark { get; set; }
        public string date { get; set; }

    }
    public class CardLedgerDetails
    {
        public string creditAmount { get; set; }
        public string debitAmount { get; set; }
        public decimal credit { get; set; }
        public decimal debit { get; set; }
        public string remark { get; set; }
        public string date { get; set; }
        public string transactionId { get; set; }
        public string link { get; set; }

    }
    public class cardamountdetails
    {
        public string CardName { get; set; }
        public string type { get; set; }
        public decimal CardLimit { get; set; }
        public bool STATUS { get; set; }
        public decimal addedamount { get; set; }
        public decimal remainingamount { get; set; }
    }

    public class CommissionTransferdetail
    {
        public decimal credit { get; set; }
        public decimal debit { get; set; }
        public string creditAmount { get; set; }
        public string debitAmount { get; set; }
        public long memberId { get; set; }
        public string TransanctionId { get; set; }
        public string PaymentType { get; set; }
        public string remarks { get; set; }
        public string status { get; set; }
        public string amount { get; set; }
        public string transDate { get; set; }
    }
    
    public class Commissionresponse
    {
        public string message { get; set; }
        public string response { get; set; }
        public long commissionId { get; set; }
        public long tdsId { get; set; }
        public long transferchargeId { get; set; }
        public long transferCommissionId { get; set; }

    }
    public class CommissionDetails
    {
        public decimal payamount { get; set; }
        public decimal transfercharge { get; set; }
        public decimal tdscharge { get; set; }
        public decimal commissionamount { get; set; }
        public decimal amount { get; set; }
        public bool IsPan { get; set; }
        public string message { get; set; }
        
    }
    public class CommissionDetailsResponse
    {
        public CommissionDetails data { get; set; }
        public Bankdetails_accountResponse bankdetails { get; set; }

    }
    public class CommissionDetailsResponseEcomm
    {
        public CommissionDetails data { get; set; }
        public Bankdetails_accountResponseEcomm bankdetails { get; set; }

    }

    public class ContactRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string type { get; set; }
        public string reference_id { get; set; }
    }
    public class VPADetail
    {
        public string Mobile { get; set; }
        public long FK_MemId { get; set; }
    }
}
