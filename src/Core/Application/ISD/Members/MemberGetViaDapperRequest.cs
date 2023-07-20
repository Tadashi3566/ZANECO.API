using Mapster;
using ZANECO.API.Domain.ISD;

namespace ZANECO.API.Application.ISD.Members;

public class MemberGetViaDapperRequest : IRequest<MemberDto>
{
    public DefaultIdType Id { get; set; }

    public MemberGetViaDapperRequest(Guid id) => Id = id;
}

public class MemberGetViaDapperRequestHandler : IRequestHandler<MemberGetViaDapperRequest, MemberDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<MemberGetViaDapperRequestHandler> _localizer;

    public MemberGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<MemberGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<MemberDto> Handle(MemberGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var member = await _repository.QueryFirstOrDefaultAsync<Member>(
            $"SELECT * FROM datazaneco.\"Members\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = member ?? throw new NotFoundException($"member {request.Id} not found.");

        return member.Adapt<MemberDto>();
    }
}