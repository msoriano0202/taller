using Newtonsoft.Json;
using System.Text;
using Taller.Common.Models;

namespace Taller.Web.Helpers
{
    public interface IApiHelper
    {
        Task<ApiResponse<T>> GetAsync<T>(string urlBase, string path);
        Task<ApiResponse<Res>> PostAsync<Res, Model>(string urlBase, string path, Model model);
        Task<ApiResponse<T>> DeleteAsync<T>(string urlBase, string path);
        Task<ApiResponse<Res>> PutAsync<Res,Model>(string urlBase, string path, Model model);
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

        public async Task<ApiResponse<Response>> PostAsync<Response, Model>(string urlBase, string path, Model model)
        {
            try
            {
                var apiClient = _httpClientFactory.CreateClient();
                apiClient.BaseAddress = new Uri(urlBase);

                var requestString = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(requestString, Encoding.UTF8, "application/json");
                var response = await apiClient.PostAsync($"{path}", stringContent);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<Response>
                    {
                        IsSuccess = false,
                        Message = content,
                    };
                }

                var data = JsonConvert.DeserializeObject<Response>(content);
                return new ApiResponse<Response>
                {
                    IsSuccess = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Response>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ApiResponse<T>> DeleteAsync<T>(string urlBase, string path)
        {
            try
            {
                var apiClient = _httpClientFactory.CreateClient();
                apiClient.BaseAddress = new Uri(urlBase);

                var response = await apiClient.DeleteAsync($"{path}");
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<T>
                    {
                        IsSuccess = false,
                        Message = content,
                    };
                }

                return new ApiResponse<T>
                {
                    IsSuccess = true
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

        public async Task<ApiResponse<Res>> PutAsync<Res, Model>(string urlBase, string path, Model model)
        {
            try
            {
                var apiClient = _httpClientFactory.CreateClient();
                apiClient.BaseAddress = new Uri(urlBase);

                var requestString = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(requestString, Encoding.UTF8, "application/json");
                var response = await apiClient.PutAsync($"{path}", stringContent);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<Res>
                    {
                        IsSuccess = false,
                        Message = content,
                    };
                }

                var data = JsonConvert.DeserializeObject<Res>(content);
                return new ApiResponse<Res>
                {
                    IsSuccess = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Res>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
