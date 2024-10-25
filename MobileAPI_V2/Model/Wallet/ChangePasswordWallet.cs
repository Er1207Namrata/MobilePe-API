using MobileAPI_V2.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace MobileAPI_V2.Models
{
    public class ChangePasswordWallet
    {
        public string EntityId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Pin { get; set; }
        public string NewPin { get; set; }


        public DataSet GetChangePin()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@EntityId", EntityId),
                    new SqlParameter("@Pin", Pin),
                    new SqlParameter("@NewPin", NewPin)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ChangeWalletPin, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class ChangePasswordResponse
    {
        public string status { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public ExceptionChangePassword exception { get; set; }
    }

    public class ExceptionChangePassword
    {
        public Cause cause { get; set; }
        public string errorCode { get; set; }
        public string message { get; set; }
        public string languageCode { get; set; }
        public List<string> errors { get; set; }
        public List<Suppressed> suppressed { get; set; }
        public string localizedMessage { get; set; }
    }

    public class WalletOtpVerify
    {
        public string OTPNO { get; set; }
        public string LoginId { get; set; }

        public DataSet GetVerifyOtpWallet()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OTP", OTPNO),
                    new SqlParameter("@Mobile", LoginId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.VerifyOTP_Wallet, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class ForgotWalletPin
    {
        public string WalletPin { get; set; }
        public string ConfirmWalletPin { get; set; }
        public string EntityId { get; set; }
        public string msg { get; set; }

        public DataSet GetForgotWalletPin()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@EntityId", EntityId),
                    new SqlParameter("@WalletPin", WalletPin)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ForgotWalletPin, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
    public class ForgotWalletPinResponse
    {
        public string respcode { get; set; }
        public string respdesc { get; set; }
        public ExceptionChangePassword exception { get; set; }
    }
    public class VerifyMobileEmail
    {
        public string Email { get; set; }
        public string LoginId { get; set; }
        public string CustomerName { get; set; }

        public DataSet GeVerification()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId", LoginId),
                    new SqlParameter("@Email", Email)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetVefMob_Email, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


    public class KYCGuideLine
    {
        public string FK_MemId { get; set; }
       


        public DataSet GetKYCGuideLine()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@FK_MemId", FK_MemId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetKYCGuideLine, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class KYCGuideLineReturnRes:KYCGuidelineResponse
    {
        public KYCGUideData Data { get; set; }
    }
    public class KYCGuidelineResponse
    {
        public string respcode { get; set; }
        public string respdesc { get; set; }
    }
    public class KYCGUideData
    {
        public string[] KycData { get; set; }
    }

    public class WalletInfo
    {
        public string mobileNo { get; set; }
        public string IsFULLKYC { get; set; }
        public DataSet GetWalletInfo()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@MobileNo",mobileNo)


            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.WalletInfo, para);
            return ds;
        }
    }
    public class WalletInfoRes
    {
        
        public string message { get; set; }
        public string status { get; set; }
        public string response { get; set; }
        public WalletInfoList Data { get; set; }

        
    }
    public class WalletInfoObject
    {
        public List<WalletInfoList> listData { get; set; }
    }
    public class WalletInfoList
    {
        
        public string Name { get; set; }
        public string contactNo { get; set; }
        public string IsFULLKYC { get; set; }

    }
    public class WalletToWalletTran
    {
        public string fromEntityId { get; set; }
        public string toEntityId { get; set; }
        public string amount { get; set; }
        public string description { get; set; }
        public string externalTransactionId { get; set; }

        public long TxId { get; set; }
        public DataSet GetWalletToWallettranAmount()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@from_entity_id",fromEntityId),
                new SqlParameter("@to_entity_id",toEntityId),
                new SqlParameter("@amount",amount),
                new SqlParameter("@txn_id",TxId),
                new SqlParameter("@txn_no",externalTransactionId),
                new SqlParameter("@narration",description)


            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetWalletToWalletTra, para);
            return ds;
        }


        public DataSet validate_user2user()
            {
            SqlParameter[] para =
            {

                new SqlParameter("@toEntityId",toEntityId),
                new SqlParameter("@fromEntityId",fromEntityId),
                new SqlParameter("@txn_amt",amount),
                new SqlParameter("@txn_no",externalTransactionId),
                new SqlParameter("@narration",description)


            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.validate_user2user, para);
            return ds;
        }
    }
    public class ExceptionSendOTP
    {
        public string detailMessage { get; set; }
        public string cause { get; set; }
        public string shortMessage { get; set; }
        public string languageCode { get; set; }
        public string errorCode { get; set; }
        public string fieldErrors { get; set; }
        public List<object> suppressed { get; set; }
    }
    public class WalletTransferRes
    {
       
       
            public ExceptionSendOTP exception { get; set; }

            public ResultWalletTopup result { get; set; }
        

    }

    public class CheckMobileEmail
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Action { get; set; }

        public DataSet GetCheckMobileEmail()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@MobileNo", Mobile),
                    new SqlParameter("@EmailId", Email),
                    new SqlParameter("@Action", Action),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.CheckEmailMobile, para);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class CHeckMobileEmailResponse
    {
        public string respcode { get; set; }
        public string respdesc { get; set; }
        public string message { get; set; }
        
    }

    public class MobileEmailVfyResponse
    {
        public string status { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public VerifyMobileEmail Data { get; set; }
    }

}
