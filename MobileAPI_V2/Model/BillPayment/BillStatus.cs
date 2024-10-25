using Microsoft.VisualBasic;
using MobileAPI_V2.Model.Travel;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using static QRCoder.PayloadGenerator.SwissQrCode;
using System.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml.Linq;

namespace MobileAPI_V2.Model.BillPayment
{
    public class BillStatus
    {
        public string Status { get; set; }
        public string ServiceName { get; set; }
        public string OperatorId { get; set; }
        public string Amount { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public DataSet GetPendingRecharge()
        {
           
            DataSet ds = Connection.ExecuteQuery("GetPendingRecharge");
            return ds;
        }
    }
    public class PendinGRecharge
    {
        public string RechargeType { get; set; }
        public string ServiceName { get; set; }
        public string Amount { get; set; }
        public string RechargeNumber { get; set; }
        public int Fk_MemId { get; set; }
    }

}
