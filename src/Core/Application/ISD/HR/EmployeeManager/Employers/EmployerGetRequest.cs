using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerGetRequest : IRequest<EmployerDetailsDto>
{
    public DefaultIdType Id { get; set; }

    public EmployerGetRequest(Guid id) => Id = id;
}

public class EmployerGetRequestHandler : IRequestHandler<EmployerGetRequest, EmployerDetailsDto>
{
    private readonly IRepository<Employer> _repository;
    private readonly IStringLocalizer<EmployerGetRequestHandler> _localizer;

    public EmployerGetRequestHandler(IRepository<Employer> repository, IStringLocalizer<EmployerGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<EmployerDetailsDto> Handle(EmployerGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Employer, EmployerDetailsDto>)new EmployerByIdWithEmployeeSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["employer not found."], request.Id));
}