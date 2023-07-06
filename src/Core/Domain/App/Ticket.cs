namespace ZANECO.API.Domain.App;

public class Ticket : AuditableEntityWithApproval<DefaultIdType>, IAggregateRoot
{
    public Ticket()
    {
    }

    public DefaultIdType GroupId { get; private set; }
    public virtual Group Group { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Impact { get; private set; }
    public string? Urgency { get; private set; }
    public string? Priority { get; private set; }
    public string? RequestedBy { get; private set; }
    public string? RequestThrough { get; private set; }
    public string? Reference { get; private set; }
    public string? AssignedTo { get; private set; }
    public DefaultIdType? OpenedBy { get; private set; }
    public DateTime? OpenedOn { get; private set; }
    public DefaultIdType? SuspendedBy { get; private set; }
    public DateTime? SuspendedOn { get; private set; }
    public DefaultIdType? ClosedBy { get; private set; }
    public DateTime? ClosedOn { get; private set; }
    public string? ImagePath { get; private set; }

    public Ticket(DefaultIdType groupID, string name, string? description, string? notes, string? imagePath)
    {
        GroupId = groupID;
        Name = name.Trim().ToUpper();
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
    }

    public Ticket Update(DefaultIdType? groupID, string name, string? description, string? notes, string? impact, string? urgency, string? priority, string? requestedBy, string? requestThrough, string? reference, string? assignedTo, string? imagePath)
    {
        if (groupID.HasValue && groupID.Value != DefaultIdType.Empty && !GroupId.Equals(groupID.Value)) GroupId = groupID.Value;
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (impact is not null && !Impact!.Equals(impact)) Impact = impact;
        if (urgency is not null && !Urgency!.Equals(urgency)) Urgency = urgency;
        if (priority is not null && !Priority!.Equals(priority)) Priority = priority;

        if (requestedBy is not null && !RequestedBy!.Equals(requestedBy)) RequestedBy = requestedBy;
        if (requestThrough is not null && !RequestThrough!.Equals(requestThrough)) RequestThrough = requestThrough;
        if (assignedTo is not null && !AssignedTo!.Equals(assignedTo)) AssignedTo = assignedTo;
        if (reference is not null && !Reference!.Equals(reference)) Reference = reference;

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;

        return this;
    }

    public Ticket Assign(string impact, string urgency, string priority, string assignedTo, string reference)
    {
        if (Impact?.Equals(impact) is false) Impact = impact;
        if (Urgency?.Equals(urgency) is false) Urgency = urgency;
        if (Priority?.Equals(priority) is false) Priority = priority;
        if (AssignedTo?.Equals(assignedTo) is false) AssignedTo = assignedTo;
        if (reference is not null && !Reference!.Equals(reference)) Reference = reference;
        Status = "ASSIGNED";
        return this;
    }

    public Ticket Open(DefaultIdType openedBy)
    {
        if (OpenedBy?.Equals(openedBy) is false) OpenedBy = openedBy;
        OpenedOn = DateTime.Now;
        Status = "OPENED";
        return this;
    }

    public Ticket Suspend(DefaultIdType suspendedBy)
    {
        if (SuspendedBy?.Equals(suspendedBy) is false) SuspendedBy = suspendedBy;
        OpenedOn = DateTime.Now;
        Status = "SUSPENDED";
        return this;
    }

    public Ticket Close(DefaultIdType closedBy)
    {
        if (ClosedBy?.Equals(closedBy) is false) ClosedBy = closedBy;
        ClosedOn = DateTime.Now;
        Status = "CLOSED";
        return this;
    }

    public Ticket Approve(DefaultIdType approvedBy)
    {
        if (ApprovedBy?.Equals(approvedBy) is false) ApprovedBy = approvedBy;
        ApprovedOn = DateTime.Now;
        Status = "APPROVED";
        return this;
    }

    public Ticket ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}