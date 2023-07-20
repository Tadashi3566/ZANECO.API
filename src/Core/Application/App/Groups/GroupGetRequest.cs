using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Groups;

public class GroupGetRequest : IRequest<GroupDto>
{
    public DefaultIdType Id { get; set; }

    public GroupGetRequest(Guid id) => Id = id;
}

public class GroupGetRequestHandler : IRequestHandler<GroupGetRequest, GroupDto>
{
    private readonly IRepositoryWithEvents<Group> _repository;
    private readonly IStringLocalizer<GroupGetRequestHandler> _localizer;

    public GroupGetRequestHandler(IRepositoryWithEvents<Group> repository, IStringLocalizer<GroupGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<GroupDto> Handle(GroupGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new GroupByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"group {request.Id} not found.");
}