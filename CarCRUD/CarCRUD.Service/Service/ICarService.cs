using CarCRUD.DataAccess.Entity;
using CarCRUD.Service.DTOs;

namespace CarCRUD.Service.Service;

public interface ICarService
{
    Car AddCar(CarDto obj);
    void DeleteCar(Guid id);
    CarDto GetById(Guid id);
    List<CarDto> GetAllCars();
    CarDto GetMostExpensive();
    void UpdateCar(CarDto obj);
    CarDto GetLowestMileageCar();
    List<CarDto> GetCarsSortedByPrice();
    List<CarDto> GetRecentCars(int years);
    List<CarDto> GetAllCarsByBrand(string brand);
    List<CarDto> SearchCarsByModel(string keyword);
    double GetAverageEngineCapacityByBrand(string brand);
    List<CarDto> GetCarsByYearRange(int startYear, int endYear);
    List<CarDto> GetCarsWithinPriceRange(double minPrice, double maxPrice);
}