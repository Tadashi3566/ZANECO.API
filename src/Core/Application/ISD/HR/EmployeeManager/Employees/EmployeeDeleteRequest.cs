using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public EmployeeDeleteRequest(Guid id) => Id = id;
}

public class EmployeeDeleteRequestHandler : IRequestHandler<EmployeeDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Employee> _repository;
    private readonly IStringLocalizer<EmployeeDeleteRequestHandler> _localizer;

    public EmployeeDeleteRequestHandler(IRepositoryWithEvents<Employee> repository, IStringLocalizer<EmployeeDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(EmployeeDeleteRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = employee ?? throw new NotFoundException(_localizer["employee not found."]);

        await _repository.DeleteAsync(employee, cancellationToken);

        return request.Id;
    }
}