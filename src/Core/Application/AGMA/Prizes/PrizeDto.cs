namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeDto : DtoExtension, IDto
{
    public DefaultIdType RaffleId { get; set; } = default!;
    public string RaffleName { get; set; } = default!;
    public string PrizeType { get; set; } = default!;
    public int Winners { get; set; } = default!;

}