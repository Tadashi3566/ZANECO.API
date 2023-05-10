namespace ZANECO.API.Domain.SMS;

public class MessageTemplate : AuditableEntity, IAggregateRoot
{
    public MessageTemplate()
    {
    }

    public string TemplateType { get; private set; } = default!;
    public string MessageType { get; private set; } = default!;
    public bool IsAPI { get; private set; } = default!;
    public bool IsSent { get; private set; } = default!;
    public DateTime Schedule { get; private set; } = default!;
    public string Subject { get; private set; } = default!;
    public string Message { get; private set; } = default!;
    public string Recipients { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public MessageTemplate(string templateType, string messageType, bool isAPI, DateTime schedule, string subject, string message, string recipients, string description, string notes, string? imagePath)
    {
        TemplateType = templateType;
        MessageType = messageType;
        IsAPI = isAPI;
        Schedule = schedule;
        Subject = subject.Trim();
        Message = message.Trim();
        Recipients = recipients;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public MessageTemplate Update(string templateType, string messageType, bool isAPI, DateTime schedule, string subject, string message, string recpients, string description, string notes, string? imagePath)
    {
        if (templateType is not null && !TemplateType.Equals(templateType)) TemplateType = templateType;
        if (messageType is not null && !MessageType.Equals(messageType)) MessageType = messageType;
        if (!IsAPI.Equals(isAPI)) IsAPI = isAPI;
        if (schedule != default && !Schedule.Equals(schedule)) Schedule = schedule;
        if (subject is not null && !Subject.Equals(subject)) Subject = subject;
        if (message is not null && !Message.Equals(message)) Message = message;
        if (recpients is not null && !Recipients.Equals(recpients)) Recipients = recpients;

        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;

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