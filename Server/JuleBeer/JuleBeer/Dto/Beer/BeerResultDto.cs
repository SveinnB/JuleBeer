namespace JuleBeer.Dto.Beer;

public class BeerResultDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public double AverageStars { get; set; }
}

public class ResultDto
{
    public int NumberOfUsers { get; set; }
    public int NumberOfRatings { get; set; }
    public List<BeerResultDto> BeerResults { get; set; }
}