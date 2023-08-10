using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteUpdateRequest : BaseRequest, IRequest<Guid>
{
    public DefaultIdType AreaId { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
}

public class RouteUpdateRequestValidator : CustomValidator<RouteUpdateRequest>
{
    public RouteUpdateRequestValidator(IReadRepository<Route> repository, IStringLocalizer<RouteUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.AreaId)
            .NotEmpty();

        RuleFor(p => p.Number)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Code)
            .NotEmpty()
            .MustAsync(async (route, code, ct) =>
                    await repository.FirstOrDefaultAsync(new RouteByCodeSpec(code), ct)
                        is not { } existingRoute || existingRoute.Id == route.Id)
                .WithMessage((_, code) => string.Format(localizer["Route {0} already exists."], code));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MustAsync(async (route, name, ct) =>
                    await repository.FirstOrDefaultAsync(new RouteByNameSpec(name), ct)
                        is not { } existingRoute || existingRoute.Id == route.Id)
                .WithMessage((_, name) => string.Format(localizer["Route already exists."], name));
    }
}

public class RouteUpdateRequestHandler : IRequestHandler<RouteUpdateRequest, Guid>
{
    private readonly IReadRepository<Area> _repoArea;
    private readonly IRepositoryWithEvents<Route> _repoRoute;

    public RouteUpdateRequestHandler(IReadRepository<Area> repoArea, IRepositoryWithEvents<Route> repoRoute, IStringLocalizer<RouteUpdateRequestHandler> localizer) =>
        (_repoArea, _repoRoute) = (repoArea, repoRoute);

    public async Task<Guid> Handle(RouteUpdateRequest request, CancellationToken cancellationToken)
    {
        var area = await _repoArea.GetByIdAsync(request.AreaId, cancellationToken);
        _ = area ?? throw new NotFoundException($"Area {request.AreaId} not found.");

        var route = await _repoRoute.GetByIdAsync(request.Id, cancellationToken);
        _ = route ?? throw new NotFoundException($"Route {request.Id} not found.");

        var updatedRoute = route.Update(area.Name, request.Number, request.Code, request.Name, request.Description, request.Notes);

        await _repoRoute.UpdateAsync(updatedRoute, cancellationToken);

        return request.Id;
    }
}