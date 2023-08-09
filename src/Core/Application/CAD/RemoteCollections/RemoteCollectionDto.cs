namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionDto : DtoExtension<RemoteCollectionDto>, IDto
{
    public int CollectorId { get; set; } = default!;
    public string Collector { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public DateTime TransactionDate { get; set; } = default!;
    public DateTime ReportDate { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string OrNumber { get; set; } = default!;


}