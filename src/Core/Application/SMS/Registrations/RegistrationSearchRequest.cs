using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.SMS.Registrations;

public class RegistrationSearchRequest : PaginationFilter, IRequest<PaginationResponse<Master2022Dto>>
{
}

public class RegistrationBySearchRequestSpec : EntitiesByPaginationFilterSpec<Master2022, Master2022Dto>
{
    public RegistrationBySearchRequestSpec(RegistrationSearchRequest request)
        : base(request) => Query.OrderBy(q => q.Name, !request.HasOrderBy());
}

public class RegistrationSearchRequestHandler : IRequestHandler<RegistrationSearchRequest, PaginationResponse<Master2022Dto>>
{
    private readonly IReadRepository<Master2022> _repository;

    public RegistrationSearchRequestHandler(IReadRepository<Master2022> repository) => _repository = repository;

    public async Task<PaginationResponse<Master2022Dto>> Handle(RegistrationSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new RegistrationBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}