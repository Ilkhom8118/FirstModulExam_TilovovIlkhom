namespace _2ModulExam_TilovovIlkhom.AccessData.Entity;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public int DurationMinutes { get; set; }
    public double Rating { get; set; }
    public long BoxOfficeEarings { get; set; }
    public DateTime ReleaseDate { get; set; }
}
