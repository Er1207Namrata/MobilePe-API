using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MobileAPI_V2.DataLayer;
using MobileAPI_V2.Model;
using MobileAPI_V2.Model.Affiliate;
using MobileAPI_V2.Model.Fineque;
using System.Collections.Generic;
using System.Data;

namespace MobileAPI_V2.Services
{
    public class AffiliateService
    {

        private readonly ConnectionString _connectionString;
        private readonly IHttpContextAccessor _ctx;
        IDbConnection con;
        public readonly string ASEkey;
        private readonly DBHelperService _DbHelperService;
        public AffiliateService(ConnectionString connectionString, DBHelperService DBHelperService)
        {
            _connectionString = connectionString;
            ASEkey = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("AESKEY").Value;
        }
        public CommonResponseDTO AffiliateFinanceRegistration(AffiliateFinanceRegistrationDTO req) => DBHelperService.ExecuteQuery<CommonResponseDTO>("sp_InsertAffiliateFinanceRegistration", req.AffiliateId, req.UserId, req.Name, req.Mobile, req.Email, req.PinCode, req.CreatedBy);

        public AffiliateFinanceRegistrationDTO GetAffiliateFinanceRegistration(AffiliateFinanceRegistrationDTO req) => DBHelperService.ExecuteQuery<AffiliateFinanceRegistrationDTO>("sp_GetAffiliateFinanceDetail", req.AffiliateId, req.UserId);
        public List<AffiliateFinanceRegistrationReportDTO> GetAllAffiliateFinanceDetailList(AffiliateFinanceRegistrationFilterDTO _req) => DBHelperService.GetAll<AffiliateFinanceRegistrationReportDTO>("sp_GetAllAffiliateFinanceDetailList", _req.MemberId, _req.AffiliateId).Table;

    }
}
