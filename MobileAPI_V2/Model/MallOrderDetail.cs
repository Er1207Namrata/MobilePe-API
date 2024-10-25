using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class MallOrderDetail
    {
        public customerOrder customerOrder { get; set; }
        public List<customerOrderItem> customerOrderItem { get; set; }

        public customerordersummary customerordersummary { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public Helpdetail helpdetails { get; set; }
    }
    public class PaymentMethod
    {
        public string imgUrl { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentId { get; set; }
    }
    public class customerOrder
    {
        public string orderId { get; set; }
        public string orderDate { get; set; }
        public long memberId { get; set; }
        public decimal discount { get; set; }
    }
    public class customerOrderItem
    {
        public string productName { get; set; }
        public string imgUrl { get; set; }
        public string sizeName { get; set; }
        public string colorName { get; set; }
        public decimal offerPrice { get; set; }
        public int qty { get; set; }
        public long pk_ProductDetailId { get; set; }
        public long Pk_OrderItemId { get; set; }
        public long vendorId { get; set; }
        public long pK_ProductId { get; set; }
        public bool IsReplaced { get; set; }

        public List<productstatus> status { get; set; }
    }
    public class productstatus
    {
        public string text { get; set; }
        public string Date { get; set; }
        public string messagestring { get; set; }
        public bool status { get; set; }
    }
    public class customerordersummary
    {
        public decimal item { get; set; }
        public decimal tax { get; set; }
        public decimal postagepackaging { get; set; }
        public decimal discount { get; set; }
        public decimal total { get; set; }

    }
    public class mallOrderStatus
    {
        public string Status { get; set; }
        public string StatusDate { get; set; }
    }
    public class CancelOrderResponse:Common
    {
        public int RedeemPoint { get; set; }
    }
}
