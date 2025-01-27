using CarCRUD.DataAccess.Entity;
using CarCRUD.Service.DTOs;
using CarCRUD.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace CarCRUD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService carService;
        public CarController()
        {
            carService = new CarService();
        }
        [HttpPost("addCar")]
        public Car AddCar(CarDto obj)
        {
            return carService.AddCar(obj);
        }
        [HttpDelete("deleteCar")]
        public void DeleteCar(Guid id)
        {
            carService.DeleteCar(id);
        }
        [HttpGet("getById")]
        public CarDto GetById(Guid id)
        {
            return carService.GetById(id);
        }
        [HttpGet("getAllCars")]
        public List<CarDto> GetAllCars()
        {
            return carService.GetAllCars();
        }
        [HttpGet("getMostExpensive")]
        public CarDto GetMostExpensive()
        {
            return carService.GetMostExpensive();
        }
        [HttpPut("updateCar")]
        public void UpdateCar(CarDto obj)
        {
            carService.UpdateCar(obj);
        }
        [HttpGet("getLowestMileageCar")]
        public CarDto GetLowestMileageCar()
        {
            return carService.GetLowestMileageCar();
        }
        [HttpGet("getCarsSortedByPrice")]
        public List<CarDto> GetCarsSortedByPrice()
        {
            return carService.GetCarsSortedByPrice();
        }
        [HttpGet("getRecentCars")]
        public List<CarDto> GetRecentCars(int years)
        {
            return carService.GetRecentCars(years);
        }
        [HttpGet("getAllCarsByBrand")]
        public List<CarDto> GetAllCarsByBrand(string brand)
        {
            return carService.GetAllCarsByBrand(brand);
        }
        [HttpGet("searchCarsByModel")]
        public List<CarDto> SearchCarsByModel(string keyword)
        {
            return carService.SearchCarsByModel(keyword);
        }
        [HttpGet("getAverageEngineCapacityByBrand")]
        public double GetAverageEngineCapacityByBrand(string brand)
        {
            return carService.GetAverageEngineCapacityByBrand(brand);
        }
        [HttpGet("getCarsByYearRange")]
        public List<CarDto> GetCarsByYearRange(int startYear, int endYear)
        {
            return carService.GetCarsByYearRange(startYear, endYear);
        }
        [HttpGet("getCarsWithinPriceRange")]
        public List<CarDto> GetCarsWithinPriceRange(double minPrice, double maxPrice)
        {
            return carService.GetCarsWithinPriceRange(minPrice, maxPrice);
        }
    }
}
