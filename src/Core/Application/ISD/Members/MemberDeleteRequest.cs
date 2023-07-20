using ZANECO.API.Domain.ISD;

namespace ZANECO.API.Application.ISD.Members;

public class MemberDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public MemberDeleteRequest(Guid id) => Id = id;
}

public class MemberDeleteRequestHandler : IRequestHandler<MemberDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Member> _repository;
    private readonly IStringLocalizer<MemberDeleteRequestHandler> _localizer;

    public MemberDeleteRequestHandler(IRepositoryWithEvents<Member> repository, IStringLocalizer<MemberDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(MemberDeleteRequest request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = member ?? throw new NotFoundException($"member {request.Id} not found.");

        await _repository.DeleteAsync(member, cancellationToken);

        return request.Id;
    }
}