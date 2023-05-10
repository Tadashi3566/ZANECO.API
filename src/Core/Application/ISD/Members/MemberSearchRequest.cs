using ZANECO.API.Domain.ISD;

namespace ZANECO.API.Application.ISD.Members;

public class MemberSearchRequest : PaginationFilter, IRequest<PaginationResponse<MemberDto>>
{
}

public class MembersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Member, MemberDto>
{
    public MembersBySearchRequestSpec(MemberSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class MemberSearchRequestHandler : IRequestHandler<MemberSearchRequest, PaginationResponse<MemberDto>>
{
    private readonly IReadRepository<Member> _repository;

    public MemberSearchRequestHandler(IReadRepository<Member> repository) => _repository = repository;

    public async Task<PaginationResponse<MemberDto>> Handle(MemberSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new MembersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}