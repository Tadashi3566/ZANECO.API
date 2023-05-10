using Mapster;
using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Groups;

public class GroupGetViaDapperRequest : IRequest<GroupDto>
{
    public DefaultIdType Id { get; set; }

    public GroupGetViaDapperRequest(Guid id) => Id = id;
}

public class GroupViaDapperGetRequestHandler : IRequestHandler<GroupGetViaDapperRequest, GroupDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<GroupViaDapperGetRequestHandler> _localizer;

    public GroupViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<GroupViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<GroupDto> Handle(GroupGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var group = await _repository.QueryFirstOrDefaultAsync<Group>(
            $"SELECT * FROM datazaneco.\"Groups\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = group ?? throw new NotFoundException(string.Format(_localizer["group not found."], request.Id));

        return group.Adapt<GroupDto>();
    }
}