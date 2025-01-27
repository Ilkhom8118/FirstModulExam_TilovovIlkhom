using _2ModulExam_TilovovIlkhom.AccessData.Entity;
using _2ModulExam_TilovovIlkhom.Ropsitory;
using _2ModulExam_TilovovIlkhom.Service.DTOs;

namespace _2ModulExam_TilovovIlkhom.Service;

public class MovieService : IMovieService
{
    private readonly IMovieRepo movies;
    private List<Movie> _movies;
    public MovieService()
    {
        movies = new MoiveRepo();
        _movies = new List<Movie>();
    }
    private Movie ConvertToEntity(MovieBaseDto obj)
    {
        return new Movie()
        {
            Title = obj.Title,
            Rating = obj.Rating,
            Director = obj.Director,
            ReleaseDate = obj.ReleaseDate.Date,
            DurationMinutes = obj.DurationMinutes,
            BoxOfficeEarings = obj.BoxOfficeEarings,
        };
    }
    private MovieGetDto ConvertToEntity(Movie obj)
    {
        var movieDto = new MovieGetDto()
        {
            Id = obj.Id,
            Title = obj.Title,
            Rating = obj.Rating,
            Director = obj.Director,
            ReleaseDate = obj.ReleaseDate.Date,
            DurationMinutes = obj.DurationMinutes,
            BoxOfficeEarings = obj.BoxOfficeEarings,
        };
        return movieDto;
    }

    public Movie AddMovie(MovieBaseDto obj)
    {
        var movie = ConvertToEntity(obj);
        movie.Id = Guid.NewGuid();
        movie.ReleaseDate = DateTime.Now;
        movies.AddMovie(movie);
        return movie;
    }

    public void DeleteMovie(Guid id)
    {
        movies.DeleteMovie(id);
    }

    public List<MovieGetDto> GetAllByMoviesDirector(string director)
    {
        var list = new List<MovieGetDto>();
        var all = movies.GetAllMovie();
        foreach (var movieDirector in all)
        {
            if (movieDirector.Director == director)
            {
                var convert = ConvertToEntity(movieDirector);
                list.Add(convert);
            }
        }
        return list;
    }

    public List<MovieGetDto> GetAllMovie()
    {
        var list = new List<MovieGetDto>();
        var moviesAll = movies.GetAllMovie();
        foreach (var moive in moviesAll)
        {
            var convert = ConvertToEntity(moive);
            list.Add(convert);
        }
        return list;
    }

    public MovieGetDto GetHigherstGrossingMovie()
    {
        var movieGetDto = new MovieGetDto();
        var money = movies.GetAllMovie();
        foreach (var moive in money)
        {
            if (moive.BoxOfficeEarings > movieGetDto.BoxOfficeEarings)
            {
                movieGetDto = ConvertToEntity(moive);
            }
        }
        return movieGetDto;
    }
    public List<MovieGetDto> GetMoviesReleasedAfterYear(int year)
    {
        var list = new List<MovieGetDto>();
        var moviesRealesed = movies.GetAllMovie();
        foreach (var movie in moviesRealesed)
        {
            if (movie.ReleaseDate.Year > year)
            {
                var convert = ConvertToEntity(movie);
                list.Add(convert);
            }
        }
        return list;
    }

    public List<MovieGetDto> GetMoviesSortedByRating()
    {
        var list = new List<MovieGetDto>();
        var movieSort = movies.GetAllMovie();
        foreach (var movie in movieSort)
        {
            var sort = ConvertToEntity(movie);
            list.Sort();
        }
        return list;
    }


    public List<MovieGetDto> GetMovieWithinDurationRange(int minMinut, int maxMinut)
    {
        var list = new List<MovieGetDto>();
        var range = movies.GetAllMovie();

        foreach (var movie in range)
        {
            if (movie.DurationMinutes >= minMinut && movie.DurationMinutes <= maxMinut)
            {
                var convert = ConvertToEntity(movie);
                list.Add(convert);
            }
        }
        return list;
    }

    public List<MovieGetDto> GetRecentMovies(int years)
    {
        var list = new List<MovieGetDto>();
        var recent = movies.GetAllMovie();
        foreach (var movie in recent)
        {
            if (movie.ReleaseDate.Year == years)
            {
                var convert = ConvertToEntity(movie);
                list.Add(convert);
            }
        }
        return list;
    }

    public MovieGetDto GetTopRatedMovie()
    {
        var mostReted = new MovieGetDto();
        foreach (var movie in GetAllMovie())
        {
            if (movie.Rating > mostReted.Rating)
            {
                mostReted = movie;
            }
        }
        return mostReted;
    }

    public long GetTotalBoxOfficeEarningsByDirector(string director)
    {
        long totalPrice = 0;
        var movieByDirector = GetAllByMoviesDirector(director);
        foreach (var price in movieByDirector)
        {
            totalPrice += price.BoxOfficeEarings;
        }
        return totalPrice;
    }

    public void UpdateMovie(MovieGetDto obj)
    {
        movies.UpdateMovie(ConvertToEntity(obj));
    }
}
