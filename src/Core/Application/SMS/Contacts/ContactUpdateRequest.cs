using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactUpdateRequest : RequestWithImageExtension<ContactUpdateRequest>, IRequest<Guid>
{
    public string ContactType { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
}

public class ContactUpdateRequestValidator : CustomValidator<ContactUpdateRequest>
{
    public ContactUpdateRequestValidator(IReadRepository<Contact> repoContactRepo, IStringLocalizer<ContactUpdateRequestValidator> localizer)
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
            .MustAsync(async (Contact, number, ct) =>
                    await repoContactRepo.FirstOrDefaultAsync(new ContactByNumberSpec(number), ct)
                        is not { } existingContact || existingContact.Id == Contact.Id)
                .WithMessage((_, code) => string.Format(localizer["Contact {0} already exists."], code));

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class ContactUpdateRequestHandler : IRequestHandler<ContactUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Contact> _repository;
    private readonly IStringLocalizer<ContactUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public ContactUpdateRequestHandler(IRepositoryWithEvents<Contact> repository, IStringLocalizer<ContactUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(ContactUpdateRequest request, CancellationToken cancellationToken)
    {
        var contact = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = contact ?? throw new NotFoundException($"Contact {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentImagePath = contact.ImagePath;
            if (!string.IsNullOrEmpty(currentImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentImagePath));
            }

            contact = contact.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Contact>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedContact = contact.Update(request.ContactType, request.Reference, ClassSms.FormatContactNumber(request.PhoneNumber), request.Name, request.Address, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedContact, cancellationToken);

        return request.Id;
    }
}