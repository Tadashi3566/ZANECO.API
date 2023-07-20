using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillGetRequest : IRequest<PowerbillDetailsDto>
{
    public DefaultIdType Id { get; set; }

    public PowerbillGetRequest(Guid id) => Id = id;
}

public class PowerbillGetRequestHandler : IRequestHandler<PowerbillGetRequest, PowerbillDetailsDto>
{
    private readonly IRepository<Powerbill> _repository;
    private readonly IStringLocalizer<PowerbillGetRequestHandler> _localizer;

    public PowerbillGetRequestHandler(IRepository<Powerbill> repository, IStringLocalizer<PowerbillGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PowerbillDetailsDto> Handle(PowerbillGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new PowerbillByIdWithEmployeeSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"powerbill {request.Id} not found.");
}