using Mapster;
using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeGetViaDapperRequest : IRequest<PrizeDto>
{
    public DefaultIdType Id { get; set; }

    public PrizeGetViaDapperRequest(Guid id) => Id = id;
}

public class PrizeViaDapperGetRequestHandler : IRequestHandler<PrizeGetViaDapperRequest, PrizeDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<PrizeViaDapperGetRequestHandler> _localizer;

    public PrizeViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<PrizeViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PrizeDto> Handle(PrizeGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var prize = await _repository.QueryFirstOrDefaultAsync<Prize>(
            $"SELECT * FROM datazaneco.\"Prizes\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = prize ?? throw new NotFoundException(string.Format(_localizer["Prize not found."], request.Id));

        return prize.Adapt<PrizeDto>();
    }
}