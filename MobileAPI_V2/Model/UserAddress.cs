using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class UserAddress
    {
        public long Pk_AddressId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Pincode { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string Landmark { get; set; }
        public string Alternate { get; set; }
        public long UserId { get; set; }
        public string createdDate { get; set; }
        public string UpdatedDate { get; set; }
        public string STATUS { get; set; }
        public string AddressType { get; set; }
    }
    public class UserAddressresponse
    {
        public List<UserAddress> address { get; set; }
        public List<UserOrderSummary> purchasedetails { get; set; }

        public cartsummary summary { get; set; }
    }
    public class UserOrderSummary
    {
        public long Pk_CartItemId { get; set; }
        public decimal ProductAmt { get; set; }
        public decimal IGST { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public int productQty { get; set; }
        public int RedeemPoint { get; set; }
        public int UserRedeemPoint { get; set; }
        public string ProductName { get; set; }
        public string imgUrl { get; set; }
        public decimal shippingcharge { get; set; }
    }
}
