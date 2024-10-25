using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class Replacement
    {
        public ReplacementItem ReplacementItem { get; set; }
        public List<ReplacementReason> ReplacementReason { get; set; }
    }
    public class ReplacementItem
    {
        public string ProductName { get; set; }
        public decimal OfferPrice { get; set; }
        public int productQty { get; set; }
        public string ImgUrl { get; set; }
    }
    public class ReplacementReason
    {
        public int Pk_ReasonId { get;set;}
        public string Reason { get;set;}
    }
    public class ReplacementRequest
    {
        public int Fk_ReasonId { get; set; }
        public string Description { get; set; }
        public long Fk_OrderItemId { get; set; }
    }

}
