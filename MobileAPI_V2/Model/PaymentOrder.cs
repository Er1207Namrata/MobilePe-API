using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Model
{
    public class PaymentOrderModel
    {
        public int ProcId { get; set; }
        public long MemberId { get; set; }
        public decimal Amount { get; set; }
        public decimal Amount1 { get; set; }
        public string PaymentMode { get; set; }
        public string Type { get; set; }
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
        public int flag { get; set; }
        public string message { get; set; }
        public string Mobile { get; set; }
        public string CustomerId { get; set; }
        public string OperatorCode { get; set; }
        public string key { get; set; }
        public string secret { get; set; }
        public string CartItemId { get; set; }
        public string AddressId { get; set; }
        public int IsPhotoCard { get; set; }
        public int Fk_CardId { get; set; }
        public int IsLocal { get; set; }
        public int ToPayCardId { get; set; }
        public string PhotoPath { get; set; }
        public string Request { get; set; }
        public string deviceId { get; set; }
        public string appversion { get; set; }
        public string os { get; set; }
        public string ipAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Dictionary<string,string> dict { get; set; }

        public DeviceInfoPayment DeviceInfo { get; set; }


        public DataSet GenerateOrderId(PaymentOrderModel model)
        {
            if (model.Type.ToUpper() == "ADDFUND")
            {
                model.Type = "AddFund|reloadable";
            }
            try
            {
                SqlParameter[] para = {
                                     
                                      new SqlParameter("@ProcId", model.ProcId),
                                      new SqlParameter("@MemberId", model.MemberId),
                                      new SqlParameter("@Amount", model.Amount),
                                      new SqlParameter("@PaymentMode", model.PaymentMode ?? string.Empty),
                                      new SqlParameter("@Type", model.Type ?? string.Empty),
                                      new SqlParameter("@PaymentId", model.PaymentId ?? string.Empty),
                                      new SqlParameter("@OrderId", model.OrderId ?? string.Empty),
                                      new SqlParameter("@Status", model.Status ?? string.Empty),
                                      new SqlParameter("@Number", model.Mobile ?? string.Empty),
                                      new SqlParameter("@OperatorCode", model.OperatorCode ?? string.Empty),
                                      new SqlParameter("@CartItemId", model.CartItemId ?? string.Empty),
                                      new SqlParameter("@AddressId", model.AddressId ?? string.Empty),
                                      new SqlParameter("@CustomerId", model.CustomerId ?? string.Empty),
                                      new SqlParameter("@IsPhotoCard", model.IsPhotoCard),
                                      new SqlParameter("@Fk_CardId", model.Fk_CardId),
                                      new SqlParameter("@PhotoPath", model.PhotoPath),
                                      new SqlParameter("@ToCardId", model.ToPayCardId),
                                      new SqlParameter("@Request", model.Request),
                                      new SqlParameter("@IsLocal", model.IsLocal),
                                      new SqlParameter("@deviceId", model.deviceId),
                                      new SqlParameter("@appversion", model.appversion),
                                      new SqlParameter("@ipAddress", model.ipAddress),
                                      new SqlParameter("@os", model.os),
                                      new SqlParameter("@Latitude", model.Latitude),
                                      new SqlParameter("@Longitude", model.Longitude),

                            };

                DataSet ds = Connection.ExecuteQuery("Proc_PaymentOrder_v2", para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }



    }
    public class DeviceInfoPayment
    {
        public string geoCode { get; set; }
        public string appId { get; set; }
        public string ipAddress { get; set; }
        public string os { get; set; }
        public string imei { get; set; }
        public string mac { get; set; }
        public string location { get; set; }
        public string telecom { get; set; }
        public string deviceType { get; set; }
        public string mobile { get; set; }
        public string deviceId { get; set; }
        public string appversion { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
    public class PaymentOrderResponse 
    {
        public PaymentOrderModel result { get; set; }
    }
}
