namespace ZANECO.API.Application.SMS.MessageIns;

public class MessageInDto : IDto
{
    public int Id { get; set; }
    public DateTime? SendTime { get; set; }
    public DateTime? ReceiveTime { get; set; }
    public string MessageFrom { get; set; } = default!;
    public string MessageTo { get; set; } = string.Empty;
    public string MessageText { get; set; } = string.Empty;
    public string? MessageType { get; set; }
    public string? SMSC { get; set; }
    public string? MessagePDU { get; set; }
    public string? Gateway { get; set; }
    public string? UserId { get; set; }
    public int MessageParts { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadOn { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}