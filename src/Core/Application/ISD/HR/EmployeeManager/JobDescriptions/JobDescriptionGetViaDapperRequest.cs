using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionGetViaDapperRequest : IRequest<JobDescriptionDto>
{
    public DefaultIdType Id { get; set; }

    public JobDescriptionGetViaDapperRequest(Guid id) => Id = id;
}

public class JobDescriptionGetViaDapperRequestHandler : IRequestHandler<JobDescriptionGetViaDapperRequest, JobDescriptionDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<JobDescriptionGetViaDapperRequestHandler> _localizer;

    public JobDescriptionGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<JobDescriptionGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<JobDescriptionDto> Handle(JobDescriptionGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var jobDescription = await _repository.QueryFirstOrDefaultAsync<JobDescription>(
            $"SELECT * FROM datazaneco.\"JobDescriptions\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = jobDescription ?? throw new NotFoundException($"Job Description {request.Id} not found.");

        return jobDescription.Adapt<JobDescriptionDto>();
    }
}