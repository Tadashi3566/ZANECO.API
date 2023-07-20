using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationGetRequest : IRequest<DesignationDetailsDto>
{
    public DefaultIdType Id { get; set; }

    public DesignationGetRequest(Guid id) => Id = id;
}

public class DesignationGetRequestHandler : IRequestHandler<DesignationGetRequest, DesignationDetailsDto>
{
    private readonly IRepository<Designation> _repository;
    private readonly IStringLocalizer<DesignationGetRequestHandler> _localizer;

    public DesignationGetRequestHandler(IRepository<Designation> repository, IStringLocalizer<DesignationGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DesignationDetailsDto> Handle(DesignationGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new DesignationByIdWithEmployeeSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"designation {request.Id} not found.");
}