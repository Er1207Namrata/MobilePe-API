using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class CartRespone
    {
        public List<CartList> cartlist { get; set; }
        public cartsummary cartsummary { get; set; }
    }
    public class cartsummary
    {
        public decimal totalAmount { get; set; }
        public decimal taxAmount { get; set; }
        public decimal discountAmount { get; set; }
        public decimal payableAmount { get; set; }
        public decimal shippingAmount { get; set; }
    }
    public class CartRequest
    {
        public long ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public long ProductDetailId { get; set; }
        public int ProductQty { get; set; }
        public decimal ProductAmt { get; set; }
        public long CreatedBy { get; set; }
        public string RequestType { get; set; }
    }
    public class CartList
    {

        public int ProductId { get; set; }
        public long Pk_CartItemId { get; set; }
        public int Fk_ProductDetailId { get; set; }
        public int ProductQty { get; set; }
        public decimal ProductAmt { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int UnitID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public int BrandID { get; set; }
        public decimal CourierPrice { get; set; }
        public decimal MRP { get; set; }
        public decimal BV { get; set; }
        public decimal width { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public string colorName { get; set; }
        public string DistributionPrice { get; set; }
        public string OfferPrice { get; set; }
        public decimal weight { get; set; }
        public string hsncode { get; set; }
        public Boolean isoffer { get; set; }
        public Boolean isnew { get; set; }
        public Boolean isshippingfree { get; set; }
        public decimal igst { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public string ImageUrl { get; set; }
        public int maxquantity { get; set; }
    }
    public class CartCount
    {
        public int count { get; set; }
    }
}
