namespace _2ModulExam_TilovovIlkhom.Service.DTOs;

public class MovieBaseDto
{
    public string Title { get; set; }
    public string Director { get; set; }
    public int DurationMinutes { get; set; }
    public double Rating { get; set; }
    public long BoxOfficeEarings { get; set; }
    public DateTime ReleaseDate { get; set; }
}
