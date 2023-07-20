using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

public class LoanDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public LoanDeleteRequest(Guid id) => Id = id;
}

public class LoanDeleteRequestHandler : IRequestHandler<LoanDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Loan> _repository;
    private readonly IStringLocalizer<LoanDeleteRequestHandler> _localizer;

    public LoanDeleteRequestHandler(IRepositoryWithEvents<Loan> repository, IStringLocalizer<LoanDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(LoanDeleteRequest request, CancellationToken cancellationToken)
    {
        var loan = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = loan ?? throw new NotFoundException($"Loan {request.Id} not found.");

        await _repository.DeleteAsync(loan, cancellationToken);

        return request.Id;
    }
}