namespace MobileAPI_V2.Model.Fineque
{
    public class LoanStatus
    {
        public string applicationId { get; set; }
        public string Fk_MemId { get; set; }
        public string transactionHashCode { get; set; }
    }
    public class LoanStatusResponse:FinqueError
    {
        public string transactionId { get; set; }
        public string applicationId { get; set; }
        public string status { get; set; }
        public string loanAmount { get; set; }
        public string loanSanctionDate { get; set; }
        public string declineReason { get; set; }
        public string remarks { get; set; }
        public string documentLink { get; set; }
        public string bankSelectionLink { get; set; }
        public string QdeUrl { get; set; }
    }
}
