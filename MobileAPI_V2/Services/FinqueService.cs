using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.DataLayer;
using MobileAPI_V2.Model.BillPayment;
using MobileAPI_V2.Model.Fineque;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Threading.Tasks;
using System.Net;
using static MobileAPI_V2.Model.BillPayment.BillPaymentCommon;
using MobileAPI_V2.Model;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace MobileAPI_V2.Services
{
    public class FinqueService
    {
        private readonly ApiManager _apiManager;
        private readonly ConnectionString _connectionString;
        private readonly IHttpContextAccessor _ctx;
        IDbConnection con;
        public readonly string ASEkey;
        public static string PartnerSecret = string.Empty;
        public static string PartnerKey = string.Empty;
        public static string merchantId = string.Empty;
        public static string HashRequestCode = string.Empty;
        private readonly DBHelperService _DbHelperService;
        public FinqueService(ApiManager ApiManager, ConnectionString connectionString, DBHelperService DBHelperService)
        {

            _apiManager = ApiManager;
            _connectionString = connectionString;
            PartnerSecret = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("PartnerSecret").Value;
            PartnerKey = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("PartnerKey").Value;
            merchantId = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("merchantId").Value;
            HashRequestCode = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("HashRequestCode").Value;
            ASEkey = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
        }
        public string GetRequestHashCode()
        {
            GenerateHash model = new GenerateHash();
            string modeReq = "";
            model.partnerKey = PartnerKey;
            model.partnerSecret = PartnerSecret;
            string body = JsonConvert.SerializeObject(model);
            var _response = _apiManager.Post<TokenResponseDTO>("generateHash/v2", body, "").Result;
            //string modeReq = _apiManager.Get("generateHash/v2");
            return modeReq;
        }
        public TokenResponseDTO GenerateToken()
        {
            FinequeLogin model = new FinequeLogin();
            TokenResponseDTO res = new TokenResponseDTO();
            model.partnerKey = PartnerKey;
            model.merchantId = merchantId;
            string Header = HashRequestCode;
            string body = JsonConvert.SerializeObject(model);
            var _response = _apiManager.Post<TokenResponseDTO>("login/v2", body, Header).Result;

            if (_response.Success)
            {
                res.token = _response.Data.token;
                res.createdAt = _response.Data.createdAt;
                res.result = _response.Data.result;
                res.userName = _response.Data.userName;
                res.partnerId = _response.Data.partnerId;
                var savReq = DBHelperService.ExecuteQuery<object>("SaveFinqueToken", res.token, res.userName, res.partnerId, res.result);
                return (res);
            }

            else
                return (new TokenResponseDTO { });
        }
        public async Task<List<FinequeProductCode>> GetProductCode()
        {
            List<FinequeProductCode> result = new List<FinequeProductCode>();
            try
            {
                result = DBHelperService.GetAll<FinequeProductCode>("GetProductCode").Table;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return (result);
        }
        public QdeUrlResponse GetQdeUrl(RequestModel reqModel)
        {
            string Header = "";
            QdeUrl model = new QdeUrl();
            QdeUrlAPiRequest _apiReqmodel = new QdeUrlAPiRequest();
            QdeUrlResponse _objres = new QdeUrlResponse();
            var getTokent = DBHelperService.ExecuteQuery<TokenResponseDTO>("GetFinqueToken");
            if(getTokent == null)
            {
                GenerateToken();
                return GetQdeUrl(reqModel);
            }
            else
            {
                 Header = getTokent.token;
            }
            
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(ASEkey, reqModel.Body);
            model = JsonConvert.DeserializeObject<QdeUrl>(dcdata1);
            _apiReqmodel.partnerId = PartnerKey;
            _apiReqmodel.partnerMerchantID = merchantId;
            _apiReqmodel.mobile = model.mobile;
            _apiReqmodel.partnerPincode = model.partnerPincode;
            _apiReqmodel.partnerShopType = model.partnerShopType;
            _apiReqmodel.productCode = model.productCode;
            model.Fk_MemId = model.Fk_MemId;
            string body = JsonConvert.SerializeObject(_apiReqmodel);
            var saveReg = DBHelperService.ExecuteQuery<SaveLoanRegistrationRes>("SaveLoanRegistedData", model.Fk_MemId, model.mobile, model.productCode, model.partnerPincode, model.partnerShopType,model.fullName,model.state,model.city);
            if (saveReg != null && saveReg.Flag == "1")
            {
                var _response = _apiManager.Post<QdeUrlResponse>("/Merchant/getQdeUrl/v2", body, Header).Result;

                if (_response.Success)
                {
                    _objres.Code = _response.Data.Code;
                    _objres.result = _response.Data.result;
                    _objres.qdeUrl = _response.Data.qdeUrl;
                    _objres.Tid = _response.Data.Tid;
                    _objres.type = _response.Data.type;
                    _objres.transactionId = _response.Data.transactionId;
                    _objres.title = _response.Data.title;
                    _objres.status = _response.Data.status;
                    _objres.traceId = _response.Data.traceId;
                    var savReq = DBHelperService.ExecuteQuery<object>("SaveLoanApplicationId", model.Fk_MemId, _objres.transactionId, model.productCode, _objres.qdeUrl);
                    return (_objres);
                }
                else if (_response.statusCode == HttpStatusCode.Unauthorized)
                {
                    var updateres = DBHelperService.ExecuteQuery<SaveLoanRegistrationRes>("DeleteloanReg", model.Fk_MemId,model.mobile,model.productCode);
                    if (!string.IsNullOrEmpty(GenerateToken().token))
                        return GetQdeUrl(reqModel);
                    else
                        return (new QdeUrlResponse { StatusCode = "401", Message = "we are facing some technical issues please try after some time." });
                }

                else
                    return (new QdeUrlResponse { StatusCode = _response.statusCode.ToString(), Message = "we are facing some technical issues please try after some time." });
            }
            else
            {
                return (new QdeUrlResponse { StatusCode = saveReg.Flag.ToString(), Message = saveReg.Message });
            }
        }
        public AppIdResponse GetAppId(RequestModel reqModel)
        {
            AppId model = new AppId();
            AppIdResponse _objres = new AppIdResponse();
            var getTokent = DBHelperService.ExecuteQuery<TokenResponseDTO>("GetFinqueToken");
            string Header = getTokent.token;
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(ASEkey, reqModel.Body);
            model = JsonConvert.DeserializeObject<AppId>(dcdata1);
            model.transactionHashCode = model.transactionHashCode;
            //string body = JsonConvert.SerializeObject(model);
            var _response = _apiManager.Get<AppIdResponse>("/Merchant/getAppId/v2/" + model.transactionHashCode, Header).Result;

            if (_response.Success)
            {
                _objres.applicationId = _response.Data.applicationId;
                _objres.fullName = _response.Data.fullName;
                _objres.loanAmount = _response.Data.loanAmount;
                _objres.loanAppliedFor = _response.Data.loanAppliedFor;
                _objres.dateOfApplication = _response.Data.dateOfApplication;
                _objres.mobile = _response.Data.mobile;
                var _savdetail = DBHelperService.ExecuteQuery<SaveLoanRegistrationRes>("SaveLoanDetail",model.Fk_MemId, _objres.applicationId, _objres.fullName, _objres.loanAmount, _objres.loanAppliedFor, _objres.dateOfApplication, _objres.mobile);

                return (_objres);
            }

            else if (_response.statusCode == HttpStatusCode.Unauthorized)
            {
                if (!string.IsNullOrEmpty(GenerateToken().token))
                    return GetAppId(reqModel);
                else
                    return (new AppIdResponse { StatusCode = "401", Message = "we are facing some technical issues please try after some time." });
            }

            else
                return (new AppIdResponse { StatusCode = _response.statusCode.ToString(), Message = "we are facing some technical issues please try after some time." });
        }

        public LoanStatusResponse GetLoanStatus(RequestModel reqModel)
        {
            string Header = "";
            LoanStatus model = new LoanStatus();
            AppId _appIdmodel = new AppId();
            AppIdResponse _appIdobjres = new AppIdResponse();
            LoanStatusResponse _objres = new LoanStatusResponse();
            var getTokent = DBHelperService.ExecuteQuery<TokenResponseDTO>("GetFinqueToken");
            if (getTokent == null)
            {
                GenerateToken();
                return GetLoanStatus(reqModel);
            }
            else
            {
                Header = getTokent.token;
            }
             
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(ASEkey, reqModel.Body);
            _appIdmodel = JsonConvert.DeserializeObject<AppId>(dcdata1);
            _appIdmodel.transactionHashCode = _appIdmodel.applicationId;
            _appIdmodel.Fk_MemId = _appIdmodel.Fk_MemId;
            string body = JsonConvert.SerializeObject(_appIdmodel);
            var _response = _apiManager.Post<AppIdResponse>("Merchant/getAppId/v2" ,body, Header).Result;

     
            if (_response.Success)
            {
                _appIdobjres.applicationId = _response.Data.applicationId; 
                _appIdobjres.fullName = _response.Data.fullName;
                _appIdobjres.loanAmount = _response.Data.loanAmount;
                _appIdobjres.loanAmount = _response.Data.loanAmount;
                _appIdobjres.loanAppliedFor = _response.Data.loanAppliedFor;
                _appIdobjres.dateOfApplication = _response.Data.dateOfApplication;
                _appIdobjres.mobile = _response.Data.mobile;
                var _savdetail = DBHelperService.ExecuteQuery<SaveLoanRegistrationRes>("SaveLoanDetail", _appIdmodel.Fk_MemId, _appIdobjres.applicationId, _appIdobjres.fullName, _appIdobjres.loanAmount, _appIdobjres.loanAppliedFor, _appIdobjres.dateOfApplication, _appIdobjres.mobile);
                string body1 = JsonConvert.SerializeObject(model);
                var _response1 = _apiManager.Get<LoanStatusResponse>("Loans/applicationStatus/" + _appIdobjres.applicationId, null).Result;
                if (_response1.Success)
                {
                    _objres.applicationId = _response1.Data.applicationId;
                    _objres.documentLink = _response1.Data.documentLink;
                    _objres.transactionId = _response1.Data.transactionId;
                    _objres.status = _response1.Data.status;
                    _objres.loanAmount = _response1.Data.loanAmount;
                    _objres.loanSanctionDate = _response1.Data.loanSanctionDate;
                    _objres.declineReason = _response1.Data.declineReason;
                    _objres.remarks = _response1.Data.remarks;
                    _objres.bankSelectionLink = _response1.Data.bankSelectionLink;
                    var _updatedetail = DBHelperService.ExecuteQuery<SaveLoanRegistrationRes>("updateLoanStatus", _appIdmodel.Fk_MemId, _appIdmodel.transactionHashCode, _objres.status, _objres.loanAmount);
                }
                _objres.transactionId = _objres.applicationId;
                _objres.Message = "Success";
                return (_objres);
            }
            else if(_appIdobjres.applicationId==null)
            {
                var _getUrl = DBHelperService.ExecuteQuery<LoanStatusResponse>("GetQudeUrl", _appIdmodel.Fk_MemId, _appIdmodel.transactionHashCode);
                if(_getUrl!=null)
                {
                    _objres.QdeUrl = _getUrl.QdeUrl;
                }
                
                return (_objres);
            }
            else if (_response.statusCode == HttpStatusCode.Unauthorized)
            {
                if (!string.IsNullOrEmpty(GenerateToken().token))
                    return GetLoanStatus(reqModel);
                else
                    return (new LoanStatusResponse { StatusCode = "401", status = "error", Message = "we are facing some technical issues please try after some time." });
            }
            
            else
                return (new LoanStatusResponse { status = "error", StatusCode = _response.statusCode.ToString(), Message = "we are facing some technical issues please try after some time." });
        }

        public async Task<List<ShopTypeDTO>> GetShopType()
        {
            List<ShopTypeDTO> result = new List<ShopTypeDTO>();
            try
            {

                result = DBHelperService.GetAll<ShopTypeDTO>("GetShopType").Table;
                
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return (result);
        }
        public async Task<List<QdeUrl>> GetLoanRegistradList(RequestModel reqModel)
        {
            List<QdeUrl> objList = new List<QdeUrl>();
            try
            {
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(ASEkey, reqModel.Body);
                QdeUrl result = JsonConvert.DeserializeObject<QdeUrl>(dcdata1);
                objList  = DBHelperService.GetAll<QdeUrl>("GetLoanRegisredList", result.Fk_MemId).Table;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return (objList);
        }
    }
}
