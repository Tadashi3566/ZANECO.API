using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionByIdSpec : Specification<RemoteCollection, RemoteCollectionDto>, ISingleResultSpecification<RemoteCollection>
{
    public RemoteCollectionByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}