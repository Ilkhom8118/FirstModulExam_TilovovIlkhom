using _2ModulExam_TilovovIlkhom.AccessData.Entity;

namespace _2ModulExam_TilovovIlkhom.Ropsitory;

public interface IMovieRepo
{
    Movie AddMovie(Movie obj);
    Movie GetById(Guid id);
    void DeleteMovie(Guid id);
    List<Movie> GetAllMovie();
    void UpdateMovie(Movie obj);
}