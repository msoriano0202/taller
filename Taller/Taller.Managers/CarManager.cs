using Taller.Contracts.Managers;
using Taller.Contracts.Services;
using Taller.Domain;

namespace Taller.Managers
{
    public class CarManager : ICarManager
    {
        private readonly ICarService _carService;
        private const decimal VALID_DIFFERENCE_AMOUNT = 5000;

        public CarManager(ICarService carService)
        {
            _carService = carService;
        }

        public List<Car> GetAllCars()
        {
            return _carService.GetAll();
        }

        public Car GetCarById(int id)
        {
            return _carService.GetById(id);
        }

        public bool AddCar(Car car)
        {
            return _carService.Add(car);
        }

        public bool DeleteCarById(int id)
        {
            return _carService.DeleteById(id);
        }

        public bool GuessCarPrice(int id, decimal price) 
        {
            var response = false;

            var car = _carService.GetById(id);
            if (car != null)
            {
                var difference = Math.Abs(price - car.Price);
                if (difference <= VALID_DIFFERENCE_AMOUNT)
                    response = true;
            }

            return response;
        }
    }
}