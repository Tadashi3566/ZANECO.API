namespace ZANECO.API.Domain.SMS;

public class MessageOut : AuditableEntity<int>, IAggregateRoot
{
    public MessageOut()
    {
    }

    public string? MessageFrom { get; private set; }
    public string MessageType { get; private set; } = default!;
    public string MessageTo { get; private set; } = default!;
    public string MessageText { get; private set; } = default!;
    public string? MessageGuid { get; private set; }
    public string? MessageInfo { get; private set; }
    public string? Gateway { get; private set; }
    public string? UserId { get; private set; }
    public string? UserInfo { get; private set; }
    public int Priority { get; private set; }
    public DateTime? Scheduled { get; private set; }
    public int ValidityPeriod { get; private set; }
    public string? TLVList { get; private set; }
    public bool IsSent { get; private set; }
    public bool IsRead { get; private set; }

    public MessageOut(string messageType, string messageTo, string messageText)
    {
        MessageType = messageType;
        MessageTo = messageTo;
        MessageText = messageText;
    }

    // public MessageOut Update(string messageTo, string messageText, string messageType)
    // {
    //    if (messageTo is not null && !MessageTo.Equals(messageTo)) MessageTo = messageTo;
    //    if (messageText is not null && !MessageText.Equals(messageText)) MessageText = messageText;
    //    if (messageType is not null && !MessageType.Equals(messageType)) MessageType = messageType;
    //    return this;
    // }

    // CREATE TABLE MessageOut(
    // Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    // MessageTo VARCHAR(80),
    // MessageFrom VARCHAR(80),
    // MessageText TEXT,
    // MessageType VARCHAR(80),
    // MessageGuid TEXT,
    // MessageInfo TEXT,
    // Gateway VARCHAR(80),
    // UserId VARCHAR(80),
    // UserInfo TEXT,
    // Priority INT,
    // Scheduled DATETIME,
    // ValidityPeriod INT,
    // TLVList TEXT,
    // IsSent TINYINT NOT NULL DEFAULT 0,
    // IsRead TINYINT NOT NULL DEFAULT 0) CHARACTER SET utf8mb4;
}