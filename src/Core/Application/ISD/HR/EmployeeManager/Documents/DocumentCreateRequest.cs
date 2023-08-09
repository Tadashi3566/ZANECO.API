using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentCreateRequest : RequestWithImageExtension<DocumentCreateRequest>, IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime DocumentDate { get; set; } = default!;
    public string DocumentType { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public bool IsPublic { get; set; } = default!;
}

public class CreateDocumentRequestValidator : CustomValidator<DocumentCreateRequest>
{
    public CreateDocumentRequestValidator()
    {
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

public class DocumentCreateRequestHandler : IRequestHandler<DocumentCreateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Document> _repository;
    private readonly IFileStorageService _file;
    private readonly IDocumentOcrJob _ocrJob;
    private readonly IJobService _jobService;

    public DocumentCreateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Document> repository, IFileStorageService file, IJobService jobService, IDocumentOcrJob ocrJob) =>
        (_repoEmployee, _repository, _file, _jobService, _ocrJob) = (repoEmployee, repository, file, jobService, ocrJob);

    public async Task<Guid> Handle(DocumentCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Document>(request.Image, FileType.Image, cancellationToken);

        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);

        var document = new Document(request.EmployeeId, employee!.FullName(), request.DocumentDate, request.DocumentType, request.Reference, request.IsPublic, request.Name, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(document, cancellationToken);

        //await _ocrJob.Recognition(document.Id, cancellationToken);
        //_jobService.Enqueue(() => _ocrJob.Recognition(document.Id, cancellationToken));
        _jobService.Schedule(() => _ocrJob.Recognition(document.Id, cancellationToken), TimeSpan.FromSeconds(10));

        return document.Id;
    }
}