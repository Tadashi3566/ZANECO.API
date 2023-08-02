namespace ZANECO.API.Domain.App;

public class TicketProgress : AuditableEntity, IAggregateRoot
{
    public TicketProgress()
    {
    }

    public DefaultIdType TicketId { get; private set; }
    public virtual Ticket Ticket { get; private set; } = default!;
    public string ProgressType { get; private set; } = default!;

    public string? ImagePath { get; private set; }

    public TicketProgress(DefaultIdType ticketID, string progressType, string name, string? description = null, string? notes = null, string? imagePath = null)
    {
        TicketId = ticketID;
        ProgressType = progressType;

        Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public TicketProgress Update(string progressType, string name, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!(progressType is null || ProgressType?.Equals(progressType) is true)) ProgressType = progressType;

        if (!(name is null || Name?.Equals(name) is true)) Name = name.Trim().ToUpper();

        if (!(description is null || Description?.Equals(description) is true)) Description = description.Trim();
        if (!(notes is null || Notes?.Equals(notes) is true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public TicketProgress ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}