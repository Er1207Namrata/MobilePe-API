using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class VoucherModel
    {
        public string brandName { get;set;}
        public string url { get;set;}

       
    }
    public class VoucherPriceDetails
    {
        public string price { get; set; }
        public long voucherId { get; set; }
        public string description { get; set; }
        public string validity { get; set; }
        public string termscondition { get; set; }
    }
    public class vocherresponse : VoucherModel
    {
        public List<VoucherPriceDetails> price { get; set; }
    }
}
