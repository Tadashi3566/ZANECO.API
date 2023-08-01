using System.ComponentModel.DataAnnotations.Schema;
using ZANECO.API.Domain.Common;

namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Designation : AuditableEntity, IAggregateRoot
{
    public Designation()
    {
    }

    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; private set; } = default!;

    public DefaultIdType EmployeeId { get; private set; }
    public int IdNumber { get; private set; }
    public string EmployeeName { get; private set; } = default!;

    public bool IsActive { get; private set; }

    public DefaultIdType ScheduleId { get; private set; }
    public string ScheduleName { get; private set; } = default!;

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Area { get; private set; } = default!;
    public string Department { get; private set; } = default!;
    public string Division { get; private set; } = default!;
    public string Section { get; private set; } = default!;
    public string Position { get; private set; } = default!;

    public string EmploymentType { get; private set; } = default!;
    public int SalaryNumber { get; private set; }
    public string SalaryName { get; private set; } = default!;
    public decimal SalaryAmount { get; private set; }
    public string RateType { get; private set; } = default!;
    public decimal RatePerDay { get; private set; }
    public int HoursPerDay { get; private set; } = 8;
    public decimal RatePerHour { get; private set; }
    public string TaxType { get; private set; } = default!;
    public string PayType { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public Designation(DefaultIdType employeeId, int idNumber, string employeeName, DateTime startDate, DateTime endDate, DateTime? regularDate, string area, string department, string division, string section, string position, string employmentType, int salaryNumber, string salaryName, decimal salaryBase, decimal salaryStep, decimal ratePerDay, string rateType, string taxType, string payType, DefaultIdType scheduleId, string scheduleName, string? description = null, string? notes = null, string? imagePath = null)
    {
        EmployeeId = employeeId;
        IdNumber = idNumber;
        EmployeeName = employeeName;

        IsActive = true;

        StartDate = startDate;
        EndDate = endDate;

        Area = area;
        Department = department;
        Division = division;
        Section = section;
        Position = position;

        EmploymentType = employmentType;
        SalaryNumber = salaryNumber;
        SalaryName = salaryName;
        PayType = payType;
        RateType = rateType;
        HoursPerDay = 8;
        TaxType = taxType;

        ScheduleId = scheduleId;
        ScheduleName = scheduleName;

        decimal ratePerHour = 0;
        decimal salaryAmount = 0;

        var dtRegularDate = regularDate ?? startDate!;

        int years = DateTimeFunctions.Years(dtRegularDate, DateTime.Today);

        switch (rateType)
        {
            case "DAILY":
                if (years < 2)
                {
                    ratePerDay = 351;
                }
                else
                {
                    ratePerDay = years switch
                    {
                        2 => 385,
                        3 => 402,
                        _ => 500,
                    };
                }

                ratePerHour = ratePerDay / 8;
                salaryAmount = ratePerDay;

                break;

            case "MONTHLY":
                ratePerDay = 0;

                int incrementYears = years / 5;

                if (incrementYears > 5)
                {
                    incrementYears = 5;
                }

                salaryAmount = (salaryStep * incrementYears) + salaryBase;
                ratePerHour = salaryAmount / 22 / 8;

                break;
        }

        RatePerDay = ratePerDay;
        RatePerHour = ratePerHour;
        SalaryAmount = salaryAmount;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Designation Update(string employeeName, DateTime startDate, DateTime endDate, DateTime regularDate, string area, string department, string division, string section, string position, string employmentType, int salaryNumber, string salaryName, decimal salaryBase, decimal salaryStep, decimal ratePerDay, string rateType, string taxType, string payType, DefaultIdType? scheduleId, string scheduleName, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!EmployeeName.Equals(employeeName)) EmployeeName = employeeName;

        if (!StartDate.Equals(startDate)) StartDate = startDate;
        if (!EndDate.Equals(endDate)) EndDate = endDate;

        if (!Area.Equals(area)) Area = area;
        if (!Department.Equals(department)) Department = department;
        if (!Division.Equals(division)) Division = division;
        if (!Section.Equals(section)) Section = section;
        if (!Position.Equals(position)) Position = position;

        if (!EmploymentType.Equals(employmentType)) EmploymentType = employmentType;
        if (!SalaryNumber.Equals(salaryNumber)) SalaryNumber = salaryNumber;
        if (!SalaryName.Equals(salaryName)) SalaryName = salaryName;
        if (!PayType.Equals(payType)) PayType = payType;
        if (!RateType.Equals(rateType)) RateType = rateType;
        if (!TaxType.Equals(taxType)) TaxType = taxType;

        if (scheduleId.HasValue && scheduleId.Value != DefaultIdType.Empty && !ScheduleId.Equals(scheduleId.Value)) ScheduleId = scheduleId.Value;
        if (!ScheduleName.Equals(scheduleName)) ScheduleName = scheduleName;

        decimal ratePerHour = 0;
        decimal salaryAmount = 0;

        if (regularDate.Equals(DateTime.MinValue))
            regularDate = startDate;

        if (endDate.Equals(DateTime.MinValue))
            endDate = DateTime.Today;

        int years = DateTimeFunctions.Years(regularDate!, endDate!);

        switch (rateType)
        {
            case "DAILY":
                if (years < 2)
                {
                    ratePerDay = 351;
                }
                else
                {
                    ratePerDay = years switch
                    {
                        2 => 385,
                        3 => 402,
                        _ => 500,
                    };
                }

                ratePerHour = ratePerDay / 8;
                salaryAmount = ratePerDay;

                break;

            case "MONTHLY":
                ratePerDay = 0;

                int incrementYears = years / 5;

                if (incrementYears > 5)
                {
                    incrementYears = 5;
                }

                salaryAmount = (salaryStep * incrementYears) + salaryBase;
                ratePerHour = salaryAmount / 22 / 8;

                break;
        }

        if (!RatePerDay.Equals(ratePerDay)) RatePerDay = ratePerDay;
        if (!RatePerHour.Equals(ratePerHour)) RatePerHour = ratePerHour;

        if (!SalaryAmount.Equals(salaryAmount)) SalaryAmount = salaryAmount;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
        return this;
    }

    public Designation Deactivate(DateTime endDate)
    {
        IsActive = false;

        if (!EndDate.Equals(endDate)) EndDate = endDate;

        return this;
    }

    public Designation Activate()
    {
        IsActive = true;

        return this;
    }

    public Designation ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}