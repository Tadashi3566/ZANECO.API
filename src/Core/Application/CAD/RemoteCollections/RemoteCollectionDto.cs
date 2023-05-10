namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int CollectorId { get; set; } = default!;
    public string Collector { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public DateTime TransactionDate { get; set; } = default!;
    public DateTime ReportDate { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string OrNumber { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    public string? Status { get; set; }
}