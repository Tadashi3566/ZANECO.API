using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType AreaId { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
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
                        is not Route existingRoute || existingRoute.Id == route.Id)
                .WithMessage((_, code) => string.Format(localizer["Route already exists"], code));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MustAsync(async (route, name, ct) =>
                    await repository.FirstOrDefaultAsync(new RouteByNameSpec(name), ct)
                        is not Route existingRoute || existingRoute.Id == route.Id)
                .WithMessage((_, name) => string.Format(localizer["Route already exists"], name));
    }
}

public class RouteUpdateRequestHandler : IRequestHandler<RouteUpdateRequest, Guid>
{
    private readonly IReadRepository<Area> _repoArea;
    private readonly IRepositoryWithEvents<Route> _repository;
    private readonly IStringLocalizer<RouteUpdateRequestHandler> _localizer;

    public RouteUpdateRequestHandler(IReadRepository<Area> repoArea, IRepositoryWithEvents<Route> repository, IStringLocalizer<RouteUpdateRequestHandler> localizer) =>
        (_repoArea, _repository, _localizer) = (repoArea, repository, localizer);

    public async Task<Guid> Handle(RouteUpdateRequest request, CancellationToken cancellationToken)
    {
        var area = await _repoArea.GetByIdAsync(request.AreaId, cancellationToken);
        _ = area ?? throw new NotFoundException(string.Format(_localizer["Area not found."], request.AreaId));

        var route = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = route ?? throw new NotFoundException(string.Format(_localizer["Route not found."], request.Id));

        var updatedRoute = route.Update(area.Name, request.Number, request.Code, request.Name, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedRoute, cancellationToken);

        return request.Id;
    }
}