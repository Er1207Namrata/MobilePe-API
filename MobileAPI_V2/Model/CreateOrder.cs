using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class CreateOrder
    {
        public int ProcId { get; set; }
        public long CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public int CouponId { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingCharges { get; set; }
        public bool IsCouponApplied { get; set; }
        public decimal CouponAmount { get; set; }
        public string PaymentId { get; set; }
        public string cartitem { get; set; }
        public string paymentstatus { get; set; }
        public bool status { get; set; }
        public long Fk_AddressId { get; set; }
        public long memberId { get; set; }
        public decimal PaidAmount { get; set; }

        public List<cartitem> cartdata { get; set; }

    }
    public class OrderResponse
    {
        public string message { get; set; }
        public string response { get; set; }
        public long Id { get; set; }
        public decimal Payout { get; set; }
    }
    public class cartitem
    {
        public string cartId { get; set; }
    }
}
