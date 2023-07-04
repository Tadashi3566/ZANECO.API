using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeGetRequest : IRequest<PrizeDto>
{
    public DefaultIdType Id { get; set; }

    public PrizeGetRequest(Guid id) => Id = id;
}

public class PrizeGetRequestHandler : IRequestHandler<PrizeGetRequest, PrizeDto>
{
    private readonly IRepositoryWithEvents<Prize> _repository;
    private readonly IStringLocalizer<PrizeGetRequestHandler> _localizer;

    public PrizeGetRequestHandler(IRepositoryWithEvents<Prize> repository, IStringLocalizer<PrizeGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PrizeDto> Handle(PrizeGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new PrizeByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Prize not found."], request.Id));
}