using AutoMapper.Execution;
using Dapper;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.DataLayer;
using MobileAPI_V2.Model;
using MobileAPI_V2.Model.Affiliate;
using MobileAPI_V2.Model.BBPS;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Model.Master;
using MobileAPI_V2.Model.Svaas;
using MobileAPI_V2.Model.Travel;
using MobileAPI_V2.Models;
using Nancy;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Razorpay.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static MobileAPI_V2.Model.FBH_Repository;
using static QRCoder.PayloadGenerator;

namespace MobileAPI_V2.Services
{
    public class DataRepositoryEcomm : IDataRepositoryEcomm
    {
        private readonly ConnectionString _connectionString;
        private readonly MallConnectionString _mallconnectionString;
        IDbConnection con;
        IConfiguration _configuration;
        private string apikey = "";
        private string OperatorDetailsUrl = "";
        private string rechargeplans = "";
        ApiRequest request;
        public DataRepositoryEcomm(ConnectionString connectionString, IConfiguration configuration, MallConnectionString mallConnectionString)
        {
            _connectionString = connectionString;
            _mallconnectionString = mallConnectionString;
            _configuration = configuration;
            apikey = _configuration["ApiKey"];
            OperatorDetailsUrl = _configuration["OperatorDetials"];
            rechargeplans = _configuration["rechargeplans"];
            request = new ApiRequest(_configuration);
        }

        public async Task<ReferralModelEcomm> GetReferralName(string ReferralCode)
        {
            ReferralModelEcomm result = new ReferralModelEcomm();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@InviteCode", ReferralCode ?? string.Empty);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    { con.Open(); }
                    var res = await con.QueryFirstAsync<ReferralModelEcomm>("sp_referral_details", parameters, commandType: CommandType.StoredProcedure);
                    result = res;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }
        public async Task<ResultSet> CheckJWTToken(string Token)
        {

            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Token", Token ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("CheckJWTToken_V2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }

        }

