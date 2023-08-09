using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public double CollectorId { get; set; } = default!;
    public string Collector { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public DateTime TransactionDate { get; set; }
    public DateTime ReportDate { get; set; }
    public string AccountNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string OrNumber { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class RemoteCollectionUpdateRequestValidator : CustomValidator<RemoteCollectionUpdateRequest>
{
    public RemoteCollectionUpdateRequestValidator(IReadRepository<RemoteCollection> repository, IStringLocalizer<RemoteCollectionUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.Collector)
            .NotEmpty()
            .MaximumLength(32);

        RuleFor(p => p.Reference)
            .NotEmpty()
            .MaximumLength(16)
            .MustAsync(async (RemoteCollection, reference, ct) =>
                    await repository.FirstOrDefaultAsync(new RemoteCollectionByReferenceSpec(reference), ct)
                        is not { } existingRemoteCollection || existingRemoteCollection.Id == RemoteCollection.Id)
                .WithMessage((_, reference) => string.Format(localizer["remoteCollection already exists."], reference));

        RuleFor(p => p.Amount)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class RemoteCollectionUpdateRequestHandler : IRequestHandler<RemoteCollectionUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<RemoteCollection> _repository;
    private readonly IStringLocalizer<RemoteCollectionUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public RemoteCollectionUpdateRequestHandler(IRepositoryWithEvents<RemoteCollection> repository, IStringLocalizer<RemoteCollectionUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(RemoteCollectionUpdateRequest request, CancellationToken cancellationToken)
    {
        var remoteCollection = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = remoteCollection ?? throw new NotFoundException($"remoteCollection {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = remoteCollection.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            remoteCollection = remoteCollection.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<RemoteCollection>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedRemoteCollection = remoteCollection.Update(request.OrNumber, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedRemoteCollection, cancellationToken);

        return request.Id;
    }
}