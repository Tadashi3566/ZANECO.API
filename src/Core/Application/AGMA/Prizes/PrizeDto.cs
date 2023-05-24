namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType RaffleId { get; set; } = default!;
    public string RaffleName { get; set; } = default!;
    public string PrizeType { get; set; } = default!;
    public int Winners { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}