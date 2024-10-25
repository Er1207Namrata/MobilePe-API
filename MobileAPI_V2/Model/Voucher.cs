using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Voucher
    {
        public string EndDate { get; set; }
        public string Value { get; set; }
        public string VoucherGCcode { get; set; }
        public string VoucherGuid { get; set; }
        public string VoucherNo { get; set; }
        public string Voucherpin { get; set; }
    }
    public class vouchercart
    {
        public long MemberId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public long VoucherId { get; set; }
        public string paymentId { get; set; }
    }
    public class PullVoucher
    {
        public string ProductGuid { get; set; }
        public string ProductName { get; set; }
        public string VoucherName { get; set; }
        public List<Voucher> Vouchers { get; set; }
    }

    public class VPullVouchersResult
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ExternalOrderIdOut { get; set; }
        public string Message { get; set; }
        public List<PullVoucher> PullVouchers { get; set; }
        public string ResultType { get; set; }
    }

    public class VoucherResponse
    {
        public VPullVouchersResult vPullVouchersResult { get; set; }
        public int VoucherId { get; set; }
        public long MemberId { get; set; }
        public string paymentId { get; set; }
        public long Pk_CartId { get; set; }

        public decimal Discountamount { get; set; }
        public string response { get; set; }

    }

    public class VoucherRequest
    {
        public string ProductGuid { get; set; }
        public int Quantity { get; set; }
        public long MemberId { get; set; }
        public string paymentId { get; set; }
        public string valid { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string BRAND { get; set; }
        public string Url { get; set; }
        public decimal amount { get; set; }
        public decimal Discountamount { get; set; }
        public int VoucherId { get; set; }
        public long Pk_CartId { get; set; }


    }
    public class vouchersummary
    {
        public string VoucherName { get; set; }
        public long RedeemPoint { get; set; }
        public long UserPoint { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public long Pk_Cart { get; set; }
        public long VoucherId { get; set; }
        public string ProductGUID { get; set; }
        public string VName { get; set; }
    }
    public class VoucherDescription
    {
        public string PRODUCTSERVICENAME { get; set; }
        public string BRAND { get; set; }
        public long VoucherId { get; set; }
        public decimal VALUE { get; set; }
        public string PRODUCTCODE { get; set; }
        public string BrandDescription { get; set; }
        public string HowtoRedeem { get; set; }
        public string WheretoRdeeem { get; set; }
        public string ImportantInstructions { get; set; }
        public string TermsConditions { get; set; }
        public string Validity { get; set; }
        public string Type { get; set; }
        public string Quantity { get; set; }

        public long MemberId { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
