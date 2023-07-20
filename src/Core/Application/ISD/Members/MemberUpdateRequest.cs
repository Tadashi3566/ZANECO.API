using ZANECO.API.Application.SMS;
using ZANECO.API.Domain.ISD;

namespace ZANECO.API.Application.ISD.Members;

public class MemberUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public int IncrementId { get; set; } = default!;
    public int ApplicationId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Municipality { get; set; } = default!;
    public string Barangay { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime? BirthDate { get; set; } = default!;
    public DateTime? ApplicationDate { get; set; } = default!;
    public DateTime? MembershipDate { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class MemberUpdateRequestValidator : CustomValidator<MemberUpdateRequest>
{
    public MemberUpdateRequestValidator(IReadRepository<Member> MemberRepo, IStringLocalizer<MemberUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (member, name, ct) =>
                    await MemberRepo.FirstOrDefaultAsync(new MemberByNameSpec(name), ct)
                        is not { } existingMember || existingMember.Id == member.Id)
                .WithMessage((_, name) => string.Format(localizer["member already exists"], name));

        RuleFor(p => p.Address)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class MemberUpdateRequestHandler : IRequestHandler<MemberUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Member> _repository;
    private readonly IStringLocalizer<MemberUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public MemberUpdateRequestHandler(IRepositoryWithEvents<Member> repository, IStringLocalizer<MemberUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(MemberUpdateRequest request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = member ?? throw new NotFoundException($"member {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = member.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            member = member.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Member>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedMember = member.Update(request.Name, request.Address, request.District, request.Municipality, request.Barangay, request.Gender, ClassSms.FormatContactNumber(request.PhoneNumber), request.BirthDate, request.ApplicationDate, request.MembershipDate, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedMember, cancellationToken);

        return request.Id;
    }
}