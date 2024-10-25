using System.Data.SqlClient;
using System.Data;
using MobileAPI_V2.Model;
using Razorpay.Api;

namespace MobileAPI_V2.Models
{
    public class UpdateCustomerVCIPStatus
    {
        public string m2ptoken { get; set; }
        public string programid { get; set; }
        public string ref3 { get; set; }
        public string ref4 { get; set; }
        public string ref5 { get; set; }
        public string event_type { get; set; }
        public string mobile { get; set; }
        public string description { get; set; }
        public string vcipid { get; set; }
        public string vcipidstatus { get; set; }
        public string kycstatus { get; set; }
        public string panstatus { get; set; }
        public string videoconfstatus { get; set; }
        public string qastatus { get; set; }
        public string livecapturestatus { get; set; }
        public string pan_aadhaar_phtstatus { get; set; }
        public string live_pan_phtstatus { get; set; }
        public string live_aadhaar_phtstatus { get; set; }
        public string agentremark { get; set; }
        public string auditorremark { get; set; }
        public string customerid { get; set; }
        public string enityId { get; set; }

        public DataSet UpdateVCIPStatus()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@vcipid",vcipid),
                new SqlParameter("@vcipidstatus",vcipidstatus),
                new SqlParameter("@agentremark",agentremark),
                new SqlParameter("@auditorremark",auditorremark),
                new SqlParameter("@enityId",enityId)


            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateVCIPStatus, para);
            return ds;
        }
        public DataSet UpdateVCIPId()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@EntityId",customerid),
                new SqlParameter("@VCIPId",vcipid)


            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateVCIPId, para);
            return ds;
        }
    }
}
