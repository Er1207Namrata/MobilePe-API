using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace MobileAPI_V2.Model
{
    public class PhotoCard
    {
        public string Mobile { get; set; }
        public string KitNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public DataSet GetCardDataList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Mobile", Mobile),
                    new SqlParameter("@page", Page),
                    new SqlParameter("@pagesize", PageSize == 0 ? 10 : PageSize),
                    new SqlParameter("@KitNo", KitNo == "" ? null : KitNo),
                    new SqlParameter("@FromDate", FromDate == "" ? null : FromDate),
                    new SqlParameter("@ToDate", ToDate == "" ? null : ToDate)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetPhotoCardData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class PhotoCardRequest
    {
        public string Mobile { get; set; }
        public string KitNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class PhotoCardResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public PhotoCardResponseList result { get; set; }
    }

    public class PhotoCardResponseList
    {
        public List<PhotoCardList>? CardList { get; set; }
    }

    public class PhotoCardList
    {
        public string totaluser { get; set; }
        public string sn { get; set; }
        public string IssueDate { get; set; }
        public string KitNo { get; set; }
        public string BIN { get; set; }
        public string CardNo { get; set; }
        public string Customername { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string ContactNo { get; set; }
        public string PhotoStatus { get; set; }
        public string PhotoURL { get; set; }
    }

}
