using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class DashboradBanner
    {
        public string link { get; set; }
        public string imgurl { get; set; }
        public string name { get; set; }
    }
    public class DashboradBannerResponse
    {
      
        public string headerText { get; set; }
        public List<DashboradBanner> data { get; set; }
       

    }
    public class CardMasterResponse
    {
        public FreeCardResponse freecard { get; set; }
        public CardMaster carddata { get; set; }
        public List<BindDropDown> blockcard { get; set; }
        public List<BindDropDown2> upgradecard { get; set; }
        public List<CommonResult> viewstatement { get; set; }
        public string url { get; set; }
    }
    public class Information
    {

        public string Header { get; set; }
        public string Informationmessage { get; set; }
        public string Link { get; set; }
        public string Url { get; set; }
        public bool Status { get; set; }
    }
    public class CardMaster
    {
        public decimal cardbalance { get; set; }
        public bool IsCardApplied { get; set; }
        public decimal ReIssueCost { get; set; }
        public decimal CardCost { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsPaymentGateway { get; set; }
    }
    public class BlockCardRequest
    {
        public string transactionId { get; set; }
        public decimal amount { get; set; }
        public int reasonId { get; set; }
        public long memberId { get; set; }
    }
    public class FreeCardResponse
    {
        public string url { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string MobileNo { get; set; }
        public decimal amount { get; set; }
    }
    public class TeamStatusHeader
    {
        public string HeaderText { get; set; }
    }
    public class TeamStatus
    {
        public string LevelName { get; set; }
        public long TeamSize { get; set; }
        public long assignedCard { get; set; }
        public long requestCard { get; set; }
        public long ActivatedCard { get; set; }
        public long PhysicalCard { get; set; }
        public string type { get; set; }
    }
    public class TeamStatusResponse
    {
        public List<TeamStatus> result { get; set; }
    }
}
