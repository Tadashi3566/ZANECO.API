using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public RouteDeleteRequest(Guid id) => Id = id;
}

public class RouteDeleteRequestHandler : IRequestHandler<RouteDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Route> _repository;
    private readonly IStringLocalizer<RouteDeleteRequestHandler> _localizer;

    public RouteDeleteRequestHandler(IRepositoryWithEvents<Route> repository, IStringLocalizer<RouteDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(RouteDeleteRequest request, CancellationToken cancellationToken)
    {
        var route = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = route ?? throw new NotFoundException(_localizer["Route not found."]);

        await _repository.DeleteAsync(route, cancellationToken);

        return request.Id;
    }
}