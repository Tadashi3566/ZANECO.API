using Mapster;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionGetViaDapperRequest : IRequest<RemoteCollectionDto>
{
    public DefaultIdType Id { get; set; }

    public RemoteCollectionGetViaDapperRequest(Guid id) => Id = id;
}

public class RemoteCollectionGetViaDapperRequestHandler : IRequestHandler<RemoteCollectionGetViaDapperRequest, RemoteCollectionDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<RemoteCollectionGetViaDapperRequestHandler> _localizer;

    public RemoteCollectionGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<RemoteCollectionGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RemoteCollectionDto> Handle(RemoteCollectionGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var remoteCollection = await _repository.QueryFirstOrDefaultAsync<RemoteCollection>(
            $"SELECT * FROM datazaneco.\"RemoteCollections\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = remoteCollection ?? throw new NotFoundException($"remoteCollection {request.Id} not found.");

        return remoteCollection.Adapt<RemoteCollectionDto>();
    }
}