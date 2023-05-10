using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

public class LoanGetRequest : IRequest<LoanDto>
{
    public DefaultIdType Id { get; set; }

    public LoanGetRequest(Guid id) => Id = id;
}

public class LoanGetRequestHandler : IRequestHandler<LoanGetRequest, LoanDto>
{
    private readonly IRepository<Loan> _repository;
    private readonly IStringLocalizer<LoanGetRequestHandler> _localizer;

    public LoanGetRequestHandler(IRepository<Loan> repository, IStringLocalizer<LoanGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<LoanDto> Handle(LoanGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Loan, LoanDto>)new LoanByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Loan not found."], request.Id));
}