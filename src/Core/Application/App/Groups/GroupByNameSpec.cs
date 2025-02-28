﻿using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Groups;

public class GroupByNameSpec : Specification<Group>, ISingleResultSpecification<Group>
{
    public GroupByNameSpec(string name) => Query.Where(p => p.Name == name);
}