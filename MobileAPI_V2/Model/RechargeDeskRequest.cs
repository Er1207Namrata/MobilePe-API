using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class RechargeDesk
    {
        public string error_code { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string ref_no { get; set; }
        public decimal after_balance { get; set; }
        public string number { get; set; }
        public decimal amount { get; set; }
        public string optr_txn_id { get; set; }


    }
    public class RechargeDeskRequest : RechargeDesk
    {
        public int ProcId { get; set; }
        public string TransactionId { get; set; }
        public string orderId { get; set; }
        public string transdate { get; set; }
       
        public string OperatorCode { get; set; }
        public string Type { get; set; }
        public long MemberId { get; set; }
        public long Id { get; set; }
        public string merchantInfoTxn { get; set; }
        public string response { get; set; }
    }
    public class VenusRecharge
    {
        public string ResponseStatus { get; set; }

        public string Description { get; set; }

        public string MerTxnID { get; set; }

        public string Mobile { get; set; }
        public string Amount { get; set; }

        public string OperatorTxnID { get; set; }
        public string OrderNo { get; set; }
    }

    public class VenusRechargeResponse
    {
        public VenusRecharge Response { get; set; }
    }
    public class VenusBillInfo
    {
        public string ResponseStatus { get; set; }

        public string Description { get; set; }

        public string MerTxnID { get; set; }

        public string ConsumerID { get; set; }
        public double DueAmount { get; set; }
       // public decimal? DueAmount { get; set; }
        public string OrderId { get; set; }
        public string ConsumerName { get; set; }
        public string DueDate { get; set; }
    }

    public class VenusBillInfoResponse 
    {

        public VenusBillInfo Response { get; set; }
    }
    public class VenusBillInfoRequest
    {
        public string OpertorCode { get; set; }
        public string TransactionNo { get; set; }
        public string CustomerId { get; set; }
        public string MobileNo { get; set; }
    }

    public class VenusBillPayRequest
    {
        public string OpertorCode { get; set; }
        public string TransactionNo { get; set; }
        public string CustomerId { get; set; }
        public string MobileNo { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string merchantInfoTxn { get; set; }
        public int ProcId { get; set; }
        public long Id { get; set; }
        public long MemberId { get; set; }
        public string Type { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string optr_txn_id { get; set; }
        public string response { get; set; }
    }

    public class VenusBillPay
    {
        public string ResponseStatus { get; set; }
        public string Description { get; set; }
        public string ConsumerID { get; set; }
        public string OrderId { get; set; }
        public string ConsumerName { get; set; }
        public decimal DueAmount { get; set; }
        public string DueDate { get; set; }
        public string BillDate { get; set; }
        public string OperatorTxnId { get; set; }
    }
    public class VenusBillPayResponse
    {
        public VenusBillPay response { get; set; }

    }

    public class ElectricityBillData
    {
        public double bill_amount { get; set; }
        public List<ElectricityBillFetch> key_value { get; set; }
        public string success { get; set; }
        public string msg { get; set; }
    }

    public class ElectricityBillFetch
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class ElectricityBillDataResponse
    {
        public ElectricityBillData data { get; set; }
    }

}
