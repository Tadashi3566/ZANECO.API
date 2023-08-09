namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleDto : DtoExtension<RaffleDto>, IDto
{
    public DateTime RaffleDate { get; set; } = default!;

}