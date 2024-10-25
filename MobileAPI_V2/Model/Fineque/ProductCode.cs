using System.Data.SqlClient;
using System.Data;
using MobileAPI_V2.Models;
using System.Collections.Generic;

namespace MobileAPI_V2.Model.Fineque
{
    public class FinequeProductCode
    {
        public string  ProductName { get; set; }
        public string  ProductCode { get; set; }
        public DataSet GetProductCode()
        {
            SqlParameter[] para =
                 {
                };
            DataSet ds = Connection.ExecuteQuery("GetProductCode", para);
            return ds;
        }
    }
    public class ProductCodeRes
    {
        public List<FinequeProductCode> Data { get; set; }
    }
   
}
