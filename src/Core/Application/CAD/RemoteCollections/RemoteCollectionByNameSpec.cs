﻿using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionByNameSpec : Specification<RemoteCollection>, ISingleResultSpecification
{
    public RemoteCollectionByNameSpec(string name) => Query.Where(p => p.Name == name);
}