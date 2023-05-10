using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

public class LoanGetViaDapperRequest : IRequest<LoanDto>
{
    public DefaultIdType Id { get; set; }

    public LoanGetViaDapperRequest(Guid id) => Id = id;
}

public class LoanGetViaDapperRequestHandler : IRequestHandler<LoanGetViaDapperRequest, LoanDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<LoanGetViaDapperRequestHandler> _localizer;

    public LoanGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<LoanGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<LoanDto> Handle(LoanGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var loan = await _repository.QueryFirstOrDefaultAsync<Loan>(
            $"SELECT * FROM datazaneco.\"Loan\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = loan ?? throw new NotFoundException(string.Format(_localizer["Loan not found."], request.Id));

        return loan.Adapt<LoanDto>();
    }
}