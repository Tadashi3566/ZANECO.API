namespace ZANECO.API.Application.Surveys.Ratings;

public class RatingDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int RateNumber { get; set; } = default!;
    public string RateName { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public string Reference { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedOn { get; set; }
}