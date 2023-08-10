using ZANECO.API.Application.SMS;
using ZANECO.API.Domain.ISD;

namespace ZANECO.API.Application.ISD.Members;

public class MemberCreateRequest : BaseRequestWithImage, IRequest<Guid>
{
    public int IncrementId { get; set; } = default!;
    public int ApplicationId { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Municipality { get; set; } = default!;
    public string Barangay { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public DateTime? ApplicationDate { get; set; }
    public DateTime? MembershipDate { get; set; }
}

public class CreateMemberRequestValidator : CustomValidator<MemberCreateRequest>
{
    public CreateMemberRequestValidator(IReadRepository<Member> memberRepo, IStringLocalizer<CreateMemberRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (name, ct) => await memberRepo.FirstOrDefaultAsync(new MemberByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["member already exists."], name));

        RuleFor(p => p.Address)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Gender)
            .NotEmpty();

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class MemberCreateRequestHandler : IRequestHandler<MemberCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Member> _repository;
    private readonly IFileStorageService _file;

    public MemberCreateRequestHandler(IRepositoryWithEvents<Member> repository, IFileStorageService file) => (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(MemberCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Member>(request.Image, FileType.Image, cancellationToken);

        var member = new Member(request.IncrementId, request.ApplicationId, request.Name, request.Address, request.District, request.Municipality, request.Barangay, request.Gender, ClassSms.FormatContactNumber(request.PhoneNumber), request.BirthDate, request.ApplicationDate, request.MembershipDate, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(member, cancellationToken);

        return member.Id;
    }
}