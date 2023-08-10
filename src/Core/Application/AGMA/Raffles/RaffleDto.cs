namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleDto : DtoExtension, IDto
{
    public DateTime RaffleDate { get; set; } = default!;

}