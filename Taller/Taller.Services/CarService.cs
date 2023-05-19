using System.Linq.Expressions;
using Taller.Contracts.Services;
using Taller.Domain;

namespace Taller.Services
{
    public class CarService : ICarService
    {
        private static List<Car> _carsData = new List<Car> 
        {
            new Car { Id = 1, Make = "Audi", Model = "R8", Year = 2018, Doors = 2, Color = "Red", Price = 79995 },
            new Car { Id = 2, Make = "Tesla", Model = "3", Year = 2018, Doors = 4, Color = "Black", Price = 54995 },
            new Car { Id = 3, Make = "Porsche", Model = " 911 991", Year = 2020, Doors = 2, Color = "White", Price = 155000 },
            new Car { Id = 4, Make = "Mercedes-Benz", Model = "GLE 63S", Year = 2021, Doors = 5, Color = "Blue", Price = 83995 },
            new Car { Id = 5, Make = "BMW", Model = "X6 M", Year = 2020, Doors = 5, Color = "Silver", Price = 62995 }
        };

        public List<Car> GetAll()
        {
            return _carsData;
        }

        public Car GetById(int id)
        {
            return _carsData.FirstOrDefault(x => x.Id == id);
        }

        public bool Add(Car car)
        {
            var response = true;

            try
            {
                var maxId = _carsData.Max(x => x.Id);
                car.Id = (maxId + 1);
                _carsData.Add(car);
            }
            catch (Exception ex)
            {
                response = false;
                Console.WriteLine($"An error ocurred when adding the car: {car.Model} {car.Make} {car.Color} {car.Year} - {ex.Message}");
            }

            return response;
        }

        public bool DeleteById(int id)
        {
            var response = false;

            var found = _carsData.FirstOrDefault(x => x.Id == id);
            if (found != null)
            {
                _carsData.Remove(found);
                response = true;
            }

            return response;
        }

        public bool Update(int id, Car car)
        {
            var result = false;

            var found = _carsData.FirstOrDefault(x => x.Id == id);
            if (found != null)
            {
                found.Make = car.Make;
                found.Model = car.Model;
                found.Year = car.Year;
                found.Doors = car.Doors;
                found.Color = car.Color;
                found.Price = car.Price;

                result = true;
            }

            return result;
        }
    }
}