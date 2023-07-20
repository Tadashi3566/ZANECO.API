using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public RemoteCollectionDeleteRequest(Guid id) => Id = id;
}

public class RemoteCollectionDeleteRequestHandler : IRequestHandler<RemoteCollectionDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<RemoteCollection> _repository;
    private readonly IStringLocalizer<RemoteCollectionDeleteRequestHandler> _localizer;

    public RemoteCollectionDeleteRequestHandler(IRepositoryWithEvents<RemoteCollection> repository, IStringLocalizer<RemoteCollectionDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(RemoteCollectionDeleteRequest request, CancellationToken cancellationToken)
    {
        var remoteCollection = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = remoteCollection ?? throw new NotFoundException($"remoteCollection {request.Id} not found.");

        await _repository.DeleteAsync(remoteCollection, cancellationToken);

        return request.Id;
    }
}