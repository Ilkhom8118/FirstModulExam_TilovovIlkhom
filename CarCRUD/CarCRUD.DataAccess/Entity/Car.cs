namespace CarCRUD.DataAccess.Entity;

public class Car
{
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public double EngineCapacity { get; set; }
    public double Price { get; set; }
    public int Mileage { get; set; }
    public string Color { get; set; }

}
