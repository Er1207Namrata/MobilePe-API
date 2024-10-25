using Newtonsoft.Json;
using System.IO;
using System.Net;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace MobileAPI_V2.Model
{
    public class IRCTCModel : Common
    {
        public object result { get; set; }
    }
    public class IRCTCModel1 : Common
    {
        public IRCTCResponseModel result { get; set; }
    }
    public class IRCTCResponseModel
    {
        public string displayMessage { get; set; }
        public string scrollMsg { get; set; }
        public string informationMessage { get; set; }
        public string timeStamp { get; set; }

    }
    public class CancelIRCTCTicket
    {
        public string success { get; set; }
    }
    public class Item
    {
        public List<StationCode> StationCode { get; set; }


    }
    public class StationCode
    {
        public string Name { get; set; }
    }
    public class stationCodeList
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class IRCTCResponse
    {

        public int statuscode { get; set; }
        public string responseText { get; set; }
    }
    public class IRCTCService
    {
        string IRCTCUserName = string.Empty;
        string IRCTCPassword = string.Empty;
        string IRCTCBASEURL = string.Empty;
        public IRCTCService(IConfiguration configuration)
        {
            IRCTCUserName = configuration["IRCTCUserName"];
            IRCTCPassword = configuration["IRCTCPassword"];
            IRCTCBASEURL = configuration["IRCTCBASEURL"];
        }

        public IRCTCResponse sendRequest(string url, string Method, string body, long MemberId)
        {
            IRCTCResponse res = new IRCTCResponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {
                url = IRCTCBASEURL + url;

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                if (request != null)
                {


                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;

                    request.ContentType = "application/json";
                    String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(IRCTCUserName + ":" + IRCTCPassword));
                    request.Headers.Add("Authorization", "Basic " + encoded);
                    if (Method == "POST")
                    {

                        using (Stream s = request.GetRequestStream())
                        {
                            using (StreamWriter sw = new StreamWriter(s))
                                sw.Write(body);
                        }

                    }
                    response = (HttpWebResponse)request.GetResponse();
                    using (Stream s = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                            responseText = sr.ReadToEnd();
                        res.responseText = responseText;

                        res.statuscode = (int)response.StatusCode;
                    }


                }

            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {

                            res.responseText = wex.Message;
                        }
                    }
                }
            }
            return res;
        }

        public IRCTCResponse RegsendRequest(string url, string Method, string body, long MemberId)
        {
            IRCTCResponse res = new IRCTCResponse();
            HttpWebResponse response = null;
            string responseText = "";
            try
            {
                url = url;

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                if (request != null)
                {


                    request.Timeout = 50000;
                    request.Method = Method;
                    request.KeepAlive = true;
                    request.UseDefaultCredentials = true;

                    request.ContentType = "application/json";
                    String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(IRCTCUserName + ":" + IRCTCPassword));
                    request.Headers.Add("Authorization", "Basic " + encoded);
                    if (Method == "POST")
                    {

                        using (Stream s = request.GetRequestStream())
                        {
                            using (StreamWriter sw = new StreamWriter(s))
                                sw.Write(body);
                        }

                    }
                    response = (HttpWebResponse)request.GetResponse();
                    using (Stream s = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                            responseText = sr.ReadToEnd();
                        res.responseText = responseText;

                        res.statuscode = (int)response.StatusCode;
                    }


                }

            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {

                            res.responseText = wex.Message;
                        }
                    }
                }
            }
            return res;
        }
        public object ToApiRequest(object requestObject)
        {
            switch (requestObject)
            {
                case JObject jObject: // objects become Dictionary<string,object>
                    return ((IEnumerable<KeyValuePair<string, JToken>>)jObject).ToDictionary(j => j.Key, j => ToApiRequest(j.Value));
                case JArray jArray: // arrays become List<object>
                    return jArray.Select(ToApiRequest).ToList();
                case JValue jValue: // values just become the value
                    return jValue.Value;
                default: // don't know what to do here
                    throw new Exception($"Unsupported type: {requestObject.GetType()}");
            }
        }

        public IRCTCModel ToApiResponse(JObject jo)
        {
            IRCTCModel commonResponse = new IRCTCModel();
            if (jo.ContainsKey("errorMessage"))
            {
                commonResponse.message = jo["errorMessage"].ToString();
                commonResponse.response = "error";
            }
            else if (jo.ContainsKey("errorMsg"))
            {
                commonResponse.message = jo["errorMsg"].ToString();
                commonResponse.response = "error";
            }
            else if (jo.ContainsKey("error"))
            {
                commonResponse.message = jo["error"].ToString();
                commonResponse.response = "error";
            }
            else if (jo.ContainsKey("success") && jo["success"].ToString() == "false")
            {
                commonResponse.message = jo["message"].ToString();
                commonResponse.response = "error";
            }

            else if (jo.ContainsKey("success") && jo["success"].ToString() == "true")
            {
                commonResponse.message = jo["message"].ToString();
                commonResponse.response = "success";
                commonResponse.result = ToApiRequest(jo);
            }
            else if (jo.ContainsKey("message"))
            {
                commonResponse.message = jo["message"].ToString();
                commonResponse.response = "error";
            }
            else
            {
                commonResponse.response = "success";
                commonResponse.message = "success";
                commonResponse.result = ToApiRequest(jo);
            }
            return commonResponse;
        }


    }

    public class BookingResponse
    {

        public string Fk_MemId { get; set; }
        public string baseFare { get; set; }
        public string cateringCharge { get; set; }
        public string distance { get; set; }
        public string dynamicFare { get; set; }
        public string enqClass { get; set; }
        public string from { get; set; }
        public string fuelAmount { get; set; }
        public string insuredPsgnCount { get; set; }
        public DateTime nextEnqDate { get; set; }
        public string otherCharge { get; set; }
        public string otpAuthenticationFlag { get; set; }
        public DateTime preEnqDate { get; set; }
        public string quota { get; set; }
        public string reqEnqParam { get; set; }
        public string reservationCharge { get; set; }
        public string serverId { get; set; }
        public string serviceTax { get; set; }
        public string superfastCharge { get; set; }
        public string tatkalFare { get; set; }
        public DateTime timeStamp { get; set; }
        public string to { get; set; }
        public string totalConcession { get; set; }
        public string totalFare { get; set; }
        public string trainName { get; set; }
        public string trainNo { get; set; }
        public string travelInsuranceCharge { get; set; }
        public string travelInsuranceServiceTax { get; set; }
        public string wpServiceCharge { get; set; }
        public string wpServiceTax { get; set; }
        public List<AvlDayList> avlDayList { get; set; }
        public List<BkgCfg> bkgCfg { get; set; }
        public string ftBookingMsgFlag { get; set; }
        public List<InformationMessage> informationMessage { get; set; }
        public string rdsTxnPwdFlag { get; set; }
        public string taRdsFlag { get; set; }
        public string totalCollectibleAmount { get; set; }
        public string trainsiteId { get; set; }
        public string upiRdsFlag { get; set; }
        public string IsCancelled { get; set; }
        public string IsDeleted { get; set; }







    }
    public class InformationMessage
    {
        public string message { get; set; }
        public string paramName { get; set; }
        public string popup { get; set; }
    }
    public class AvlDayList
    {
        public string availablityDate { get; set; }
        public string availablityStatus { get; set; }
        public string availablityType { get; set; }
        public string currentBkgFlag { get; set; }
        public string reason { get; set; }
        public string reasonType { get; set; }
        public string wlType { get; set; }
    }

    public class BkgCfg
    {
        public string acuralBooking { get; set; }
        public List<string> applicableBerthTypes { get; set; }
        public string atasEnable { get; set; }
        public string bedRollFlagEnabled { get; set; }
        public string beyondArpBooking { get; set; }
        public List<string> bonafideCountryList { get; set; }
        public string captureAddress { get; set; }
        public string childBerthMandatory { get; set; }
        public string foodChoiceEnabled { get; set; }
        public string forgoConcession { get; set; }
        public string gatimaanTrain { get; set; }
        public string gstDetailInputFlag { get; set; }
        public string gstinPattern { get; set; }
        public string idRequired { get; set; }
        public string lowerBerthApplicable { get; set; }
        public string maxARPDays { get; set; }
        public string maxChildAge { get; set; }
        public string maxIdCardLength { get; set; }
        public string maxInfants { get; set; }
        public string maxMasterListPsgn { get; set; }
        public string maxNameLength { get; set; }
        public string maxPassengerAge { get; set; }
        public string maxPassengers { get; set; }
        public string maxPassportLength { get; set; }
        public string maxRetentionDays { get; set; }
        public string minIdCardLength { get; set; }
        public string minNameLength { get; set; }
        public string minPassengerAge { get; set; }
        public string minPassportLength { get; set; }
        public string newTimeTable { get; set; }
        public string pmfInputEnable { get; set; }
        public string pmfInputMandatory { get; set; }
        public string pmfInputMaxLength { get; set; }
        public string redemptionBooking { get; set; }
        public string seniorCitizenApplicable { get; set; }
        public string specialTatkal { get; set; }
        public string srctnwAge { get; set; }
        public string srctzTAge { get; set; }
        public string srctznAge { get; set; }
        public string suvidhaTrain { get; set; }
        public string trainsiteId { get; set; }
        public string travelInsuranceEnabled { get; set; }
        public string travelInsuranceFareMsg { get; set; }
        public string twoSSReleaseFlag { get; set; }
        public string uidMandatoryFlag { get; set; }
        public string uidVerificationMasterListFlag { get; set; }
        public string uidVerificationPsgnInputFlag { get; set; }
        public List<string> validIdCardTypes { get; set; }
    }

    public class PrintTicketResposnse1
    {
        public DateTime boardingDate { get; set; }
        public string boardingStn { get; set; }
        public string cateringCharge { get; set; }
        public string distance { get; set; }
        public string fuelAmount { get; set; }
        public DateTime journeyDate { get; set; }
        public string otpAuthenticationFlag { get; set; }
        public string reservationCharge { get; set; }
        public string serverId { get; set; }
        public string serviceTax { get; set; }
        public string superfastCharge { get; set; }
        public string tatkalFare { get; set; }
        public DateTime timeStamp { get; set; }
        public string totalFare { get; set; }
        public string trainName { get; set; }
        public string trainOwner { get; set; }
        public string wpServiceCharge { get; set; }
        public string wpServiceTax { get; set; }
        public string arrivalTime { get; set; }
        public string avlForVikalp { get; set; }
        public DateTime bookingDate { get; set; }
        public string canSpouseFlag { get; set; }
        public string complaintFlag { get; set; }
        public string departureTime { get; set; }
        public DateTime destArrvDate { get; set; }
        public string destStn { get; set; }
        public string fromStn { get; set; }
        public GstCharge gstCharge { get; set; }
        public string insuranceOpted { get; set; }
        public string journeyClass { get; set; }
        public string journeyLap { get; set; }
        public string journeyQuota { get; set; }
        public string lapNumber { get; set; }
        public string linkedPnr { get; set; }
        public string mahakalFlag { get; set; }
        public string mealChoiceEnable { get; set; }
        public string mlJourneyType { get; set; }
        public string mlReservationStatus { get; set; }
        public string mlTimeDiff { get; set; }
        public string mlTransactionStatus { get; set; }
        public string mlUserId { get; set; }
        public string multiLapFlag { get; set; }
        public string numberOfAdults { get; set; }
        public string numberOfChilds { get; set; }
        public string numberOfpassenger { get; set; }
        public string pnrNumber { get; set; }
        public string policyStatus { get; set; }
        public List<PsgnDtlList> psgnDtlList { get; set; }
        public string reasonIndex { get; set; }
        public string reasonType { get; set; }
        public string requestedClientTransactionId { get; set; }
        public string reservationId { get; set; }
        public string reservationStatus { get; set; }
        public string resvnUptoStn { get; set; }
        public string sai { get; set; }
        public string sectorId { get; set; }
        public string timeDiff { get; set; }
        public string timeTableFlag { get; set; }
        public string totalCollectibleAmount { get; set; }
        public string totalRefundAmount { get; set; }
        public string trainNumber { get; set; }
        public string travelnsuranceRefundAmount { get; set; }
        public string vikalpStatus { get; set; }
        public string insuranceCharge { get; set; }
        public string insuranceCompany { get; set; }
        public string insuranceCompanyUrl { get; set; }
    }

    public class PrintTicketResposnse
    {
        public DateTime boardingDate { get; set; }
        public string boardingStn { get; set; }
        public string cateringCharge { get; set; }
        public string distance { get; set; }
        public string fuelAmount { get; set; }
        public DateTime journeyDate { get; set; }
        public string otpAuthenticationFlag { get; set; }
        public string reservationCharge { get; set; }
        public string serverId { get; set; }
        public string serviceTax { get; set; }
        public string superfastCharge { get; set; }
        public string tatkalFare { get; set; }
        public DateTime timeStamp { get; set; }
        public string totalFare { get; set; }
        public string trainName { get; set; }
        public string trainOwner { get; set; }
        public string wpServiceCharge { get; set; }
        public string wpServiceTax { get; set; }
        public string arrivalTime { get; set; }
        public string avlForVikalp { get; set; }
        public DateTime bookingDate { get; set; }
        public string canSpouseFlag { get; set; }
        public string complaintFlag { get; set; }
        public string departureTime { get; set; }
        public DateTime destArrvDate { get; set; }
        public string destStn { get; set; }
        public string fromStn { get; set; }
        public GstCharge gstCharge { get; set; }
        public string insuranceOpted { get; set; }
        public string journeyClass { get; set; }
        public string journeyLap { get; set; }
        public string journeyQuota { get; set; }
        public string lapNumber { get; set; }
        public string linkedPnr { get; set; }
        public string mahakalFlag { get; set; }
        public string mealChoiceEnable { get; set; }
        public string mlJourneyType { get; set; }
        public string mlReservationStatus { get; set; }
        public string mlTimeDiff { get; set; }
        public string mlTransactionStatus { get; set; }
        public string mlUserId { get; set; }
        public string multiLapFlag { get; set; }
        public string numberOfAdults { get; set; }
        public string numberOfChilds { get; set; }
        public string numberOfpassenger { get; set; }
        public string pnrNumber { get; set; }
        public string policyStatus { get; set; }
        public PsgnDtlList psgnDtlList { get; set; }
        public string reasonIndex { get; set; }
        public string reasonType { get; set; }
        public string requestedClientTransactionId { get; set; }
        public string reservationId { get; set; }
        public string reservationStatus { get; set; }
        public string resvnUptoStn { get; set; }
        public string sai { get; set; }
        public string sectorId { get; set; }
        public string timeDiff { get; set; }
        public string timeTableFlag { get; set; }
        public string totalCollectibleAmount { get; set; }
        public string totalRefundAmount { get; set; }
        public string trainNumber { get; set; }
        public string travelnsuranceRefundAmount { get; set; }
        public string vikalpStatus { get; set; }
        public string insuranceCharge { get; set; }
        public string insuranceCompany { get; set; }
        public string insuranceCompanyUrl { get; set; }
    }
    public class GstCharge
    {
        public string irctcCgstCharge { get; set; }
        public string irctcIgstCharge { get; set; }
        public string irctcSgstCharge { get; set; }
        public string irctcUgstCharge { get; set; }
        public string totalIrctcGst { get; set; }
        public string totalPRSGst { get; set; }
        public string invoiceNumber { get; set; }
        public string suplierAddress { get; set; }
        public string sacCode { get; set; }
        public string gstinSuplier { get; set; }
        public string sgstRate { get; set; }
        public string igstRate { get; set; }
        public string cgstRate { get; set; }
        public string taxableAmt { get; set; }
        public string prsSuplierStateCode { get; set; }
        public string prsSuplierState { get; set; }
    }

    public class PsgnDtlList
    {
        public string bookingBerthCode { get; set; }
        public string bookingBerthNo { get; set; }
        public string bookingCoachId { get; set; }
        public string bookingStatus { get; set; }
        public string bookingStatusIndex { get; set; }
        public string currentBerthCode { get; set; }
        public string currentBerthNo { get; set; }
        public string currentStatus { get; set; }
        public string currentStatusIndex { get; set; }
        public string dropWaitlistFlag { get; set; }
        public string fareChargedPercentage { get; set; }
        public string passUPN { get; set; }
        public string passengerAge { get; set; }
        public string passengerBerthChoice { get; set; }
        public string passengerGender { get; set; }
        public string passengerIcardFlag { get; set; }
        public string passengerName { get; set; }
        public string passengerNationality { get; set; }
        public string passengerNetFare { get; set; }
        public string passengerSerialNumber { get; set; }
        public string policyNumber { get; set; }
        public string psgnwlType { get; set; }
        public string validationFlag { get; set; }
    }

    public class CommonTrainStation
    {
         public TrainStationListRes result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }

    public class TrainStationListRes
    {
        public List<TrainBtwnStnsList> trainBtwnStnsList { get; set; }
        public List<string> quotaList { get; set; }
        public string serverId { get; set; }
        public DateTime timeStamp { get; set; }
        public string vikalpInSpecialTrainsAccomFlag { get; set; }
        public string oneStopJourny { get; set; }
        public string serveyFlag { get; set; }
        public string alternateEnquiryFlag { get; set; }
        public string statuscode { get; set; }
    }
    public class TrainBtwnStnsList
    {
        public string trainNumber { get; set; }
        public string trainName { get; set; }
        public string fromStnCode { get; set; }
        public string toStnCode { get; set; }
        public string arrivalTime { get; set; }
        public string departureTime { get; set; }
        public string distance { get; set; }
        public string duration { get; set; }
        public string runningMon { get; set; }
        public string runningTue { get; set; }
        public string runningWed { get; set; }
        public string runningThu { get; set; }
        public string runningFri { get; set; }
        public string runningSat { get; set; }
        public string runningSun { get; set; }
        public List<string> avlClasses { get; set; }
        public List<string> trainType { get; set; }
        public string atasOpted { get; set; }
        public string flexiFlag { get; set; }
        public string trainOwner { get; set; }
        public string trainsiteId { get; set; }
    }

    public class CommonUserStatus
    {
        public UserStatusres result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }
    public class UserStatusres
    {
        public string userId { get; set; }
        public string status { get; set; }
        public string serverId { get; set; }
        public string timeStamp { get; set; }
        public string infoFlag { get; set; }
        public string errorIndex { get; set; }
        
        
    }

    public class CommonTrainScheduleEnquiry
    {
        public TrainScheduleEnquiry result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }
    public class TrainScheduleEnquiry
    {
        public string trainNumber { get; set; }
        public string trainName { get; set; }
        public string stationFrom { get; set; }
        public string stationTo { get; set; }
        public string trainOwner { get; set; }
        public string trainRunsOnMon { get; set; }
        public string trainRunsOnTue { get; set; }
        public string trainRunsOnWed { get; set; }
        public string trainRunsOnThu { get; set; }
        public string trainRunsOnFri { get; set; }
        public string trainRunsOnSat { get; set; }
        public string trainRunsOnSun { get; set; }
        public string serverId { get; set; }
        public DateTime timeStamp { get; set; }
        public string duration { get; set; }
        public List<StationList> stationList { get; set; }
    }

    public class StationList
    {
        public string stationCode { get; set; }
        public string stationName { get; set; }
        public string arrivalTime { get; set; }
        public string departureTime { get; set; }
        public string routeNumber { get; set; }
        public string haltTime { get; set; }
        public string distance { get; set; }
        public string dayCount { get; set; }
        public string stnSerialNumber { get; set; }
        public string boardingDisabled { get; set; }
    }

    public class CommonIRCTCPINDetails
    {
        public IRCTCPINDetails result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }
    public class IRCTCPINDetails
    {
        public string state { get; set; }
        public List<string> stateList { get; set; }
        public List<string> cityList { get; set; }
        public string serverId { get; set; }
        public DateTime timeStamp { get; set; }
    }



    // get booking Detail Model

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CommonIRCTCBookingDetails
    {
        public GetBokkingDetailRes result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }

    public class BookingGstCharge
    {
        public string totalPRSGst { get; set; }
        public string irctcCgstCharge { get; set; }
        public string irctcSgstCharge { get; set; }
        public string irctcIgstCharge { get; set; }
        public string irctcUgstCharge { get; set; }
        public string totalIrctcGst { get; set; }
    }

    public class BookingPsgnDtlList
    {
        public string passengerSerialNumber { get; set; }
        public string passengerName { get; set; }
        public string passengerAge { get; set; }
        public string passengerGender { get; set; }
        public string passengerBerthChoice { get; set; }
        public string passengerIcardFlag { get; set; }
        public string passengerNationality { get; set; }
        public string fareChargedPercentage { get; set; }
        public string validationFlag { get; set; }
        public string bookingStatusIndex { get; set; }
        public string bookingStatus { get; set; }
        public string bookingCoachId { get; set; }
        public string bookingBerthNo { get; set; }
        public string bookingBerthCode { get; set; }
        public string currentStatusIndex { get; set; }
        public string currentStatus { get; set; }
        public string currentBerthNo { get; set; }
        public string currentBerthCode { get; set; }
        public string passengerNetFare { get; set; }
        public string policyNumber { get; set; }
        public string psgnwlType { get; set; }
        public string dropWaitlistFlag { get; set; }
    }

    public class GetBokkingDetailRes
    {
        public string trainName { get; set; }
        public string distance { get; set; }
        public string boardingStn { get; set; }
        public string boardingDate { get; set; }
        public string journeyDate { get; set; }
        public string trainOwner { get; set; }
        public string reservationCharge { get; set; }
        public string superfastCharge { get; set; }
        public string fuelAmount { get; set; }
        public string tatkalFare { get; set; }
        public string serviceTax { get; set; }
        public string cateringCharge { get; set; }
        public string totalFare { get; set; }
        public string wpServiceCharge { get; set; }
        public string wpServiceTax { get; set; }
        public string travelInsuranceCharge { get; set; }
        public string travelInsuranceServiceTax { get; set; }
        public string insuredPsgnCount { get; set; }
        public string serverId { get; set; }
        public string timeStamp { get; set; }
        public string otpAuthenticationFlag { get; set; }
        public string reservationId { get; set; }
        public string lapNumber { get; set; }
        public string requestedClientTransactionId { get; set; }
        public string numberOfpassenger { get; set; }
        public string timeTableFlag { get; set; }
        public string pnrNumber { get; set; }
        public string departureTime { get; set; }
        public string arrivalTime { get; set; }
        public string reasonType { get; set; }
        public string reasonIndex { get; set; }
        public List<string> informationMessage { get; set; }
        public string destArrvDate { get; set; }
        public string bookingDate { get; set; }
        public string numberOfChilds { get; set; }
        public string numberOfAdults { get; set; }
        public string trainNumber { get; set; }
        public string fromStn { get; set; }
        public string destStn { get; set; }
        public string resvnUptoStn { get; set; }
        public string journeyClass { get; set; }
        public string journeyQuota { get; set; }
        public string insuranceCharge { get; set; }
        public string totalCollectibleAmount { get; set; }
        public List<BookingPsgnDtlList> psgnDtlList { get; set; }
        public string reservationStatus { get; set; }
        public string policyIssueDate { get; set; }
        public string insuranceCompany { get; set; }
        public string insuranceCompanyUrl { get; set; }
        public string insuranceOpted { get; set; }
        public string policyStatus { get; set; }
        public string avlForVikalp { get; set; }
        public string vikalpStatus { get; set; }
        public BookingGstCharge gstCharge { get; set; }
        public string linkedPnr { get; set; }
        public string sai { get; set; }
        public string journeyLap { get; set; }
        public string sectorId { get; set; }
        public string canSpouseFlag { get; set; }
        public string mahakalFlag { get; set; }
        public string mealChoiceEnable { get; set; }
        public string complaintFlag { get; set; }
        public string travelnsuranceRefundAmount { get; set; }
        public string multiLapFlag { get; set; }
        public string mlUserId { get; set; }
        public string mlReservationStatus { get; set; }
        public string mlTransactionStatus { get; set; }
        public string mlJourneyType { get; set; }
        public string timeDiff { get; set; }
        public string mlTimeDiff { get; set; }
        public string totalRefundAmount { get; set; }
    }

    // Ticket Refund Details

    public class CommonTicketRefundDetails
    {
        public TicketRefundRes result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }
    public class CanList
    {
        public string success { get; set; }
        public string refundAmount { get; set; }
        public string pnrNo { get; set; }
        public string amountCollected { get; set; }
        public string cashDeducted { get; set; }
        public string cancelledDate { get; set; }
        public List<string> name { get; set; }
        public List<string> currentStatus { get; set; }
        public string message { get; set; }
        public string cancellationId { get; set; }
        public GstChargeDTO gstChargeDTO { get; set; }
        public string travelinsuranceRefundAmount { get; set; }
    }

    public class GstChargeDTO
    {
        public string totalPRSGst { get; set; }
        public string prsSuplierState { get; set; }
        public string canChrgGst { get; set; }
        public string canChrgCgst { get; set; }
        public string canChrgSgst { get; set; }
        public string canChrgIgst { get; set; }
        public string canChrgUgst { get; set; }
        public string creditGst { get; set; }
        public string creditCgst { get; set; }
        public string creditSgst { get; set; }
        public string creditIgst { get; set; }
        public string creditUgst { get; set; }
        public string cancelInvoiceNo { get; set; }
    }

    public class TicketRefundRes
    {
        public string totalAmount { get; set; }
        public List<CanList> canList { get; set; }
        public string serverId { get; set; }
        public string timeStamp { get; set; }
    }




}
