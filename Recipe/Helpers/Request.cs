using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime;
using System.Text;

namespace Recipe.Helpers
{
    public class Request
    {
        private readonly IConfiguration _config;
        private readonly string BaseUrl;
        public Request(IConfiguration config)
        {
            _config = config;
            BaseUrl = _config.GetSection("FoodApiUrl").Value;

        }
        public async Task<ApiResponse<T>> ApiCallPost<T>(string controller,string method,object? obj,bool onAuth = false)
        {
            var ApiResponse = new ApiResponse<T>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (onAuth)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ""); //new JwtUtility(Configuration).GenerateTokenCentral(auth));
                    }
                    string _params = obj == null ? string.Empty : JsonConvert.SerializeObject(obj);
                    client.Timeout = TimeSpan.FromMinutes(10);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var result = await client.PostAsync($"{BaseUrl}{controller}/{method}", new StringContent(_params, Encoding.UTF8, "application/json"));

                    var response = await result.Content.ReadAsStringAsync();
                    if (!result.IsSuccessStatusCode)
                    {
                        ApiResponse.Success = false;
                        ApiResponse.StatusCode = (int)result.StatusCode;
                        ApiResponse.Message = response;
                        throw new Exception(response);
                    }
                    return JsonConvert.DeserializeObject<ApiResponse<T>>(response)!;
                }
            }
            catch (Exception ex)
            {
                return ApiResponse;
            }
        }
    }
}
