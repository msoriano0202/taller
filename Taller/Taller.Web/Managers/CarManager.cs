using Taller.Dto.Response;
using Taller.Web.Models;
using Taller.Web.Services;

namespace Taller.Web.Managers
{
    public interface ICarManager 
    {
        Task<List<CarResponse>> GetAllCars();
    }


    public class CarManager : ICarManager
    {
        private readonly ICarService _carService;

        public CarManager(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<List<CarResponse>> GetAllCars() 
        {
            var response = await _carService.GetAllCars();

            if (response.IsSuccess)
                return response.Data;

            return new List<CarResponse>();
        }
    }
}
