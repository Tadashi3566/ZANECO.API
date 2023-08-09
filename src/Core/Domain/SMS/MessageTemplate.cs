namespace ZANECO.API.Domain.SMS;

public class MessageTemplate : AuditableEntity, IAggregateRoot
{
    public MessageTemplate()
    {
    }

    public string TemplateType { get; private set; } = default!;
    public string MessageType { get; private set; } = default!;
    public bool IsAPI { get; private set; }
    public bool IsSent { get; private set; }
    public DateTime Schedule { get; private set; }
    public string Message { get; private set; } = default!;
    public string Recipients { get; private set; } = default!;

    public MessageTemplate(string templateType, string messageType, bool isAPI, DateTime schedule, string name, string message, string recipients, string? description = null, string? notes = null, string? imagePath = null)
    {
        TemplateType = templateType;
        MessageType = messageType;
        IsAPI = isAPI;
        Schedule = schedule;
        Name = name.Trim();
        Message = message.Trim();
        Recipients = recipients;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public MessageTemplate Update(string templateType, string messageType, bool isAPI, DateTime schedule, string name, string message, string recpients, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (templateType is not null && !TemplateType.Equals(templateType)) TemplateType = templateType;
        if (messageType is not null && !MessageType.Equals(messageType)) MessageType = messageType;
        if (!IsAPI.Equals(isAPI)) IsAPI = isAPI;
        if (schedule != default && !Schedule.Equals(schedule)) Schedule = schedule;
        if (name is not null && !Name.Equals(name)) Name = name;
        if (message is not null && !Message.Equals(message)) Message = message;
        if (recpients is not null && !Recipients.Equals(recpients)) Recipients = recpients;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public MessageTemplate Sent()
    {
        IsSent = true;

        return this;
    }

    public MessageTemplate ClearFilePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}