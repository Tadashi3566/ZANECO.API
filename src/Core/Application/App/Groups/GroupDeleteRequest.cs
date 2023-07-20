using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Groups;

public class GroupDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public GroupDeleteRequest(Guid id) => Id = id;
}

public class GroupDeleteRequestHandler : IRequestHandler<GroupDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Group> _repository;
    private readonly IStringLocalizer<GroupDeleteRequestHandler> _localizer;

    public GroupDeleteRequestHandler(IRepositoryWithEvents<Group> repository, IStringLocalizer<GroupDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(GroupDeleteRequest request, CancellationToken cancellationToken)
    {
        var group = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = group ?? throw new NotFoundException($"Group {request.Id} not found.");

        await _repository.DeleteAsync(group, cancellationToken);

        return request.Id;
    }
}