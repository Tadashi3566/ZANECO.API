using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentGetRequest : IRequest<AdjustmentDto>
{
    public DefaultIdType Id { get; set; }

    public AdjustmentGetRequest(Guid id) => Id = id;
}

public class AdjustmentGetRequestHandler : IRequestHandler<AdjustmentGetRequest, AdjustmentDto>
{
    private readonly IRepository<Adjustment> _repository;
    private readonly IStringLocalizer<AdjustmentGetRequestHandler> _localizer;

    public AdjustmentGetRequestHandler(IRepository<Adjustment> repository, IStringLocalizer<AdjustmentGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AdjustmentDto> Handle(AdjustmentGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync((ISpecification<Adjustment, AdjustmentDto>)new AdjustmentByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Adjustment not found."], request.Id));
}