using ZANECO.API.Application.App.Groups;
using ZANECO.API.Domain.App;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionCreateRequest : IRequest<Guid>
{
    public string GroupName { get; set; } = default!;
    public string BillMonth { get; set; } = default!;
    public decimal KWHPurchased { get; set; } = default!;

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class CreatePowerConsumptionRequestValidator : CustomValidator<PowerConsumptionCreateRequest>
{
    public CreatePowerConsumptionRequestValidator()
    {
        RuleFor(p => p.GroupName)
           .NotEmpty()
           .MaximumLength(16);

        RuleFor(p => p.KWHPurchased)
            .GreaterThanOrEqualTo(0);
    }
}

public class PowerConsumptionCreateRequestHandler : IRequestHandler<PowerConsumptionCreateRequest, Guid>
{
    private readonly IReadRepository<Group> _repoGroup;
    private readonly IRepositoryWithEvents<PowerConsumption> _repository;

    public PowerConsumptionCreateRequestHandler(IReadRepository<Group> repoGroup, IRepositoryWithEvents<PowerConsumption> repository) =>
        (_repoGroup, _repository) = (repoGroup, repository);

    public async Task<Guid> Handle(PowerConsumptionCreateRequest request, CancellationToken cancellationToken)
    {
        var group = await _repoGroup.FirstOrDefaultAsync(new GroupByNameSpec(request.GroupName), cancellationToken);

        var powerConsumption = new PowerConsumption(group!.Id, group.Code, group.Name, request.BillMonth, request.KWHPurchased, request.Description, request.Notes);

        await _repository.AddAsync(powerConsumption, cancellationToken);

        return powerConsumption.Id;
    }
}