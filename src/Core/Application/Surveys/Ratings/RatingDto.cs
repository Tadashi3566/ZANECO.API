namespace ZANECO.API.Application.Surveys.Ratings;

public class RatingDto : DtoExtension<RatingDto>, IDto
{
    public int RateNumber { get; set; } = default!;
    public string RateName { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public DateTime? CreatedOn { get; set; }
}