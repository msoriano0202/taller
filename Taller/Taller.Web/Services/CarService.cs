using Taller.Common.Models;
using Taller.Dto.Request;
using Taller.Dto.Response;
using Taller.Web.Helpers;

namespace Taller.Web.Services
{
    public interface ICarService
    {
        Task<ApiResponse<List<CarResponse>>> GetAllCars();
        Task<ApiResponse<CarResponse>> GetCarById(int id);
        Task<ApiResponse<bool>> GuessCarPrice(int id, decimal price);
    }

    public class CarService : ICarService
    {
        private readonly IApiHelper _apiHelper;

        private string _carApiBaseUrl = "https://localhost:7244/";

        public CarService(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<ApiResponse<List<CarResponse>>> GetAllCars()
        {
            var response = await _apiHelper.GetAsync<List<CarResponse>>(_carApiBaseUrl, $"api/v1/cars");
            return response;
        }

        public async Task<ApiResponse<CarResponse>> GetCarById(int id)
        {
            var response = await _apiHelper.GetAsync<CarResponse>(_carApiBaseUrl, $"api/v1/cars/{id}");
            return response;
        }

        public async Task<ApiResponse<bool>> GuessCarPrice(int id, decimal price)
        {
            var model = new GuessCarPriceRequest { Id = id, Price = price };
            var response = await _apiHelper.PostAsync<bool, GuessCarPriceRequest>(_carApiBaseUrl, $"api/v1/cars/GuessCarPrice", model);
            return response;
        }
    }
}
