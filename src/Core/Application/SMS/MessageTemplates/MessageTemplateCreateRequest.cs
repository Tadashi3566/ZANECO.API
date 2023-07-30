using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateCreateRequest : IRequest<Guid>
{
    public string TemplateType { get; set; } = default!;
    public string MessageType { get; set; } = default!;
    public bool IsAPI { get; set; } = true;
    public string Subject { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string Recipients { get; set; } = default!;
    public DateTime Schedule { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class MessageTemplateCreateRequestValidator : CustomValidator<MessageTemplateCreateRequest>
{
    public MessageTemplateCreateRequestValidator()
    {
        RuleFor(p => p.TemplateType)
             .NotEmpty()
             .MaximumLength(maximumLength: 32);

        RuleFor(p => p.MessageType)
             .NotEmpty()
             .MaximumLength(32);

        RuleFor(p => p.Subject)
             .NotEmpty()
             .MaximumLength(1024);

        RuleFor(p => p.Message)
             .NotEmpty()
             .MaximumLength(1500);

        RuleFor(p => p.Recipients)
             .NotEmpty()
             .MinimumLength(9);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class MessageTemplateCreateRequestHandler : IRequestHandler<MessageTemplateCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<MessageTemplate> _repository;
    private readonly IFileStorageService _file;

    public MessageTemplateCreateRequestHandler(IRepositoryWithEvents<MessageTemplate> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(MessageTemplateCreateRequest request, CancellationToken cancellationToken)
    {
        if (request.Schedule < DateTime.Today)
            throw new ArgumentException("Message Schedule should be at least today.");

        string imagePath = await _file.UploadAsync<MessageTemplate>(request.Image, FileType.Image, cancellationToken);

        var messageTemplate = new MessageTemplate(request.TemplateType, request.MessageType, request.IsAPI, request.Schedule, request.Subject, request.Message, request.Recipients, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(messageTemplate, cancellationToken);

        return messageTemplate.Id;
    }
}