using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentUpdateRequest : BaseRequestWithImage, IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime? DocumentDate { get; set; }
    public string DocumentType { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public bool IsPublic { get; set; } = default!;
    public string? Content { get; set; }
    public string? Raw { get; set; }
}

public class DocumentUpdateRequestValidator : CustomValidator<DocumentUpdateRequest>
{
    public DocumentUpdateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<DocumentUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoEmployee.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["employee not found."], id));

        RuleFor(p => p.DocumentType)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(p => p.Reference)
            .NotEmpty();

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class DocumentUpdateRequestHandler : IRequestHandler<DocumentUpdateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Document> _repository;
    private readonly IFileStorageService _file;
    private readonly IDocumentOcrJob _ocrJob;
    private readonly IJobService _jobService;

    public DocumentUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Document> repository, IFileStorageService file, IDocumentOcrJob ocrJob, IJobService jobService) =>
        (_repoEmployee, _repository, _file, _ocrJob, _jobService) = (repoEmployee, repository, file, ocrJob, jobService);

    public async Task<Guid> Handle(DocumentUpdateRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        var document = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = document ?? throw new NotFoundException($"document {request.Id} not found.");

        // Remove old file if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentFilePath = document.ImagePath;
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentFilePath));
            }

            document = document.ClearFilePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Document>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedDocument = document.Update(employee!.FullName(), (DateTime)request.DocumentDate!, request.DocumentType, request.Reference, request.IsPublic, request.Name, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedDocument, cancellationToken);

        //await _ocrJob.Recognition(document.Id, cancellationToken);
        //_jobService.Enqueue(() => _ocrJob.Recognition(document.Id, cancellationToken));
        _jobService.Schedule(() => _ocrJob.Recognition(document.Id, cancellationToken), TimeSpan.FromSeconds(10));

        return request.Id;
    }
}