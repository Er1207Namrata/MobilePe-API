using System.Data.SqlClient;
using System.Data;
using Razorpay.Api;
using System;

namespace MobileAPI_V2.Model.Ecommerce
{
    public class ENewDashboard
    {
        public long memberId { get; set; }
        public string appVersion { get; set; }
        public string appType { get; set; }
        public DataSet GetDashBoardData()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@memberId", memberId),
                                      new SqlParameter("@appVersion",appVersion),
                                      new SqlParameter("@appType",appType)

                                  };

                DataSet ds = Connection.ExecuteQuery("GetDashBoardData", para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetBusinessDashbaord()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@MemberId", memberId),
                                      new SqlParameter("@appVersion",appVersion),
                                      new SqlParameter("@appType",appType)

                                  };

                DataSet ds = Connection.ExecuteQuery("GetBusinessDashBoard", para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
