using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionCreateRequest : RequestWithImageExtension<JobDescriptionCreateRequest>, IRequest<Guid>
{
    public int Rank { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string ReportsTo { get; set; } = default!;
}

public class CreateJobDescriptionRequestValidator : CustomValidator<JobDescriptionCreateRequest>
{
    public CreateJobDescriptionRequestValidator(IReadRepository<JobDescription> repository)
    {
        RuleFor(p => p.Rank)
            .NotEmpty()
        .GreaterThan(0);

        RuleFor(p => p.Number)
            .NotEmpty()
            .GreaterThan(0)
            .MustAsync(async (number, ct) =>
                await repository.FirstOrDefaultAsync(new JobDescriptionByNumberSpec(number), ct) is null)
            .WithMessage((_, number) =>
                $"Job Description Number {number} already exists.");

        RuleFor(p => p.Department)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(p => p.ReportsTo)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (name, ct) =>
                await repository.FirstOrDefaultAsync(new JobDescriptionByNameSpec(name), ct) is null)
            .WithMessage((_, name) =>
                $"Job Description Name {name} already exists.");

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class JobDescriptionCreateRequestHandler : IRequestHandler<JobDescriptionCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<JobDescription> _repository;
    private readonly IFileStorageService _file;

    public JobDescriptionCreateRequestHandler(
        IRepositoryWithEvents<JobDescription> repository,
        IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(JobDescriptionCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<JobDescription>(request.Image, FileType.Image, cancellationToken);

        var jobDescription = new JobDescription(request.Rank, request.Number, request.Department, request.ReportsTo, request.Name, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(jobDescription, cancellationToken);

        return jobDescription.Id;
    }
}