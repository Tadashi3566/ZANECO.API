using Mapster;
using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerGetViaDapperRequest : IRequest<WinnerDto>
{
    public DefaultIdType Id { get; set; }

    public WinnerGetViaDapperRequest(Guid id) => Id = id;
}

public class WinnerViaDapperGetRequestHandler : IRequestHandler<WinnerGetViaDapperRequest, WinnerDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<WinnerViaDapperGetRequestHandler> _localizer;

    public WinnerViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<WinnerViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<WinnerDto> Handle(WinnerGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var winner = await _repository.QueryFirstOrDefaultAsync<Winner>(
            $"SELECT * FROM datazaneco.\"Winners\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = winner ?? throw new NotFoundException($"Winner {request.Id} not found.");

        return winner.Adapt<WinnerDto>();
    }
}