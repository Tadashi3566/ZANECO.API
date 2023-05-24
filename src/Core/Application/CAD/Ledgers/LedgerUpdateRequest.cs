using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Ledgers;

public class LedgerUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public Guid AccountId { get; set; }
    public double IdCode { get; set; } = default!;
    public string AccountNumber { get; set; } = string.Empty;
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

    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
}

public class LedgerUpdateRequestValidator : CustomValidator<LedgerUpdateRequest>
{
}

public class LedgerUpdateRequestHandler : IRequestHandler<LedgerUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Ledger> _repository;
    private readonly IStringLocalizer<LedgerUpdateRequestHandler> _localizer;

    public LedgerUpdateRequestHandler(IRepositoryWithEvents<Ledger> repository, IStringLocalizer<LedgerUpdateRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(LedgerUpdateRequest request, CancellationToken cancellationToken)
    {
        var ledger = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = ledger ?? throw new NotFoundException(string.Format(_localizer["Ledger not found."], request.Id));

        var updatedLedger = ledger.Update(request.AccountNumber, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedLedger, cancellationToken);

        return request.Id;
    }
}