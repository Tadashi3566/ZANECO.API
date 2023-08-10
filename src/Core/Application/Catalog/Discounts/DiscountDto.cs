namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountDto : BaseDto, IDto
{
    public float Percentage { get; set; } = default!;
}