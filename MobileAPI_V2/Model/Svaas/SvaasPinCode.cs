using System.Collections.Generic;

namespace MobileAPI_V2.Model.Svaas
{
    public class SvaasPinCode
    {
        public string PinCode { get; set; }
    }
    public class SvaasPinCodeResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public PinCodeResult result { get; set; }
    }
    public class PinCodeResult
    {
        public List<PinCodeData> lstPincode { get; set; }
    }
    public class PinCodeData
    {
        public string state { get; set; }
        public string city { get; set; }
        public string url { get; set; }
       

    }
}
