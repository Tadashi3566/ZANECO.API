namespace ZANECO.API.Application.SMS.MessageIns;

public class MessageInDto : DtoExtension<int>, IDto
{
    public DateTime? SendTime { get; set; }
    public DateTime? ReceiveTime { get; set; }
    public string MessageFrom { get; set; } = default!;
    public string MessageTo { get; set; } = default!;
    public string MessageText { get; set; } = default!;
    public string? MessageType { get; set; }
    public string? SMSC { get; set; }
    public string? MessagePDU { get; set; }
    public string? Gateway { get; set; }
    public string? UserId { get; set; }
    public int MessageParts { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadOn { get; set; }
}