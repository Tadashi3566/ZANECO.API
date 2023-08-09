using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionSearchRequest : PaginationFilter, IRequest<PaginationResponse<JobDescriptionDto>>
{
}

public class JobDescriptionsBySearchRequestSpec : EntitiesByPaginationFilterSpec<JobDescription, JobDescriptionDto>
{
    public JobDescriptionsBySearchRequestSpec(JobDescriptionSearchRequest request)
        : base(request) => Query.OrderBy(x => x.Number, !request.HasOrderBy());
}

public class JobDescriptionSearchRequestHandler : IRequestHandler<JobDescriptionSearchRequest, PaginationResponse<JobDescriptionDto>>
{
    private readonly IReadRepository<JobDescription> _repository;

    public JobDescriptionSearchRequestHandler(IReadRepository<JobDescription> repository) => _repository = repository;

    public async Task<PaginationResponse<JobDescriptionDto>> Handle(JobDescriptionSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new JobDescriptionsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}