using Mapster;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Areas;

public class AreaGetViaDapperRequest : IRequest<AreaDto>
{
    public DefaultIdType Id { get; set; }

    public AreaGetViaDapperRequest(Guid id) => Id = id;
}

public class AreaGetViaDapperRequestHandler : IRequestHandler<AreaGetViaDapperRequest, AreaDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<AreaGetViaDapperRequestHandler> _localizer;

    public AreaGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<AreaGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AreaDto> Handle(AreaGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var area = await _repository.QueryFirstOrDefaultAsync<Area>(
        $"SELECT * FROM datazaneco.\"Areas\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = area ?? throw new NotFoundException($"Area {request.Id} not found.");

        return area.Adapt<AreaDto>();
    }
}