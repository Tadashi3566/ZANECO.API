namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class TimeLog : AuditableEntityWithApproval<DefaultIdType>, IAggregateRoot
{
    public TimeLog()
    {
    }

    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;
    public string LogType { get; private set; } = default!;
    public DateTime LogDate { get; private set; } = default!;
    public DateTime LogDateTime { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public TimeLog(DefaultIdType employeeId, string employeeName, string logType, DateTime logDate, string description, string notes, string imagePath)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        LogType = logType;
        LogDate = logDate;
        LogDateTime = DateTime.Now;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public TimeLog Update(string logType, DateTime logDate, DateTime logDateTime, string description, string notes, string imagePath)
    {
        if (logType is not null && !LogType.Equals(logType)) LogType = logType;
        if (!LogDate.Equals(logDate)) LogDate = logDate;
        if (logDateTime != default && !LogDateTime.Equals(logDateTime)) LogDateTime = logDateTime;

        if (description is not null && Description?.Equals(description) is false) Description = description.Trim();
        if (notes is not null && Notes?.Equals(notes) is false) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && ImagePath?.Equals(imagePath) is false) ImagePath = imagePath;

        return this;
    }

    public TimeLog ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}