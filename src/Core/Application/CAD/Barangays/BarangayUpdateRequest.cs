using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Barangays;

public class BarangayUpdateRequest : RequestExtension BarangayUpdateRequest>, IRequest<Guid>
{
    public DefaultIdType AreaId { get; set; } = default!;
}

public class BarangayUpdateRequestValidator : CustomValidator<BarangayUpdateRequest>
{
    public BarangayUpdateRequestValidator(IReadRepository<Barangay> BarangayRepo, IStringLocalizer<BarangayUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.AreaId)
            .NotEmpty();

        RuleFor(p => p.Name)
            .NotEmpty()
            .MustAsync(async (Barangay, name, ct) =>
                    await BarangayRepo.FirstOrDefaultAsync(new BarangayByNameSpec(name), ct)
                        is not { } existingBarangay || existingBarangay.Id == Barangay.Id)
                .WithMessage((_, name) => string.Format(localizer["Barangay already exists."], name));
    }
}

public class BarangayUpdateRequestHandler : IRequestHandler<BarangayUpdateRequest, Guid>
{
    private readonly IReadRepository<Area> _repoArea;
    private readonly IRepositoryWithEvents<Barangay> _repository;
    private readonly IStringLocalizer<BarangayUpdateRequestHandler> _localizer;

    public BarangayUpdateRequestHandler(IReadRepository<Area> repoArea, IRepositoryWithEvents<Barangay> repository, IStringLocalizer<BarangayUpdateRequestHandler> localizer) =>
        (_repoArea, _repository, _localizer) = (repoArea, repository, localizer);

    public async Task<Guid> Handle(BarangayUpdateRequest request, CancellationToken cancellationToken)
    {
        var area = await _repoArea.GetByIdAsync(request.AreaId, cancellationToken);
        _ = area ?? throw new NotFoundException($"Area {request.AreaId} not found.");

        var barangay = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = barangay ?? throw new NotFoundException($"Barangay {request.Id} not found.");

        var updatedBarangay = barangay.Update(area.Name, request.Name, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedBarangay, cancellationToken);

        return request.Id;
    }
}