using Mapster;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Barangays;

public class BarangayGetViaDapperRequest : IRequest<BarangayDto>
{
    public DefaultIdType Id { get; set; }

    public BarangayGetViaDapperRequest(Guid id) => Id = id;
}

public class BarangayGetViaDapperRequestHandler : IRequestHandler<BarangayGetViaDapperRequest, BarangayDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<BarangayGetViaDapperRequestHandler> _localizer;

    public BarangayGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<BarangayGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<BarangayDto> Handle(BarangayGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var barangay = await _repository.QueryFirstOrDefaultAsync<Barangay>(
        $"SELECT * FROM datazaneco.\"Barangays\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = barangay ?? throw new NotFoundException(string.Format(_localizer["Barangay not found."], request.Id));

        return barangay.Adapt<BarangayDto>();
    }
}