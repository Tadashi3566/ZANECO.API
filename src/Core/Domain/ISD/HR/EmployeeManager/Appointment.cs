namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Appointment : AuditableEntityWithApproval<int>, IAggregateRoot
{
    public Appointment()
    {
    }

    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;
    public string AppointmentType { get; private set; } = default!;
    public string Subject { get; private set; } = default!;
    public DateTime StartDateTime { get; private set; } = default!;
    public DateTime EndDateTime { get; private set; } = default!;
    public string? Location { get; private set; }
    public int Hours { get; private set; } = default!;
    public bool IsAllDay { get; private set; } = default!;

    public bool IsReadonly { get; private set; } = default!;
    public bool IsBlock { get; private set; } = default!;
    public int? CalendarId { get; private set; }
    public int? RecurrenceID { get; private set; }
    public string? RecurrenceRule { get; private set; }
    public string? RecurrenceException { get; private set; }
    public string? CssClass { get; private set; }

    public string? ImagePath { get; private set; }

    public Appointment(DefaultIdType employeeId, string employeeName, string appointmentType, string subject, DateTime startDateTime, DateTime endDateTime, string? location, int hours, bool isAllDay, DefaultIdType? recommendedBy, DefaultIdType? approvedBy, string? description, string? notes, string imagePath) //, bool isReadOnly, bool isBlock, int? recurrenceId, string? recurrenceRule, string? recurrenceException, string? cssClass
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        AppointmentType = appointmentType.Trim().ToUpper();
        Subject = subject;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Location = location;
        Hours = hours;
        IsAllDay = isAllDay;

        //IsReadonly = false;
        //IsBlock = false;

        //RecurrenceID = recurrenceId;
        //RecurrenceRule = recurrenceRule;
        //RecurrenceException = recurrenceException;
        //CssClass = cssClass;

        RecommendedBy = recommendedBy;
        ApprovedBy = approvedBy;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        Status = "PENDING";

        ImagePath = imagePath;
    }

    //public Appointment Update(string appointmentType, string subject, DateTime startDateTime, DateTime endDateTime, string? location, int hours, bool isAllDay, bool isReadOnly, bool isBlock, int? recurrenceId, string? recurrenceRule, string? recurrenceException, string? cssClass, DefaultIdType? recommendedBy, DefaultIdType? approvedBy, string? description, string? notes, string imagePath)
    public Appointment Update(string appointmentType, string subject, DateTime startDateTime, DateTime endDateTime, string? location, int hours, bool isAllDay, DefaultIdType? recommendedBy, DefaultIdType? approvedBy, string? description, string? notes, string imagePath)
    {
        if (appointmentType is not null && !AppointmentType.Equals(appointmentType)) AppointmentType = appointmentType.Trim().ToUpper();
        if (subject is not null && !Subject.Equals(subject)) Subject = subject;
        if (startDateTime != default && !StartDateTime.Equals(startDateTime)) StartDateTime = startDateTime;
        if (endDateTime != default && !EndDateTime.Equals(endDateTime)) EndDateTime = endDateTime;
        if (location is not null && !Location!.Equals(location)) Location = location;
        if (!Hours.Equals(hours)) Hours = hours;
        if (!IsAllDay.Equals(isAllDay)) IsAllDay = isAllDay;

        //if (!IsReadonly.Equals(isReadOnly)) IsReadonly = isReadOnly;
        //if (!IsBlock.Equals(isBlock)) IsBlock = isBlock;
        //if (!RecurrenceID.Equals(recurrenceId)) RecurrenceID = recurrenceId;
        //if (recurrenceRule is not null && !RecurrenceRule!.Equals(recurrenceRule)) RecurrenceRule = recurrenceRule;
        //if (recurrenceException is not null && !RecurrenceException!.Equals(recurrenceException)) RecurrenceException = recurrenceException;
        //if (cssClass is not null && !CssClass!.Equals(cssClass)) CssClass = cssClass;

        if (recommendedBy is not null && !RecommendedBy.Equals(recommendedBy)) RecommendedBy = recommendedBy;
        if (approvedBy is not null && !ApprovedBy.Equals(approvedBy)) ApprovedBy = approvedBy;

        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();

        if (!(imagePath is null || ImagePath?.Equals(imagePath) is true)) ImagePath = imagePath;

        return this;
    }

    public Appointment Send(DefaultIdType userId)
    {
        LastModifiedBy = userId;
        LastModifiedOn = DateTime.Now;

        RecommendedOn = null;
        ApprovedOn = null;

        Status = "PENDING";

        return this;
    }

    public Appointment Cancel(DefaultIdType userId)
    {
        LastModifiedBy = userId;
        LastModifiedOn = DateTime.Now;

        RecommendedOn = null;
        ApprovedOn = null;

        Status = "CANCELLED";

        return this;
    }

    public Appointment Recommend(DefaultIdType recommender, string recommenderName)
    {
        if (RecommendedBy.Equals(recommender))
        {
            RecommendedOn = DateTime.Now;
            RecommenderName = recommenderName;
            Status = "RECOMMENDED";
        }

        return this;
    }

    public Appointment Approve(DefaultIdType approver, string approverName)
    {
        if (ApprovedBy.Equals(approver))
        {
            ApprovedOn = DateTime.Now;
            ApproverName = approverName;
            Status = "APPROVED";
        }

        return this;
    }

    public Appointment Disapprove(DefaultIdType approver, string approverName)
    {
        if (ApprovedBy.Equals(approver))
        {
            RecommendedOn = null;
            ApprovedOn = null;
            ApproverName = approverName;
            Status = "DISAPPROVED";
        }

        return this;
    }

    public Appointment ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}