using ZANECO.API.Application.App.Groups;
using ZANECO.API.Domain.App;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionUpdateRequest : RequestExtension PowerConsumptionUpdateRequest>, IRequest<Guid>
{
    public string GroupName { get; set; } = default!;
    public string BillMonth { get; set; } = default!;
    public decimal KWHPurchased { get; set; } = default!;
}

public class PowerConsumptionUpdateRequestValidator : CustomValidator<PowerConsumptionUpdateRequest>
{
    public PowerConsumptionUpdateRequestValidator(IReadRepository<PowerConsumption> PowerConsumptionRepo, IStringLocalizer<PowerConsumptionUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.GroupName)
           .NotEmpty()
           .MaximumLength(16);

        RuleFor(p => p.KWHPurchased)
            .GreaterThanOrEqualTo(0);
    }
}

public class PowerConsumptionUpdateRequestHandler : IRequestHandler<PowerConsumptionUpdateRequest, Guid>
{
    private readonly IReadRepository<Group> _repoGroup;
    private readonly IRepositoryWithEvents<PowerConsumption> _repository;
    private readonly IStringLocalizer<PowerConsumptionUpdateRequestHandler> _localizer;

    public PowerConsumptionUpdateRequestHandler(IReadRepository<Group> repoGroup, IRepositoryWithEvents<PowerConsumption> repository, IStringLocalizer<PowerConsumptionUpdateRequestHandler> localizer) =>
        (_repoGroup, _repository, _localizer) = (repoGroup, repository, localizer);

    public async Task<Guid> Handle(PowerConsumptionUpdateRequest request, CancellationToken cancellationToken)
    {
        var group = await _repoGroup.FirstOrDefaultAsync(new GroupByNameSpec(request.GroupName), cancellationToken);

        var powerConsumption = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = powerConsumption ?? throw new NotFoundException($"PowerConsumption {request.Id} not found.");

        var updatedPowerConsumption = powerConsumption.Update(group!.Code, group.Name, request.BillMonth, request.KWHPurchased, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedPowerConsumption, cancellationToken);

        return request.Id;
    }
}