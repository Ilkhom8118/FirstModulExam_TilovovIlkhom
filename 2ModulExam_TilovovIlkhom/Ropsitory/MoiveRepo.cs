using _2ModulExam_TilovovIlkhom.AccessData.Entity;
using System.Text.Json;

namespace _2ModulExam_TilovovIlkhom.Ropsitory;

public class MoiveRepo : IMovieRepo
{
    private string Path;
    private List<Movie> movies;
    public MoiveRepo()
    {
        Path = "../../../AccessData/Data/Movie.json";
        movies = new List<Movie>();
        if (!File.Exists(Path))
        {
            File.WriteAllText(Path, "[]");
        }
    }
    private void SaveInformation(List<Movie> obj)
    {
        var json = JsonSerializer.Serialize(obj);
        File.WriteAllText(Path, json);
    }
    private List<Movie> GetAllMovies()
    {
        var json = File.ReadAllText(Path);
        var file = JsonSerializer.Deserialize<List<Movie>>(json);
        return file;
    }
    public Movie AddMovie(Movie obj)
    {
        obj.Id = Guid.NewGuid();
        movies.Add(obj);
        SaveInformation(movies);
        return obj;
    }

    public void DeleteMovie(Guid id)
    {
        var guId = GetById(id);
        movies.Remove(guId);
        SaveInformation(movies);
    }

    public List<Movie> GetAllMovie()
    {
        return GetAllMovies();
    }

    public Movie GetById(Guid id)
    {
        foreach (var movie in movies)
        {
            if (movie.Id == id)
            {
                return movie;
            }
        }
        return null;
    }

    public void UpdateMovie(Movie obj)
    {
        var id = GetById(obj.Id);
        movies[movies.IndexOf(id)] = obj;
        SaveInformation(movies);
    }
}
