namespace MobileAPI_V2.Model.Fineque
{
    public class AppId
    {
       public string transactionHashCode { get; set; }
       public string applicationId { get; set; }
       public string Fk_MemId { get; set; }
    }
    public class AppIdResponse: FinqueError
    {
        public string applicationId { get; set; }
        public string fullName { get; set; }
        public string loanAmount { get; set; }
        public string loanAppliedFor { get; set; }
        public string dateOfApplication { get; set; }
        public string mobile { get; set; }
    }
}
