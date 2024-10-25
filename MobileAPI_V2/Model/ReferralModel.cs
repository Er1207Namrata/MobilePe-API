using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace MobileAPI_V2.Model
{
    public class ReferralModelEcomm
    {
        public string Name { get; set; }
    }
    public class TokenModel
    {
        public string token { get; set; }
    }

    public class PincodeModel
    {
        public string StateName { get; set; }
        public string StateId { get; set; }
        public string DistrictName { get; set; }
        public string DistrictId { get; set; }
        public string Pincode { get; set; }

    }
    public class Login
    {
        public DeviceInfo deviceInfo { get; set; }
        public long MemberId { get; set; }

        public string LoginID { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string OSId { get; set; }
        public string UserType { get; set; }
        public string Update { get; set; }
    }
    
   

    public class Registration
    {
        public string MobileNo { get; set; }

        public string InvitedBy { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Pincode { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string OSId { get; set; }
        public string UserType { get; set; }
        public string Otp { get; set; }
        public string dob { get; set; }
    }

    public class WebViewLoginRequest
    {
        public string token { get; set; }
    }

    public class LoginResponse
    {

        public bool IsCardAddressAllow { get; set; }
        public decimal refundableamount { get; set; }
        public bool IsChangedPassword { get; set; }
        public string MobileNo { get; set; }
        public string InvitedBy { get; set; }
        public string InviteCode { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string DOB { get; set; }
        public string JoiningDate { get; set; }
        public string LastLoginDate { get; set; }
        public string District { get; set; }

        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string LoginId { get; set; }
        public string Gender { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public string NomineeDOB { get; set; }
        public string GuardianName { get; set; }
        public string GuardianAddress { get; set; }
        public string GuardianMobileNo { get; set; }
        public string GuardianRelation { get; set; }
        public string KYCStatus { get; set; }
        public string BankStatus { get; set; }
        public string MarritalStatus { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string ProfilePic { get; set; }
        public string AlternateMobile { get; set; }
        public string CurrState { get; set; }
        public string CurrDistrict { get; set; }
        public string CurrPincode { get; set; }
        public string CurrAddress1 { get; set; }
        public bool IsOTPVerified { get; set; }
        public long MemberId { get; set; }
        public string PANNo { get; set; }
        public string AddressProofType { get; set; }
        public string AddressProofNo { get; set; }
        public int currstateId { get; set; }
        public int currdistrictId { get; set; }
        public int districtId { get; set; }
        public int stateId { get; set; }
        public string vpa { get; set; }
        public string PhysicalCard { get; set; }
        public string UserDeviceId { get; set; }
        public string SponsorDeviceId { get; set; }
        public string SponsorName { get; set; }
        public long SponsorId { get; set; }
        public bool isActive { get; set; }
        public bool IsEmailVerified { get; set; }
        public int flag { get; set; }
        public string Msg { get; set; }
        public string token { get; set; }
        public Bankdetails_accountResponse bankdetails { get; set; }
    }


    public class ForgotPassword
    {
        public string LoginId { get; set; }
        public string NewPassword { get; set; }
        public int ProcId { get; set; }

        public string OTP { get; set; }
    }
    public class OTPResult
    {
        public string msg { get; set; }
        public string OTP { get; set; }
        public int Flag { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }


    public class OTPRequest
    {
        //public DeviceInfo deviceInfo { get; set; }
        public string OTPNO { get; set; }
        public string LoginId { get; set; }
        public int ProcId { get; set; }
        public string Purpose { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }

        public string OSId { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
        public long memberId { get; set; }
        public string mid { get; set; }
        public string KitNo { get; set; }

    }
    public class DeviceInfo
    {
        public string deviceId { get; set; }
        public string deviceType { get; set; }
        public string os { get; set; }
        public string appVersion { get; set; }
        public string telecom { get; set; }
        public string geoCode { get; set; }
        public string appId { get; set; }
        public string ipAddress { get; set; }
        public string location { get; set; }
        public string mobile { get; set; }
    }
    public class AppVersionResponse
    {
        public bool IsForced { get; set; }
        public string message { get; set; }
        public string version { get; set; }
        public string response { get; set; }
        public long Id { get; set; }

    }
    public class ChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public long memberId { get; set; }
    }
    public class SignOutRequest
    {
        public long FK_MemId { get; set; }
        public string OSId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; }
    }

    public class ProfileRequest
    {
        public string MobileNo { get; set; }
        public string PhysicalCard { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Pincode { get; set; }
        public int StateId { get; set; }
        public string DOB { get; set; }
        public int DistrictId { get; set; }
        public string Token { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string LoginId { get; set; }
        public string Gender { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public string NomineeDOB { get; set; }
        public string GuardianName { get; set; }
        public string GuardianAddress { get; set; }
        public string GuardianMobileNo { get; set; }
        public string GuardianRelation { get; set; }
        public string KYCStatus { get; set; }
        public string BankStatus { get; set; }
        public string MarritalStatus { get; set; }
        public string Address1 { get; set; }
        public string AlternateMobile { get; set; }
        public int CurrStateId { get; set; }
        public int CurrDistrictId { get; set; }
        public string CurrPincode { get; set; }
        public string CurrAddress1 { get; set; }
        public string PANNo { get; set; }
        public string AddressProofType { get; set; }
        public string AddressProofNo { get; set; }

        public long MemberId { get; set; }
    }

    public class CommissionCalculationRequest
    {

        public int Number { get; set; }
        public decimal Amount { get; set; }
        public string CommType { get; set; }
    }


    public class CommissionCalculation
    {
        public decimal Amount { get; set; }
    }
    public class BusinessLevelResult
    {
        public IEnumerable<BusinessLevel> CUG { get; set; }
        public IEnumerable<Club> OtherIncome { get; set; }
        public bool kycstatus { get; set; }
        public int bankstatus { get; set; }

    }
    public class BusinessLevel
    {
        public string Level { get; set; }
        public string CompleteLevel { get; set; }
        public string LevelName { get; set; }
    }
    public class Club
    {
        public string clubName { get; set; }
        public string Daysleft { get; set; }
        public long UserRefered { get; set; }
        public bool Status { get; set; }
    }
    public class Wallet
    {
        public decimal UnclearAmount { get; set; }
        public decimal HoldAmount { get; set; }
        public decimal CommissionAmount { get; set; }
        public long RewardPoint { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsOTPVerified { get; set; }
        public bool IsPaySecurity { get; set; }
        public bool IsKitActive { get; set; }
        public string MobilePeGoldClubMember { get; set; }
        public string CODBenifits { get; set; }
        public string Qualification { get; set; }
        public string Benifits { get; set; }
        public string TransactionId { get; set; }

        public decimal TotalAmount { get; set; }
    }
    public class Walletdata
    {
        public string name { get; set; }
        public long value { get; set; }
    }
    public class walletResult
    {
        public Wallet wallet { get; set; }
        public IEnumerable<Walletdata> walletdata { get; set; }
    }

    public class OperatorCodeModel
    {
        public string OperatorName { get; set; }
        public string OperatorCode { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBillFetchRequired { get; set; }

    }
    public class BillerModel
    {
        public string BillerName { get; set; }
        public string Icon { get; set; }


    }
    public class RegistrationV3
    {
        // public DeviceInfo deviceInfo { get; set; }
        public long MemberId { get; set; }

        public string MobileNo { get; set; }
        public string InvitedBy { get; set; }
        public string Email { get; set; }
        public int ProcId { get; set; }
        public string FullName { get; set; }
        public string Pincode { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string OSId { get; set; }
        public string Otp { get; set; }
        public string Password { get; set; }
        public string Dob { get; set; }
        public string ConfirmPassword { get; set; }

    }

    public class CardDispatchDetail
    {

        public string landmark { get; set; }
        public long memberId { get; set; }
        public string NameonCard { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string pincode { get; set; }
        public long state { get; set; }
        public long city { get; set; }
        public string stateName { get; set; }
        public string cityName { get; set; }
        public string refno { get; set; }
        public string imgurl { get; set; }
        public string CardImageUrl { get; set; }
        public string transactionId { get; set; }
        public bool IsEdit { get; set; }
        public string trackLink { get; set; }
        public string docketno { get; set; }
        public string dispatchdate { get; set; }
    }

   
}

    public class ViewProfile
    {
        public string Fk_MemId { get; set; }
    }




