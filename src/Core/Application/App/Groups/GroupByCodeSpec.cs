﻿using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Groups;

public class GroupByCodeSpec : Specification<Group>, ISingleResultSpecification<Group>
{
    public GroupByCodeSpec(string code) => Query.Where(p => p.Code == code);
}