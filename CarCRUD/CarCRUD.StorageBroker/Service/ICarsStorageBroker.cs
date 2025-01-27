using CarCRUD.DataAccess.Entity;

namespace CarCRUD.StorageBroker.Service;

public interface ICarsStorageBroker
{
    Car AddCar(Car obj);
    void DeleteCar(Guid id);
    void UpdateCar(Car obj);
    List<Car> GetAllCars();
    Car GetById(Guid id);
}