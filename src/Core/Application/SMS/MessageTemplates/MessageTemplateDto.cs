namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateDto : DtoExtension, IDto
{
    public string TemplateType { get; set; } = default!;
    public string MessageType { get; set; } = default!;
    public bool IsAPI { get; set; } = default!;
    public bool IsSent { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string Recipients { get; set; } = default!;
    public DateTime Schedule { get; set; } = default!;
}