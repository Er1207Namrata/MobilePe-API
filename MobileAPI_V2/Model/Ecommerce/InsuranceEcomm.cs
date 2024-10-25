using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{

    public class BankDetailResponseEcomm
    {

        public long bankstatus { get; set; }
        public BankDetail data { get; set; }
    }
    
        public class BankDetailEcomm
    {
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
    }
    public class InsuranceEcomm
    {
        public string Insuranceurl { get; set; }
    }
    public class BindDropDownEcomm
    {
        public int value { get; set; }
        public string text { get; set; }
    }
    public class BindDropDown2Ecomm
    {
        public string value { get; set; }
        public string text { get; set; }
    }
    public class CommonResultEcomm
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
    public class ThriweDataEcomm
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string ValidityFrom { get; set; }
        public string ValidityTo { get; set; }
        public string URL { get; set; }
    }
    public class ThriweTextEcomm
    {
        public string TEXT { get; set; }
        public int SRNO { get; set; }
      
    }
}
