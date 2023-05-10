using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public EmployerDeleteRequest(Guid id) => Id = id;
}

public class EmployerDeleteRequestHandler : IRequestHandler<EmployerDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Employer> _repository;
    private readonly IStringLocalizer<EmployerDeleteRequestHandler> _localizer;

    public EmployerDeleteRequestHandler(IRepositoryWithEvents<Employer> repository, IStringLocalizer<EmployerDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(EmployerDeleteRequest request, CancellationToken cancellationToken)
    {
        var employer = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = employer ?? throw new NotFoundException(_localizer["employer not found."]);

        await _repository.DeleteAsync(employer, cancellationToken);

        return request.Id;
    }
}