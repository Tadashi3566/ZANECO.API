using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Barangays;

public class BarangayGetRequest : IRequest<BarangayDto>
{
    public DefaultIdType Id { get; set; }

    public BarangayGetRequest(Guid id) => Id = id;
}

public class BarangayGetRequestHandler : IRequestHandler<BarangayGetRequest, BarangayDto>
{
    private readonly IRepository<Barangay> _repository;
    private readonly IStringLocalizer<BarangayGetRequestHandler> _localizer;

    public BarangayGetRequestHandler(IRepository<Barangay> repository, IStringLocalizer<BarangayGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<BarangayDto> Handle(BarangayGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new BarangayByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Barangay not found."], request.Id));
}