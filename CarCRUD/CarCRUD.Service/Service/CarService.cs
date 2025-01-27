using CarCRUD.DataAccess.Entity;
using CarCRUD.Service.DTOs;
using CarCRUD.StorageBroker.Service;

namespace CarCRUD.Service.Service;

public class CarService : ICarService
{
    private readonly ICarsStorageBroker _cars;
    public CarService()
    {
        _cars = new CarsStorageBroker();
    }
    private Car ConvertToEntity(CarDto obj)
    {
        return new Car()
        {
            Year = obj.Year,
            Brand = obj.Brand,
            Color = obj.Color,
            Model = obj.Model,
            Price = obj.Price,
            Mileage = obj.Mileage,
            Id = obj.Id ?? Guid.NewGuid(),
            EngineCapacity = obj.EngineCapacity,
        };
    }

    private CarDto ConvertToDto(Car obj)
    {
        return new CarDto()
        {
            Id = obj.Id,
            Year = obj.Year,
            Brand = obj.Brand,
            Color = obj.Color,
            Model = obj.Model,
            Price = obj.Price,
            Mileage = obj.Mileage,
            EngineCapacity = obj.EngineCapacity,
        };
    }
    public Car AddCar(CarDto obj)
    {
        var convert = ConvertToEntity(obj);
        return _cars.AddCar(convert);
    }

    public void DeleteCar(Guid id)
    {
        _cars.DeleteCar(id);
    }

    public List<CarDto> GetAllCars()
    {
        return _cars.GetAllCars().Select(c => ConvertToDto(c)).ToList();
    }

    public List<CarDto> GetAllCarsByBrand(string brand)
    {
        return GetAllCars().Where(c => c.Brand == brand).ToList();
    }

    public double GetAverageEngineCapacityByBrand(string brand)
    {
        return GetAllCars().Where(c => c.Brand == brand).Average(c => c.EngineCapacity);
    }

    public CarDto GetById(Guid id)
    {
        var res = GetAllCars().FirstOrDefault(c => c.Id == id);
        if (res is null)
        {
            throw new Exception("Bunday Id topilmadi");
        }
        return res;
    }

    public List<CarDto> GetCarsByYearRange(int startYear, int endYear)
    {
        return GetAllCars().Where(c => c.Year > startYear && c.Year < endYear).ToList();
    }

    public List<CarDto> GetCarsSortedByPrice()
    {
        return GetAllCars().OrderBy(c => c.Price).ToList();
    }

    public List<CarDto> GetCarsWithinPriceRange(double minPrice, double maxPrice)
    {
        return GetAllCars().Where(c => c.Price >= minPrice && c.Price <= maxPrice).ToList();
    }

    public CarDto GetLowestMileageCar()
    {
        var res = GetAllCars().OrderBy(c => c.Mileage).FirstOrDefault();
        if (res is null)
        {
            throw new Exception("Bunday moshina topilmadi!");
        }
        return res;
    }

    public CarDto GetMostExpensive()
    {
        var res = GetAllCars().OrderByDescending(c => c.Price).FirstOrDefault();
        if (res is null)
        {
            throw new Exception("Bunday moshina topilmadi!");
        }
        return res;
    }

    public List<CarDto> GetRecentCars(int years)
    {
        return GetAllCars().Where(c => c.Year > years).ToList();
    }

    public List<CarDto> SearchCarsByModel(string keyword)
    {
        return GetAllCars().Where(c => c.Model.Contains(keyword)).ToList();
    }

    public void UpdateCar(CarDto obj)
    {
        _cars.UpdateCar(ConvertToEntity(obj));
    }
}
