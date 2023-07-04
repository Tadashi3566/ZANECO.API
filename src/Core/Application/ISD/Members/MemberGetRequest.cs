using ZANECO.API.Domain.ISD;

namespace ZANECO.API.Application.ISD.Members;

public class MemberGetRequest : IRequest<MemberDto>
{
    public DefaultIdType Id { get; set; }

    public MemberGetRequest(Guid id) => Id = id;
}

public class MemberGetRequestHandler : IRequestHandler<MemberGetRequest, MemberDto>
{
    private readonly IRepository<Member> _repository;
    private readonly IStringLocalizer<MemberGetRequestHandler> _localizer;

    public MemberGetRequestHandler(IRepository<Member> repository, IStringLocalizer<MemberGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<MemberDto> Handle(MemberGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new MemberByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["member not found."], request.Id));
}