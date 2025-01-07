using _2ModulExam_TilovovIlkhom.Service;
using _2ModulExam_TilovovIlkhom.Service.DTOs;

namespace _2ModulExam_TilovovIlkhom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IMovieService serive = new MovieService();
            var obj = new MovieBaseDto();
            obj.Title = "Salom";
            var id = Guid.Parse("98aa8ff0-d9fc-4f9a-8349-3008461f1a71");
            serive.AddMovie(obj);

        }
    }
}
