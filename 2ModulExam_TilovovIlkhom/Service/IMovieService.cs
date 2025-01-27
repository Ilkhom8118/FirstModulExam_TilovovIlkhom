using _2ModulExam_TilovovIlkhom.AccessData.Entity;
using _2ModulExam_TilovovIlkhom.Service.DTOs;

namespace _2ModulExam_TilovovIlkhom.Service;

public interface IMovieService
{
    Movie AddMovie(MovieBaseDto obj);
    void DeleteMovie(Guid id);
    void UpdateMovie(MovieGetDto obj);
    List<MovieGetDto> GetAllMovie();
    List<MovieGetDto> GetAllByMoviesDirector(string director);
    MovieGetDto GetTopRatedMovie();
    List<MovieGetDto> GetMoviesReleasedAfterYear(int year);
    MovieGetDto GetHigherstGrossingMovie();
    List<MovieGetDto> GetMovieWithinDurationRange(int minMinut, int maxMinut);
    long GetTotalBoxOfficeEarningsByDirector(string director);
    List<MovieGetDto> GetMoviesSortedByRating();
    List<MovieGetDto> GetRecentMovies(int years);
}
// GetAllByMoviesDirector : Rejissorning barcha filmlarini oling, Hammasini kino direktori tomonidan oling
// GetTopRatedMovie : Eng yuqori reytingli filmni oling
// GetMoviesReleasedAfterYear : Yildan keyin chiqadigan filmlarni oling
// GetHigherstGrossingMovie : Eng yuqori daromad keltiradigan filmni oling
// SearchMovieByTitle : Filmni nomi bo'yicha qidirish
// GetMovieWithinDurationRange : Muddati ichida filmni oling
// GetTotalBoxOfficeEarningsByDirector : Direktor tomonidan jami kassa daromadini oling
// GetMoviesSortedByRating : Reyting bo'yicha saralangan filmlarni oling
// GetRecentMovies : Oxirgi filmlarni oling

//GetTotalBoxOfficeEarningsByDirector