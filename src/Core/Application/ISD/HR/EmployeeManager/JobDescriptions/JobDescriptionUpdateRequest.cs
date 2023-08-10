using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionUpdateRequest : BaseRequestWithImage, IRequest<Guid>
{
    public int Rank { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string ReportsTo { get; set; } = default!;
}

public class JobDescriptionUpdateRequestValidator : CustomValidator<JobDescriptionUpdateRequest>
{
    public JobDescriptionUpdateRequestValidator(IReadRepository<JobDescription> repository)
    {
        RuleFor(p => p.Rank)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Number)
            .GreaterThan(0)
            .MustAsync(async (jobDescription, number, ct) => await repository.FirstOrDefaultAsync(new JobDescriptionByNumberSpec(number), ct)
                        is not { } existingJobDescription || existingJobDescription.Id == jobDescription.Id)
            .WithMessage((_, number) => $"Job Description Number {number} already exists.");

        RuleFor(p => p.Department)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(p => p.ReportsTo)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (jobDescription, name, ct) => await repository.FirstOrDefaultAsync(new JobDescriptionByNameSpec(name), ct)
                        is not { } existingJobDescription || existingJobDescription.Id == jobDescription.Id)
            .WithMessage((_, name) => $"Job Description Name {name} already exists.");

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class JobDescriptionUpdateRequestHandler : IRequestHandler<JobDescriptionUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<JobDescription> _repository;
    private readonly IFileStorageService _file;

    public JobDescriptionUpdateRequestHandler(
        IRepositoryWithEvents<JobDescription> repository,
        IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(JobDescriptionUpdateRequest request, CancellationToken cancellationToken)
    {
        var jobDescription = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = jobDescription ?? throw new NotFoundException($"Job Description {request.Id} not found.");

        // Remove old file if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentFilePath = jobDescription.ImagePath;
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentFilePath));
            }

            jobDescription = jobDescription.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<JobDescription>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedJobDescription = jobDescription.Update(request.Rank, request.Number, request.Department, request.ReportsTo, request.Name, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedJobDescription, cancellationToken);

        return request.Id;
    }
}