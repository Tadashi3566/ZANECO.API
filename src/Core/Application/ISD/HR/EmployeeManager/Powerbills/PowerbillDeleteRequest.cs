using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public PowerbillDeleteRequest(Guid id) => Id = id;
}

public class PowerbillDeleteRequestHandler : IRequestHandler<PowerbillDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Powerbill> _repository;
    private readonly IStringLocalizer<PowerbillDeleteRequestHandler> _localizer;

    public PowerbillDeleteRequestHandler(IRepositoryWithEvents<Powerbill> repository, IStringLocalizer<PowerbillDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(PowerbillDeleteRequest request, CancellationToken cancellationToken)
    {
        var powerbill = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = powerbill ?? throw new NotFoundException($"Powerbill {request.Id} not found.");

        await _repository.DeleteAsync(powerbill, cancellationToken);

        return request.Id;
    }
}