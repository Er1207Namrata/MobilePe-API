using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class CardData
    {
        public string CardName { get; set; }
        public string CardAmount { get; set; }
        public string CardDescription { get; set; }
        public string CardTypeId { get; set; }
        public string CardWithPhotoAmt { get; set; }
        public string IsCurrent { get; set; }
        public string IsUpgrade { get; set; }
        public string UpgradAmt { get; set; }
        public string UpgradeAmtWithPhoto { get; set; }
        public string AddOnAmt { get; set; }
        public string AddonWithPhoto { get; set; }
        public string ReplaceAmt { get; set; }
        public string ReplaceWithPhoto { get; set; }
        public string PhotoAmt { get; set; }
        public string IsReplace { get; set; }
        

    }
    public class CardDataResponse
    {

        public string response { get; set; }
        public string message { get; set; }
        public CardResult result { get; set; }

    }
    public class CardRequestModel
    {
        public int Fk_memId { get; set; }
        public string Type { get; set; }
    }
    public class CardRequestModelLog
    {
        public int Fk_memId { get; set; }
    }

    public class CardResult
    {
      

        public List<CardData> lstCardData { get; set; }
    }
    public class PaymentResult
    {
       
        public List<PaymentData> lstPaymentOption { get; set; }
    }
    public class PaymentData
    {
        public string Name { get; set; }
        public string Narration { get; set; }
        public string Icon { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool AppStatus { get; set; }
        public int Flag { get; set; }


    }
    public class PaymentResponse
    {

        public string response { get; set; }
        public string message { get; set; }
        public bool AppStatus { get; set; }
        public string Title { get; set; }
        public int Flag { get; set; }
        public PaymentResult result { get; set; }

    }
    public class ReplaceCard
    {
        public string Fk_MemId { get; set; }
        public string CUSTOMERMOBILENO { get; set; }
        public string CARDID { get; set; }
        public string KitNo { get; set; }
        public string OldKitNo { get; set; }


    }
    public class ReplaceCardRespone
    {
        public string Fk_MemId { get; set; }
        public string CardId { get; set; }
        
        public string MobileNo { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public string KitNo { get; set; }
        public string OldKitNo { get; set; }

    }
    public class ReplaceCardLog
    {
        public string response { get; set; }
        public string message { get; set; }

    }

  
}
