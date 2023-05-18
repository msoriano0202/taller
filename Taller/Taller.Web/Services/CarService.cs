using Taller.Common.Models;
using Taller.Dto.Response;
using Taller.Web.Helpers;

namespace Taller.Web.Services
{
    public interface ICarService
    {
        Task<ApiResponse<List<CarResponse>>> GetAllCars();
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
    }
}
