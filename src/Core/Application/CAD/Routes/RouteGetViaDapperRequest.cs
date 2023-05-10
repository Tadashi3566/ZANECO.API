using Mapster;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteGetViaDapperRequest : IRequest<RouteDto>
{
    public DefaultIdType Id { get; set; }

    public RouteGetViaDapperRequest(Guid id) => Id = id;
}

public class RouteGetViaDapperRequestHandler : IRequestHandler<RouteGetViaDapperRequest, RouteDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<RouteGetViaDapperRequestHandler> _localizer;

    public RouteGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<RouteGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RouteDto> Handle(RouteGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var route = await _repository.QueryFirstOrDefaultAsync<Route>(
            $"SELECT * FROM datazaneco.\"Routes\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = route ?? throw new NotFoundException(string.Format(_localizer["Route not found."], request.Id));

        return route.Adapt<RouteDto>();
    }
}