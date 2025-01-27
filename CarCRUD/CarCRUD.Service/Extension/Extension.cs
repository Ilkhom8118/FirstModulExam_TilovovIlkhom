using CarCRUD.Service.DTOs;

namespace CarCRUD.Service.Extension
{
    public static class Extension
    {
        public static double Kilometr(this int millage)
        {
            var kilometr = millage * 0.62;
            return kilometr;
        }
        public static void Expensive(this List<CarDto> obj)
        {
            var total = 0d;
            foreach (var sum in obj)
            {
                total += sum.Price;
            }
        }
    }
}
