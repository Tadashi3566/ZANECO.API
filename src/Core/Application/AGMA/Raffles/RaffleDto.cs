namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleDto : BaseDto, IDto
{
    public DateTime RaffleDate { get; set; } = default!;

}