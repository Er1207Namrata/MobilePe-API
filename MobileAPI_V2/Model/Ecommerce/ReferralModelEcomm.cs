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
    public class ReferralModelEcommEcomm
    {
        public string Name { get; set; }
    }
    public class TokenModelEcomm
    {
        public string token { get; set; }
    }
    public class PincodeModelEcomm
    {
        public string StateName { get; set; }
        public string StateId { get; set; }
        public string DistrictName { get; set; }
        public string DistrictId { get; set; }
        public string Pincode { get; set; }

    }
    public class LoginEcomm
    {
        public DeviceInfoEcomm deviceInfo { get; set; }
        public long MemberId { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string OSId { get; set; }
        public string UserType { get; set; }
        public string Update { get; set; }
        public string appVersion { get;  set; }
        public string telecom { get;  set; }
        public string geoCode { get;  set; }
        public string appId { get;  set; }
        public string ipAddress { get;  set; }
        public string location { get;  set; }
        public string mobile { get;  set; }
    }
    public class RegistrationEcomm
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
    public class WebViewLoginRequestEcomm
    {
        public string token { get; set; }
    }
    public class LoginResponseEcomm
    {

        public bool isCardAddressAllow { get; set; }
        public int refundableAmount { get; set; }
        public bool isChangedPassword { get; set; }
        public string mobileNo { get; set; }
        public string invitedBy { get; set; }
        public string inviteCode { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string pincode { get; set; }
        public string state { get; set; }
        public string dob { get; set; }
        public string joiningDate { get; set; }
        public string lastLoginDate { get; set; }
        public string district { get; set; }

        public string motherName { get; set; }
        public string fatherName { get; set; }
        public string loginId { get; set; }
        public string gender { get; set; }
        public string nomineeName { get; set; }
        public string nomineeRelation { get; set; }
        public string nomineeDOB { get; set; }
        public string guardianName { get; set; }
        public string guardianAddress { get; set; }
        public string guardianMobileNo { get; set; }
        public string guardianRelation { get; set; }
        public string kycStatus { get; set; }
        public string bankStatus { get; set; }
        public string marritalStatus { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string profilePic { get; set; }
        public string alternateMobile { get; set; }
        public string currState { get; set; }
        public string currDistrict { get; set; }
        public string currPincode { get; set; }
        public string currAddress1 { get; set; }
        public bool isOTPVerified { get; set; }
        public long memberId { get; set; }
        public string panNo { get; set; }
        public string addressProofType { get; set; }
        public string addressProofNo { get; set; }
        public int currstateId { get; set; }
        public int currdistrictId { get; set; }
        public int districtId { get; set; }
        public int stateId { get; set; }
        public string vpa { get; set; }
        public string physicalCard { get; set; }
        public string userDeviceId { get; set; }
        public string sponsorDeviceId { get; set; }
        public string sponsorName { get; set; }
        public long sponsorId { get; set; }
        public bool isActive { get; set; }
        public bool isEmailVerified { get; set; }
        public int flag { get; set; }
        public string msg { get; set; }
        public string token { get; set; }
        public bool AppStatus { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Bankdetails_accountResponseEcomm bankDetails { get; set; }
    }
    public class ForgotPasswordEcomm
    {
        public DeviceInfoEcomm deviceInfo { get; set; }
        public string LoginId { get; set; }
        public string NewPassword { get; set; }
        public int ProcId { get; set; }

        public string OTP { get; set; }
    }
    public class OTPResultEcomm
    {
        public string msg { get; set; }
        public string OTP { get; set; }
        public int Flag { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
    public class OTPRequestEcomm
    {
        public DeviceInfoEcomm deviceInfo { get; set; }
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
    public class DeviceInfoEcomm
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
    public class AppVersionResponseEcomm
    {
        public bool IsForced { get; set; }
        public string message { get; set; }
        public string version { get; set; }
        public string response { get; set; }
        public long Id { get; set; }

    }
    public class ChangePasswordEcomm
    {
        public DeviceInfoEcomm deviceInfo { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string OSId { get; set; }
        public long memberId { get; set; }
    }
    public class SignOutRequestEcomm
    {
        public DeviceInfoEcomm deviceInfo { get; set; }
        public long FK_MemId { get; set; }
        public string OSId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; }
    }
    public class ProfileRequestEcomm
    {
        public DeviceInfoEcomm deviceInfo { get; set; }
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
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
    }
    public class CommissionCalculationRequestEcomm
    {

        public int Number { get; set; }
        public decimal Amount { get; set; }
        public string CommType { get; set; }
    }
    public class CommissionCalculationEcomm
    {
        public decimal amount { get; set; }
    }
    public class BusinessLevelResultEcomm
    {
        public IEnumerable<BusinessLevelEcomm> cug { get; set; }
        public IEnumerable<ClubEcomm> otherIncome { get; set; }
        public bool kycStatus { get; set; }
        public int bankStatus { get; set; }

    }
    public class BusinessLevelEcomm
    {
        public string level { get; set; }
        public string completeLevel { get; set; }
        public string levelName { get; set; }
    }
    public class ClubEcomm
    {
        public string clubName { get; set; }
        public string daysLeft { get; set; }
        public long userRefered { get; set; }
        public bool status { get; set; }
    }
    public class WalletEcomm
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
    public class WalletdataEcomm
    {
        public string name { get; set; }
        public long value { get; set; }
    }
    public class walletResultEcomm
    {
        public WalletEcomm wallet { get; set; }
        public IEnumerable<WalletdataEcomm> walletdata { get; set; }
    }
    public class OperatorCodeModelEcomm
    {
        public string OperatorName { get; set; }
        public string OperatorCode { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBillFetchRequired { get; set; }

    }
    public class BillerModelEcomm
    {
        public string BillerName { get; set; }
        public string Icon { get; set; }


    }
    public class RegistrationV3Ecomm
    {
        public DeviceInfoEcomm deviceInfo { get; set; }
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
        public string ConfirmPassword { get; set; }
        public string Dob { get; set; }

    }
    public class CardDispatchDetailEcomm
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
