using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Groups;

public class GroupSearchRequest : PaginationFilter, IRequest<PaginationResponse<GroupDto>>
{
}

public class GroupBySearchRequestSpec : EntitiesByPaginationFilterSpec<Group, GroupDto>
{
    public GroupBySearchRequestSpec(GroupSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Parent, !request.HasOrderBy());
}

public class GroupSearchRequestHandler : IRequestHandler<GroupSearchRequest, PaginationResponse<GroupDto>>
{
    private readonly IReadRepository<Group> _repository;

    public GroupSearchRequestHandler(IReadRepository<Group> repository) => _repository = repository;

    public async Task<PaginationResponse<GroupDto>> Handle(GroupSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new GroupBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}