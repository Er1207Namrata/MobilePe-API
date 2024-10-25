using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace MobileAPI_V2.Model.BillPayment
{
    public class BillerProvider
    {

    }
    public class BillPayProviderResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public GetBillPayProviderList Response { get; set; }
        
    }

    public class GetBillPayProvider
    {
        public string ebill_code { get; set; }
        public string ebill_type { get; set; }
        public string ebill_Name { get; set; }
        public string ebill_board { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public bool? IsOptional { get; set; }
        public bool? IsOptional1 { get; set; }
        public string Type1 { get; set; }
        public string Format1 { get; set; }

    }
    public class GetBillPayProviderList
    {
        public List<GetBillPayProvider> BillPayProviderList { get; set; }
    }
    public class BillPayProviderRequest
    {
        public string ebill_type { get; set; }

        
    }


}
