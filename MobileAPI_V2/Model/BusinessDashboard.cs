using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{

    public class BusinessDashboardV3
    {
        public string image { get; set; }
        public bool IsSuperUser { get; set; }
        public CommonData First { get; set; }
        public CUGSize Second { get; set; }
        public CommonData Third { get; set; }
        public List<BusinessCommonIncome> Fouth { get; set; }
        public List<BusinessCommonIncome> Fifth { get; set; }
        public List<WalletregistrationList> Walletlst { get; set; }
    }

    
    public class CommonData
    {
        public string Header { get; set; }
        public List<CommonOrderdata> data { get; set; }
        //public List<WalletregistrationList> Walletlst { get; set; }

    }
    public class CUGSize
    {
        public string Header { get; set; }
        public List<BusinessLevel> data { get; set; }
    }
    public class BusinessDashboardV2
    {
        public userifnoV2 userifno { get; set; }
        public List<Club> Club { get; set; }
        public List<BusinessLevel> CugSize { get; set; }
        public List<CommonOrderdata> topdata { get; set; }
    }

    public class BusinessDashboard
    {
        public userifno userifno { get; set; }
        public List<Club> Club { get; set; }
        public List<BusinessLevel> CugSize { get; set; }


    }
    
    public class BusinessCommonIncome
    {
        public string Target { get; set; }
        public string Remaining { get; set; }
        public string Status { get; set; }
        public decimal amount { get; set; }
    }
    public class userifno
    {
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public string VPA { get; set; }
        public string LastLogin { get; set; }
        public bool KycVerification { get; set; }
        public int BankVerification { get; set; }
    }
    public class userifnoV2
    {
       
        public string ImageUrl { get; set; }      
        public bool KycVerification { get; set; }
        public int BankVerification { get; set; }
    }

    public class WalletregistrationList
    {
        public string RegDate { get; set; }
        public string KyCStatus { get; set; }
    }
}
