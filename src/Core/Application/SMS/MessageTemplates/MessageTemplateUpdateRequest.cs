using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateUpdateRequest : BaseRequestWithImage, IRequest<Guid>
{
    public string TemplateType { get; set; } = default!;
    public string MessageType { get; set; } = "sms.automatic";
    public bool IsAPI { get; set; } = true;
    public string Message { get; set; } = default!;
    public string Recipients { get; set; } = default!;
    public DateTime Schedule { get; set; } = DateTime.Today;
}

public class MessageTemplateUpdateRequestValidator : CustomValidator<MessageTemplateUpdateRequest>
{
    public MessageTemplateUpdateRequestValidator()
    {
        RuleFor(p => p.TemplateType)
             .NotEmpty()
             .MaximumLength(32);

        RuleFor(p => p.MessageType)
             .NotEmpty()
             .MaximumLength(32);

        RuleFor(p => p.Name)
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

public class MessageTemplateUpdateRequestHandler : IRequestHandler<MessageTemplateUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<MessageTemplate> _repository;
    private readonly IFileStorageService _file;

    public MessageTemplateUpdateRequestHandler(IRepositoryWithEvents<MessageTemplate> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(MessageTemplateUpdateRequest request, CancellationToken cancellationToken)
    {
        var messageTemplate = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = messageTemplate ?? throw new NotFoundException($"Message Template {request.Id} not found.");

        // Remove old file if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentFilePath = messageTemplate.ImagePath;
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentFilePath));
            }

            messageTemplate = messageTemplate.ClearFilePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<MessageTemplate>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedMessageTemplate = messageTemplate.Update(request.TemplateType, request.MessageType, request.IsAPI, request.Schedule, request.Name, request.Message, request.Recipients, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedMessageTemplate, cancellationToken);

        return request.Id;
    }
}