using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionGetRequest : IRequest<RemoteCollectionDto>
{
    public DefaultIdType Id { get; set; }

    public RemoteCollectionGetRequest(Guid id) => Id = id;
}

public class RemoteCollectionGetRequestHandler : IRequestHandler<RemoteCollectionGetRequest, RemoteCollectionDto>
{
    private readonly IRepository<RemoteCollection> _repository;
    private readonly IStringLocalizer<RemoteCollectionGetRequestHandler> _localizer;

    public RemoteCollectionGetRequestHandler(IRepository<RemoteCollection> repository, IStringLocalizer<RemoteCollectionGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RemoteCollectionDto> Handle(RemoteCollectionGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<RemoteCollection, RemoteCollectionDto>)new RemoteCollectionByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["RemoteCollection not found."], request.Id));
}