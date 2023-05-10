using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailGetRequest : IRequest<EmployeePayrollDetailDto>
{
    public DefaultIdType Id { get; set; }

    public EmployeePayrollDetailGetRequest(Guid id) => Id = id;
}

public class EmployeePayrollDetailGetRequestHandler : IRequestHandler<EmployeePayrollDetailGetRequest, EmployeePayrollDetailDto>
{
    private readonly IRepository<EmployeePayrollDetail> _repository;
    private readonly IStringLocalizer<EmployeePayrollDetailGetRequestHandler> _localizer;

    public EmployeePayrollDetailGetRequestHandler(IRepository<EmployeePayrollDetail> repository, IStringLocalizer<EmployeePayrollDetailGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<EmployeePayrollDetailDto> Handle(EmployeePayrollDetailGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<EmployeePayrollDetail, EmployeePayrollDetailDto>)new EmployeePayrollDetailByIdWithEmployeeSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["EmployeePayrollDetail not found."], request.Id));
}