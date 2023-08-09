using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public JobDescriptionDeleteRequest(Guid id) => Id = id;
}

public class JobDescriptionDeleteRequestHandler : IRequestHandler<JobDescriptionDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<JobDescription> _repository;
    private readonly IStringLocalizer<JobDescriptionDeleteRequestHandler> _localizer;

    public JobDescriptionDeleteRequestHandler(IRepositoryWithEvents<JobDescription> repository, IStringLocalizer<JobDescriptionDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(JobDescriptionDeleteRequest request, CancellationToken cancellationToken)
    {
        var jobDescription = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = jobDescription ?? throw new NotFoundException($"JobDescriptions {request.Id} not found.");

        await _repository.DeleteAsync(jobDescription, cancellationToken);

        return request.Id;
    }
}