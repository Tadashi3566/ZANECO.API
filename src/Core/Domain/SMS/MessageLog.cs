namespace ZANECO.API.Domain.SMS;

public class MessageLog : AuditableEntity<int>, IAggregateRoot
{
    public MessageLog()
    {
    }

    public string? Connector { get; private set; }
    public string? Gateway { get; private set; }
    public DateTime? SendTime { get; private set; }
    public DateTime? ReceiveTime { get; private set; }
    public int StatusCode { get; private set; }
    public string? StatusText { get; private set; }
    public string? MessageFrom { get; private set; }
    public string MessageTo { get; private set; } = default!;
    public string MessageType { get; private set; } = default!;
    public string MessageText { get; private set; } = default!;
    public string? MessageHash { get; private set; }
    public string? MessageGuid { get; private set; }
    public string? MessageInfo { get; private set; }
    public string? MessageId { get; private set; }
    public string? ErrorCode { get; private set; }
    public string? ErrorText { get; private set; }
    public string? UserId { get; private set; }
    public string? UserInfo { get; private set; }
    public int MessageParts { get; private set; }
    public string? MessagePDU { get; private set; }

    public MessageLog(string connector, string gateway, string messageType, string messageFrom, string messageTo, string messageText, string messageGuid, int messageParts, int statusCode, string statusText, string errorCode, string errorText, string messageHash, string? description = "", string? notes = "")
    {
        Connector = connector;
        Gateway = gateway;

        SendTime = DateTime.Now;

        MessageType = messageType;
        MessageFrom = messageFrom;
        MessageTo = messageTo;
        MessageText = messageText;
        MessageHash = messageHash;
        MessageGuid = messageGuid;
        MessageParts = messageParts;

        StatusCode = statusCode;
        StatusText = statusText;

        ErrorCode = errorCode;
        ErrorText = errorText;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();
    }

    public MessageLog Update(string? description, string? notes)
    {
        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();

        return this;
    }

    // CREATE TABLE MessageLog(
    // Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    // SendTime DATETIME,
    // ReceiveTime DATETIME,
    // StatusCode INT,
    // StatusText VARCHAR(80),
    // MessageTo VARCHAR(80),
    // MessageFrom VARCHAR(80),
    // MessageText TEXT,
    // MessageType VARCHAR(80),
    // MessageGuid TEXT,
    // MessageInfo TEXT,
    // MessageId VARCHAR(80),
    // ErrorCode VARCHAR(80),
    // ErrorText TEXT,
    // Gateway VARCHAR(80),
    // MessageParts INT,
    // MessagePDU TEXT,
    // Connector VARCHAR(80),
    // UserId VARCHAR(80),
    // UserInfo TEXT) CHARACTER SET utf8mb4;
}