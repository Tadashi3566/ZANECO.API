using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionByReferenceSpec : Specification<RemoteCollection>, ISingleResultSpecification
{
    public RemoteCollectionByReferenceSpec(string reference) =>
        Query.Where(p => p.Reference == reference);
}