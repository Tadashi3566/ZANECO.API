using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentGetRequest : IRequest<EmployeeAdjustmentDto>
{
    public DefaultIdType Id { get; set; }

    public EmployeeAdjustmentGetRequest(Guid id) => Id = id;
}

public class EmployeeAdjustmentGetRequestHandler : IRequestHandler<EmployeeAdjustmentGetRequest, EmployeeAdjustmentDto>
{
    private readonly IRepository<EmployeeAdjustment> _repository;
    private readonly IStringLocalizer<EmployeeAdjustmentGetRequestHandler> _localizer;

    public EmployeeAdjustmentGetRequestHandler(IRepository<EmployeeAdjustment> repository, IStringLocalizer<EmployeeAdjustmentGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<EmployeeAdjustmentDto> Handle(EmployeeAdjustmentGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new EmployeeAdjustmentByIdWithEmployeeSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["EmployeeAdjustment not found."], request.Id));
}