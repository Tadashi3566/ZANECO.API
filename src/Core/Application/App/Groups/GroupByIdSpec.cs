using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Groups;

public class GroupByIdSpec : Specification<Group, GroupDto>, ISingleResultSpecification<Group>
{
    public GroupByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}