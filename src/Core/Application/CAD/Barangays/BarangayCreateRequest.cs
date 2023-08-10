using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Barangays;

public class BarangayCreateRequest : BaseRequest, IRequest<Guid>
{
    public DefaultIdType AreaId { get; set; } = default!;
}

public class CreateBarangayRequestValidator : CustomValidator<BarangayCreateRequest>
{
    public CreateBarangayRequestValidator(IReadRepository<Barangay> BarangayRepo, IStringLocalizer<CreateBarangayRequestValidator> localizer)
    {
        RuleFor(p => p.AreaId)
            .NotEmpty();

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (name, ct) => await BarangayRepo.FirstOrDefaultAsync(new BarangayByNameSpec(name), ct) is null)
            .WithMessage((_, Barangay) => string.Format(localizer["Barangay already exists."], Barangay));
    }
}

public class BarangayCreateRequestHandler : IRequestHandler<BarangayCreateRequest, Guid>
{
    private readonly IReadRepository<Area> _repoArea;
    private readonly IRepositoryWithEvents<Barangay> _repository;

    public BarangayCreateRequestHandler(IReadRepository<Area> repoArea, IRepositoryWithEvents<Barangay> repository) =>
        (_repoArea, _repository) = (repoArea, repository);

    public async Task<Guid> Handle(BarangayCreateRequest request, CancellationToken cancellationToken)
    {
        var area = await _repoArea.GetByIdAsync(request.AreaId, cancellationToken);
        _ = area ?? throw new NotFoundException($"Area {request.AreaId} not found.");

        var barangay = new Barangay(request.AreaId, area.Name, request.Name, request.Description, request.Notes);

        await _repository.AddAsync(barangay, cancellationToken);

        return barangay.Id;
    }
}