namespace ZANECO.API.Application.SMS.MessageLogs;

public class MessageLogDto : IDto
{
    public int Id { get; set; }
    public DateTime? SendTime { get; set; }
    public string? MessageFrom { get; set; }
    public string MessageTo { get; set; } = default!;
    public string MessageType { get; set; } = default!;
    public string MessageText { get; set; } = default!;
    public int StatusCode { get; set; }
    public string? StatusText { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}