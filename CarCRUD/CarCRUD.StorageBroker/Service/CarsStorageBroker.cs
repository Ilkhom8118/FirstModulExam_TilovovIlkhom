using CarCRUD.DataAccess.Entity;
using System.Text.Json;

namespace CarCRUD.StorageBroker.Service;

public class CarsStorageBroker : ICarsStorageBroker
{
    private readonly string _filePath;
    private readonly string _directoryPath;
    private List<Car> _cars;
    public CarsStorageBroker()
    {
        _directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Car.json");
        _cars = new List<Car>();
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }
    private void SaveInformation(List<Car> obj)
    {
        var json = JsonSerializer.Serialize(obj);
        File.WriteAllText(_filePath, json);
    }
    private List<Car> GetAlls()
    {
        var json = File.ReadAllText(_filePath);
        var file = JsonSerializer.Deserialize<List<Car>>(json);
        return file;
    }
    public Car AddCar(Car obj)
    {
        _cars.Add(obj);
        SaveInformation(_cars);
        return obj;
    }

    public void DeleteCar(Guid id)
    {
        var guId = GetById(id);
        _cars.Remove(guId);
        SaveInformation(_cars);
    }

    public List<Car> GetAllCars()
    {
        return GetAlls();
    }

    public Car GetById(Guid id)
    {
        var res = GetAlls().Where(c => c.Id == id).FirstOrDefault();
        if (res is null)
        {
            throw new Exception("Siz kiritgan Id topilmadi!");
        }
        return res;
    }

    public void UpdateCar(Car obj)
    {
        var id = GetById(obj.Id);
        _cars[_cars.IndexOf(id)] = obj;
    }
}
