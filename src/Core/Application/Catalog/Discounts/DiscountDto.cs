namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountDto : DtoExtension<DiscountDto>, IDto
{
    public float Percentage { get; set; } = default!;


}