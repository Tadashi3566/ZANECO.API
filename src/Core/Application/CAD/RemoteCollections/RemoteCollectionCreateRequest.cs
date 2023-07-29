using System.Text.RegularExpressions;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionCreateRequest : IRequest<Guid>
{
    public double CollectorId { get; set; } = default!;
    public string Collector { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public string Date { get; set; } = default!;
    public string Time { get; set; } = default!;
    public DateTime ReportDate { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string OrNumber { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class RemoteCollectionCreateRequestValidator : CustomValidator<RemoteCollectionCreateRequest>
{
    public RemoteCollectionCreateRequestValidator(IReadRepository<RemoteCollection> RemoteCollectionRepo, IStringLocalizer<RemoteCollectionCreateRequestValidator> localizer)
    {
        RuleFor(p => p.Collector)
            .NotEmpty()
            .MaximumLength(32);

        RuleFor(p => p.Reference)
            .NotEmpty()
            .MaximumLength(16)
            .MustAsync(async (reference, ct) => await RemoteCollectionRepo.FirstOrDefaultAsync(new RemoteCollectionByReferenceSpec(reference), ct) is null)
            .WithMessage((_, reference) => string.Format(localizer["remoteCollection already exists"], reference));

        RuleFor(p => p.Amount)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class RemoteCollectionCreateRequestHandler : IRequestHandler<RemoteCollectionCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<RemoteCollection> _repository;
    private readonly IFileStorageService _file;

    public RemoteCollectionCreateRequestHandler(IRepositoryWithEvents<RemoteCollection> repository, IFileStorageService file) => (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(RemoteCollectionCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<RemoteCollection>(request.Image, FileType.Image, cancellationToken);

        string dateTimeString = $"{request.Date} {request.Time}";

        DateTime transactionDateTime = ConvertToDateTime(dateTimeString);

        string input = request.Name;
        string name = Regex.Replace(input, @"[^a-zA-Z0-9,\s\.]+", string.Empty);

        var remoteCollection = new RemoteCollection(request.CollectorId, request.Collector, request.Reference, transactionDateTime, request.ReportDate, request.AccountNumber, request.Amount, name, request.OrNumber, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(remoteCollection, cancellationToken);

        return remoteCollection.Id;
    }

    private static DateTime ConvertToDateTime(string strDateTime)
    {
        string sDateTime;
        string[] sDate = strDateTime.Split('/');
        sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
        return Convert.ToDateTime(sDateTime);
    }
}