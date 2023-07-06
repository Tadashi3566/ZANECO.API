using System.ComponentModel.DataAnnotations.Schema;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Attendance : AuditableEntityWithApproval<DefaultIdType>, IAggregateRoot
{
    public Attendance()
    {
    }

    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; private set; } = default!;

    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;

    public virtual Schedule Schedule { get; private set; } = default!;
    public DefaultIdType ScheduleId { get; private set; }
    public string ScheduleName { get; private set; } = default!;

    public virtual ScheduleDetail ScheduleDetail { get; private set; } = default!;
    public DefaultIdType ScheduleDetailId { get; private set; }
    public string ScheduleDetailDay { get; private set; } = default!;
    public int ScheduleHours { get; private set; }

    public virtual Payroll Payroll { get; private set; } = default!;
    public DefaultIdType? PayrollId { get; private set; }
    public string? PayrollName { get; private set; }

    public string DayType { get; private set; } = default!;
    public DateTime AttendanceDate { get; private set; }

    public bool IsOvertime { get; private set; }

    public DateTime ScheduleTimeIn1 { get; private set; }
    public DateTime ScheduleTimeOut1 { get; private set; }
    public DateTime ScheduleTimeIn2 { get; private set; }
    public DateTime ScheduleTimeOut2 { get; private set; }

    public DateTime? ActualTimeIn1 { get; private set; }
    public DateTime? ActualTimeOut1 { get; private set; }
    public DateTime? ActualTimeIn2 { get; private set; }
    public DateTime? ActualTimeOut2 { get; private set; }

    public int LateMinutes { get; private set; }
    public int UnderTimeMinutes { get; private set; }
    public double TotalHours { get; private set; }
    public double PaidHours { get; private set; }
    public bool IsPaid { get; private set; }

    public string? ImagePathIn1 { get; private set; }
    public string? ImagePathOut1 { get; private set; }
    public string? ImagePathIn2 { get; private set; }
    public string? ImagePathOut2 { get; private set; }

    public Attendance(DefaultIdType employeeId, string employeeName, DefaultIdType scheduleId, string scheduleName, string dayType, DateTime attendanceDate, DefaultIdType scheduleDetailId, string scheduleDetailDay, int scheduleHours, DateTime scheduleTimeIn1, DateTime scheduleTimeOut1, DateTime scheduleTimeIn2, DateTime scheduleTimeOut2, string status, string? description = "", string? notes = "")
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        ScheduleId = scheduleId;
        ScheduleName = scheduleName;

        DayType = dayType;
        AttendanceDate = attendanceDate;
        ScheduleDetailId = scheduleDetailId;
        ScheduleDetailDay = scheduleDetailDay;
        ScheduleHours = scheduleHours;

        ScheduleTimeIn1 = scheduleTimeIn1;
        ScheduleTimeOut1 = scheduleTimeOut1;
        ScheduleTimeIn2 = scheduleTimeIn2;
        ScheduleTimeOut2 = scheduleTimeOut2;

        LateMinutes = 0;
        UnderTimeMinutes = 0;
        TotalHours = 0;
        PaidHours = 0;

        Status = status;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public Attendance Calculate(bool isOvertime, int lateMinutes, int underTimeMinutes, double totalHours) //DateTime? timeOutDate,
    {
        if (!IsOvertime.Equals(isOvertime)) IsOvertime = isOvertime;

        if (!LateMinutes.Equals(lateMinutes)) LateMinutes = lateMinutes;
        if (!UnderTimeMinutes.Equals(underTimeMinutes)) UnderTimeMinutes = underTimeMinutes;
        if (!TotalHours.Equals(totalHours)) TotalHours = totalHours;

        if (DayType.Equals("ON-DUTY"))
        {
            if (totalHours > ScheduleHours)
            {
                PaidHours = ScheduleHours;
            }
        }

        return this;
    }

    public Attendance Update(string dayType, DateTime scheduleTimeIn1, DateTime scheduleTimeOut1, DateTime scheduleTimeIn2, DateTime scheduleTimeOut2, DateTime? actualTimeIn1, DateTime? actualTimeOut1, DateTime? actualTimeIn2, DateTime? actualTimeOut2, bool isOvertime, double paidHours, string status, string? description = "", string? notes = "") //, DateTime? timeOutDate
    {
        if (!DayType.Equals(dayType)) DayType = dayType;

        if (!ScheduleTimeIn1.Equals(scheduleTimeIn1)) ScheduleTimeIn1 = scheduleTimeIn1;
        if (!ScheduleTimeOut1.Equals(scheduleTimeOut1)) ScheduleTimeOut1 = scheduleTimeOut1;
        if (!ScheduleTimeIn2.Equals(scheduleTimeIn2)) ScheduleTimeIn2 = scheduleTimeIn2;
        if (!ScheduleTimeOut2.Equals(scheduleTimeOut2)) ScheduleTimeOut2 = scheduleTimeOut2;

        if (!IsOvertime.Equals(isOvertime)) IsOvertime = isOvertime;

        if (!actualTimeIn1.Equals(default) && !ActualTimeIn1.Equals(actualTimeIn1)) ActualTimeIn1 = actualTimeIn1;
        if (!actualTimeOut1.Equals(default) && !ActualTimeOut1.Equals(actualTimeOut1)) ActualTimeOut1 = actualTimeOut1;
        if (!actualTimeIn2.Equals(default) && !ActualTimeIn2.Equals(actualTimeIn2)) ActualTimeIn2 = actualTimeIn2;
        if (!actualTimeOut2.Equals(default) && !ActualTimeOut2.Equals(actualTimeOut2)) ActualTimeOut2 = actualTimeOut2;

        if (!PaidHours.Equals(paidHours)) PaidHours = paidHours;

        if (status is not null && !Status!.Equals(status)) Status = status;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }

    public Attendance Reschedule(DateTime scheduleTimeIn1, DateTime scheduleTimeOut1, DateTime scheduleTimeIn2, DateTime scheduleTimeOut2, string? description = "", string? notes = "")
    {
        if (!ScheduleTimeIn1.Equals(scheduleTimeIn1)) ScheduleTimeIn1 = scheduleTimeIn1;
        if (!ScheduleTimeOut1.Equals(scheduleTimeOut1)) ScheduleTimeOut1 = scheduleTimeOut1;
        if (!ScheduleTimeIn2.Equals(scheduleTimeIn2)) ScheduleTimeIn2 = scheduleTimeIn2;
        if (!ScheduleTimeOut2.Equals(scheduleTimeOut2)) ScheduleTimeOut2 = scheduleTimeOut2;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }

    public Attendance UpdateEmployeeName(string employeeName)
    {
        if (!EmployeeName.Equals(employeeName)) EmployeeName = employeeName;

        return this;
    }

    public Attendance TimeIn1(DateTime? actualTimeIn1, string imagePathIn1)
    {
        if (actualTimeIn1 is not null && !ActualTimeIn1.Equals(actualTimeIn1)) ActualTimeIn1 = actualTimeIn1;
        ImagePathIn1 = imagePathIn1;

        return this;
    }

    public Attendance TimeOut1(DateTime? actualTimeOut1, string imagePathOut1)
    {
        if (actualTimeOut1 is not null && !ActualTimeOut1.Equals(actualTimeOut1)) ActualTimeOut1 = actualTimeOut1;
        ImagePathOut1 = imagePathOut1;

        return this;
    }

    public Attendance TimeIn2(DateTime? actualTimeIn2, string imagePathIn2)
    {
        if (actualTimeIn2 is not null && !ActualTimeIn2.Equals(actualTimeIn2)) ActualTimeIn2 = actualTimeIn2;
        ImagePathIn2 = imagePathIn2;

        return this;
    }

    public Attendance TimeOut2(DateTime? actualTimeOut2, string imagePathOut2)
    {
        if (actualTimeOut2 is not null && !ActualTimeOut2.Equals(actualTimeOut2)) ActualTimeOut2 = actualTimeOut2;
        ImagePathOut2 = imagePathOut2;

        return this;
    }

    public Attendance SetPayroll(Guid payrollId, string payrollName)
    {
        PayrollId = payrollId;
        PayrollName = payrollName;
        IsPaid = true;

        return this;
    }

    //public Attendance Send(Guid employeeId)
    //{
    //    Status = "PENDING";

    //    LastModifiedBy = employeeId;
    //    LastModifiedOn = DateTime.Now;

    //    RecommendedOn = null;
    //    ApprovedOn = null;

    //    return this;
    //}

    //public Attendance Cancel(Guid employeeId)
    //{
    //    Status = "CANCELLED";

    //    LastModifiedBy = employeeId;
    //    LastModifiedOn = DateTime.Now;

    //    RecommendedOn = null;
    //    ApprovedOn = null;

    //    return this;
    //}

    //public Attendance Recommend(Guid employeeId, string employeeName)
    //{
    //    Status = "RECOMMENDED";

    //    RecommendedBy = employeeId;
    //    RecommenderName = employeeName;
    //    RecommendedOn = DateTime.Now;

    //    return this;
    //}

    public Attendance Approve(Guid employeeId, string employeeName, string appointmentType, string description)
    {
        Status = "APPROVED";

        ApprovedBy = employeeId;
        ApproverName = employeeName;
        ApprovedOn = DateTime.Now;

        DayType = appointmentType;
        Description = description;

        return this;
    }

    //public Attendance Disapprove(Guid employeeId, string employeeName)
    //{
    //    Status = "DISAPPROVED";

    //    ApprovedBy = employeeId;
    //    ApproverName = employeeName;
    //    ApprovedOn = DateTime.Now;

    //    return this;
    //}
}