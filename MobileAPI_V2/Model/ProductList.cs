using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class ProductList
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public bool IsReturnAllowed { get; set; }
        public bool IsExchangeAllowed { get; set; }
        public string ImgUrl { get; set; }
        public string ProductCode { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public decimal MRP { get; set; }
        public decimal OfferPrice { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public long totalitem { get; set; }
        public long PK_ProductId { get; set; }
        public long Pk_ProductDetailId { get; set; }

    }
    public class ProductData
    {
        public List<ProductList> products { get; set; }
        public List<sort> sort { get; set; }
        public List<filter> filter { get; set; }

    }
    public class ProductListResponse 
    {
        public List<ProductList> result { get; set; }
    }
}
