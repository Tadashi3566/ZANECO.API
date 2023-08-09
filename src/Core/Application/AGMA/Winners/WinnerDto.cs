namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerDto : DtoExtension<WinnerDto>, IDto
{
    public DefaultIdType RaffleId { get; set; } = default!;
    public string RaffleName { get; set; } = default!;
    public DefaultIdType PrizeId { get; set; } = default!;
    public string PrizeName { get; set; } = default!;
    public string Address { get; set; } = default!;

}