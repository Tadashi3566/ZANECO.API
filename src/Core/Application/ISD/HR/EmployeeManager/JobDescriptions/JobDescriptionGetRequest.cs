using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionGetRequest : IRequest<JobDescriptionDto>
{
    public DefaultIdType Id { get; set; }

    public JobDescriptionGetRequest(Guid id) => Id = id;
}

public class JobDescriptionGetRequestHandler : IRequestHandler<JobDescriptionGetRequest, JobDescriptionDto>
{
    private readonly IRepository<JobDescription> _repository;
    private readonly IStringLocalizer<JobDescriptionGetRequestHandler> _localizer;

    public JobDescriptionGetRequestHandler(IRepository<JobDescription> repository, IStringLocalizer<JobDescriptionGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<JobDescriptionDto> Handle(JobDescriptionGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new JobDescriptionByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"Job Description {request.Id} not found.");
}