﻿using Taller.Dto.Request;
using Taller.Dto.Response;
using Taller.Web.Services;

namespace Taller.Web.Managers
{
    public interface ICarManager 
    {
        Task<List<CarResponse>> GetAllCars();
        Task<CarResponse> GetCarById(int id);
        Task<bool> GuessCarPrice(int id, decimal price);
        Task<bool> AddCar(CreateCarRequest createCarRequest);
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

        public async Task<CarResponse> GetCarById(int id)
        {
            var response = await _carService.GetCarById(id);

            if (response.IsSuccess)
                return response.Data;

            return null;
        }

        public async Task<bool> GuessCarPrice(int id, decimal price)
        {
            var response = await _carService.GuessCarPrice(id, price);
            return response.Data;
        }

        public async Task<bool> AddCar(CreateCarRequest createCarRequest)
        {
            var response = await _carService.AddCar(createCarRequest);
            if (response.IsSuccess) return response.Data;

            return false;
        }
    }
}
