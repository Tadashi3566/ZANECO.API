using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactCreateRequest : IRequest<Guid>
{
    public string ContactType { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class ContactCreateRequestValidator : CustomValidator<ContactCreateRequest>
{
    public ContactCreateRequestValidator(IReadRepository<Contact> repoContact, IStringLocalizer<ContactCreateRequestValidator> localizer)
    {
        RuleFor(p => p.ContactType)
             .NotEmpty()
             .MaximumLength(16);

        RuleFor(p => p.Reference)
             .NotEmpty()
             .MinimumLength(4)
             .MaximumLength(10)
             .Unless(u => u.ContactType.Equals("EMPLOYEE") && u.Reference.Equals("0"));

        RuleFor(p => p.PhoneNumber)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(13)
            .MustAsync(async (code, ct) => await repoContact.FirstOrDefaultAsync(new ContactByNumberSpec(code), ct) is null)
            .WithMessage((_, code) => string.Format(localizer["Contact already exists."], code));

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class ContactCreateRequestHandler : IRequestHandler<ContactCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Contact> _repository;
    private readonly IFileStorageService _file;

    public ContactCreateRequestHandler(IRepositoryWithEvents<Contact> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(ContactCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Contact>(request.Image, FileType.Image, cancellationToken);

        var contact = new Contact(request.ContactType, request.Reference, ClassSms.FormatContactNumber(request.PhoneNumber), request.Name, request.Address, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(contact, cancellationToken);

        return contact.Id;
    }
}