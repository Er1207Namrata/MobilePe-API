using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{

    public class BankDetailResponse
    {

        public long bankstatus { get; set; }
        public BankDetail data { get; set; }
    }
    
        public class BankDetail
    {
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
    }
    public class Insurance
    {
        public string Insuranceurl { get; set; }
    }
    public class BindDropDown
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
    public class BindDropDown2
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
    public class BindDropDown2Req
    {
        public int ProcId { get; set; }
        public string data { get; set; }
    }
    public class CommonResult
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
    public class ThriweData
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string ValidityFrom { get; set; }
        public string ValidityTo { get; set; }
        public string URL { get; set; }
    }
    public class ThriweText
    {
        public string TEXT { get; set; }
        public int SRNO { get; set; }
      
    }
}
