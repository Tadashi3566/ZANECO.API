namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime RaffleDate { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}