        public async Task<ResultSet> CheckMobileNo(string mobileno)
        {

            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@mobileno", mobileno ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("Proc_CheckMobileNo_V2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }

        }

        public async Task<PincodeModel> GetAreaDetailByPincode(string pincode)
        {
            PincodeModel result = new PincodeModel();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Pincode", pincode);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<PincodeModel>("sp_pincode_info", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }

        }

        public async Task<LoginResponseEcomm> Login(LoginEcomm model)
        {
            DynamicParameters parameters = new DynamicParameters();

            LoginResponseEcomm result = new LoginResponseEcomm();
            parameters.Add("@InfodeviceId", model.DeviceId ?? string.Empty);
            parameters.Add("@InfodeviceType", model.DeviceType ?? string.Empty);
            parameters.Add("@os", model.OSId ?? string.Empty);
            parameters.Add("@appVersion", model.appVersion ?? string.Empty);
            parameters.Add("@telecom", model.telecom ?? string.Empty);
            parameters.Add("@geoCode", model.geoCode ?? string.Empty);
            parameters.Add("@appId", model.appId ?? string.Empty);
            parameters.Add("@ipAddress", model.ipAddress ?? string.Empty);
            parameters.Add("@location", model.location ?? string.Empty);
            parameters.Add("@mobile", model.mobile ?? string.Empty);

            parameters.Add("@LoginID", model.LoginID ?? string.Empty);
            parameters.Add("@Password", model.Password ?? string.Empty);
            parameters.Add("@DeviceId", model.DeviceId ?? string.Empty);
            parameters.Add("@OSId", model.OSId ?? string.Empty);
            parameters.Add("@DeviceType", model.DeviceType ?? string.Empty);
            parameters.Add("@UserType", model.UserType ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<LoginResponseEcomm>("sp_user_login", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }
        public async Task<Common> Insetnotification(string notification)
        {
            Common result = new Common();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@xmlnotification", notification);


            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<Common>("Proc_InsertNotification_v2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }

        }

        //public async Task<ResultSet> Register(Registration model)
        //{
        //    DynamicParameters parameters = new DynamicParameters();
        //    ResultSet result = new ResultSet();
        //    parameters.Add("@MobileNo", model.MobileNo ?? string.Empty);
        //    parameters.Add("@InvitedBy", model.InvitedBy ?? string.Empty);
        //    parameters.Add("@Password", model.Password ?? string.Empty);
        //    parameters.Add("@Email", model.Email ?? string.Empty);
        //    parameters.Add("@FName", model.FirstName ?? string.Empty);
        //    parameters.Add("@MName", model.MiddleName ?? string.Empty);
        //    parameters.Add("@LName", model.LastName ?? string.Empty);
        //    parameters.Add("@Pincode", model.Pincode ?? string.Empty);
        //    parameters.Add("@State", model.State);
        //    parameters.Add("@City", model.City);

        //    using (con = new SqlConnection(_connectionString.Value))
        //    {
        //        try
        //        {
        //            if (con.State == ConnectionState.Closed) { con.Open(); }
        //            result = await con.QueryFirstAsync<ResultSet>("CustomerRegisterationByReferal_v2", parameters, commandType: CommandType.StoredProcedure, commandTimeout: con.ConnectionTimeout);
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        finally
        //        {
        //            con.Close();
        //        }
        //        return result;
        //    }
        //}

        public async Task<Registrationresponse> CustomerRegistration(Registration model)
        {
            Registrationresponse res = new Registrationresponse();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@MobileNo", model.MobileNo ?? string.Empty);
            parameters.Add("@InvitedBy", model.InvitedBy ?? string.Empty);
            parameters.Add("@Password", model.Password ?? string.Empty);
            parameters.Add("@Email", model.Email ?? string.Empty);
            parameters.Add("@FName", model.FirstName ?? string.Empty);
            parameters.Add("@MName", model.MiddleName ?? string.Empty);
            parameters.Add("@LName", model.LastName ?? string.Empty);
            parameters.Add("@Pincode", model.Pincode ?? string.Empty);
            parameters.Add("@State", model.State);
            parameters.Add("@City", model.City);
            parameters.Add("@DeviceId", model.DeviceId ?? string.Empty);
            parameters.Add("@OSId", model.OSId ?? string.Empty);
            parameters.Add("@DeviceType", model.DeviceType ?? string.Empty);
            parameters.Add("@UserType", model.UserType ?? string.Empty);
            parameters.Add("@dob", model.dob ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("CustomerRegisterationByReferal_v3", parameters, commandType: CommandType.StoredProcedure, commandTimeout: con.ConnectionTimeout);
                    var registrationresponse = result.ReadSingle<ResultSet>();
                    var loginresponse = result.ReadSingle<LoginResponse>();
                    res.CustomerRegistrationResponse = registrationresponse;
                    res.LoginResponse = loginresponse;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return res;
            }
        }

        public async Task<ResultSet> EmailLog(EmailLog model)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FromMailId", model.FromMailId ?? string.Empty);
            parameters.Add("@ToMailId", model.ToMailId ?? string.Empty);
            parameters.Add("@Subject", model.Subject ?? string.Empty);
            parameters.Add("@Message", model.Message ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("Proc_EmailLog_v2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }
        public async Task<ResultSet> ForgetPassword(ForgotPasswordEcomm model)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@ProcId", model.ProcId);
            parameters.Add("@LoginID", model.LoginId ?? string.Empty);
            parameters.Add("@NewPassword", model.NewPassword ?? string.Empty);
            parameters.Add("@InfodeviceId", model.deviceInfo.deviceId ?? string.Empty);
            parameters.Add("@InfodeviceType", model.deviceInfo.deviceType ?? string.Empty);
            parameters.Add("@os", model.deviceInfo.os ?? string.Empty);
            parameters.Add("@appVersion", model.deviceInfo.appVersion ?? string.Empty);
            parameters.Add("@telecom", model.deviceInfo.telecom ?? string.Empty);
            parameters.Add("@geoCode", model.deviceInfo.geoCode ?? string.Empty);
            parameters.Add("@appId", model.deviceInfo.appId ?? string.Empty);
            parameters.Add("@ipAddress", model.deviceInfo.ipAddress ?? string.Empty);
            parameters.Add("@location", model.deviceInfo.location ?? string.Empty);
            parameters.Add("@mobile", model.deviceInfo.mobile ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("ForgotPassword_AES", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }
        public async Task<OTPResultEcomm> OTPProcess(OTPRequestEcomm model)
        {
            OTPResultEcomm result = new OTPResultEcomm();
            DynamicParameters parameters = new DynamicParameters();

            //parameters.Add("@InfodeviceId", model.deviceInfo.deviceId ?? string.Empty);
            //parameters.Add("@InfodeviceType", model.deviceInfo.deviceType ?? string.Empty);
            //parameters.Add("@os", model.deviceInfo.os ?? string.Empty);
            //parameters.Add("@appVersion", model.deviceInfo.appVersion ?? string.Empty);
            //parameters.Add("@telecom", model.deviceInfo.telecom ?? string.Empty);
            //parameters.Add("@geoCode", model.deviceInfo.geoCode ?? string.Empty);
            //parameters.Add("@appId", model.deviceInfo.appId ?? string.Empty);
            //parameters.Add("@ipAddress", model.deviceInfo.ipAddress ?? string.Empty);
            //parameters.Add("@location", model.deviceInfo.location ?? string.Empty);
            //parameters.Add("@mobile", model.deviceInfo.mobile ?? string.Empty);


            parameters.Add("@ProcId", model.ProcId);
            parameters.Add("@LoginID", model.LoginId ?? string.Empty);
            parameters.Add("@OTP", model.OTPNO ?? string.Empty);
            parameters.Add("@Purpose", model.Purpose ?? string.Empty);
            parameters.Add("@Name", model.Name ?? string.Empty);
            parameters.Add("@DeviceId", model.DeviceId ?? string.Empty);
            parameters.Add("@DeviceType", model.DeviceType ?? string.Empty);
            parameters.Add("@OSId", model.OSId ?? string.Empty);
            parameters.Add("@UserType", model.UserType ?? string.Empty);
            parameters.Add("@memberId", model.memberId);
            parameters.Add("@KitNo", model.KitNo);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<OTPResultEcomm>("sp_send_otp", parameters, commandType: CommandType.StoredProcedure, commandTimeout: con.ConnectionTimeout);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<AppVersionResponse> GetAppVersionDetail(string ostype, string version)
        {
            AppVersionResponse result = new AppVersionResponse();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@ostype", ostype);
            parameters.Add("@version", version);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<AppVersionResponse>("Proc_GetAppVerisionDetail_v2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<LoginResponseEcomm> GetUserProfile(long MemberId)
        {
            LoginResponseEcomm result = new LoginResponseEcomm();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<LoginResponseEcomm>("Proc_GetUserProfile_v2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<Bankdetails_accountResponseEcomm> bankdetails(long memberId)
        {
            Bankdetails_accountResponseEcomm result = new Bankdetails_accountResponseEcomm();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<Bankdetails_accountResponseEcomm>("bankdetails_v2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<ResultSetEcomm> ChangePassword(ChangePasswordEcomm model)
        {
            ResultSetEcomm result = new ResultSetEcomm();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@OldPassword", model.OldPassword ?? string.Empty);
            parameters.Add("@NewPassword", model.NewPassword ?? string.Empty);
            parameters.Add("@Fk_MemId", model.memberId);
            parameters.Add("@InfodeviceId", model.deviceInfo.deviceId ?? string.Empty);
            parameters.Add("@InfodeviceType", model.deviceInfo.deviceType ?? string.Empty);
            parameters.Add("@DeviceId", model.DeviceId ?? string.Empty);
            parameters.Add("@DeviceType", model.DeviceType ?? string.Empty);
            parameters.Add("@os", model.deviceInfo.os ?? string.Empty);
            parameters.Add("@appVersion", model.deviceInfo.appVersion ?? string.Empty);
            parameters.Add("@telecom", model.deviceInfo.telecom ?? string.Empty);
            parameters.Add("@geoCode", model.deviceInfo.geoCode ?? string.Empty);
            parameters.Add("@appId", model.deviceInfo.appId ?? string.Empty);
            parameters.Add("@ipAddress", model.deviceInfo.ipAddress ?? string.Empty);
            parameters.Add("@location", model.deviceInfo.location ?? string.Empty);
            parameters.Add("@mobile", model.deviceInfo.mobile ?? string.Empty);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSetEcomm>("ChangePassword_v2", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<ResultSetEcomm> SignOut(SignOutRequestEcomm model)
        {
            ResultSetEcomm result = new ResultSetEcomm();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@OSId", model.OSId ?? string.Empty);
            parameters.Add("@UserType", model.UserType ?? string.Empty);
            parameters.Add("@InfodeviceId", model.deviceInfo.deviceId ?? string.Empty);
            parameters.Add("@InfodeviceType", model.deviceInfo.deviceType ?? string.Empty);
            parameters.Add("@DeviceId", model.DeviceId ?? string.Empty);
            parameters.Add("@DeviceType", model.DeviceType ?? string.Empty);
            parameters.Add("@os", model.deviceInfo.os ?? string.Empty);
            parameters.Add("@appVersion", model.deviceInfo.appVersion ?? string.Empty);
            parameters.Add("@telecom", model.deviceInfo.telecom ?? string.Empty);
            parameters.Add("@geoCode", model.deviceInfo.geoCode ?? string.Empty);
            parameters.Add("@appId", model.deviceInfo.appId ?? string.Empty);
            parameters.Add("@ipAddress", model.deviceInfo.ipAddress ?? string.Empty);
            parameters.Add("@location", model.deviceInfo.location ?? string.Empty);
            parameters.Add("@mobile", model.deviceInfo.mobile ?? string.Empty);

            parameters.Add("@Token", model.Token ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSetEcomm>("SignOut_v2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<ResultSetEcomm> EditProfile(ProfileRequestEcomm model)
        {
            ResultSetEcomm result = new ResultSetEcomm();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@MemberId", model.MemberId);
            parameters.Add("@MobileNo", model.MobileNo ?? string.Empty);
            parameters.Add("@Email", model.Email ?? string.Empty);
            parameters.Add("@FirstName", model.FirstName ?? string.Empty);
            parameters.Add("@LastName", model.LastName ?? string.Empty);
            parameters.Add("@MiddleName", model.MiddleName ?? string.Empty);
            parameters.Add("@StateId", model.StateId);
            parameters.Add("@DistrictId", model.DistrictId);
            parameters.Add("@Pincode", model.Pincode ?? string.Empty);
            parameters.Add("@Address1", model.Address1 ?? string.Empty);
            parameters.Add("@CurrStateId", model.CurrStateId);
            parameters.Add("@CurrDistrictId", model.CurrDistrictId);
            parameters.Add("@CurrPincode", model.CurrPincode ?? string.Empty);
            parameters.Add("@CurrAddress1", model.CurrAddress1 ?? string.Empty);
            parameters.Add("@DOB", model.DOB ?? string.Empty);
            parameters.Add("@MotherName", model.MotherName ?? string.Empty);
            parameters.Add("@FatherName", model.FatherName ?? string.Empty);
            parameters.Add("@Gender", model.Gender ?? string.Empty);
            parameters.Add("@NomineeName", model.NomineeName ?? string.Empty);
            parameters.Add("@NomineeRelation", model.NomineeRelation ?? string.Empty);
            parameters.Add("@NomineeDOB", model.NomineeDOB ?? string.Empty);
            parameters.Add("@GuardianName", model.GuardianName ?? string.Empty);
            parameters.Add("@GuardianAddress", model.GuardianAddress ?? string.Empty);
            parameters.Add("@GuardianMobileNo", model.GuardianMobileNo ?? string.Empty);
            parameters.Add("@GuardianRelation", model.GuardianRelation ?? string.Empty);
            parameters.Add("@MarritalStatus", model.MarritalStatus ?? string.Empty);
            parameters.Add("@AlternateMobile", model.AlternateMobile ?? string.Empty);
            parameters.Add("@PANNo", model.PANNo ?? string.Empty);
            parameters.Add("@AddressProofType", model.AddressProofType ?? string.Empty);
            parameters.Add("@AddressProofNo", model.AddressProofNo ?? string.Empty);
            parameters.Add("@PhysicalCard", model.PhysicalCard ?? string.Empty);
            parameters.Add("@InfodeviceId", model.deviceInfo.deviceId ?? string.Empty);
            parameters.Add("@InfodeviceType", model.deviceInfo.deviceType ?? string.Empty);
            parameters.Add("@DeviceId", model.DeviceId ?? string.Empty);
            parameters.Add("@DeviceType", model.DeviceType ?? string.Empty);
            parameters.Add("@os", model.deviceInfo.os ?? string.Empty);
            parameters.Add("@appVersion", model.deviceInfo.appVersion ?? string.Empty);
            parameters.Add("@telecom", model.deviceInfo.telecom ?? string.Empty);
            parameters.Add("@geoCode", model.deviceInfo.geoCode ?? string.Empty);
            parameters.Add("@appId", model.deviceInfo.appId ?? string.Empty);
            parameters.Add("@ipAddress", model.deviceInfo.ipAddress ?? string.Empty);
            parameters.Add("@location", model.deviceInfo.location ?? string.Empty);
            parameters.Add("@mobile", model.deviceInfo.mobile ?? string.Empty);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSetEcomm>("Proc_EditProfile_v2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<BusinessLevelResultEcomm> BusinessLevel(long MemberId)
        {

            BusinessLevelResultEcomm data = new BusinessLevelResultEcomm();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("Proc_GetCUGSize_v2", parameters, commandType: CommandType.StoredProcedure);
                    var BusinessLeveldata = result.Read<BusinessLevelEcomm>();
                    var Club = result.Read<ClubEcomm>();
                    var Status = result.ReadFirstOrDefault<BusinessLevelResultEcomm>();
                    data.cug = BusinessLeveldata;
                    data.otherIncome = Club;
                    data.kycStatus = Status.kycStatus;
                    data.bankStatus = Status.bankStatus;


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return data;
            }
        }

        public async Task<mobileoperatorEcomm> OperatorDetails(string mobilenumber)
        {
            mobileoperatorEcomm operatordetails = new mobileoperatorEcomm();
            try
            {
                OperatorDetailsUrl += "number=" + mobilenumber + "&apikey=" + apikey;
                var response = await ApiRequest.sendrequest(OperatorDetailsUrl, "Get", null);
                operatordetails = JsonConvert.DeserializeObject<mobileoperatorEcomm>(response);

            }
            catch (Exception ex)
            {
                operatordetails.success = false;
                operatordetails.message = ex.Message;
            }
            return operatordetails;
        }

        public async Task<RechargePlanDataEcomm> RechargePlan(string operator_id, string circle_id)
        {
            RechargePlanDataEcomm rechargePlan = new RechargePlanDataEcomm();

            rechargeplans += "operator_id=" + operator_id + "&apikey=" + apikey + "&circle_id=" + circle_id + "&limit=500";
            var response = await ApiRequest.sendrequest(rechargeplans, "Get", null);
            rechargePlan = JsonConvert.DeserializeObject<RechargePlanDataEcomm>(response);
            return rechargePlan;
        }

        public async Task<List<ExpenseEcomm>> GETSupper30Expense(string type)
        {
            List<ExpenseEcomm> result = new List<ExpenseEcomm>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Type", type);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<ExpenseEcomm>("Proc_GetSuper30Expenses_v2", parameters, commandType: CommandType.StoredProcedure);
                    var Super30Expenselist = res.ToList();
                    result = Super30Expenselist;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<List<DirectEcomm>> GetDirect(long MemberId, long OldMemberId)
        {
            List<DirectEcomm> result = new List<DirectEcomm>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            parameters.Add("@OldMemberId", OldMemberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<DirectEcomm>("GetDirectMembers_v2", parameters, commandType: CommandType.StoredProcedure);
                    var DirectList = res.ToList();
                    result = DirectList;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }
        public async Task<List<DirectEcomm>> GetLevelWiseDetail(long MemberId, string level, string type, string searchId, int page)
        {
            List<DirectEcomm> result = new List<DirectEcomm>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            parameters.Add("@level", level);
            parameters.Add("@type", type);
            parameters.Add("@searchId", searchId);
            parameters.Add("@page", page);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<DirectEcomm>("Proc_GetLevelWiseDetail_v2", parameters, commandType: CommandType.StoredProcedure);
                    var DirectList = res.ToList();
                    result = DirectList;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }
        public async Task<CugItemTempEcomm> GetTeamStatus(long MemberId, string searchId)
        {
            CugItemTempEcomm result = new CugItemTempEcomm();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            parameters.Add("@searchId", searchId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {

                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryMultipleAsync("GetTeamStatus_V2", parameters, commandType: CommandType.StoredProcedure);
                    var topheader = res.Read<BindDropDownEcomm>().ToList();
                    var CugItem = res.Read<BindDropDownEcomm>().ToList();
                    var Datum = res.Read<DatumEcomm>().ToList();

                    result.header1 = topheader;
                    result.header2 = CugItem;
                    result.data = Datum;


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }
        public async Task<List<RecentRechargeEcomm>> GetRecentRecharge(long MemberId, string type = "", int page = 1)
        {
            List<RecentRechargeEcomm> result = new List<RecentRechargeEcomm>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            parameters.Add("@type", type);
            parameters.Add("@page", page);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<RecentRechargeEcomm>("Proc_GetRecentRecharge_V2", parameters, commandType: CommandType.StoredProcedure);

                    result = res.ToList();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<List<MinistatementEcomm>> GetMiniStatement(long MemberId, int page)
        {
            List<MinistatementEcomm> result = new List<MinistatementEcomm>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            parameters.Add("@page", page);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<MinistatementEcomm>("GetMinistatementDetails_v2", parameters, commandType: CommandType.StoredProcedure);
                    var Ministatement = res.ToList();
                    result = Ministatement;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<FranchiseDetailsResponse> GetFranchiseDetails(int DistrictId)
        {
            FranchiseDetailsResponse result = new FranchiseDetailsResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@DistrictId", DistrictId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<FranchiseDetails>("Proc_GetFranchiseDetails_V2", parameters, commandType: CommandType.StoredProcedure);
                    var FranchiseDetails = res.ToList();
                    result.result = FranchiseDetails;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }


        public async Task<List<OperatorCodeModel>> GetOperatorsCode(int ProcId, string Type, string opt)
        {
            List<OperatorCodeModel> result = new List<OperatorCodeModel>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@procId", ProcId);
            parameters.Add("@type", Type);
            parameters.Add("@operator", opt);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<OperatorCodeModel>("Proc_GetOperatorsCode_V2", parameters, commandType: CommandType.StoredProcedure);
                    result = res.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<Common> SubFranchiseCardRequest(SubFranchiseCardRequest model)
        {
            Common result = new Common();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@NumberOfCard", model.numberofCard);
            parameters.Add("@SecurityAmount", model.securityAmount);
            parameters.Add("@Fk_paymentId", model.fk_PaymentId);
            parameters.Add("@TransactionId", model.transactionId);
            parameters.Add("@CreatedBy", model.createdBy);
            parameters.Add("@Paymentdate", model.paymentdate);
            parameters.Add("@RequestType", model.requestType);
            parameters.Add("@FranchiseId", model.fk_FranchiseId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<Common>("Proc_SubFranchiseCardRequest", parameters, commandType: CommandType.StoredProcedure);
                    result = res.SingleOrDefault();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<PaymentOrderResponse> PaymentOrder(PaymentOrderModel model)
        {
            if (model.Type.ToUpper() == "ADDFUND")
            {
                model.Type = "AddFund|reloadable";
            }
            PaymentOrderResponse result = new PaymentOrderResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProcId", model.ProcId);
            parameters.Add("@MemberId", model.MemberId);
            parameters.Add("@Amount", model.Amount);
            parameters.Add("@PaymentMode", model.PaymentMode ?? string.Empty);
            parameters.Add("@Type", model.Type ?? string.Empty);
            parameters.Add("@PaymentId", model.PaymentId ?? string.Empty);
            parameters.Add("@OrderId", model.OrderId ?? string.Empty);
            parameters.Add("@Status", model.Status ?? string.Empty);
            parameters.Add("@Number", model.Mobile ?? string.Empty);
            parameters.Add("@OperatorCode", model.OperatorCode ?? string.Empty);
            parameters.Add("@CartItemId", model.CartItemId ?? string.Empty);
            parameters.Add("@AddressId", model.AddressId ?? string.Empty);
            parameters.Add("@CustomerId", model.CustomerId ?? string.Empty);
            parameters.Add("@IsPhotoCard", model.IsPhotoCard);
            parameters.Add("@Fk_CardId", model.Fk_CardId);
            parameters.Add("@PhotoPath", model.PhotoPath);
            parameters.Add("@ToCardId", model.ToPayCardId);
            parameters.Add("@Request", model.Request);
            parameters.Add("@IsLocal", model.IsLocal);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<PaymentOrderModel>("Proc_PaymentOrder_v2", parameters, commandType: CommandType.StoredProcedure);
                    result.result = res.SingleOrDefault();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<LedgerResult> Ledger(long MemberId, string Type, string level, int monthId, int yearId)
        {
            LedgerResult obj = new LedgerResult();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            parameters.Add("@type", Type);
            parameters.Add("@level", level);
            parameters.Add("@monthId", monthId);
            parameters.Add("@yearId", yearId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("Ledger_V2", parameters, commandType: CommandType.StoredProcedure);
                    var balance = result.Read<LedgerBalance>().FirstOrDefault();
                    var balancedetails = result.Read<LedgerDetails>();


                    obj.balance = balance;
                    obj.balancedetails = balancedetails.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<MobilePeRegularResult> MobilePeRegularEarnings(long MemberId)
        {
            MobilePeRegularResult obj = new MobilePeRegularResult();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("GetMobilePeRegularEarnings_v2", parameters, commandType: CommandType.StoredProcedure);
                    var summarydata = result.Read<ResultCommon>();
                    var MobilePeRegularEarningsDetails = result.Read<MobilePeRegularEarningsDetails>();


                    obj.summarydata = summarydata.ToList();
                    obj.Income = MobilePeRegularEarningsDetails;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<SelfCUGMoreResult> SelfCUGMore(long MemberId, int monthId, int year)
        {
            SelfCUGMoreResult obj = new SelfCUGMoreResult();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            parameters.Add("@MonthId", monthId);
            parameters.Add("@Year", year);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("GetSelfCUGMore_V2", parameters, commandType: CommandType.StoredProcedure);
                    var SelfCUGMore = result.ReadSingle<SelfCUGMore>();
                    var SelfMotiveClub = result.ReadSingle<SelfMotiveClub>();
                    var SelfHighValueOfYearlyClub = result.ReadSingle<SelfHighValueOfYearlyClub>();
                    var GoldMemberShip = result.ReadSingle<GoldMemberShip>();
                    var ExtraEarnings = result.ReadSingle<ExtraEarnings>();
                    obj.SelfCUGMore = SelfCUGMore;
                    obj.SelfMotiveClub = SelfMotiveClub;
                    obj.SelfHighValueOfYearlyClub = null;
                    obj.GoldMemberShip = GoldMemberShip;
                    obj.ExtraEarnings = ExtraEarnings;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<CUGMoreResponse> CUGMore(CUGMoreRequest model)
        {
            CUGMoreResponse obj = new CUGMoreResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", model.memberId);
            parameters.Add("@MonthId", model.monthId);
            parameters.Add("@Year", model.year);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("Proc_GetCUGMoreV3", parameters, commandType: CommandType.StoredProcedure);
                    var CUGMore = result.Read<CUGMore>();
                    var MonthlyCUGAchieverClub = result.ReadSingle<MonthlyCUGAchieverClub>();
                    var CUGExtraEarningFromCard = result.ReadSingle<CUGExtraEarningFromCard>();

                    obj.items = CUGMore;
                    obj.MonthlyCUGAchieverClub = MonthlyCUGAchieverClub;
                    obj.CUGExtraEarningFromCard = CUGExtraEarningFromCard;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return obj;
            }
        }

        public async Task<List<CUGMoreDetails>> CUGMoreDetails(CUGMoreRequest model)
        {
            List<CUGMoreDetails> obj = new List<CUGMoreDetails>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", model.memberId);
            parameters.Add("@monthId", model.monthId);
            parameters.Add("@year", model.year);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<CUGMoreDetails>("GetCUGMoreDetailsV3", parameters, commandType: CommandType.StoredProcedure);


                    obj = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<CardActivationMore> cardActivationMore(CardActivationEarningRequest model)
        {
            CardActivationMore obj = new CardActivationMore();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", model.memberId);
            parameters.Add("@MonthId", model.monthId);
            parameters.Add("@Year", model.year);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await
                        con.QueryAsync<CardActivationEarning>("Proc_GetCardActivationEarningV3",
                        parameters,
                        commandType:
                        CommandType.StoredProcedure);
                    obj.CardActivationEarning = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return obj;
            }
        }

        public async Task<MobilePeClub> MobilePeClub(long MemberId, int monthId, int year)
        {
            MobilePeClub obj = new MobilePeClub();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            parameters.Add("@MonthId", monthId);
            parameters.Add("@YearId", year);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<MobilePeClubResult>("GetMobilePeClub_V2", parameters, commandType: CommandType.StoredProcedure);

                    obj.result = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return obj;
            }
        }

        public async Task<EarningBankTrfResponse> EarningBankTrf(long MemberId, int monthId, int year)
        {
            EarningBankTrfResponse obj = new EarningBankTrfResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            parameters.Add("@month", monthId);
            parameters.Add("@year", year);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<EarningBankTrf>("GetBankEarningTRF_V2", parameters, commandType: CommandType.StoredProcedure);


                    obj.result = result.ToList();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return obj;
            }
        }

        public async Task<ResultSet> RedeemRewardPoints(RedeamRewardPoints model)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Fk_MemId", model.memberId);
            parameters.Add("@NumberofPoint", model.numberOfPoints);
            parameters.Add("@Remark", model.remark);
            parameters.Add("@type", model.type);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<ResultSet>("RedeamRewardPoint_V2", parameters, commandType: CommandType.StoredProcedure);
                    result = res.SingleOrDefault();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }


        public async Task<List<AffiliateModel>> GetAffiliateProgram(long MemberId, string type)
        {
            List<AffiliateModel> result = new List<AffiliateModel>();
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@memberId", MemberId);
                    parameters.Add("@type", type);

                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var data = await con.QueryAsync<AffiliateModel>("Proc_GetAffiliateProgram", parameters, commandType: CommandType.StoredProcedure);
                    result = data.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();

                }

                return result;
            }
        }

        public async Task<List<AffiliateOffer>> GetAffiliateOffers()
        {
            List<AffiliateOffer> result = new List<AffiliateOffer>();
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var data = await con.QueryAsync<AffiliateOffer>("Proc_AffliateOffer", null, commandType: CommandType.StoredProcedure);
                    result = data.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();

                }

                return result;
            }
        }

        public async Task<List<AffilicateCategory>> GetAffiliateCategory()
        {
            List<AffilicateCategory> result = new List<AffilicateCategory>();
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var data = await con.QueryAsync<AffilicateCategory>("Proc_GetAffiliateCategory", null, commandType: CommandType.StoredProcedure);
                    result = data.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }


        public async Task<ResultSet> CheckEmail_Mobile(string data)
        {

            DynamicParameters parameters = new DynamicParameters();
            ResultSet res = new ResultSet();
            parameters.Add("@data", data);
            using (con = new SqlConnection(_connectionString.Value))
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                var result = await con.QueryFirstAsync<ResultSet>("Proc_CheckEmail_Mobile", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result;
            }
        }

        public async Task<Common> EmailConfirmationLink(string Token, string Type, string Mobile, string EmaiId, string encrypteddata)
        {
            DynamicParameters parameters = new DynamicParameters();
            ResultSet res = new ResultSet();
            parameters.Add("@type", Type);
            parameters.Add("@token", Token);
            parameters.Add("@mobile", Mobile);
            parameters.Add("@emailId", EmaiId);
            parameters.Add("@encrypteddata", encrypteddata);
            using (con = new SqlConnection(_connectionString.Value))
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                var result = await con.QueryFirstAsync<Common>("Emailverification", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result;
            }
        }

        public async Task<BusinessDashboard> BusinessDashboard(long MemberId)
        {
            BusinessDashboard obj = new BusinessDashboard();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("Proc_GetBusinessDashboardDetails", parameters, commandType: CommandType.StoredProcedure);

                    var BusinessLevel = result.Read<BusinessLevel>();
                    var Club = result.Read<Club>();
                    var userifno = result.ReadSingle<userifno>();

                    obj.userifno = userifno;
                    obj.CugSize = BusinessLevel.ToList();
                    obj.Club = Club.ToList();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<DashboardResponse> BusinessTransaction(long MemberId, long ProcId)
        {
            DashboardResponse obj = new DashboardResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            parameters.Add("@procId", ProcId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("Proc_GetDashboardV2", parameters, commandType: CommandType.StoredProcedure);
                    if (ProcId == 1)
                    {
                        var UserWallet = result.Read<UserWallet>();
                        obj.totalAmount = result.Read<decimal>().FirstOrDefault();
                        obj.UserWallet = UserWallet.ToList();
                    }
                    if (ProcId == 2 || ProcId == 3)
                    {
                        var transactiondetails = result.Read<transactiondetails>();
                        obj.totalAmount = result.Read<decimal>().FirstOrDefault();
                        obj.transactiondetails = transactiondetails.ToList();
                    }


                    obj.totalAmount = result.Read<decimal>().FirstOrDefault();



                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<DashboradBannerResponse> DashboradBanner(long MemberId)
        {
            DashboradBannerResponse result = new DashboradBannerResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var data = await con.QueryMultipleAsync("Proc_Dashboardbanner_v2", parameters, commandType: CommandType.StoredProcedure);
                    var header = data.Read<ResultSet>().FirstOrDefault();
                    var banner = data.Read<DashboradBanner>().ToList();
                    result.data = banner;
                    result.headerText = header.msg;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<Common> CheckAmountByTransactionId(string paymentId)
        {
            Common result = new Common();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@paymentId", paymentId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var data = await con.QueryAsync<Common>("Proc_CheckAmountByPaymentId", parameters, commandType: CommandType.StoredProcedure);
                    result = data.FirstOrDefault();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<ResultSet> OperatorRecharge_V2(RechargeDeskRequest model)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@procId", model.ProcId);
            parameters.Add("@transId", model.Id);
            parameters.Add("@Fk_MemberId", model.MemberId);
            parameters.Add("@Number", model.number ?? string.Empty);
            parameters.Add("@OperatorCode", model.OperatorCode ?? string.Empty);
            parameters.Add("@Amount", model.amount);
            parameters.Add("@TransactionId", model.merchantInfoTxn ?? string.Empty);
            parameters.Add("@PaymentId", model.TransactionId ?? string.Empty);
            parameters.Add("@Type", model.Type ?? string.Empty);
            parameters.Add("@status", model.status ?? string.Empty);
            parameters.Add("@description", model.description ?? string.Empty);
            parameters.Add("@optr_txn_id", model.optr_txn_id ?? string.Empty);
            parameters.Add("@response", model.response ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("Proc_Recharge_V2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }


        public async Task<ResultSet> ImageUpload(string FilePath, string doctype, long memberId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@FilePath", FilePath ?? string.Empty);
            parameters.Add("@doctype", doctype ?? string.Empty);
            parameters.Add("@memberId", memberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("ImageUpload", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }


        public async Task<AllLedgerResponse> AllLedger(long MemberId, string type, int monthId, int yearId)
        {
            AllLedgerResponse obj = new AllLedgerResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            parameters.Add("@type", type);
            parameters.Add("@monthId", monthId);
            parameters.Add("@yearId", yearId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("Proc_GetLedger", parameters, commandType: CommandType.StoredProcedure);

                    var summary = result.Read<ResultCommon>().ToList();
                    var transactiondetails = result.Read<ResultCommon>().ToList();
                    obj.summarydata = summary;
                    obj.transactiondata = transactiondetails;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<AllLedgerResponse> LevelWiseLedger(long MemberId, string type, int monthId, int yearId)
        {
            AllLedgerResponse obj = new AllLedgerResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            parameters.Add("@type", type);
            parameters.Add("@monthId", monthId);
            parameters.Add("@yearId", yearId);


            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("Proc_GetLevelWiseLedger", parameters, commandType: CommandType.StoredProcedure);

                    var summary = result.Read<ResultCommon>().ToList();
                    var transactiondetails = result.Read<ResultCommon>().ToList();
                    obj.summarydata = summary;
                    obj.transactiondata = transactiondetails;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<List<FCM>> GetNotification(long MemberId)
        {
            List<FCM> obj = new List<FCM>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", MemberId);


            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<FCM>("GetFCMNotification", parameters, commandType: CommandType.StoredProcedure);


                    obj = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }
        public async Task<NotificationCount> GetNoticationCount(long MemberId)
        {
            NotificationCount obj = new NotificationCount();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", MemberId);


            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<NotificationCount>("GetNotificationCount", parameters, commandType: CommandType.StoredProcedure);


                    obj = result.SingleOrDefault();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }
        public async Task<Common> ReadNotification(ReadNotification model)
        {
            Common obj = new Common();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", model.memberId);
            parameters.Add("@Pk_NotificationId", model.pk_NotificationId);
            parameters.Add("@Type", model.type);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<Common>("ReadNotification", parameters, commandType: CommandType.StoredProcedure);
                    obj = result.SingleOrDefault();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<List<BindDropDown>> GetDropDown(int ProcId, string dataId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProcId", ProcId);
            parameters.Add("@dataId", dataId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                var r = await con.QueryAsync<BindDropDown>("BindDropDown", parameters, commandType: CommandType.StoredProcedure);
                var result = r.ToList();
                con.Close();
                return result;
            }
        }
        public async Task<List<BindDropDown2>> GetDropDown2(int ProcId, string dataId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProcId", ProcId);
            parameters.Add("@dataId", dataId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                var r = await con.QueryAsync<BindDropDown2>("BindDropDown", parameters, commandType: CommandType.StoredProcedure);
                var result = r.ToList();
                con.Close();
                return result;
            }
        }
        public Task<InsuranceEcomm> GetInsuranceUrl(int serviceId, long memberId)
        {
            throw new NotImplementedException();
        }

        public async Task<Common> FreeCardCostProduct(FREECARDCOSTRequest model)
        {
            Common result = new Common();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProcId", model.ProcId);
            parameters.Add("@MobileNo", model.MobileNo);
            parameters.Add("@ProductId", model.ProductId);
            parameters.Add("@memberId", model.memberId);
            parameters.Add("@transactionId", model.transactionId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<Common>("Proc_AssignProduct", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<List<CardLedgerDetails>> CardLedger(long MemberId, string type)
        {
            List<CardLedgerDetails> obj = new List<CardLedgerDetails>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            parameters.Add("@type", type);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var r = await con.QueryAsync<CardLedgerDetails>("CardLedger", parameters, commandType: CommandType.StoredProcedure);
                    obj = r.ToList();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<Common> BlockCard(BlockCardRequest model)
        {
            Common result = new Common();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Fk_IssueId", model.reasonId);
            parameters.Add("@BlockedBy", model.memberId);
            parameters.Add("@transactionId", model.transactionId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<Common>("Proc_BlockCard", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }

        }

        public async Task<CardMasterResponse> GetCardDetails(long MemberId)
        {
            CardMasterResponse result = new CardMasterResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var data = await con.QueryMultipleAsync("Proc_GetMasterCardDetails", parameters, commandType: CommandType.StoredProcedure);

                    var freecard = data.Read<FreeCardResponse>().FirstOrDefault();
                    var carddata = data.Read<CardMaster>().FirstOrDefault();
                    var blockcarddata = data.Read<BindDropDown>().ToList();
                    var upgradecarddata = data.Read<BindDropDown2>().ToList();
                    result.freecard = freecard;
                    result.carddata = carddata;
                    result.blockcard = blockcarddata;
                    result.upgradecard = upgradecarddata;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<ResultSet> VerifyPhysicalCard(string Cardno, long MemberId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@cardno", Cardno);
            parameters.Add("@memberId", MemberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("Proc_CheckExistingPhysicalCard", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<ResultSet> CreateRazorpayXContact(CreateRazorPayContactRequest model, int procId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@procId", procId);
            parameters.Add("@name", model.name ?? string.Empty);
            parameters.Add("@email", model.email ?? string.Empty);
            parameters.Add("@contact", model.contact ?? string.Empty);
            parameters.Add("@type", model.type ?? string.Empty);
            parameters.Add("@reference_id", model.reference_id ?? string.Empty);
            parameters.Add("@MemberId", model.memberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("Proc_CreateRazorPayContact_V2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<ResultSet> UpdateRazorpayXContact(CreateContactResponse model, int procId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@procId", procId);
            parameters.Add("@name", model.name ?? string.Empty);
            parameters.Add("@email", model.email ?? string.Empty);
            parameters.Add("@contact", model.contact ?? string.Empty);
            parameters.Add("@type", model.type ?? string.Empty);
            parameters.Add("@reference_id", model.reference_id ?? string.Empty);
            parameters.Add("@id", model.id ?? string.Empty);
            parameters.Add("@created_at", model.created_at ?? string.Empty);
            parameters.Add("@acitve", model.active);
            parameters.Add("@MemberId", 0);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("Proc_CreateRazorPayContact_V2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<ResultSet> UpdateRazorpayXFundAccount(FundAccountResponse model)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@ContactId", model.contact_id ?? string.Empty);
            parameters.Add("@bankId", model.id ?? string.Empty);
            parameters.Add("@account_type", model.account_type ?? string.Empty);
            parameters.Add("@ifsc", model.bank_account.ifsc ?? string.Empty);
            parameters.Add("@bank_name", model.bank_account.bank_name ?? string.Empty);
            parameters.Add("@account_number", model.bank_account.account_number ?? string.Empty);
            parameters.Add("@entity", model.entity ?? string.Empty);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("updatefundAccountDetails_V2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }
        public async Task<ResultSet> insertpayout(payoutsRequest model, int procId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@procId", procId);
            parameters.Add("@payoutId", model.payoutId ?? string.Empty);
            parameters.Add("@fundId", model.fund_account_id ?? string.Empty);
            parameters.Add("@entity", model.entity ?? string.Empty);
            parameters.Add("@amount", model.amount);
            parameters.Add("@mode", model.mode);
            parameters.Add("@type", model.purpose ?? string.Empty);
            parameters.Add("@created_at", model.created_at ?? string.Empty);
            parameters.Add("@status", model.status);
            parameters.Add("@Pk_CustomerPayoutId", model.Pk_CustomerPayoutId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("insertpayout_V2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }
        public async Task<List<cardamountdetails>> Cardamountdetails(long Id, string type)
        {
            List<cardamountdetails> result = new List<cardamountdetails>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", Id);



            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<cardamountdetails>("CardAmountData", parameters, commandType: CommandType.StoredProcedure);
                    result = res.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }

        }

        public async Task<List<CommissionTransferdetail>> GetCommissionTransfer(long MemberId)
        {
            List<CommissionTransferdetail> result = new List<CommissionTransferdetail>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<CommissionTransferdetail>("Proc_GetCommissionTransfered_V2", parameters, commandType: CommandType.StoredProcedure);
                    result = res.ToList();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }

        }

        public async Task<CommissionDetails> transferedCommissionDetails(long memberId, decimal amount)
        {
            CommissionDetails result = new CommissionDetails();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);
            parameters.Add("@amount", amount);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    result = await con.QueryFirstAsync<CommissionDetails>("Proc_CommissionTransfereddetails", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }

        }

        public async Task<Commissionresponse> transfercommission(int ProcId, long memberId, decimal amount, string accountnumber, string transId, long commissionId
           , long tdsId, long transferchargeId, long transferCommissionId)
        {
            Commissionresponse result = new Commissionresponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProcId", ProcId);
            parameters.Add("@memberId", memberId);
            parameters.Add("@amount", amount);
            parameters.Add("@accountnumber", accountnumber);
            parameters.Add("@transId", transId);
            parameters.Add("@commissionId", commissionId);
            parameters.Add("@tdsId", tdsId);
            parameters.Add("@transferchargeId", transferchargeId);
            parameters.Add("@transferCommissionId", transferCommissionId);


            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<Commissionresponse>("Proc_Commissiontransferv2", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<List<Information>> GetInformationMessage(long memberId, string appType, string appVersion)
        {
            List<Information> objres = new List<Information>();
            DynamicParameters parameters = new DynamicParameters();


            parameters.Add("@memberId", memberId);
            parameters.Add("@appType", appType);
            parameters.Add("@appVersion", appVersion);


            using (con = new SqlConnection(_connectionString.Value))
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                var result = await con.QueryAsync<Information>("GetInformationMessage_V2", parameters, commandType: CommandType.StoredProcedure);
                objres = result.ToList();
                con.Close();

                return objres;
            }
        }

        public async Task<List<FAQ>> FAQ(long Id)
        {

            DynamicParameters parameters = new DynamicParameters();


            parameters.Add("@Id", Id);
            List<FAQ> res = new List<FAQ>();
            using (con = new SqlConnection(_connectionString.Value))
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                var result = await con.QueryAsync<FAQ>("GetFAQV2", parameters, commandType: CommandType.StoredProcedure);
                res = result.ToList();
                con.Close();
                return res;
            }
        }

        public async Task<OrderDetailResponse> OrderDetail(long TransId)
        {
            OrderDetailResponse result = new OrderDetailResponse();
            result.orderdetails = new OrderDetails();
            result.orderhead = new OrderHead();
            result.orderstatus = new OrderStatus();
            result.payment = new Model.Payment();
            result.summarydetails = new SummaryDetails();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@transId", TransId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    var r = await con.QueryMultipleAsync("OrderDetail", parameters, commandType: CommandType.StoredProcedure);
                    var orderhead = r.Read<CommonOrderdata>().ToList();
                    var orderdetails = r.Read<CommonOrderdata>().ToList();
                    var orderstatus = r.Read<CommonOrderdata>().ToList();
                    var summarydetails = r.Read<CommonOrderdata>().ToList();
                    var payment = r.Read<CommonOrderdata>().ToList();
                    var help = r.Read<Helpdetail>().FirstOrDefault();
                    result.orderhead.data = orderhead;
                    result.orderdetails.data = orderdetails;
                    result.orderstatus.data = orderstatus;
                    result.summarydetails.data = summarydetails;
                    result.payment.data = payment;
                    result.helpdetails = help;


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<Common> SupportRequest(SupportRequest model)
        {
            Common result = new Common();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Fk_SupportImageId", model.Fk_SupportImageId);
            parameters.Add("@Fk_SupportTypeId", model.Fk_SupportTypeId);
            parameters.Add("@IssueMessage", model.IssueMessage);
            parameters.Add("@CreatedBy", model.CreatedBy);
            parameters.Add("@RepliedBy", model.RepliedBy);


            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<Common>("Proc_SupportRequest", parameters, commandType: CommandType.StoredProcedure);

                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<List<SupportRequestdata>> SupportRequestStatus(long memberId)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@memberId", memberId);


            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<SupportRequestdata>("GetSupportRequestById", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result.ToList();
            }
        }

        public async Task<List<SupportMessageDetails>> SupportRequestByTicket(string TicketNo)
        {
            DynamicParameters parameters = new DynamicParameters();


            parameters.Add("@TicketNo", TicketNo);

            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<SupportMessageDetails>("SupportMessageByTicketNo", parameters, commandType: CommandType.StoredProcedure);

                con.Dispose();

                return result.ToList();
            }
        }

        public async Task<Common> RepliedMessage(ReplyMessage model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Pk_TicketId", model.Pk_TicketId);
            parameters.Add("@ReplyMessage", model.RepliedMessage);
            parameters.Add("@RepliedBy", model.RepliedBy);
            parameters.Add("@CreatedBy", model.CreatedBy);
            parameters.Add("@Fk_SupportImageId", model.Fk_SupportImageId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<Common>("Proc_ReplyMessage", parameters, commandType: CommandType.StoredProcedure);

                con.Dispose();

                return result.SingleOrDefault();
            }
        }

        public async Task<List<PendingRechargeRequest>> GetPendingRecharge()
        {
            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<PendingRechargeRequest>("Proc_GetPendingRecharge", null, commandType: CommandType.StoredProcedure);

                con.Dispose();

                return result.ToList();
            }
        }

        public async Task<ResultSet> PineCardHistroy(long memberId, string externalId, string request, string response, string Token, string APIName)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ExternalTransId", externalId);
            parameters.Add("@memberId", memberId);
            parameters.Add("@Request", request);
            parameters.Add("@Response", response);
            parameters.Add("@Token", Token);
            parameters.Add("@APIName", APIName);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<ResultSet>("Proc_PineCardHistroy", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;



            }
        }

        public async Task<ResultSet> GetCardReference(long memberId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@memberId", memberId);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<ResultSet>("Proc_GetCardNo", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;



            }
        }

        public async Task<ResultSet> Getpinetoken(int procId, string token, string expirytime)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@ProcId", procId);
            parameters.Add("@Token", token);
            parameters.Add("@tokenExpiryTime", expirytime);

            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryFirstOrDefaultAsync<ResultSet>("Proc_pinecardtoken", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<List<CommissionModel>> GetGeneratedCommission(long Id)
        {
            List<CommissionModel> result = new List<CommissionModel>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", Id);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                var r = await con.QueryAsync<CommissionModel>("Proc_GetCommissionForNotification", parameters, commandType: CommandType.StoredProcedure);
                result = r.ToList();
                con.Dispose();
                return result;



            }
        }


        public async Task<DashboardV2Response> DashboardDetailV2(long memberId)
        {
            DashboardV2Response result = new DashboardV2Response();


            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    var r = await con.QueryMultipleAsync("Proc_GetDashboardDetailV2", parameters, commandType: CommandType.StoredProcedure);
                    result = r.Read<DashboardV2Response>().FirstOrDefault();
                    var topbanner = r.Read<banner>().ToList();
                    var topheader = r.Read<DashboardCommondata>().ToList();
                    var data1 = r.Read<DashboardCommondata>().ToList();
                    var middlebanner = r.Read<banner>().ToList();
                    var data2 = r.Read<DashboardCommondata>().ToList();
                    var data3 = r.Read<DashboardCommondata>().ToList();
                    result.topheader = new Topheader();
                    result.data1 = new Data1();
                    result.data2 = new Data2();
                    result.data3 = new Data3();
                    result.topheader = new Topheader();

                    result.topbanner = topbanner;
                    result.topheader.data = topheader;
                    result.middlebanner = middlebanner;
                    result.data1.data = data1;
                    result.data2.data = data2;
                    result.data3.data = data3;



                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<ProductDetailsResponse> ProductDetail(long productDetailsId)
        {
            ProductDetailsResponse result = new ProductDetailsResponse();
            ProductDetails pd = new ProductDetails();
            List<ImageList> l = new List<ImageList>();
            List<ProductReviews> r = new List<ProductReviews>();
            List<sizedetails> sizedetails = new List<sizedetails>();
            List<Colordetails> Colordetails = new List<Colordetails>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@productDetailsId", productDetailsId);

            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    var res = await con.QueryMultipleAsync("ProductDetails_V2", parameters, commandType: CommandType.StoredProcedure);

                    var productdetails = res.ReadSingle<ProductDetails>();
                    var imagelist = res.Read<ImageList>();
                    var productreviews = res.Read<ProductReviews>();
                    var sizedetailslist = res.Read<sizedetails>();
                    var Colordetailslist = res.Read<Colordetails>();
                    pd = productdetails;
                    pd.images = imagelist.ToList();
                    pd.productReviews = productreviews.ToList();
                    pd.sizedetails = sizedetailslist.ToList();
                    pd.Colordetails = Colordetailslist.ToList();
                    result.productdetails = pd;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<Common> AddCartItem(CartRequest model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProductId", model.ProductId);
            parameters.Add("@SizeId", model.SizeId);
            parameters.Add("@Fk_ProductDetailId", model.ProductDetailId);
            parameters.Add("@productQty", model.ProductQty);
            parameters.Add("@ProductAmt", model.ProductAmt);
            parameters.Add("@CreatedBy", model.CreatedBy);
            parameters.Add("@ColorId", model.ColorId);
            parameters.Add("@RequestType", model.RequestType);


            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<Common>("Api_AddCartItem", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result.FirstOrDefault();
            }
        }

        public async Task<List<CartList>> GetCartItem(long UserId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CreatedBy", UserId);
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<CartList>("ApI_CartItemList", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result.ToList();
            }
        }

        public async Task<Common> RemoveCartItem(long Pk_CartItemId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Pk_CartItemId", Pk_CartItemId);
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<Common>("ApI_CartItemRemove", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result.SingleOrDefault();
            }
        }

        public async Task<Common> UpdateProductQuantity(long Pk_CartItemId, string type)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Pk_CartItemId", Pk_CartItemId);
            parameters.Add("@type", type);
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<Common>("UpdateProductQuantity", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result.SingleOrDefault();
            }
        }

        public async Task<Common> CheckDeliveryAddress(long ProductId, string pincode)
        {
            DynamicParameters parameters = new DynamicParameters();
            Common results = new Common();
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                parameters.Add("@ProductId", ProductId);
                parameters.Add("@Pincode", pincode);
                var sqlData = await con.QueryAsync<Common>("CheckDeliveryAddress", parameters, commandType: CommandType.StoredProcedure);
                results = sqlData.SingleOrDefault();

                con.Close();
                return results;
            }
        }

        public async Task<Common> AddReview(Review model)
        {
            DynamicParameters parameters = new DynamicParameters();
            Common results = new Common();
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                parameters.Add("@productId", model.productId);
                parameters.Add("@userId", model.userId);
                parameters.Add("@rating", model.rating);
                parameters.Add("@review", model.review);
                var sqlData = await con.QueryAsync<Common>("AddReview", parameters, commandType: CommandType.StoredProcedure);
                results = sqlData.SingleOrDefault();

                con.Close();
                return results;
            }
        }

        public async Task<List<ProductList>> ProductList(int? CategoryId, string BrandId, int? SizeId, int? ColorId, int? SubategoryId, int Offset, int Page, string Sort)
        {
            List<ProductList> results = new List<ProductList>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", CategoryId);
            parameters.Add("@BrandId", BrandId);
            parameters.Add("@SizeId", SizeId);
            parameters.Add("@Sort", Sort);
            parameters.Add("@offset", Offset);
            parameters.Add("@page", Page);
            parameters.Add("@Subcategory", SubategoryId);

            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var sqlData = await con.QueryAsync<ProductList>("Api_ProductList", parameters, commandType: CommandType.StoredProcedure);
                results = sqlData.ToList();
                con.Close();
                return results;
            }
        }
        public async Task<ProductData> ProductList_V2(int? CategoryId, string BrandId, int? SizeId, int? ColorId, int? SubategoryId, int Offset, int Page, string Sort, int SearchTypeId, int SearchId)
        {
            ProductData results = new ProductData();
            List<ProductList> productLists = new List<ProductList>();
            List<sort> sorts = new List<sort>();
            List<filter> filters = new List<filter>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", CategoryId);
            parameters.Add("@BrandId", BrandId);
            parameters.Add("@SizeId", SizeId);
            parameters.Add("@Sort", Sort);
            parameters.Add("@offset", Offset);
            parameters.Add("@page", Page);
            parameters.Add("@Subcategory", SubategoryId);
            parameters.Add("@SearchTypeId", SearchTypeId);
            parameters.Add("@SearchId", SearchId);

            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var sqlData = await con.QueryMultipleAsync("Api_ProductList_V2", parameters, commandType: CommandType.StoredProcedure);
                var productlist = sqlData.Read<ProductList>();
                var sortlist = sqlData.Read<sort>();
                var filterlist = sqlData.Read<filter>();
                productLists = productlist.ToList();
                sorts = sortlist.ToList();
                filters = filterlist.ToList();
                con.Close();


                results.products = productLists;
                results.sort = sorts;
                results.filter = filters;

                return results;
            }
        }

        public async Task<ReviewedUser> ReviewUser(long memberId)
        {
            DynamicParameters parameters = new DynamicParameters();
            ReviewedUser results = new ReviewedUser();
            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                parameters.Add("@memberId", memberId);

                var sqlData = await con.QueryAsync<ReviewedUser>("GetMemberNameAndImage", parameters, commandType: CommandType.StoredProcedure);
                results = sqlData.SingleOrDefault();

                con.Close();
                return results;
            }
        }

        public async Task<vocherresponse> GetVoucherBrand(string Brand)
        {
            DynamicParameters parameters = new DynamicParameters();
            vocherresponse vocherresponse = new vocherresponse();
            VoucherModel voucherModels = new VoucherModel();
            List<VoucherPriceDetails> results = new List<VoucherPriceDetails>();
            using (con = new SqlConnection(_connectionString.Value))

            {
                con.Open();
                parameters.Add("@Brand", Brand);

                var sqlData = await con.QueryMultipleAsync("Api_GetVoucherByBrandId", parameters, commandType: CommandType.StoredProcedure);
                var voucher = sqlData.Read<VoucherModel>();
                var pricelist = sqlData.Read<VoucherPriceDetails>();
                var voucherdetails = voucher.SingleOrDefault();
                vocherresponse.brandName = voucherdetails.brandName;
                vocherresponse.url = voucherdetails.url;
                vocherresponse.price = pricelist.ToList();

                con.Close();
                return vocherresponse;
            }
        }

        public async Task<Common> UserAddress(UserAddress model)
        {
            DynamicParameters parameters = new DynamicParameters();
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                parameters.Add("@Pk_AddressId", model.Pk_AddressId);
                parameters.Add("@Name", model.Name);
                parameters.Add("@Mobile", model.Mobile);
                parameters.Add("@Pincode", model.Pincode);
                parameters.Add("@Locality", model.Location);
                parameters.Add("@Address", model.Address);
                parameters.Add("@state", model.state);
                parameters.Add("@city", model.city);
                parameters.Add("@Landmark", model.Landmark);
                parameters.Add("@Alternate", model.Alternate);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@STATUS", model.STATUS);
                parameters.Add("@AddressType", model.AddressType);

                var result = await con.QueryAsync<Common>("Proc_UserAddress", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result.SingleOrDefault();
            }
        }
        public async Task<Common> DeleteUserAddress(UserAddress model)
        {
            DynamicParameters parameters = new DynamicParameters();
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                parameters.Add("@Pk_AddressId", model.Pk_AddressId);


                var result = await con.QueryAsync<Common>("DeleteUserAddress", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result.SingleOrDefault();
            }
        }

        public async Task<UserAddressresponse> GetUserAddress(long memberId, string RequestType)
        {
            DynamicParameters parameters = new DynamicParameters();
            UserAddressresponse results = new UserAddressresponse();
            List<UserAddress> userAddresses = new List<UserAddress>();
            List<UserOrderSummary> userOrderSummaries = new List<UserOrderSummary>();
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                parameters.Add("@UserId", memberId);
                parameters.Add("@RequestType", RequestType);

                var sqlData = await con.QueryMultipleAsync("GetUserAddress", parameters, commandType: CommandType.StoredProcedure);
                var address = sqlData.Read<UserAddress>();
                var orders = sqlData.Read<UserOrderSummary>();
                userAddresses = address.ToList();
                userOrderSummaries = orders.ToList();
                results.address = userAddresses;
                results.purchasedetails = userOrderSummaries;
                con.Close();
                return results;
            }
        }

        public async Task<OrderResponse> CreateOrder(CreateOrder model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", model.CustomerId);
            parameters.Add("@TotalAmount", model.TotalAmount);
            parameters.Add("@TaxAmount", model.TaxAmount);
            parameters.Add("@IsCouponApplied", model.IsCouponApplied);
            parameters.Add("@CouponAmount", model.CouponAmount);
            parameters.Add("@ShippingCharges", model.ShippingCharges);
            parameters.Add("@PaymentId", model.PaymentId);
            parameters.Add("@paymentstatus", model.paymentstatus);
            parameters.Add("@status", model.status);
            parameters.Add("@Fk_AddressId", model.Fk_AddressId);
            parameters.Add("@PaidAmount", model.PaidAmount);
            parameters.Add("@cartitemxml", model.cartitem);

            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<OrderResponse>("Proc_CustomerOrder", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result.SingleOrDefault();
            }
        }

        public async Task<Common> RedeemPoint(long Id, int point, long orderId, string PaymentId, decimal amount, decimal commpayout)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userId", Id);
            parameters.Add("@orderId", orderId);
            parameters.Add("@point", point);
            parameters.Add("@PaymentId", PaymentId);
            parameters.Add("@amount", amount);
            parameters.Add("@commpayout", commpayout);

            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<Common>("RedeemUserPoint", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result.SingleOrDefault();
            }
        }

        public async Task<MallOrderDetail> MallOrderDetail(string paymentId)
        {
            DynamicParameters parameters = new DynamicParameters();
            MallOrderDetail results = new MallOrderDetail();
            customerOrder customerOrder = new customerOrder();
            List<customerOrderItem> customerOrderItem = new List<customerOrderItem>();
            parameters.Add("@paymentId", paymentId);
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();


                var sqlData = await con.QueryMultipleAsync("Api_OrderDetails", parameters, commandType: CommandType.StoredProcedure);
                var data1 = sqlData.Read<customerOrder>();
                var data2 = sqlData.Read<customerOrderItem>();
                customerOrder = data1.SingleOrDefault();
                customerOrderItem = data2.ToList();
                results.customerOrder = customerOrder;
                results.customerOrderItem = customerOrderItem;
                con.Close();
                return results;
            }
        }

        public async Task<List<mallOrderStatus>> Getorderstatus(string orderno, string vendorId, long ProductId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Orderno", orderno);
            parameters.Add("@VendorId", vendorId);
            parameters.Add("@ProductId", ProductId);
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<mallOrderStatus>("GetOrderStatus_V2 ", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result.ToList();
            }
        }
        public async Task<List<productstatus>> GetorderstatusV3(string orderno, string vendorId, long ProductId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Orderno", orderno);
            parameters.Add("@VendorId", vendorId);
            parameters.Add("@ProductId", ProductId);
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<productstatus>("GetOrderStatus_V3 ", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result.ToList();
            }
        }
        public async Task<CancelOrderResponse> CancelOrder(long pk_OrderItemId, long memberId, string reason)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Pk_OrderItemId", pk_OrderItemId);
            parameters.Add("@memberId", memberId);
            parameters.Add("@reason", reason);
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<CancelOrderResponse>("Api_CancelOrderV2 ", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return result.SingleOrDefault();
            }
        }

        public async Task<Common> CreatePoint(long Id, int point, long orderId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userId", Id);
            parameters.Add("@orderId", orderId);
            parameters.Add("@point", point);

            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<Common>("CreateUserPoint", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result.SingleOrDefault();
            }
        }

        public async Task<VoucherRequest> checkvoucher(long MemberId, long VoucherId)
        {
            VoucherRequest result = new VoucherRequest();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@VoucherId", VoucherId);
            parameters.Add("@MemberId", MemberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    var res = await con.QueryAsync<VoucherRequest>("GetVoucherAmount", parameters, commandType: CommandType.StoredProcedure);
                    result = res.FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return result;
            }
        }

        public async Task<Common> UpdateVoucherV2(VoucherResponse model, string voucherdetails)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@VoucherId", model.VoucherId);
            parameters.Add("@ErrorCode", model.vPullVouchersResult.ErrorCode);
            parameters.Add("@ErrorMessage", model.vPullVouchersResult.ErrorMessage);
            parameters.Add("@ExternalOrderIdOut", model.vPullVouchersResult.ExternalOrderIdOut);
            parameters.Add("@Message", model.vPullVouchersResult.Message);
            parameters.Add("@ProductGuid", model.vPullVouchersResult.PullVouchers[0].ProductGuid);
            parameters.Add("@ProductName", model.vPullVouchersResult.PullVouchers[0].ProductName);
            parameters.Add("@VoucherName", model.vPullVouchersResult.PullVouchers[0].VoucherName);
            parameters.Add("@EndDate", model.vPullVouchersResult.PullVouchers[0].Vouchers[0].EndDate);
            parameters.Add("@Value", model.vPullVouchersResult.PullVouchers[0].Vouchers[0].Value);
            parameters.Add("@VoucherGCcode", model.vPullVouchersResult.PullVouchers[0].Vouchers[0].VoucherGCcode);
            parameters.Add("@VoucherGuid", model.vPullVouchersResult.PullVouchers[0].Vouchers[0].VoucherGuid);
            parameters.Add("@VoucherNo", model.vPullVouchersResult.PullVouchers[0].Vouchers[0].VoucherNo);
            parameters.Add("@VoucherPin", model.vPullVouchersResult.PullVouchers[0].Vouchers[0].Voucherpin);
            parameters.Add("@ResultType", model.vPullVouchersResult.ResultType);
            parameters.Add("@PaymentId", model.paymentId);
            parameters.Add("@MemberId", model.MemberId);
            parameters.Add("@Pk_CartId", model.Pk_CartId);
            parameters.Add("@Discountamount", model.Discountamount);
            parameters.Add("@Response", model.response);
            parameters.Add("@voucherdetails", voucherdetails);
            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<Common>("Proc_BuyVoucher", parameters, commandType: CommandType.StoredProcedure);

                con.Dispose();

                return result.FirstOrDefault();
            }
        }

        public async Task<Common> AddVoucherItem(vouchercart model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", model.MemberId);
            parameters.Add("@Qty", model.Quantity);
            parameters.Add("@TotalAmount", model.TotalAmount);
            parameters.Add("@VoucherId", model.VoucherId);
            parameters.Add("@paymentId", model.paymentId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<Common>("AddItemVocherCart", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result.SingleOrDefault();
            }
        }

        public async Task<MallOrderDetail> GetPaymentMethod(string paymentId)
        {
            MallOrderDetail objres = new MallOrderDetail();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@paymentId", paymentId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var sqlData = await con.QueryMultipleAsync("GetPaymentMethod", parameters, commandType: CommandType.StoredProcedure);
                objres.paymentMethod = sqlData.Read<PaymentMethod>().FirstOrDefault();
                objres.helpdetails = sqlData.Read<Helpdetail>().FirstOrDefault();


                con.Close();
                return objres;


            }
        }

        public async Task<Replacement> GetReplacementItem(long pk_OrderItemId)
        {
            DynamicParameters parameters = new DynamicParameters();
            Replacement replacement = new Replacement();
            ReplacementItem replacementItem = new ReplacementItem();
            List<ReplacementReason> replacementReasons = new List<ReplacementReason>();

            parameters.Add("@pk_OrderItemId", pk_OrderItemId);
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var sqlData = await con.QueryMultipleAsync("GetReplacementProduct", parameters, commandType: CommandType.StoredProcedure);
                var r = sqlData.ReadSingle<ReplacementItem>();
                var r2 = sqlData.Read<ReplacementReason>();
                con.Close();
                replacement.ReplacementItem = r;
                replacement.ReplacementReason = r2.ToList();
                return replacement;
            }
        }

        public async Task<Common> ReplacementRequest(ReplacementRequest model)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Fk_OrderItemId", model.Fk_OrderItemId);
            parameters.Add("@Fk_ReasonId", model.Fk_ReasonId);
            parameters.Add("@Description", model.Description);
            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                var sqlData = await con.QueryAsync<Common>("PROC_ReplacementRequest", parameters, commandType: CommandType.StoredProcedure);
                con.Close();
                return sqlData.SingleOrDefault();
            }
        }

        public async Task<Common> GetCartCount(long memberId)
        {
            DynamicParameters parameters = new DynamicParameters();

            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                parameters.Add("@memberId", memberId);
                parameters.Add("@Type", "android");

                var sqlData = await con.QueryAsync<Common>("GetCartItemCount", parameters, commandType: CommandType.StoredProcedure);


                con.Close();
                return sqlData.SingleOrDefault();
            }
        }

        public async Task<List<filterdata>> GetFilterData(string type)
        {
            DynamicParameters parameters = new DynamicParameters();

            using (con = new SqlConnection(_mallconnectionString.Value))
            {
                con.Open();
                parameters.Add("@type", type.ToLower());

                var sqlData = await con.QueryAsync<filterdata>("GetFilterList", parameters, commandType: CommandType.StoredProcedure);


                con.Close();
                return sqlData.ToList();
            }
        }


        public async Task<MinistatementV2ResponseEcomm> GetMiniStatement_V2(long MemberId, int page, string transtype, string categorytype)
        {
            MinistatementV2ResponseEcomm result = new MinistatementV2ResponseEcomm();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            parameters.Add("@page", page);
            parameters.Add("@size", 50);
            parameters.Add("@transtype", transtype);
            parameters.Add("@categorytype", categorytype);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryMultipleAsync("GetMinistatementDetails_v3", parameters, commandType: CommandType.StoredProcedure);
                    result.transType = res.Read<CommonOrderdataEcomm>().ToList();
                    result.categoryType = res.Read<CommonOrderdataEcomm>().ToList();
                    result.data = res.Read<MinistatementEcomm>().ToList();


                }
                catch (Exception ex)
                {
                    string m = ex.Message;
                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<LedgerResultV2> LedgerV2(long MemberId, string Type, string commtype, string transtype, string categoryType, int monthId, int yearId, int page)
        {
            LedgerResultV2 obj = new LedgerResultV2();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            parameters.Add("@type", Type);
            parameters.Add("@commtype", commtype);
            parameters.Add("@transtype", transtype);
            parameters.Add("@categoryType", categoryType);
            parameters.Add("@monthId", monthId);
            parameters.Add("@yearId", yearId);
            parameters.Add("@page", page);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("Ledger_V3", parameters, commandType: CommandType.StoredProcedure);
                    obj.transtype = result.Read<CommonOrderdata>().ToList();
                    obj.categorytype = result.Read<CommonOrderdata>().ToList();

                    var balance = result.Read<LedgerBalance>().FirstOrDefault();
                    var balancedetails = result.Read<LedgerDetails>();


                    obj.balance = balance;
                    obj.balancedetails = balancedetails.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }


        public async Task<AllLedgerResponse> AllLedgerV2(long MemberId, string type, int monthId, int yearId, string Level)
        {
            AllLedgerResponse obj = new AllLedgerResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            parameters.Add("@type", type);
            parameters.Add("@monthId", monthId);
            parameters.Add("@yearId", yearId);
            parameters.Add("@Level", Level);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("Proc_GetLedgerV2", parameters, commandType: CommandType.StoredProcedure);

                    var summary = result.Read<ResultCommon>().ToList();
                    var transactiondetails = result.Read<ResultCommon>().ToList();
                    obj.summarydata = summary;
                    obj.transactiondata = transactiondetails;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }



        }
        public async Task<BusinessDashboardV2> BusinessDashboardV2(long MemberId)
        {
            BusinessDashboardV2 obj = new BusinessDashboardV2();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("Proc_GetBusinessDashboardDetails_V2", parameters, commandType: CommandType.StoredProcedure);

                    var BusinessLevel = result.Read<BusinessLevel>();
                    var Club = result.Read<Club>();
                    var userifno = result.ReadSingle<userifnoV2>();

                    obj.topdata = result.Read<CommonOrderdata>().ToList();
                    obj.userifno = userifno;
                    obj.CugSize = BusinessLevel.ToList();
                    obj.Club = Club.ToList();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }

        public async Task<CustomerHelpResponse> GetHelp(HelpRequest model)
        {
            CustomerHelpResponse response = new CustomerHelpResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@transId", model.transId);
            parameters.Add("@Type", model.type);
            parameters.Add("@ProcId", model.procId);
            parameters.Add("@ImageId", model.imageId);
            parameters.Add("@RequestTypeId", model.requestTypeId);
            parameters.Add("@subsupportId", model.subsupportId);
            parameters.Add("@Message", model.message ?? string.Empty);
            parameters.Add("@MemberId", model.memberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryMultipleAsync("GetHelp", parameters, commandType: CommandType.StoredProcedure);
                    response.assistantDetails = res.Read<AssistantDetails>().SingleOrDefault();
                    response.TopData = res.Read<CustomerDetail>().SingleOrDefault();
                    response.TopData.data = res.Read<CommonOrderdata>().ToList();
                    response.replydata = res.Read<CustomerHelpReplyResponse>().ToList();


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return response;
            }
        }

        public async Task<List<ViewTicket>> ViewTicket(long memberId)
        {
            List<ViewTicket> viewTickets = new List<ViewTicket>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<ViewTicket>("ViewTickets", parameters, commandType: CommandType.StoredProcedure);
                    viewTickets = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return viewTickets;
            }
        }

        public async Task<ViewTicketMessages> ViewTicketMessages(long memberId, string ticketno)
        {
            ViewTicketMessages messages = new ViewTicketMessages();
            List<ViewTicketDetailMessages> replydata = new List<ViewTicketDetailMessages>();
            ViewTicket topheader = new ViewTicket();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);
            parameters.Add("@TicketNumber", ticketno);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryMultipleAsync("ViewTicketsMessage", parameters, commandType: CommandType.StoredProcedure);
                    var topheaderdata = result.ReadSingle<ViewTicket>();
                    var reply = result.Read<ViewTicketDetailMessages>();
                    messages.topheader = topheaderdata;
                    messages.replydata = reply.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return messages;
            }
        }

        public async Task<Common> AddCommissionTransferRequest(long MemberId, decimal Amount, decimal tds, string TransactionId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            parameters.Add("@TdsAmount", tds);
            parameters.Add("@Amount", Amount);
            parameters.Add("@TransactionId", TransactionId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<Common>("AddCommissionRequestData", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result.FirstOrDefault();
            }
        }

        public async Task<Common> AddActivityDetails(string IPAddress, string Request, string controller, string action, string url, string Response)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ControllerName", controller ?? string.Empty);
            parameters.Add("@ActionName", action ?? string.Empty);
            parameters.Add("@Url", url ?? string.Empty);
            parameters.Add("@IPAddress", IPAddress ?? string.Empty);
            parameters.Add("@RequestData", Request ?? string.Empty);
            parameters.Add("@Response", Response ?? string.Empty);

            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryAsync<Common>("AddApiRequest", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result.FirstOrDefault();
            }
        }

        public async Task<ResultSet> BillRecharge(VenusBillPayRequest model)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@procId", model.ProcId);
            parameters.Add("@transId", model.Id);
            parameters.Add("@Fk_MemberId", model.MemberId);
            parameters.Add("@Number", model.CustomerId ?? string.Empty);
            parameters.Add("@OperatorCode", model.OpertorCode ?? string.Empty);
            parameters.Add("@Amount", model.Amount);
            parameters.Add("@TransactionId", model.merchantInfoTxn ?? string.Empty);
            parameters.Add("@PaymentId", model.TransactionNo ?? string.Empty);
            parameters.Add("@Type", model.Type ?? string.Empty);
            parameters.Add("@status", model.status ?? string.Empty);
            parameters.Add("@description", model.description ?? string.Empty);
            parameters.Add("@optr_txn_id", model.optr_txn_id ?? string.Empty);
            parameters.Add("@Response", model.response ?? string.Empty);
            parameters.Add("@MobileNo", model.MobileNo ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<ResultSet>("Proc_BillRecharge", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return result;
            }
        }
        //////////////////////////Version 3 Start
        public async Task<BusinessDashboardV3> BusinessDashboardV3(long MemberId)
        {
            BusinessDashboardV3 obj = new BusinessDashboardV3();
            obj.First = new CommonData();
            obj.Second = new CUGSize();
            obj.Third = new CommonData();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", MemberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    var result = await
                        con.QueryMultipleAsync("Proc_GetBusinessDashboardDetails_V3",
                        parameters, commandType: CommandType.StoredProcedure);
                    obj.image = result.Read<string>().FirstOrDefault();
                    obj.IsSuperUser = result.Read<bool>().FirstOrDefault();
                    obj.First.data = result.Read<CommonOrderdata>().ToList();
                    obj.Second.data = result.Read<BusinessLevel>().ToList();
                    obj.Third.data = result.Read<CommonOrderdata>().ToList();
                    obj.Fouth = result.Read<BusinessCommonIncome>().ToList();
                    obj.Fifth = result.Read<BusinessCommonIncome>().ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return obj;
            }
        }
        public async Task<DashboardV3Response> DashboardDetailV3(long memberId)
        {
            DashboardV3Response result = new DashboardV3Response();

            result.data1 = new Data1();
            result.data2 = new Data1();
            result.data3 = new Data1();
            result.data4 = new Data1();
            result.data5 = new Data1();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    var r = await con.QueryMultipleAsync("Proc_GetDashboardDetailV3", parameters, commandType: CommandType.StoredProcedure);
                    var card = r.Read<DashboardCardResponse>().FirstOrDefault();
                    var reward = r.Read<DashboardCommondata>().ToList();
                    var banner1 = r.Read<banner>().ToList();
                    var banner2 = r.Read<banner>().ToList();
                    var data1 = r.Read<DashboardCommondata>().ToList();
                    var data2 = r.Read<DashboardCommondata>().ToList();
                    var data3 = r.Read<DashboardCommondata>().ToList();
                    var data4 = r.Read<DashboardCommondata>().ToList();
                    var data5 = r.Read<DashboardCommondata>().ToList();
                    result.card = card;
                    result.reward = reward;
                    result.banner1 = banner1;
                    result.banner2 = banner2;
                    result.data1.data = data1;
                    result.data2.data = data2;
                    result.data3.data = data3;
                    result.data4.data = data4;
                    result.data5.data = data5;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<DashboardV3Response> DashboardDetailV4(long memberId)
        {
            DashboardV3Response result = new DashboardV3Response();

            result.data1 = new Data1();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    var r = await con.QueryMultipleAsync("Proc_GetDashboardDetailV4", parameters, commandType: CommandType.StoredProcedure);
                    var card = r.Read<DashboardCardResponse>().FirstOrDefault();
                    var reward = r.Read<DashboardCommondata>().ToList();
                    var banner1 = r.Read<banner>().ToList();
                    var banner2 = r.Read<banner>().ToList();
                    var data1 = r.Read<DashboardCommondata>().ToList();

                    result.card = card;
                    result.reward = reward;
                    result.banner1 = banner1;
                    result.banner2 = banner2;
                    result.data1.data = data1;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<DashboardV5ResponseEcomm> DashboardDetailV5(long memberId, string IsLocal)
        {
            DashboardV5ResponseEcomm result = new DashboardV5ResponseEcomm();
            result.voucher = new Data1Ecomm();
            result.mall = new Data1Ecomm();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);
            parameters.Add("@IsLocal", IsLocal);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    var r = await con.QueryMultipleAsync("sp_user_dashboard", parameters, commandType: CommandType.StoredProcedure);
                    var card = r.Read<DashboardCardResponseV5Ecomm>().FirstOrDefault();
                    var reward = r.Read<DashboardCommondataEcomm>().ToList();
                    var banner1 = r.Read<bannerEcomm>().ToList();
                    var banner2 = r.Read<bannerEcomm>().ToList();
                    var maindata = r.Read<DashboardCommondataEcomm>().ToList();
                    var voucher = r.Read<DashboardCommondataEcomm>().ToList();
                    var mall = r.Read<DashboardCommondataEcomm>().ToList();
                    var cardVarient = r.Read<BindDropDown2Ecomm>().ToList();
                    var ThriweList = r.Read<ThriweDataEcomm>().ToList();
                    var Thriwetext = r.Read<ThriweTextEcomm>().ToList();

                    result.card = card;
                    result.card.cardVarient = cardVarient;
                    result.reward = reward;
                    result.banner1 = banner1;
                    result.banner2 = banner2;
                    result.thriweList = ThriweList;
                    result.thriwetext = Thriwetext;
                    result.voucher.data = voucher;
                    result.mall.data = mall;
                    if (maindata != null && maindata.Count() > 1)
                    {
                        result.maindata = new List<TopheaderEcomm>();
                        var sublistHeader = maindata.Where(x => x.text == x.value).ToList();

                        int k = 0;
                        for (int i = 0; i < sublistHeader.Count; i++)
                        {

                            result.maindata.Add(new TopheaderEcomm { header = sublistHeader[i].text });
                            result.maindata[i].data = new List<DashboardCommondataEcomm>();
                            for (int j = k; j < maindata.Count; j++, k++)
                            {
                                if (result.maindata[i].header != maindata[j].value)
                                {
                                    result.maindata[i].data.Add(maindata[j]);
                                }
                                if (maindata[j].value == "View More")
                                {
                                    k++;
                                    break;
                                }

                            }
                        }

                    }

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }
        public async Task<RegistrationresponseEcomm> CustomerRegistrationV3(RegistrationV3Ecomm model)
        {
            RegistrationresponseEcomm res = new RegistrationresponseEcomm();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@InfodeviceId", model.deviceInfo.deviceId ?? string.Empty);
            parameters.Add("@InfodeviceType", model.deviceInfo.deviceType ?? string.Empty);
            parameters.Add("@os", model.deviceInfo.os ?? string.Empty);
            parameters.Add("@appVersion", model.deviceInfo.appVersion ?? string.Empty);
            parameters.Add("@telecom", model.deviceInfo.telecom ?? string.Empty);
            parameters.Add("@geoCode", model.deviceInfo.geoCode ?? string.Empty);
            parameters.Add("@appId", model.deviceInfo.appId ?? string.Empty);
            parameters.Add("@ipAddress", model.deviceInfo.ipAddress ?? string.Empty);
            parameters.Add("@location", model.deviceInfo.location ?? string.Empty);
            parameters.Add("@mobile", model.deviceInfo.mobile ?? string.Empty);


            parameters.Add("@ProcId", model.ProcId);
            parameters.Add("@MemberId", model.MemberId);
            parameters.Add("@MobileNo", model.MobileNo ?? string.Empty);
            parameters.Add("@InvitedBy", model.InvitedBy ?? string.Empty);
            parameters.Add("@Password", model.Password ?? string.Empty);
            parameters.Add("@Email", model.Email ?? string.Empty);
            parameters.Add("@FullName", model.FullName ?? string.Empty);
            parameters.Add("@Pincode", model.Pincode ?? string.Empty);
            parameters.Add("@State", model.State);
            parameters.Add("@City", model.City);
            parameters.Add("@DeviceId", model.DeviceId ?? string.Empty);
            parameters.Add("@OSId", model.OSId ?? string.Empty);
            parameters.Add("@DeviceType", model.DeviceType ?? string.Empty);
            parameters.Add("@DOB", model.Dob ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    var result = await con.QueryMultipleAsync("sp_user_registration", parameters, commandType: CommandType.StoredProcedure, commandTimeout: con.ConnectionTimeout);
                    var registrationresponse = result.ReadSingle<ResultSetEcomm>();
                    var loginresponse = result.ReadSingle<LoginResponseEcomm>();
                    res.CustomerRegistrationResponse = registrationresponse;
                    res.LoginResponse = loginresponse;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return res;
            }


        }


        //Added by maqsood on 29-12-2023
        public async Task<RegistrationresponseEcomm> CustomerRegistration_V2(RegistrationV3Ecomm model)
        {
            RegistrationresponseEcomm res = new RegistrationresponseEcomm();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@InfodeviceId", model.deviceInfo.deviceId ?? string.Empty);
            parameters.Add("@InfodeviceType", model.deviceInfo.deviceType ?? string.Empty);
            parameters.Add("@os", model.deviceInfo.os ?? string.Empty);
            parameters.Add("@appVersion", model.deviceInfo.appVersion ?? string.Empty);
            parameters.Add("@telecom", model.deviceInfo.telecom ?? string.Empty);
            parameters.Add("@geoCode", model.deviceInfo.geoCode ?? string.Empty);
            parameters.Add("@appId", model.deviceInfo.appId ?? string.Empty);
            parameters.Add("@ipAddress", model.deviceInfo.ipAddress ?? string.Empty);
            parameters.Add("@location", model.deviceInfo.location ?? string.Empty);
            parameters.Add("@mobile", model.deviceInfo.mobile ?? string.Empty);


            parameters.Add("@ProcId", model.ProcId);
            parameters.Add("@MemberId", model.MemberId);
            parameters.Add("@MobileNo", model.MobileNo ?? string.Empty);
            parameters.Add("@InvitedBy", model.InvitedBy ?? string.Empty);
            parameters.Add("@Password", model.Password ?? string.Empty);
            parameters.Add("@Email", model.Email ?? string.Empty);
            parameters.Add("@FullName", model.FullName ?? string.Empty);
            parameters.Add("@Pincode", model.Pincode ?? string.Empty);
            parameters.Add("@State", model.State);
            parameters.Add("@City", model.City);
            parameters.Add("@DeviceId", model.DeviceId ?? string.Empty);
            parameters.Add("@OSId", model.OSId ?? string.Empty);
            parameters.Add("@DeviceType", model.DeviceType ?? string.Empty);
            parameters.Add("@DOB", model.Dob ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    var result = await con.QueryMultipleAsync("sp_user_registration_v2", parameters, commandType: CommandType.StoredProcedure, commandTimeout: con.ConnectionTimeout);
                    var registrationresponse = result.ReadSingle<ResultSetEcomm>();
                    if (registrationresponse != null)
                    {
                        if (registrationresponse.flag == 1)
                        {
                            var loginresponse = result.ReadSingle<LoginResponseEcomm>();
                            res.LoginResponse = loginresponse;
                        }
                    }
                    res.CustomerRegistrationResponse = registrationresponse;


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return res;
            }


        }

        //Added by maqsood on 29-12-2023
        public async Task<(bool, string)> UserAccountVerification(int MemberId, string Email, string date)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);
            parameters.Add("@Email", Email);
            parameters.Add("@Date", date);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    var result = await con.QueryMultipleAsync("sp_CustomerEmailVerification", parameters, commandType: CommandType.StoredProcedure, commandTimeout: con.ConnectionTimeout);
                    var response = result.ReadSingle<ResultSetEcomm>();
                    return (response.flag == 1 ? true : false, response.msg);
                }
                catch (Exception ex)
                {
                    return (false, "We are facing some technical issues please try after some time");
                }
                finally
                {
                    con.Close();

                }
            }
        }
        public async Task<ResultSet> AddCardDispatchDetail(CardDispatchDetail model)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", model.memberId);
            parameters.Add("@NameonCard", model.NameonCard ?? string.Empty);
            parameters.Add("@Address1", model.Address1 ?? string.Empty);
            parameters.Add("@Address2", model.Address2 ?? string.Empty);
            parameters.Add("@PinCode", model.pincode ?? string.Empty);
            parameters.Add("@StateId", model.state);
            parameters.Add("@CityId", model.city);
            parameters.Add("@RefNo", model.refno ?? string.Empty);
            parameters.Add("@TransactionId", model.transactionId ?? string.Empty);
            parameters.Add("@ImgUrl", model.imgurl ?? string.Empty);
            parameters.Add("@CardImage", model.CardImageUrl ?? string.Empty);
            parameters.Add("@landmark", model.landmark ?? string.Empty);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("Proc_AddCardDispatchAddress", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<ResultSet> CheckNewUserCardStatus(long memberId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("Proc_CheckNewUserCardStatus", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<BankDetailResponse> GetBank(long memberId)
        {
            BankDetailResponse result = new BankDetailResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var r = await con.QueryMultipleAsync("Proc_GetBankStatusDetail", parameters, commandType: CommandType.StoredProcedure);
                    result.bankstatus = r.ReadSingle<BankDetailResponse>().bankstatus;
                    result.data = r.ReadSingle<BankDetail>();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }
        public async Task<ResultSet> CheckBankStatus(long memberId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var r = await con.QueryFirstAsync("proc_checkbankStatus", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<ResultSet> SaveWebHookString(string document, long MemberId, long Id)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@document", document ?? string.Empty);
            parameters.Add("@FK_memId", MemberId);
            parameters.Add("@Id", Id);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<ResultSet>("Proc_SaveWebHookResponse", parameters, commandType: CommandType.StoredProcedure);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return result;
            }
        }
        public async Task<PaymentOrderModel> GetPaymentOrderDetail(string OrderId)
        {
            PaymentOrderModel result = new PaymentOrderModel();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OrderId", OrderId ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<PaymentOrderModel>("Proc_GetPaymentOrderDetail", parameters, commandType: CommandType.StoredProcedure);

                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return result;
            }
        }

        public async Task<ResultSet> PaymentStatus(string paymentId, string type, string orderId, string status)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@orderid", orderId ?? string.Empty);
            parameters.Add("@status", status ?? string.Empty);
            parameters.Add("@Payment_id", paymentId ?? string.Empty);
            parameters.Add("@Type", type ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<ResultSet>("Proc_UpdatePaymentStatus", parameters, commandType: CommandType.StoredProcedure);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return result;
            }
        }


        #region //Dainik 




        public async Task<ResultSet> EasyBillRecharge(EasyBillPay model, EasyBillPayResponse billPayResponse)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@procId", model.ProcId);
            parameters.Add("@transId", model.transId);
            parameters.Add("@Fk_MemberId", model.memberId);
            parameters.Add("@Number", model.Account ?? string.Empty);
            parameters.Add("@OperatorCode", model.SPKey ?? string.Empty);
            parameters.Add("@Amount", model.Amount);
            parameters.Add("@TransactionId", model.APIRequestID ?? string.Empty);
            parameters.Add("@PaymentId", model.APIRequestID ?? string.Empty);
            parameters.Add("@Type", model.type);
            parameters.Add("@status", billPayResponse.STATUS ?? string.Empty);
            parameters.Add("@description", billPayResponse.MSG ?? string.Empty);
            parameters.Add("@optr_txn_id", billPayResponse.RPID ?? string.Empty);
            parameters.Add("@Response", billPayResponse.ERRORCODE ?? string.Empty);
            parameters.Add("@MobileNo", model.CustomerNumber ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<ResultSet>("Proc_BillRecharge", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return result;
            }
        }
        #endregion
        public async Task<ResultSet> WalletRegistration(WalletCreationRequest model, int ProcId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@ProcID", ProcId);
            parameters.Add("@TRANSID", model.TRANSID);
            parameters.Add("@TRANSACTIONID", model.TRANSACTIONID ?? string.Empty);
            parameters.Add("@KYCFLAG", model.KYCFLAG ?? string.Empty);
            parameters.Add("@USERNAME", model.CUSTOMERNAME ?? string.Empty);
            parameters.Add("@USERMIDDLENAME", model.CUSTOMERMIDDLENAME ?? string.Empty);
            parameters.Add("@USERLASTNAME", model.CUSTOMERLASTNAME ?? string.Empty);
            parameters.Add("@USERDATEOFBIRTH", model.CUSTOMERDOB ?? string.Empty);
            parameters.Add("@USEREMAILID", model.CUSTOMEREMAILID ?? string.Empty);
            parameters.Add("@USERMOBILENO", model.CUSTOMERMOBILENO ?? string.Empty);
            parameters.Add("@PINCODE", model.PINCODE ?? string.Empty);
            parameters.Add("@CUSTOMERADDRESS", model.CUSTOMERADDRESS ?? string.Empty);
            parameters.Add("@GENDER", model.GENDER ?? string.Empty);
            parameters.Add("@FK_MEMID", model.MemberId);
            parameters.Add("@CUSTOMERIDIDPROOFTYPE", model.CUSTOMERIDIDPROOFTYPE);
            parameters.Add("@CUSTOMERIDPROOFNO", model.CUSTOMERIDPROOFNO);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<ResultSet>("Proc_WalletRegistration", parameters, commandType: CommandType.StoredProcedure);

                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return result;
            }
        }

        public async Task<List<SupportTypeList>> SupportType(int Id)
        {
            List<SupportTypeList> result = new List<SupportTypeList>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProcId", Id);


            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    var res = await con.QueryAsync<SupportTypeList>("BindDropDown", parameters, commandType: CommandType.StoredProcedure);
                    result = res.ToList();

                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
                return result;
            }

        }

        public async Task<vouchersummary> GetVoucherSummary(long MemberId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MemberId", MemberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryFirstAsync<vouchersummary>("GetVoucherCartSummary", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<VoucherDescription> GetVoucherDescriptionDetails(string Brand, string Product)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Brand", Brand);
            parameters.Add("@Product", Product);
            using (con = new SqlConnection(_connectionString.Value))
            {
                con.Open();
                var result = await con.QueryFirstAsync<VoucherDescription>("GetVoucherDetails", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<LoginResponse> WebViewLogin(string token)
        {
            DynamicParameters parameters = new DynamicParameters();
            LoginResponse result = new LoginResponse();
            parameters.Add("@tokenId", token ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<LoginResponse>("WebViewLogin", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }


        public async Task<ResultSet> MaintainSessionIdForWallet(WalletTransactionHandShakeRequest model)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@TransactionId", model.TRANSACTIONID);
            parameters.Add("@SessionId", model.SESSIONID);
            parameters.Add("@SType", model.type);
            parameters.Add("@MemberId", model.memberId);
            parameters.Add("@MobileNo", model.MOBILENO);
            parameters.Add("@Amount", model.AMOUNT);
            parameters.Add("@ProcId", model.ProcId);
            parameters.Add("@Status", model.Status);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<ResultSet>("Proc_tblTransSession", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;



            }
        }


        public async Task<ResultSet> WalletRecharge(WalletTop model)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@procId", model.procId);
            parameters.Add("@MemberId", model.MemberId);
            parameters.Add("@TransanctionId", model.TransactionId);
            parameters.Add("@status", model.status);
            parameters.Add("@remarks", model.Remarks);
            parameters.Add("@creditamount", model.creditamount);
            parameters.Add("@debitamount", model.debitamount);
            parameters.Add("@type", model.type);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<ResultSet>("Proc_WalletTransaction", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;



            }
        }

        public async Task<Name_EmailModel> GetUserName_Email(string dataId, string type = "")
        {
            Name_EmailModel result = new Name_EmailModel();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@dataId", dataId);
            parameters.Add("@type", type);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<Name_EmailModel>("Proc_GetUserDetails", parameters, commandType: CommandType.StoredProcedure);

                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }
                return result;
            }
        }

        public async Task<ResultSet> VirtualAccount(virtualaccountsResponse model, long Id)
        {
            ResultSet result = new ResultSet();

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CustomerId", Id);
            parameters.Add("@virtualaccountId", model.id);
            parameters.Add("@name", model.name ?? string.Empty);
            parameters.Add("@entity", model.entity ?? string.Empty);
            parameters.Add("@status", model.status ?? string.Empty);
            parameters.Add("@description", model.description ?? string.Empty);
            parameters.Add("@receiverId", model.receivers[0].id ?? string.Empty);
            parameters.Add("@receiverentity", model.receivers[0].entity ?? string.Empty);
            parameters.Add("@receiverusername", model.receivers[0].username ?? string.Empty);
            parameters.Add("@handle", model.receivers[0].handle ?? string.Empty);
            parameters.Add("@receiveraddress", model.receivers[0].address ?? string.Empty);
            parameters.Add("@close_by", model.close_by);
            parameters.Add("@created_at", model.created_at);
            parameters.Add("@closed_at", model.closed_at);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<ResultSet>("UserVirtualAccount", parameters, commandType: CommandType.StoredProcedure);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }
                return result;
            }
        }

        public async Task<ResultSet> GetCardUserMobileNumber(long Id)
        {
            ResultSet result = new ResultSet();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", Id);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = await con.QueryFirstAsync<ResultSet>("GetCardUserMobileNumber", parameters, commandType: CommandType.StoredProcedure);

                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }
                return result;
            }
        }

        public async Task<ResultSet> PostWalletTransaction(PostWalletTransactionRequest model)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@procId", model.ProcId);
            parameters.Add("@TransactionId", model.TransactionId);
            parameters.Add("@APITransactionId", model.APITransactionId);
            parameters.Add("@TransDate", model.TransDate);
            parameters.Add("@MobileNo", model.MobileNo);
            parameters.Add("@Amount", model.Amount);
            parameters.Add("@RefundId", model.RefundId);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<ResultSet>("Proc_WalletDebitTransaction", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;



            }
        }

        public async Task<List<PostWalletTransactionRequest>> GetDebitWalletTransaction(int ProcId)
        {
            List<PostWalletTransactionRequest> result = new List<PostWalletTransactionRequest>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@procId", ProcId);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                var res = await con.QueryAsync<PostWalletTransactionRequest>("Proc_WalletDebitTransaction", parameters, commandType: CommandType.StoredProcedure);
                result = res.ToList();
                con.Dispose();
                return result;



            }
        }


        public async Task<WalletTransactionLog> WalletTransactionAction(WalletTransactionLog model)
        {
            try
            {


                WalletTransactionLog result = new WalletTransactionLog();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@procId", model.ProcId);
                parameters.Add("@Id", model.Id);
                parameters.Add("@Request", model.Request);
                parameters.Add("@Response", model.WalletResponse);
                parameters.Add("@TransactionId", model.WalletTransactionId);
                parameters.Add("@RequestFrom", model.RequestFrom);
                parameters.Add("@MemberId", model.MemberId);
                parameters.Add("@Type", model.Type);

                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    result = await con.QueryFirstOrDefaultAsync<WalletTransactionLog>("Proc_WalletTransactionLog", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<PendingWalletTransaction>> GetPendingWalletTransaction()
        {
            List<PendingWalletTransaction> walletTransactionList = new List<PendingWalletTransaction>();
            DynamicParameters dynamicParameters1 = new DynamicParameters();
            List<PendingWalletTransaction> walletTransaction;
            using (this.con = (IDbConnection)new SqlConnection(this._connectionString.Value))
            {
                this.con.Open();
                IDbConnection con = this.con;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                List<PendingWalletTransaction> list = (await SqlMapper.QueryAsync<PendingWalletTransaction>(con, "Proc_PendingWalletTransaction", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3)).ToList<PendingWalletTransaction>();
                this.con.Dispose();
                walletTransaction = list;
            }
            return walletTransaction;
        }
        public async Task<ResultSet> TrainTicketBooking(string transactonId, string data, long MemberId, int procId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@procId", procId);
            parameters.Add("@MemberId", MemberId);
            parameters.Add("@ClientTransactionId", transactonId);
            parameters.Add("@data", data);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<ResultSet>("Proc_TrainTicketBooking", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<CardDispatchDetail> GetCardDispatchDetailByUser(long MemberId)
        {
            CardDispatchDetail result = new CardDispatchDetail();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@MemberId", MemberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<CardDispatchDetail>("Proc_GetCardDispatchDetailByUser", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }
        public async Task<WALLETBALANCEResponse> WalletBalance(int ProcId, string mobileno)
        {
            WALLETBALANCEResponse result = new WALLETBALANCEResponse();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@procId", ProcId);
            parameters.Add("@mobileno", mobileno);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<WALLETBALANCEResponse>("Proc_TempWalletDetail", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<WalletInfoResponse> WalletInfo(int ProcId, string mobileno)
        {
            WalletInfoResponse result = new WalletInfoResponse();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@procId", ProcId);
            parameters.Add("@mobileno", mobileno);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<WalletInfoResponse>("Proc_Walletinfo", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }
        public async Task<List<WalletMiniStatement>> WalletStatement(int ProcId, string mobileno, string fromdate, string todate)
        {
            List<WalletMiniStatement> result = new List<WalletMiniStatement>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@procId", ProcId);
            parameters.Add("@mobileno", mobileno);
            parameters.Add("@fromdate", fromdate);
            parameters.Add("@todate", todate);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<WalletMiniStatement>("Proc_TempWalletDetail", parameters, commandType: CommandType.StoredProcedure);
                    result = res.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<walletTopUp> ValidateWallet_Topup(walletTopUp model)
        {
            walletTopUp result = new walletTopUp();
            DynamicParameters parameters = new DynamicParameters();


            parameters.Add("@TransactionId", model.TransactionId);


            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<walletTopUp>("spValidatewalletTopUp", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<walletTopUp> SaveWallet_Topup(walletTopUp model)
        {
            walletTopUp result = new walletTopUp();
            DynamicParameters parameters = new DynamicParameters();


            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@MobileNO", model.MobileNO);
            parameters.Add("@TopupAmount", model.TopupAmount);
            parameters.Add("@TransactionId", model.TransactionId);
            parameters.Add("@Remarks", model.Remarks);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<walletTopUp>("SaveWalletTopUp", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<walletTopUp> UpdateWalletTopUp(walletTopUp model)
        {
            walletTopUp result = new walletTopUp();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@TransactionId", model.TransactionId);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<walletTopUp>("UpdateWalletTopUp", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<SaveBookingResponse> SaveBookingResponse(BookingResponse bookingResponse)
        {
            SaveBookingResponse result = new SaveBookingResponse();
            DynamicParameters parameters = new DynamicParameters();

            DataTable dtinformationMessage = Common.ToDataTable(bookingResponse.informationMessage);
            DataTable dtavlDayList = Common.ToDataTable(bookingResponse.avlDayList);
            DataTable dtbkgCfg = Common.ToDataTable(bookingResponse.bkgCfg);
            parameters.Add("@Fk_MemId", bookingResponse.Fk_MemId);
            parameters.Add("@baseFare", bookingResponse.baseFare);
            parameters.Add("@cateringCharge", bookingResponse.cateringCharge);
            parameters.Add("@distance", bookingResponse.distance);
            parameters.Add("@dynamicFare", bookingResponse.dynamicFare);
            parameters.Add("@enqClass", bookingResponse.enqClass);
            parameters.Add("@fromlocation", bookingResponse.from);
            parameters.Add("@fuelAmount", bookingResponse.fuelAmount);
            parameters.Add("@insuredPsgnCount", bookingResponse.insuredPsgnCount);
            parameters.Add("@nextEnqDate", bookingResponse.nextEnqDate);
            parameters.Add("@otherCharge", bookingResponse.otherCharge);
            parameters.Add("@otpAuthenticationFlag", bookingResponse.otpAuthenticationFlag);
            parameters.Add("@preEnqDate", bookingResponse.preEnqDate);
            parameters.Add("@quota", bookingResponse.quota);
            parameters.Add("@reqEnqParam", bookingResponse.reqEnqParam);
            parameters.Add("@reservationCharge", bookingResponse.reservationCharge);
            parameters.Add("@serverId", bookingResponse.serverId);
            parameters.Add("@serviceTax", bookingResponse.serviceTax);
            parameters.Add("@superfastCharge", bookingResponse.superfastCharge);
            parameters.Add("@tatkalFare", bookingResponse.tatkalFare);
            parameters.Add("@timeStamp", bookingResponse.timeStamp);
            parameters.Add("@tolocation", bookingResponse.to);
            parameters.Add("@totalConcession", bookingResponse.totalConcession);
            parameters.Add("@totalFare", bookingResponse.totalFare);
            parameters.Add("@trainName", bookingResponse.trainName);
            parameters.Add("@trainNo", bookingResponse.trainNo);
            parameters.Add("@travelInsuranceCharge", bookingResponse.travelInsuranceCharge);
            parameters.Add("@travelInsuranceServiceTax", bookingResponse.travelInsuranceServiceTax);
            parameters.Add("@wpServiceCharge", bookingResponse.wpServiceCharge);
            parameters.Add("@wpServiceTax", bookingResponse.wpServiceTax);
            parameters.Add("@ftBookingMsgFlag", bookingResponse.ftBookingMsgFlag);
            parameters.Add("@rdsTxnPwdFlag", bookingResponse.rdsTxnPwdFlag);
            parameters.Add("@taRdsFlag", bookingResponse.taRdsFlag);
            parameters.Add("@totalCollectibleAmount", bookingResponse.totalCollectibleAmount);
            parameters.Add("@trainsiteId", bookingResponse.trainsiteId);
            parameters.Add("@upiRdsFlag", bookingResponse.upiRdsFlag);
            parameters.Add("@IsCancelled", bookingResponse.IsCancelled);
            parameters.Add("@IsDeleted", bookingResponse.IsDeleted);
            parameters.Add("@dtInformationMessage", dtinformationMessage);
            parameters.Add("@dtBkgCfg", dtbkgCfg);
            parameters.Add("@dtAvlDayList", dtavlDayList);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<SaveBookingResponse>("SaveIRCTCResponse", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<walletOTP_Log> SaveWallet_OTPLog(walletOTP_Log model)
        {
            walletOTP_Log result = new walletOTP_Log();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@MobileNO", model.MobileNO);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);
            parameters.Add("@opcode", model.Opcode);


            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<walletOTP_Log>("SaveWalletOTP", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }


        public async Task<ToPaywalletReg> ValidateWallet_Reg(ToPaywalletReg model)
        {
            ToPaywalletReg result = new ToPaywalletReg();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<ToPaywalletReg>("ValidateToPayWalletRegistration", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<ToPaywalletReg> SaveTopayWallet_Reg(WalletCreationRequestV2DTO model)
        {
            ToPaywalletReg result = new ToPaywalletReg();
            DynamicParameters parameters = new DynamicParameters();


            parameters.Add("@FK_MemId", model.memberId);

            parameters.Add("@Title", model.CUSTOMERTITLE);
            parameters.Add("@Name", model.CUSTOMERNAME);
            parameters.Add("@MiddleName", model.CUSTOMERMIDDLENAME);
            parameters.Add("@LastName", model.CUSTOMERLASTNAME);
            parameters.Add("@Gender", model.GENDER);
            parameters.Add("@Password", model.PASSWORD);
            parameters.Add("@confirmPassword", model.CONFIRMPASSWORD);
            parameters.Add("@MobileNo", model.CUSTOMERMOBILENO);
            parameters.Add("@OTP", model.OTP);
            parameters.Add("@CardType", model.CARDTYPE);
            parameters.Add("@IdProofType", model.CUSTOMERIDIDPROOFTYPE);
            parameters.Add("@IdProofNo", model.CUSTOMERIDPROOFNO);
            parameters.Add("@IdProofExpiry", model.CUSTOMERIDIDPROOFEXPIRY);
            parameters.Add("@Dob", model.CUSTOMERDOB);
            parameters.Add("@City", model.CITY);
            parameters.Add("@State", model.STATE);
            parameters.Add("@Pincode", model.PINCODE);
            parameters.Add("@Address", model.CUSTOMERADDRESS);
            parameters.Add("@Address2", model.CUSTOMERADDRESS2);
            parameters.Add("@Address3", model.CUSTOMERADDRESS3);
            parameters.Add("@Kycflag", model.KYCFLAG);
            parameters.Add("@PancardNo", model.PANCARDNO);
            parameters.Add("@ProofType", model.ADDRESSPROOFTYPE);
            parameters.Add("@ProofNo", model.ADDRESSPROOFNO);
            parameters.Add("@ProofExpiry", model.ADDRESSPROOFEXPIRY);
            parameters.Add("@mailId", model.CUSTOMEREMAILID);
            parameters.Add("@enableNotification", model.ENABLENOTIFICATION);
            parameters.Add("@WalletPin", model.WALLETPIN);
            parameters.Add("@ConfirmWalletPin", model.CONFIRMWALLETPIN);
            parameters.Add("@KitNo", model.KITNO);


            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<ToPaywalletReg>("saveToPayWalletRegistration", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<ToPaywalletReg> UpdateWalletReg_Response(ToPaywalletReg model)
        {
            ToPaywalletReg result = new ToPaywalletReg();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@MobileNo", model.MobileNO);
            parameters.Add("@OTP", model.OTP);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);
            parameters.Add("@kit_no", model.kit_no);
            parameters.Add("@card_id", model.card_id);
            // parameters.Add("@KitType", model.KitType);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<ToPaywalletReg>("UpdateToPayWalletRegistration", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }


        public async Task<WalletBalance> SaveWalletBalance(WalletBalance model)
        {
            WalletBalance result = new WalletBalance();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@MobileNo", model.MobileNO);
            parameters.Add("@walletbalance", model.walletbalance);
            parameters.Add("@Monthlyloadlimit", model.Monthlyloadlimit);
            parameters.Add("@Monthlyremaininglimit", model.Monthlyremaininglimit);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<WalletBalance>("SaveWalletBalance", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<WalletSetPreferencesSave> SaveWalletSetPreferences(WalletSetPreferencesSave model)
        {
            WalletSetPreferencesSave result = new WalletSetPreferencesSave();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.Fk_MemId);
            parameters.Add("@MobileNo", model.CUSTOMERMOBILENO);
            parameters.Add("@ContactLess", model.CONTACTLESS);
            parameters.Add("@ATM", model.ATM);
            parameters.Add("@ECOM", model.ECOM);
            parameters.Add("@POS", model.POS);
            parameters.Add("@DCC", model.DCC);
            parameters.Add("@NFS", model.NFS);
            parameters.Add("@INTERNATIONAL", model.INTERNATIONAL);
            parameters.Add("@KITNO", model.KITNO);
            parameters.Add("@Response", model.response);
            parameters.Add("@message", model.message);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<WalletSetPreferencesSave>("SaveWalletSetPreferences", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<WalletBlockUnblock_log> SaveWalletBlockUnblock(WalletBlockUnblock_log model)
        {
            WalletBlockUnblock_log result = new WalletBlockUnblock_log();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@MobileNo", model.MobileNO);
            parameters.Add("@BlockStatus", model.BlockStatus);
            parameters.Add("@CardId", model.CardId);
            parameters.Add("@KITNO", model.KITNO);
            parameters.Add("@Remarks", model.Remarks);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<WalletBlockUnblock_log>("SaveWalletBlockUnblock", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<WalletDebit_log> SaveWalletDebit(WalletDebit_log model)
        {
            WalletDebit_log result = new WalletDebit_log();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProcId", model.OpCode);
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@MobileNO", model.MobileNo);
            parameters.Add("@TransactionId", model.TransactionId);
            parameters.Add("@DrAmount", model.DrAmount);
            parameters.Add("@CrAmount", model.CrAmount);
            parameters.Add("@Remarks", model.Remarks);
            parameters.Add("@response", model.response);
            parameters.Add("@Type", model.Type);
            parameters.Add("@walletPin", model.walletPin);
            parameters.Add("@message", model.message);
            parameters.Add("@Fk_CardId", model.Fk_CardId);
            parameters.Add("@KitNo", model.KitNo);
            parameters.Add("@ToPayCardId", model.card_id);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<WalletDebit_log>("SaveWalletDebit_Credit", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }


        public async Task<WalletSetlimit_log> SaveWalletLimitV2(WalletSetlimit_log model)
        {
            WalletSetlimit_log result = new WalletSetlimit_log();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@ProcId", model.ProcId);
            parameters.Add("@MobileNO", model.MobileNo);
            parameters.Add("@TXNTYPE", model.TXNTYPE);
            parameters.Add("@LIMIT", model.LIMIT);
            parameters.Add("@CARDID", model.CARDID);
            parameters.Add("@KITNO", model.KITNO);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<WalletSetlimit_log>("SaveSetWalletCardLimit", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<WalletSetPIN_log> SaveWalletSetPINV2(WalletSetPIN_log model)
        {
            WalletSetPIN_log result = new WalletSetPIN_log();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@MobileNO", model.MobileNo);
            parameters.Add("@CardId", model.CardId);
            parameters.Add("@KITNO", model.KITNO);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<WalletSetPIN_log>("SaveWalletSetPin", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<WalletSetPIN_log> SaveCardinfo(WalletSetPIN_log model)
        {
            WalletSetPIN_log result = new WalletSetPIN_log();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@MobileNO", model.MobileNo);
            parameters.Add("@CardId", model.CardId);
            parameters.Add("@KITNO", model.KITNO);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<WalletSetPIN_log>("SaveCardinfo", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<WalletDashboard_log> GetDashboard(WalletDashboard_log model)
        {
            WalletDashboard_log result = new WalletDashboard_log();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<WalletDashboard_log>("GetWalletDashboard", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<List<CardData>> GetCardData(CardRequestModelLog data)
        {
            List<CardData> viewTickets = new List<CardData>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OpCode", 1);
            parameters.Add("@Fk_MemId", data.Fk_memId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<CardData>("ManageCardTypeMaster", parameters, commandType: CommandType.StoredProcedure);
                    viewTickets = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return viewTickets;
            }
        }



        public async Task<WalletV2WalletCommonResponse> ApplyCard(ApplyCard model)
        {
            WalletV2WalletCommonResponse result = new WalletV2WalletCommonResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.Fk_MemId);
            parameters.Add("@Fk_CardId", model.Fk_CardId);
            parameters.Add("@OpCode", 1);
            parameters.Add("@IsPhotoCard", model.IsPhotoCard);
            parameters.Add("@PhotoPath", model.PhotoPath);
            parameters.Add("@OrderId", model.OrderId);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<WalletV2WalletCommonResponse>("MemberCard", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<ReplaceCardLog> ReplaceCard(ReplaceCardRespone model)
        {
            ReplaceCardLog result = new ReplaceCardLog();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.Fk_MemId);
            parameters.Add("@OldCardId", model.CardId);
            parameters.Add("@MobileNo", model.MobileNo);
            parameters.Add("@CardId", model.CardId);
            parameters.Add("@OldKitNo", model.OldKitNo);
            parameters.Add("@KitNo", model.KitNo);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<ReplaceCardLog>("ReplaceCard", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<UpdateMobileLog> UpdateMobile(UpdateMobileLog model)
        {
            UpdateMobileLog result = new UpdateMobileLog();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.Fk_MemId);
            parameters.Add("@OldMobileNo", model.OldMobileNo);
            parameters.Add("@NewMobileNo", model.NewMobileNo);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<UpdateMobileLog>("UpdateMobileNo", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<walletResetOTP_Log> SaveWalletResetOTPLog(walletResetOTP_Log model)
        {
            walletResetOTP_Log result = new walletResetOTP_Log();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@MobileNO", model.MobileNO);
            parameters.Add("@OTP", model.OTP);
            parameters.Add("@walletPin", model.WalletPin);
            parameters.Add("@confirmWalletPin", model.ComfirmWalletPin);
            parameters.Add("@response", model.response);
            parameters.Add("@message", model.message);
            parameters.Add("@opcode", model.Opcode);


            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<walletResetOTP_Log>("SaveWalletResetOTP", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }


        public async Task<List<CardViewDetailResult>> CardViewInfo(CardViewDetails_log data)
        {
            List<CardViewDetailResult> viewTickets = new List<CardViewDetailResult>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Fk_MemId", data.Fk_MemId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<CardViewDetailResult>("Proc_CardTypeInfo", parameters, commandType: CommandType.StoredProcedure);
                    viewTickets = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return viewTickets;
            }
        }
        public async Task<AddedOnRequest> AddedOnRequest(AddedOnRequest data)
        {

            DynamicParameters parameters = new DynamicParameters();
            AddedOnRequest result = new AddedOnRequest();
            parameters.Add("@OrderId", data.OrderId);
            parameters.Add("@PaymentId", data.PaymentId);
            parameters.Add("@KitNo", data.kit_no);
            parameters.Add("@ToPayCardId", data.card_id);
            parameters.Add("@OpCode", 3);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<AddedOnRequest>("MemberCard", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<TokenResponse> GetToken()
        {

            DynamicParameters parameters = new DynamicParameters();
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                TokenResponse result = await con.QueryFirstOrDefaultAsync<TokenResponse>("GetTravelToken", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<TokenResponse> getdata()
        {

            DynamicParameters parameters = new DynamicParameters();
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                TokenResponse result = await con.QueryFirstOrDefaultAsync<TokenResponse>("getdata", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<KitNoResponse> AssignKit_No(KitNoResponse model)
        {
            KitNoResponse result = new KitNoResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FK_MemId", model.FK_MemId);
            parameters.Add("@OpCode", model.OpCode);
            parameters.Add("@MobileNo", model.MobileNO);
            parameters.Add("@KitType", model.KitType);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<KitNoResponse>("SP_AssignMemberKit", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();


                return result;
            }
        }





        public async Task<List<KitsAvailabilityList>> GetKitAvailability()
        {
            DynamicParameters parameters = new DynamicParameters();
            List<KitsAvailabilityList> KitList = new List<KitsAvailabilityList>();


            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<KitsAvailabilityList>("GetKitsAvailability", parameters, commandType: CommandType.StoredProcedure);
                    KitList = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return KitList;
            }


        }


        public async Task<BlockandUnblockStatusRequest> BlockandUnblockStatus(BlockandUnblockStatusRequest model)
        {
            BlockandUnblockStatusRequest result = new BlockandUnblockStatusRequest();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@KITNO", model.KITNO);
            parameters.Add("@FK_MemId", model.FK_MemId);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<BlockandUnblockStatusRequest>("SP_CheckWalletBlockUnblock", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<CheckCardTypeResponse> CheckKitType(CheckCardTypeRequest model)
        {
            CheckCardTypeResponse result = new CheckCardTypeResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@KITNO", model.KITNO);
            parameters.Add("@FK_MemId", model.FK_MemId);


            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("SP_GetKitsDetails", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<CheckCardTypeResponse> GetAvailableKit(int OldCardId, int NewCardId, long Fk_MemId, string entityid)
        {
            CheckCardTypeResponse result = new CheckCardTypeResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OldCardId", OldCardId);
            parameters.Add("@NewCardId", NewCardId);
            parameters.Add("@Fk_MemId", Fk_MemId);
            parameters.Add("@entityid", entityid);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("GetAvailableKit", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<CheckCardTypeResponse> SaveTravelRequest(string Request, string Type, int Fk_MemId)
        {
            CheckCardTypeResponse result = new CheckCardTypeResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Request", Request);
            parameters.Add("@Type", Type);
            parameters.Add("@Fk_MemId", Fk_MemId);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("SaveTravelRequest", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<CheckCardTypeResponse> SaveBookingResponse(TravelSaveResponse bookTicketResponse)
        {
            try
            {

                CheckCardTypeResponse result = new CheckCardTypeResponse();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Fk_MemId", bookTicketResponse.Fk_MemId);
                parameters.Add("@TraceId", bookTicketResponse.TraceId);
                parameters.Add("@BookingId", bookTicketResponse.BookingId);
                parameters.Add("@PNRNo", bookTicketResponse.PNRNo);
                parameters.Add("@SSRDenied", bookTicketResponse.SSRDenied);
                parameters.Add("@SSRMessage", bookTicketResponse.SSRMessage);
                parameters.Add("@Status", bookTicketResponse.Status);
                parameters.Add("@IsPriceChanged", bookTicketResponse.IsPriceChanged);
                parameters.Add("@IsTimeChanged", bookTicketResponse.IsTimeChanged);
                parameters.Add("@IsSuccess", bookTicketResponse.IsSuccess);
                parameters.Add("@Message", bookTicketResponse.Message);
                parameters.Add("@IsLcc", bookTicketResponse.IsLcc);
                parameters.Add("@tblFlightItinerary", bookTicketResponse.tblFlightItinerary.AsTableValuedParameter("[dbo].[FlightItinerary]"));
                parameters.Add("@dtSegment", bookTicketResponse.dtSegment.AsTableValuedParameter("[dbo].[dtSegment]"));
                parameters.Add("@dtSegmentR", bookTicketResponse.dtSegmentR.AsTableValuedParameter("[dbo].[dtSegmentR]"));

                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("SaveFlightResponse", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<CheckCardTypeResponse> SaveErrorBookingResponse(TravelSaveResponse bookTicketResponse)
        {
            try
            {

                CheckCardTypeResponse result = new CheckCardTypeResponse();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Fk_MemId", bookTicketResponse.Fk_MemId);
                parameters.Add("@TraceId", bookTicketResponse.TraceId);
                parameters.Add("@BookingId", bookTicketResponse.BookingId);
                parameters.Add("@PNRNo", bookTicketResponse.PNRNo);
                parameters.Add("@SSRDenied", bookTicketResponse.SSRDenied);
                parameters.Add("@SSRMessage", bookTicketResponse.SSRMessage);
                parameters.Add("@Status", bookTicketResponse.Status);
                parameters.Add("@IsPriceChanged", bookTicketResponse.IsPriceChanged);
                parameters.Add("@IsTimeChanged", bookTicketResponse.IsTimeChanged);
                parameters.Add("@IsSuccess", bookTicketResponse.IsSuccess);
                parameters.Add("@Message", bookTicketResponse.Message);
                parameters.Add("@IsLcc", bookTicketResponse.IsLcc);


                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("SaveFlightResponse", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BookingList>> GetBookingList(int FK_MemId)
        {
            List<BookingList> BookingList = new List<BookingList>();
            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Fk_MemId", FK_MemId);


                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    var result = await con.QueryAsync<BookingList>("GetBookingDetails", parameters, commandType: CommandType.StoredProcedure);
                    BookingList = result.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return BookingList;
        }


        public async Task<List<PaymentData>> GetPaymentOption(int FK_MemId, string Type)
        {
            List<PaymentData> viewTickets = new List<PaymentData>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Fk_MemId", FK_MemId);
            parameters.Add("@Type", Type);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<PaymentData>("GetPaymentOption", parameters, commandType: CommandType.StoredProcedure);
                    viewTickets = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return viewTickets;
            }
        }

        public async Task<CheckMobileTransfer> CheckMobileForTransfer(string MobileNo)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MobileNo", MobileNo);
            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                CheckMobileTransfer result = await con.QueryFirstOrDefaultAsync<CheckMobileTransfer>("CheckMobileTransfer", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<List<TrainBookingList>> GetTrainBookingList(int FK_MemId)
        {
            List<TrainBookingList> BookingList = new List<TrainBookingList>();
            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Fk_MemId", FK_MemId);


                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    var result = await con.QueryAsync<TrainBookingList>("GetTrainBookingList", parameters, commandType: CommandType.StoredProcedure);
                    BookingList = result.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return BookingList;
        }
        public async Task<CheckCardTypeResponse> CancellationResponse(string ClientTransactionId, string Response)
        {
            CheckCardTypeResponse result = new CheckCardTypeResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ClientTransactionId", ClientTransactionId);
            parameters.Add("@Response", Response);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("SaveCancellationResponse", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }
        public async Task<CheckCardTypeResponse> UpdateNonLCCBookingResponse(TravelSaveResponse bookTicketResponse)
        {
            try
            {

                CheckCardTypeResponse result = new CheckCardTypeResponse();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@TraceId", bookTicketResponse.TraceId);
                parameters.Add("@IsSuccess", bookTicketResponse.IsSuccess);
                parameters.Add("@Message", bookTicketResponse.Message);

                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("UpdateNonLCCBookingResponse", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<ResultTravel> SaveTransactionData(long FK_MemId, string TransactionId, string PaymentType, string Remark, string ActionType, decimal Amount, string OperatorTxnID, string status, string Number, string OperatorCode)
        {
            try
            {

                ResultTravel result = new ResultTravel();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@FK_MemId", FK_MemId);
                parameters.Add("@TransactionId", TransactionId);
                parameters.Add("@PaymentType", PaymentType);
                parameters.Add("@Remark", Remark);
                parameters.Add("@ActionType", ActionType);
                parameters.Add("@Amount", Amount);
                parameters.Add("@RechargeNumber", Number);
                parameters.Add("@Status", status);
                parameters.Add("@OperatorTxnID", OperatorTxnID);
                parameters.Add("@OperatorCode", OperatorCode);

                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    result = await con.QueryFirstOrDefaultAsync<ResultTravel>("SaveTransactionData", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<WalletSetPin> WalletSetPin(WalletSetPin walletSetPin)
        {
            try
            {

                WalletSetPin result = new WalletSetPin();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@CUSTOMERMOBILENO", walletSetPin.CUSTOMERMOBILENO);
                parameters.Add("@WALLETPIN", walletSetPin.WALLETPIN);

                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    result = await con.QueryFirstOrDefaultAsync<WalletSetPin>("UpdateWalletPin", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BillPayRequest_log> SaveBillPayRequsest(BillPayRequest_log billmodel)
        {
            try
            {

                BillPayRequest_log result = new BillPayRequest_log();
                DynamicParameters parameters = new DynamicParameters();
                DataTable dticustomer_params = Common.ToDataTable(billmodel.customerlist);
                parameters.Add("@biller_code", billmodel.bill_code);
                parameters.Add("@agent_code", billmodel.agent_code);
                parameters.Add("@amount", billmodel.amount);
                parameters.Add("@remakrs", billmodel.remakrs);
                parameters.Add("@ref_id", billmodel.ref_id);
                parameters.Add("@mobile", billmodel.mobile);
                parameters.Add("@client_ref", billmodel.client_ref);
                parameters.Add("@quick_pay", billmodel.quick_pay);
                parameters.Add("@dtcustomer_params", dticustomer_params.AsTableValuedParameter("[dbo].[dtcustomer_params]"));
                parameters.Add("@plan_details_id", billmodel.plan_details_id);
                parameters.Add("@plan_details_type", billmodel.plan_details_type);
                parameters.Add("@Type", billmodel.Type);

                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    result = await con.QueryFirstOrDefaultAsync<BillPayRequest_log>("SaveBillPayRequest", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<CheckCardTypeResponse> SaveBBPSRequest(string Request, string Response, string Type, int Fk_MemId, string OrderId)
        {
            CheckCardTypeResponse result = new CheckCardTypeResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Request", Request);
            parameters.Add("@Response", Response);
            parameters.Add("@Type", Type);
            parameters.Add("@Fk_MemId", Fk_MemId);
            parameters.Add("@OrderId", OrderId);

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("SaveBBPSRequest", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<List<BillerModel>> GetBiller()
        {
            List<BillerModel> result = new List<BillerModel>();

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryAsync<BillerModel>("GetBillerCategory", null, commandType: CommandType.StoredProcedure);
                    result = res.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

                return result;
            }
        }

        public async Task<DashboardV5Response> NewDashboard(long memberId)
        {
            DashboardV5Response result = new DashboardV5Response();
            result.voucher = new Data1();
            result.mall = new Data1();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@memberId", memberId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    var r = await con.QueryMultipleAsync("Proc_GetDashboardDetailV5", parameters, commandType: CommandType.StoredProcedure);
                    var card = r.Read<DashboardCardResponseV5>().FirstOrDefault();
                    var reward = r.Read<DashboardCommondata>().ToList();
                    var banner1 = r.Read<banner>().ToList();
                    var banner2 = r.Read<banner>().ToList();
                    var maindata = r.Read<DashboardCommondata>().ToList();
                    var voucher = r.Read<DashboardCommondata>().ToList();
                    var mall = r.Read<DashboardCommondata>().ToList();
                    var cardVarient = r.Read<BindDropDown2>().ToList();

                    result.card = card;
                    result.card.cardVarient = cardVarient;
                    result.reward = reward;
                    result.banner1 = banner1;
                    result.banner2 = banner2;
                    result.voucher.data = voucher;
                    result.mall.data = mall;
                    if (maindata != null && maindata.Count() > 1)
                    {
                        result.maindata = new List<Topheader>();
                        var sublistHeader = maindata.Where(x => x.text == x.value).ToList();

                        int k = 0;
                        for (int i = 0; i < sublistHeader.Count; i++)
                        {

                            result.maindata.Add(new Topheader { header = sublistHeader[i].text });
                            result.maindata[i].data = new List<DashboardCommondata>();
                            for (int j = k; j < maindata.Count; j++, k++)
                            {
                                if (result.maindata[i].header != maindata[j].value)
                                {
                                    result.maindata[i].data.Add(maindata[j]);
                                }
                                if (maindata[j].value == "View More")
                                {
                                    k++;
                                    break;
                                }

                            }
                        }

                    }

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<List<PinCodeData>> GetSvaasAvailbility(string Pincode)
        {
            List<PinCodeData> pinCodeDatas = new List<PinCodeData>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Pincode", Pincode);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<PinCodeData>("GetSvaasPinCode", parameters, commandType: CommandType.StoredProcedure);
                    pinCodeDatas = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return pinCodeDatas;
            }

        }

        public async Task<SvaasSaveResponse> SvaasPaymentReponse(SvaaspaymentResponse svaaspaymentResponse)
        {
            DynamicParameters parameters = new DynamicParameters();
            SvaasSaveResponse result = new SvaasSaveResponse();
            parameters.Add("@MobileNo", svaaspaymentResponse.MobileNo);
            parameters.Add("@UserId", svaaspaymentResponse.UserId);
            parameters.Add("@TransactionNo", svaaspaymentResponse.TransactionNo);
            parameters.Add("@ProductName", svaaspaymentResponse.ProductName);
            parameters.Add("@ProductId", svaaspaymentResponse.ProductId);
            parameters.Add("@CustomerName", svaaspaymentResponse.CustomerName);
            parameters.Add("@Status", svaaspaymentResponse.Status);
            parameters.Add("@CustomerMobileNo", svaaspaymentResponse.CustomerMobileNo);
            parameters.Add("@CustomerEmail", svaaspaymentResponse.CustomerEmail);
            parameters.Add("@Amount", svaaspaymentResponse.Amount);
            parameters.Add("@BillerName", svaaspaymentResponse.BillerName);
            parameters.Add("@BillerMobileNo", svaaspaymentResponse.BillerMobileNo);
            parameters.Add("@BillerEmail", svaaspaymentResponse.BillerEmail);
            parameters.Add("@BillerAddress", svaaspaymentResponse.BillerAddress);
            parameters.Add("@Pincode", svaaspaymentResponse.Pincode);
            parameters.Add("@ProductDescription", svaaspaymentResponse.ProductDescription);
            parameters.Add("@ProductLink", svaaspaymentResponse.ProductLink);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<SvaasSaveResponse>("SaveSvaasresponse", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }

        public async Task<CheckCardTypeResponse> SaveBillPaymentResponse(BillPaymentResponse billPaymentResponse)
        {
            try
            {

                CheckCardTypeResponse result = new CheckCardTypeResponse();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Fk_MemId", billPaymentResponse.Fk_MemId);
                parameters.Add("@RechargeType", billPaymentResponse.RechargeType);
                parameters.Add("@ServiceName", billPaymentResponse.ServiceName);
                parameters.Add("@Amount", billPaymentResponse.Amount);
                parameters.Add("@RechargeNumber", billPaymentResponse.RechargeNumber);
                parameters.Add("@TransId", billPaymentResponse.TransId);
                parameters.Add("@PaymentId", billPaymentResponse.PaymentId);
                parameters.Add("@circle", billPaymentResponse.circle);
                parameters.Add("@Status", billPaymentResponse.Status);
                parameters.Add("@Message", billPaymentResponse.Message);


                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("SavebillPaymentResponse", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<CheckCardTypeResponse> UpdateBillpayemnt(string accountid, string transtype, string txid)
        {
            try
            {

                CheckCardTypeResponse result = new CheckCardTypeResponse();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@txid", txid);
                parameters.Add("@transtype", transtype);
                parameters.Add("@accountid", accountid);


                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("UpdatebillPayment", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<GetBillPayProvider>> GetBilPayBillers(string Type)
        {
            List<GetBillPayProvider> BookingList = new List<GetBillPayProvider>();
            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ebill_Type", Type);


                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    var result = await con.QueryAsync<GetBillPayProvider>("GetBillPayProviders", parameters, commandType: CommandType.StoredProcedure);
                    BookingList = result.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return BookingList;
        }

        public async Task<List<OrderData>> GetSvaasOrders(string Fk_MemId)
        {
            List<OrderData> orderData = new List<OrderData>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Fk_MemId", Fk_MemId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<OrderData>("GetSvaasOrders", parameters, commandType: CommandType.StoredProcedure);
                    orderData = result.ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }


                return orderData;
            }

        }

        public async Task<ResultTravel> GetBillPaymentRequest(string OrderId)
        {
            ResultTravel result = new ResultTravel();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@OrderId", OrderId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = con.Query<ResultTravel>("GetBillPaymentRequest", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return result;
            }
        }

        public async Task<ResultSet> UpdateMemberCard(string paymentId, string OrderId, int OpCode, string KitNo, int ToPayCardId)
        {
            ResultSet result = new ResultSet();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PaymentId", paymentId ?? string.Empty);
            parameters.Add("@OrderId", OrderId ?? string.Empty);
            parameters.Add("@OpCode", OpCode);
            parameters.Add("@KitNo", KitNo);
            parameters.Add("@ToPayCardId", ToPayCardId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    con.Open();
                    result = con.Query<ResultSet>("MemberCard", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                }
                catch
                {
                    throw;
                }
                finally
                {
                    con.Dispose();
                }

                return result;
            }
        }

        public async Task<CheckCardTypeResponse> UpdateOpertortxnId(string accountid, string transtype, string txid)
        {
            try
            {

                CheckCardTypeResponse result = new CheckCardTypeResponse();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@txid", txid);
                parameters.Add("@transtype", transtype);
                parameters.Add("@accountid", accountid);


                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("UpdateOpertortxnId", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<CheckMobileTransfer> RecievedCard(RecivedCardRequest recivedCardRequest)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@KitNo", recivedCardRequest.KitNo);
                parameters.Add("@OTP", recivedCardRequest.OTP);
                parameters.Add("@Fk_MemId", recivedCardRequest.Fk_MemId);
                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    CheckMobileTransfer result = await con.QueryFirstOrDefaultAsync<CheckMobileTransfer>("UpdateRecievedCard", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LoginResponseEcomm> AutoLogin(LoginEcomm model)
        {
            DynamicParameters parameters = new DynamicParameters();

            LoginResponseEcomm result = new LoginResponseEcomm();
            parameters.Add("@InfodeviceId", model.DeviceId);
            parameters.Add("@InfodeviceType", model.DeviceType);
            parameters.Add("@os", model.OSId);
            parameters.Add("@appVersion", model.appVersion);
            parameters.Add("@telecom", model.telecom);
            parameters.Add("@geoCode", model.geoCode);
            parameters.Add("@appId", model.appId);
            parameters.Add("@ipAddress", model.ipAddress);
            parameters.Add("@location", model.location);
            parameters.Add("@mobile", model.mobile);

            parameters.Add("@MemberId", model.MemberId);

            parameters.Add("@DeviceId", model.DeviceId ?? string.Empty);
            parameters.Add("@OSId", model.OSId ?? string.Empty);
            parameters.Add("@DeviceType", model.DeviceType ?? string.Empty);
            parameters.Add("@UserType", model.UserType ?? string.Empty);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    result = await con.QueryFirstAsync<LoginResponseEcomm>("sp_auto_login", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return result;
            }
        }
        public async Task<List<MallProduct>> GetMallCategory(string Fk_MemId)
        {
            List<MallProduct> orderData = new List<MallProduct>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Fk_MemId", Fk_MemId);

            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var result = await con.QueryAsync<MallProduct>("GetCategoryMall", parameters, commandType: CommandType.StoredProcedure);
                    orderData = result.ToList();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return orderData;
            }
        }

        public async Task<SupportFAQResponse> SaveSupportFAQ(SupportFAQList passengermodel)
        {

            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Title", passengermodel.Title);
                parameters.Add("@Discription", passengermodel.Discription);
                parameters.Add("@Link", passengermodel.Link);


                using (con = new SqlConnection(_connectionString.Value))
                {

                    con.Open();
                    SupportFAQResponse result = await con.QueryFirstOrDefaultAsync<SupportFAQResponse>("SaveSuppoertFAQ", parameters, commandType: CommandType.StoredProcedure);
                    con.Dispose();
                    return result;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<CheckCardTypeResponse> SaveLoginLog(string Request, string Response, string MobileNo)
        {
            CheckCardTypeResponse result = new CheckCardTypeResponse();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Request", Request);
            parameters.Add("@Response", Response);
            parameters.Add("@ProfilerId", MobileNo);
            parameters.Add("@Type", "Login");

            using (con = new SqlConnection(_connectionString.Value))
            {

                con.Open();
                result = await con.QueryFirstOrDefaultAsync<CheckCardTypeResponse>("SaveRequestResponse", parameters, commandType: CommandType.StoredProcedure);
                con.Dispose();
                return result;
            }
        }

        public async Task<List<AffiliateLinkDTO>> GetAffiliateLinks()
        {
            List<AffiliateLinkDTO> response = new List<AffiliateLinkDTO>();
            DynamicParameters parameters = new DynamicParameters();
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryMultipleAsync("sp_GetAffiliateLink", parameters, commandType: CommandType.StoredProcedure);
                    response = res.Read<AffiliateLinkDTO>().ToList();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return response;
            }
        }
        public async Task<CustomerInfoDTO> GetMemberDetailByMemberId(int MemberId)
        {
            CustomerInfoDTO response = new CustomerInfoDTO();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Fk_MemberId", MemberId);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryMultipleAsync("sp_GetEmailByMemberId", parameters, commandType: CommandType.StoredProcedure);
                    response = res.Read<CustomerInfoDTO>().FirstOrDefault();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return response;
            }
        } 
        public async Task<CommonResponseDTO> CheckProductPinCodeExistence(ProductPinCodeDTO model)
        {
            CommonResponseDTO response = new CommonResponseDTO();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProductId", model.ProductId);
            parameters.Add("@PinCode", model.PinCode);
            using (con = new SqlConnection(_connectionString.Value))
            {
                try
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                    var res = await con.QueryMultipleAsync("sp_CheckProductPinCodeExistence", parameters, commandType: CommandType.StoredProcedure);
                    response = res.Read<CommonResponseDTO>().FirstOrDefault();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
                return response;
            }
        }

    }
}
