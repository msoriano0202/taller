using Taller.Common.Models;
using Taller.Dto.Request;
using Taller.Dto.Response;
using Taller.Web.Helpers;

namespace Taller.Web.Services
{
    public interface ICarService
    {
        Task<ApiResponse<List<CarResponse>>> GetAllCarsAsync();
        Task<ApiResponse<CarResponse>> GetCarByIdAsync(int id);
        Task<ApiResponse<bool>> GuessCarPriceAsync(int id, decimal price);
        Task<ApiResponse<bool>> AddCarAsync(CreateCarRequest createCarRequest);
        Task<ApiResponse<bool>> DeleteCarAsync(int id);
        Task<bool> UpdateCarAsync(int id, UpdateCarRequest updateCarRequest);
    }

    public class CarService : ICarService
    {
        private readonly IApiHelper _apiHelper;

        private string _carApiBaseUrl = "https://localhost:7244/";

        public CarService(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<ApiResponse<List<CarResponse>>> GetAllCarsAsync()
        {
            var response = await _apiHelper.GetAsync<List<CarResponse>>(_carApiBaseUrl, $"api/v1/cars");
            return response;
        }

        public async Task<ApiResponse<CarResponse>> GetCarByIdAsync(int id)
        {
            var response = await _apiHelper.GetAsync<CarResponse>(_carApiBaseUrl, $"api/v1/cars/{id}");
            return response;
        }

        public async Task<ApiResponse<bool>> GuessCarPriceAsync(int id, decimal price)
        {
            var model = new GuessCarPriceRequest { Id = id, Price = price };
            var response = await _apiHelper.PostAsync<bool, GuessCarPriceRequest>(_carApiBaseUrl, $"api/v1/cars/GuessCarPrice", model);
            return response;
        }

        public async Task<ApiResponse<bool>> AddCarAsync(CreateCarRequest createCarRequest)
        {
            var response = await _apiHelper.PostAsync<bool, CreateCarRequest>(_carApiBaseUrl, $"api/v1/cars", createCarRequest);
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteCarAsync(int id)
        {
            var response = await _apiHelper.DeleteAsync<bool>(_carApiBaseUrl, $"api/v1/cars/{id}");
            return response;
        }

        public async Task<bool> UpdateCarAsync(int id, UpdateCarRequest updateCarRequest)
        {
            var response = await _apiHelper.PutAsync<bool, UpdateCarRequest>(_carApiBaseUrl, $"api/v1/cars/{id}", updateCarRequest);
            return response.IsSuccess;
        }
    }
}
