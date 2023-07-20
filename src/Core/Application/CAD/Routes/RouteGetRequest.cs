using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteGetRequest : IRequest<RouteDto>
{
    public DefaultIdType Id { get; set; }

    public RouteGetRequest(Guid id) => Id = id;
}

public class RouteGetRequestHandler : IRequestHandler<RouteGetRequest, RouteDto>
{
    private readonly IRepository<Route> _repository;
    private readonly IStringLocalizer<RouteGetRequestHandler> _localizer;

    public RouteGetRequestHandler(IRepository<Route> repository, IStringLocalizer<RouteGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RouteDto> Handle(RouteGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new RouteByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"Route {request.Id} not found.");
}