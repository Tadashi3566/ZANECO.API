using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Barangays;

public class BarangayDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public BarangayDeleteRequest(Guid id) => Id = id;
}

public class BarangayDeleteRequestHandler : IRequestHandler<BarangayDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Barangay> _repository;
    private readonly IStringLocalizer<BarangayDeleteRequestHandler> _localizer;

    public BarangayDeleteRequestHandler(IRepositoryWithEvents<Barangay> repository, IStringLocalizer<BarangayDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(BarangayDeleteRequest request, CancellationToken cancellationToken)
    {
        var barangay = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = barangay ?? throw new NotFoundException($"Barangay {request.Id} not found.");

        await _repository.DeleteAsync(barangay, cancellationToken);

        return request.Id;
    }
}