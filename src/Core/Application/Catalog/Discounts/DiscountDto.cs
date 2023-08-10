namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountDto : DtoExtension, IDto
{
    public float Percentage { get; set; } = default!;


}