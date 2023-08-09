using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Areas;

public class AreaCreateRequest : IRequest<Guid>
{
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CreateAreaRequestValidator : CustomValidator<AreaCreateRequest>
{
    public CreateAreaRequestValidator(IReadRepository<Area> AreaRepo, IStringLocalizer<CreateAreaRequestValidator> localizer)
    {
        RuleFor(p => p.Number)
            .GreaterThan(0)
            .MustAsync(async (number, ct) => await AreaRepo.FirstOrDefaultAsync(new AreaByNumberSpec(number), ct) is null)
            .WithMessage((_, area) => string.Format(localizer["Area already exists."], area));

        RuleFor(p => p.Code)
            .NotEmpty()
            .MustAsync(async (code, ct) => await AreaRepo.FirstOrDefaultAsync(new AreaByCodeSpec(code), ct) is null)
            .WithMessage((_, area) => string.Format(localizer["Area already exists."], area));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (name, ct) => await AreaRepo.FirstOrDefaultAsync(new AreaByNameSpec(name), ct) is null)
            .WithMessage((_, area) => string.Format(localizer["Area already exists."], area));
    }
}

public class AreaCreateRequestHandler : IRequestHandler<AreaCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Area> _repository;

    public AreaCreateRequestHandler(IRepositoryWithEvents<Area> repository) => _repository = repository;

    public async Task<Guid> Handle(AreaCreateRequest request, CancellationToken cancellationToken)
    {
        var area = new Area(request.Number, request.Code, request.Name, request.Description, request.Notes);

        await _repository.AddAsync(area, cancellationToken);

        return area.Id;
    }
}