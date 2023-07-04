using ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime? DocumentDate { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public bool IsPublic { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Raw { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class DocumentUpdateRequestValidator : CustomValidator<DocumentUpdateRequest>
{
    public DocumentUpdateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<DocumentUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoEmployee.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["employee not found."], id));

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class DocumentUpdateRequestHandler : IRequestHandler<DocumentUpdateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Document> _repository;
    private readonly IStringLocalizer<DocumentUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;
    private readonly IDocumentOcrJob _ocrJob;
    private readonly IJobService _jobService;

    public DocumentUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Document> repository, IStringLocalizer<DocumentUpdateRequestHandler> localizer, IFileStorageService file, IDocumentOcrJob ocrJob, IJobService jobService) =>
        (_repoEmployee, _repository, _localizer, _file, _ocrJob, _jobService) = (repoEmployee, repository, localizer, file, ocrJob, jobService);

    public async Task<Guid> Handle(DocumentUpdateRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);

        var document = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = document ?? throw new NotFoundException(string.Format(_localizer["document not found."], request.Id));

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

        var updatedDocument = document.Update(employee!.FullName(), (DateTime)request.DocumentDate!, request.DocumentType, request.Reference, request.IsPublic, request.Name, request.Description, request.Notes, imagePath!);

        await _repository.UpdateAsync(updatedDocument, cancellationToken);

        _jobService.Enqueue(() => _ocrJob.Recognition(document.Id, cancellationToken));

        return request.Id;
    }
}