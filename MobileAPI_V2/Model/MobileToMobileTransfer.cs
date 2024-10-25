using System.Collections.Generic;

namespace MobileAPI_V2.Model
{
    public class MobileToMobileTransfer
    {
        public string CUSTOMERMOBILENO { get; set; }
        public string RECEIVERMOBILENO { get; set; }
        public int TRANSAMOUNT { get; set; }
        public string TRANSACTIONID { get; set; }
        public string Remarks { get; set; }
        public string WALLETPIN { get; set; }
    }
    public class TransferResponse
    {
        public string response { get; set; }
        public string message { get; set; }
      
    }
    public class StationCoderes
    {
        public List<StationResult> result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public int id { get; set; }
        public object transactionId { get; set; }
        public object filepath { get; set; }

    }
    public class StationResult
    {
        public string text { get; set; }
        public string value { get; set; }
    }
}
