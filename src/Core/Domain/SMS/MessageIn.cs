namespace ZANECO.API.Domain.SMS;

public class MessageIn : AuditableEntity<int>, IAggregateRoot
{
    public MessageIn()
    {
    }

    public DateTime? SendTime { get; private set; }
    public DateTime? ReceiveTime { get; private set; }
    public string MessageFrom { get; private set; } = default!;
    public string MessageTo { get; private set; } = default!;
    public string MessageText { get; private set; } = default!;
    public string? MessageType { get; private set; }
    public string? SMSC { get; private set; }
    public string? MessagePDU { get; private set; }
    public string? Gateway { get; private set; }
    public string? UserId { get; private set; }
    public int MessageParts { get; private set; }
    public bool IsRead { get; private set; }
    public DateTime? ReadOn { get; private set; }

    public MessageIn(DateTime sendTime, string messageFrom, string messageTo, string messageText, string messageType, string smsc, string messagePDU, string gateway, string userId, int messageParts, string? description = null, string? notes = null)
    {
        SendTime = sendTime;
        MessageFrom = messageFrom;
        MessageTo = messageTo;
        MessageText = messageText;
        MessageType = messageType;
        SMSC = smsc;
        MessagePDU = messagePDU;
        Gateway = gateway;
        UserId = userId;
        MessageParts = messageParts;

        Name = string.Empty;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public MessageIn Update(string description, string notes)
    {
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (!IsRead)
        {
            IsRead = true;
            ReadOn = DateTime.Now;
        }

        return this;
    }

    public MessageIn Read()
    {
        if (!IsRead)
        {
            IsRead = true;
            ReadOn = DateTime.Now;
        }

        return this;
    }

    // CREATE TABLE MessageIn(
    // Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    // SendTime DATETIME,
    // ReceiveTime DATETIME,
    // MessageFrom VARCHAR(80),
    // MessageTo VARCHAR(80),
    // SMSC VARCHAR(80),
    // MessageText TEXT,
    // MessageType VARCHAR(80),
    // MessageParts INT,
    // MessagePDU TEXT,
    // Gateway VARCHAR(80),
    // UserId VARCHAR(80)) CHARACTER SET utf8mb4;
}