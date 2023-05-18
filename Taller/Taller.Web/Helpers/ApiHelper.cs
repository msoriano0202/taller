using Newtonsoft.Json;
using Taller.Common.Models;

namespace Taller.Web.Helpers
{
    public interface IApiHelper
    {
        Task<ApiResponse<T>> GetAsync<T>(string urlBase, string path);
    }

    public class ApiHelper : IApiHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiHelper(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string urlBase, string path)
        {
            try
            {
                var apiClient = _httpClientFactory.CreateClient();
                apiClient.BaseAddress = new Uri(urlBase);

                var response = await apiClient.GetAsync($"{path}");
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<T>
                    {
                        IsSuccess = false,
                        Message = content,
                    };
                }

                var data = JsonConvert.DeserializeObject<T>(content);
                return new ApiResponse<T>
                {
                    IsSuccess = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

    }
}
