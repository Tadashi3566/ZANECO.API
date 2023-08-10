using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Ledgers;

public class LedgerCreateRequest : BaseRequest, IRequest<Guid>
{
    public Guid AccountId { get; set; }
    public double IdCode { get; set; }
    public string AccountNumber { get; set; } = default!;
    public string? BillMonth { get; set; }
    public string? BillNumber { get; set; }

    public double LastReading { get; set; }
    public double KWH { get; set; }

    public decimal UCNPCSD { get; set; }
    public decimal UCNPCSCC { get; set; }
    public decimal UCDUSCC { get; set; }
    public decimal UCME { get; set; }
    public decimal UCETR { get; set; }
    public decimal UCEC { get; set; }
    public decimal UCCSR { get; set; }

    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal Balance { get; set; }

    public decimal VAT { get; set; }
    public decimal VATGeneration { get; set; }
    public decimal VATTransmission { get; set; }
    public decimal VATDistribution { get; set; }
    public decimal VATSLGeneration { get; set; }
    public decimal VATSLTransmission { get; set; }

    public decimal VATDiscount { get; set; }
    public decimal VATGenerationDiscount { get; set; }
    public decimal VATTransmissionDiscount { get; set; }
    public decimal VATDistributionDiscount { get; set; }
    public decimal VATSystemLossDiscount { get; set; }

    public string? Collector { get; set; }
    public DateTime PostingDate { get; set; }
}

public class CreateLedgerRequestValidator : CustomValidator<LedgerCreateRequest>
{
}

public class LedgerCreateRequestHandler : IRequestHandler<LedgerCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Ledger> _repository;

    public LedgerCreateRequestHandler(IRepositoryWithEvents<Ledger> repository) => _repository = repository;

    public async Task<Guid> Handle(LedgerCreateRequest request, CancellationToken cancellationToken)
    {
        var ledger = new Ledger(request.AccountNumber, request.Description, request.Notes);

        await _repository.AddAsync(ledger, cancellationToken);

        return ledger.Id;
    }
}