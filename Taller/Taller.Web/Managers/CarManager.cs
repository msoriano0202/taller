using Taller.Dto.Request;
using Taller.Dto.Response;
using Taller.Web.Services;

namespace Taller.Web.Managers
{
    public interface ICarManager 
    {
        Task<List<CarResponse>> GetAllCarsAsync();
        Task<CarResponse> GetCarByIdAsync(int id);
        Task<bool> GuessCarPriceAsync(int id, decimal price);
        Task<bool> AddCarAsync(CreateCarRequest createCarRequest);
        Task<bool> DeleteCarAsync(int id);
    }


    public class CarManager : ICarManager
    {
        private readonly ICarService _carService;

        public CarManager(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<List<CarResponse>> GetAllCarsAsync() 
        {
            var response = await _carService.GetAllCarsAsync();

            if (response.IsSuccess)
                return response.Data;

            return new List<CarResponse>();
        }

        public async Task<CarResponse> GetCarByIdAsync(int id)
        {
            var response = await _carService.GetCarByIdAsync(id);

            if (response.IsSuccess)
                return response.Data;

            return null;
        }

        public async Task<bool> GuessCarPriceAsync(int id, decimal price)
        {
            var response = await _carService.GuessCarPriceAsync(id, price);
            return response.Data;
        }

        public async Task<bool> AddCarAsync(CreateCarRequest createCarRequest)
        {
            var response = await _carService.AddCarAsync(createCarRequest);
            return response.IsSuccess;
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var response = await _carService.DeleteCarAsync(id);
            return response.IsSuccess;
        }
    }
}
