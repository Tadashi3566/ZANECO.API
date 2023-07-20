using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Rates;

public class RateGetRequest : IRequest<RateDto>
{
    public DefaultIdType Id { get; set; }

    public RateGetRequest(Guid id) => Id = id;
}

public class RateGetRequestHandler : IRequestHandler<RateGetRequest, RateDto>
{
    private readonly IRepository<Rate> _repository;
    private readonly IStringLocalizer<RateGetRequestHandler> _localizer;

    public RateGetRequestHandler(IRepository<Rate> repository, IStringLocalizer<RateGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RateDto> Handle(RateGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new RateByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"rate {request.Id} not found.");
}