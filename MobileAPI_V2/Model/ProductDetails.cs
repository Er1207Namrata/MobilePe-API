using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class ProductDetails
    {
        public string productName { get;set; }
        public string productDescription { get; set; }
        public string mainImageUrl { get; set; }
        public decimal offerPrice { get; set; }
        public decimal mrp { get; set; }
        public int quantity { get; set; }
        public int ratingCount { get; set; }
        public long productId { get; set; }
        public long productDetailsId { get; set; }
        public List<ImageList> images { get; set; }
        public List<ProductReviews> productReviews{ get; set; }
        public List<sizedetails> sizedetails { get; set; }
        public List<Colordetails> Colordetails { get; set; }
        public List<avialableoffer> avialableoffer { get; set; }

    }

    public class avialableoffer
    {
        public string offer { get; set; }
    }
    public class sizedetails
    {
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public long Pk_ProductDetailId { get; set; }

    }
    public class Colordetails
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public long Pk_ProductDetailId { get; set; }

    }
    public class ImageList
    {
        public int sn { get; set; }
        public string ImageUrl { get; set; }
    }
    public class ProductReviews
    {
        public int sn { get; set; }
        public string userName { get; set; }
        public string userImage { get; set; }
        public long user_id{ get; set; }
        public string rating { get; set; }
        public string reviewMessage { get; set; }
    }
    public class ReviewedUser
    {
        public string Name { get; set; }
        public string ProfilePic { get; set; }
    }
    public class ProductDetailsResponse
    {
        public ProductDetails productdetails { get; set; }       
    }
}
