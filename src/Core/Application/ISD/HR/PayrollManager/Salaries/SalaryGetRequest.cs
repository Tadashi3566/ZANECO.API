using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;

public class SalaryGetRequest : IRequest<SalaryDto>
{
    public DefaultIdType Id { get; set; }

    public SalaryGetRequest(DefaultIdType id) => Id = id;
}

public class SalaryGetRequestHandler : IRequestHandler<SalaryGetRequest, SalaryDto>
{
    private readonly IRepository<Salary> _repository;
    private readonly IStringLocalizer<SalaryGetRequestHandler> _localizer;

    public SalaryGetRequestHandler(IRepository<Salary> repository, IStringLocalizer<SalaryGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<SalaryDto> Handle(SalaryGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new SalaryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Salary not found."], request.Id));
}