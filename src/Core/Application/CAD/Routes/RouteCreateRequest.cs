using ZANECO.API.Application.CAD.Barangays;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteCreateRequest : IRequest<Guid>
{
    public DefaultIdType AreaId { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class CreateRouteRequestValidator : CustomValidator<RouteCreateRequest>
{
    public CreateRouteRequestValidator(IReadRepository<Route> RouteRepo, IStringLocalizer<CreateRouteRequestValidator> localizer)
    {
        RuleFor(p => p.AreaId)
            .NotEmpty();

        RuleFor(p => p.Number)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Code)
            .NotEmpty()
            .MustAsync(async (code, ct) => await RouteRepo.FirstOrDefaultAsync(new RouteByCodeSpec(code), ct) is null)
            .WithMessage((_, Route) => string.Format(localizer["Route already exists"], Route));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MustAsync(async (name, ct) => await RouteRepo.FirstOrDefaultAsync(new RouteByNameSpec(name), ct) is null)
            .WithMessage((_, Route) => string.Format(localizer["Route already exists"], Route));
    }
}

public class RouteCreateRequestHandler : IRequestHandler<RouteCreateRequest, Guid>
{
    private readonly IReadRepository<Area> _repoArea;
    private readonly IRepositoryWithEvents<Route> _repository;
    private readonly IStringLocalizer<BarangayUpdateRequestHandler> _localizer;

    public RouteCreateRequestHandler(IReadRepository<Area> repoArea, IRepositoryWithEvents<Route> repository, IStringLocalizer<BarangayUpdateRequestHandler> localizer) =>
        (_repoArea, _repository, _localizer) = (repoArea, repository, localizer);

    public async Task<Guid> Handle(RouteCreateRequest request, CancellationToken cancellationToken)
    {
        var area = await _repoArea.GetByIdAsync(request.AreaId, cancellationToken);
        _ = area ?? throw new NotFoundException(string.Format(_localizer["Area not found."], request.AreaId));

        var route = new Route(request.AreaId, area.Name, request.Number, request.Code, request.Name, request.Description, request.Notes);

        await _repository.AddAsync(route, cancellationToken);

        return route.Id;
    }
}