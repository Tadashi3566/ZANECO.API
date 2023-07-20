using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public AdjustmentDeleteRequest(Guid id) => Id = id;
}

public class AdjustmentDeleteRequestHandler : IRequestHandler<AdjustmentDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Adjustment> _repository;
    private readonly IStringLocalizer<AdjustmentDeleteRequestHandler> _localizer;

    public AdjustmentDeleteRequestHandler(IRepositoryWithEvents<Adjustment> repository, IStringLocalizer<AdjustmentDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(AdjustmentDeleteRequest request, CancellationToken cancellationToken)
    {
        var adjustment = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = adjustment ?? throw new NotFoundException($"Adjustment {request.Id} not found.");

        await _repository.DeleteAsync(adjustment, cancellationToken);

        return request.Id;
    }
}