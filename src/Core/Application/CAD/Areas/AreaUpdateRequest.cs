using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Areas;

public class AreaUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class AreaUpdateRequestValidator : CustomValidator<AreaUpdateRequest>
{
    public AreaUpdateRequestValidator(IReadRepository<Area> repository, IStringLocalizer<AreaUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.Number)
            .GreaterThan(0)
            .MustAsync(async (area, number, ct) =>
                    await repository.FirstOrDefaultAsync(new AreaByNumberSpec(number), ct)
                        is not Area existingArea || existingArea.Id == area.Id)
                .WithMessage((_, name) => string.Format(localizer["Area already exists"], name));

        RuleFor(p => p.Code)
            .NotEmpty()
            .MustAsync(async (area, code, ct) =>
                    await repository.FirstOrDefaultAsync(new AreaByCodeSpec(code), ct)
                        is not Area existingArea || existingArea.Id == area.Id)
                .WithMessage((_, name) => string.Format(localizer["Area already exists"], name));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MustAsync(async (area, name, ct) =>
                    await repository.FirstOrDefaultAsync(new AreaByNameSpec(name), ct)
                        is not Area existingArea || existingArea.Id == area.Id)
                .WithMessage((_, name) => string.Format(localizer["Area already exists"], name));
    }
}

public class AreaUpdateRequestHandler : IRequestHandler<AreaUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Area> _repository;
    private readonly IStringLocalizer<AreaUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public AreaUpdateRequestHandler(IRepositoryWithEvents<Area> repository, IStringLocalizer<AreaUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(AreaUpdateRequest request, CancellationToken cancellationToken)
    {
        var area = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = area ?? throw new NotFoundException(string.Format(_localizer["Area not found."], request.Id));

        var updatedArea = area.Update(request.Number, request.Code, request.Name, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedArea, cancellationToken);

        return request.Id;
    }
}