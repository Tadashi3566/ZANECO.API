using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public DesignationDeleteRequest(Guid id) => Id = id;
}

public class DesignationDeleteRequestHandler : IRequestHandler<DesignationDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Designation> _repository;
    private readonly IStringLocalizer<DesignationDeleteRequestHandler> _localizer;

    public DesignationDeleteRequestHandler(IRepositoryWithEvents<Designation> repository, IStringLocalizer<DesignationDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DesignationDeleteRequest request, CancellationToken cancellationToken)
    {
        var designation = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = designation ?? throw new NotFoundException(_localizer["designation not found."]);

        await _repository.DeleteAsync(designation, cancellationToken);

        return request.Id;
    }
}