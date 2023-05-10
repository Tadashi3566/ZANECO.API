namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string TemplateType { get; set; } = default!;
    public string MessageType { get; set; } = default!;
    public bool IsAPI { get; set; } = default!;
    public bool IsSent { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string Recipients { get; set; } = default!;
    public DateTime Schedule { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}