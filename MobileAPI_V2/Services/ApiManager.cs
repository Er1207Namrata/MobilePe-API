using MobileAPI_V2.Model.BillPayment;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MobileAPI_V2.Model.Fineque;
using MobileAPI_V2.Model;

namespace MobileAPI_V2.Services
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public T Data { get; set; }
    }
    public class ApiManager
    {
        public static string FinequeURL = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly DBHelperService _DbHelperService;
        public ApiManager(IHttpContextAccessor acc, HttpClient httpClient, DBHelperService DBHelperService)
        {

            this._httpContextAccessor = acc;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            FinequeURL = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("FinequeURL").Value;

        }

        public async Task<ApiResponse<T>> Post<T>(string endpoint, string data,string Header)
        {
            var response = new ApiResponse<T>();
            try
            {

                if (endpoint.Contains("login/v2"))
                {
                    _httpClient.DefaultRequestHeaders.Add("RequestHash", Header);
                }
                else
                {
                    string AuthToken = "Bearer " + Header;
                    _httpClient.DefaultRequestHeaders.Add("Authorization", AuthToken);
                }
                var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                string FinqUrl = FinequeURL + endpoint;
                HttpResponseMessage apiResponse = await _httpClient.PostAsync(FinqUrl, httpContent);
                var savReq = DBHelperService.ExecuteQuery<object>("SaveFinequeReqRes", data, response.Data, endpoint);
                if (apiResponse.IsSuccessStatusCode)
                {
                    string responseData = await apiResponse.Content.ReadAsStringAsync();
                    T responseDataDeserialized = JsonConvert.DeserializeObject<T>(responseData);

                    response.Success = true;
                    response.Data = responseDataDeserialized;
                }
                else
                {
                    response.Success = false;
                    response.statusCode = apiResponse.StatusCode;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                //response.statusCode = ex.Message;
                response.statusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<ApiResponse<T>> Get<T>(string endpoint, string Header)
        {
            string AuthToken = "";
            var response = new ApiResponse<T>();
            try
            {

                if (endpoint.Contains("login/v2"))
                {
                    _httpClient.DefaultRequestHeaders.Add("RequestHash", Header);
                }
                else
                {
                   if(Header!=null)
                    {
                        AuthToken = "Bearer " + Header;
                        _httpClient.DefaultRequestHeaders.Add("Authorization", AuthToken);
                    }
                        
                    
                     
                }
                
                string FinqUrl = FinequeURL + endpoint;
                HttpResponseMessage apiResponse = await _httpClient.GetAsync(FinqUrl);
                var savReq = DBHelperService.ExecuteQuery<object>("SaveFinequeReqRes", response.Data, endpoint);
                if (apiResponse.IsSuccessStatusCode)
                {
                    string responseData = await apiResponse.Content.ReadAsStringAsync();
                    T responseDataDeserialized = JsonConvert.DeserializeObject<T>(responseData);

                    response.Success = true;
                    response.Data = responseDataDeserialized;
                }
                else
                {
                    response.Success = false;
                    response.statusCode = apiResponse.StatusCode;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                //response.statusCode = ex.Message;
                response.statusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
    

}

