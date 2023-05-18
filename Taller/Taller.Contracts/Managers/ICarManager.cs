using Taller.Domain;

namespace Taller.Contracts.Managers
{
    public interface ICarManager
    {
        List<Car> GetAllCars();
        Car GetCarById(int id);
        bool AddCar(Car car);
        bool DeleteCarById(int id);
        bool GuessCarPrice(int id, decimal price);
    }
}
