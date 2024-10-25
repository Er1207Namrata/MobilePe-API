using MobileAPI_V2.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace MobileAPI_V2.Models
{
    public class WalletTopup
    {
        public string fromEntityId { get; set; }
        public string toEntityId { get; set; }
        public string yapcode { get; set; }
        public string productId { get; set; }
        public string Type { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
        public string transactionType { get; set; }
        public string business { get; set; }
        public string businessEntityId { get; set; }
        public string transactionOrigin { get; set; }
        public string externalTransactionId { get; set; }
        public string PaymentId { get; set; }
        public string request { get; set; }
        public string PhotoPath { get; set; }
        public int OldCardId { get; set; }
        public int Fk_CardId { get; set; }
        public int IsHold { get; set; }
        public long Fk_MemId { get; set; }
        
        public long TxId { get;  set; }
        public string EntityId { get;  set; }

        public DataSet SaveWalletTopup()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@EntityId",toEntityId),
                new SqlParameter("@Narration",description),
                new SqlParameter("@Amount",amount),
                new SqlParameter("@TransactionNo",externalTransactionId),
                new SqlParameter("@TxId",TxId),
                new SqlParameter("@PaymentId",PaymentId),
                new SqlParameter("@IsHold",IsHold)
              

            };
            
            DataSet ds = Connection.ExecuteQuery(ProcedureName.WalletTopup, para);
            return ds;
        }
        public DataSet WalletDebit()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@EntityId",fromEntityId),
                new SqlParameter("@Narration",description),
                new SqlParameter("@Amount",amount),
                new SqlParameter("@TransactionNo",externalTransactionId),
                new SqlParameter("@TxId",TxId),
                new SqlParameter("@Type",Type),
                new SqlParameter("@Fk_CardId",Fk_CardId),
                new SqlParameter("@request",request),
                new SqlParameter("@PhotoPath",PhotoPath)


            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.WalletDebit, para);
            return ds;
        }
        public DataSet GetCustomerDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@EntityId",fromEntityId),
              


            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCustomerDetails, para);
            return ds;
        }
        public DataSet UpdateMemberCard(string paymentId, string OrderId, int OpCode, string KitNo, int ToPayCardId,string Type)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PaymentId",PaymentId),
                new SqlParameter("@OrderId",OrderId),
                new SqlParameter("@OpCode",OpCode),
                new SqlParameter("@KitNo",KitNo),
                new SqlParameter("@ToPayCardId",ToPayCardId),
                new SqlParameter("@Type",Type)
               

            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.MemberCard, para);
            return ds;
        }
        public DataSet Getuser2user_tranHistory()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@EntityId",EntityId)
              
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.wallet_to_walletTran_Hist, para);
            return ds;
        }

    }
   
    public class WalletTopupResponse
    {
        public ExceptionData exception { get; set; }
        public ResultWalletTopup result { get; set; }
    }
    public class ResultWalletTopup
    {
        public long txId { get; set; }
    }
    public class WalletTransactionResponse
    {
        public List<WalletTransactionResult> result { get; set; }
        public ExceptionData exception { get; set; }
       // public Pagination? pagination { get; set; }
    }

    public class WalletTransactionResult
    {
        public Transaction transaction { get; set; }
        public string balance { get; set; }
    }
    public class Transaction
    {
        public string amount { get; set; }
        public double balance { get; set; }
        public string transactionType { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public string txRef { get; set; }
        public string businessId { get; set; }
        public string beneficiaryName { get; set; }
        public string beneficiaryType { get; set; }
        public  string beneficiaryId { get; set; }
        public  string description { get; set; }
        public  string otherPartyName { get; set; }
        public  string otherPartyId { get; set; }
        public  string txnOrigin { get; set; }
        public  string transactionStatus { get; set; }
        public  string status { get; set; }
        public  string yourWallet { get; set; }
        public  string beneficiaryWallet { get; set; }
        public  string externalTransactionId { get; set; }
        public  string retrivalReferenceNo { get; set; }
        public  string authCode { get; set; }
        public  string billRefNo { get; set; }
        public  string bankTid { get; set; }
        public  string acquirerId { get; set; }
        public  string mcc { get; set; }
        public  string convertedAmount { get; set; }
        public  string networkType { get; set; }
        public  string limitCurrencyCode { get; set; }
        public  string kitNo { get; set; }
        public  string sorTxnId { get; set; }
        public  string transactionCurrencyCode { get; set; }
        public  string fxConvDetails { get; set; }
        public  string convDetails { get; set; }
        public  string disputedDto { get; set; }
        public  string disputeRef { get; set; }
        public string accountNo { get; set; }
    }

    public class WalletBalResponse
    {
        public List<ResultWalletBal> result { get; set; }
        public ExceptionData exception { get; set; }
        public object pagination { get; set; }
    }
    public class ResultWalletBal
    {
        public string entityId { get; set; }
        public string productId { get; set; }
        public string balance { get; set; }
        public string lienBalance { get; set; }
    }

    public class wallettowallethistlist
    {
        public string TransactionDate { get; set; }
        public string Narration { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
        
        public string ToName { get; set; }
        public string FromName { get; set; }
        public string TransactionNo { get; set; }
        
    }
    public class wallettowallethist_res
    {
        public string message { get; set; }
        public string status { get; set; }
        public wallettowalletDataRes ListData { get; set; }

    }
    public class wallettowalletDataRes
    {
        public List<wallettowallethistlist> Data { get; set; }
    }

}
