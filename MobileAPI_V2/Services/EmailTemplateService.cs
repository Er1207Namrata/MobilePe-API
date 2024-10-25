using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MobileAPI_V2.Services
{
    public class EmailTemplateService
    {
        private readonly IHostingEnvironment _env;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string BaseUrl;
        string VerificationBaseUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("VerificationBaseUrl").Value;
        public EmailTemplateService(IHostingEnvironment env, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
           
            _env = env;
            _httpContextAccessor = httpContextAccessor;           
            BaseUrl = VerificationBaseUrl + "/{0}?_={1}";
            _hostingEnvironment = hostingEnvironment;
        }

        public string AccountVerificationTemplate(string Name, string key)
        {
            string VerificationLink = string.Format(BaseUrl, "User/Account/Verification/step/2/v1/proceed/11.00.290.887.00/cyUYbbmbTYUvvcRDET", key);
            string TemplateFilePath = "EmailTemplates/AccountVerificationTemplate.html";
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, TemplateFilePath);
            string TemplateContent = System.IO.File.ReadAllText(filePath);
            TemplateContent = TemplateContent.Replace("[UserName]", Name).Replace("[AccountVerificationLink]", VerificationLink);
            return TemplateContent;
        }
    }
}
