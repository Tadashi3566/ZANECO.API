using ZANECO.API.Domain.Common;

namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Employee : AuditableEntity, IAggregateRoot
{
    // Basic
    public bool IsActive { get; private set; }
    public int Number { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string FirstName { get; private set; } = default!;
    public string MiddleName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string? Extension { get; private set; }
    public string Gender { get; private set; } = default!;
    public string? PhoneNumber { get; private set; }
    public string? Email { get; private set; }
    public string? CivilStatus { get; private set; }
    public string? Address { get; private set; }
    public string? BirthPlace { get; private set; }

    // Employment
    public DefaultIdType DesignationId { get; private set; }

    public DateTime BirthDate { get; private set; }
    public DateTime HireDate { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime RegularDate { get; private set; }

    public string? Area { get; private set; }
    public string? Department { get; private set; }
    public string? Division { get; private set; }
    public string? Section { get; private set; }
    public string? Position { get; private set; }

    // Benefits
    public string? Sss { get; private set; }
    public string? Phic { get; private set; }
    public string? Hdmf { get; private set; }
    public string? Tin { get; private set; }

    // Payroll
    public string? EmploymentType { get; private set; }

    public int SalaryNumber { get; private set; }
    public string? SalaryName { get; private set; }
    public decimal SalaryAmount { get; private set; }
    public string? RateType { get; private set; }
    public int DaysPerMonth { get; private set; } = default!;
    public decimal RatePerDay { get; private set; }
    public int HoursPerDay { get; private set; }
    public decimal RatePerHour { get; private set; }
    public string? TaxType { get; private set; }
    public string? PayType { get; private set; }
    public string? PayThrough { get; private set; }

    public DefaultIdType ScheduleId { get; private set; }
    public string? ScheduleName { get; private set; }

    // Emergency
    public string? EmergencyPerson { get; private set; }

    public string? EmergencyAddress { get; private set; }
    public string? EmergencyNumber { get; private set; }
    public string? EmergencyRelation { get; private set; }
    public string? FatherName { get; private set; }
    public string? MotherName { get; private set; }

    // Education
    public string? Education { get; private set; }

    public string? Course { get; private set; }
    public string? Award { get; private set; }

    // Others
    public string? BloodType { get; private set; }

    public string FullName()
    {
        return Extension?.Length > 0 ? $"{FirstName} {MiddleName} {LastName} {Extension}".Trim() : $"{FirstName} {MiddleName} {LastName}".Trim();
    }

    public string FullInitialName()
    {
        return MiddleName?.Length > 0 ? $"{FirstName} {MiddleName[..1]}. {LastName} {Extension}".Trim() : $"{FirstName} {LastName} {Extension}".Trim();
    }

    public string TitleFullInitialName()
    {
        return MiddleName?.Length > 0 ? $"{Title}. {FirstName} {MiddleName[..1]}. {LastName} {Extension}".Trim() : $"{Title} {FirstName} {LastName} {Extension}".Trim();
    }

    public string NameFull()
    {
        return Extension?.Length > 0 ? $"{LastName} {Extension}, {FirstName} {MiddleName}".Trim() : $"{LastName}, {FirstName} {MiddleName}".Trim();
    }

    public string NameFullInitial()
    {
        return MiddleName?.Length > 0 ? $"{LastName}, {FirstName} {MiddleName[..1]}.".Trim() : $"{LastName}, {FirstName}".Trim();
    }

    public Employee(int number, string title, string firstName, string? middleName, string lastName, string? extension, string gender, string? phoneNumber, string? email, string? civilStatus, string? address, DateTime birthDate, string? birthPlace, DateTime hireDate, DateTime regularDate, string? sss, string? phic, string? hdmf, string? tin, string? emergencyPerson, string? emergencyNumber, string? emergencyAddress, string? emergencyRelation, string? fatherName, string? motherName, string? education, string? course, string? award, string? bloodType, string? description = null, string? notes = null, string? imagePath = null)
    {
        Number = number;
        Title = title;
        FirstName = firstName.Trim().ToUpper();

        MiddleName = string.IsNullOrEmpty(middleName) ? default! : middleName!.Trim().ToUpper();

        LastName = lastName.Trim().ToUpper();

        Extension = extension is not null ? extension!.Trim().ToUpper() : null;

        Gender = gender;
        PhoneNumber = phoneNumber;
        Email = email;
        CivilStatus = civilStatus;
        Address = string.IsNullOrEmpty(address) ? default! : address!.Trim();
        BirthDate = birthDate;
        BirthPlace = string.IsNullOrEmpty(birthPlace) ? default! : birthPlace!.Trim();

        HireDate = hireDate;
        RegularDate = regularDate;

        Sss = string.IsNullOrEmpty(sss) ? default! : sss!.Trim();
        Phic = string.IsNullOrEmpty(phic) ? default! : phic!.Trim();
        Hdmf = string.IsNullOrEmpty(hdmf) ? default! : hdmf!.Trim();
        Tin = string.IsNullOrEmpty(tin) ? default! : tin!.Trim();

        EmergencyPerson = string.IsNullOrEmpty(emergencyPerson) ? default! : emergencyPerson!.Trim();
        EmergencyNumber = string.IsNullOrEmpty(emergencyNumber) ? default! : emergencyNumber!.Trim();
        EmergencyAddress = string.IsNullOrEmpty(emergencyAddress) ? default! : emergencyAddress!.Trim();
        EmergencyRelation = string.IsNullOrEmpty(emergencyRelation) ? default! : emergencyRelation!.Trim();
        FatherName = string.IsNullOrEmpty(fatherName) ? default! : fatherName!.Trim();
        MotherName = string.IsNullOrEmpty(motherName) ? default! : motherName!.Trim();

        Education = string.IsNullOrEmpty(education) ? default! : education!.Trim();
        Course = string.IsNullOrEmpty(course) ? default! : course!.Trim();
        Award = string.IsNullOrEmpty(award) ? default! : award!.Trim();

        BloodType = string.IsNullOrEmpty(bloodType) ? default! : bloodType!;

        Name = string.Empty;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Employee Update(int number, string title, string firstName, string? middleName, string lastName, string? extension, string gender, string? phoneNumber, string? email, string? civilStatus, string? address, DateTime birthDate, string? birthPlace, DateTime hireDate, DateTime regularDate, string? sss, string? phic, string? hdmf, string? tin, string? emergencyPerson, string? emergencyNumber, string? emergencyAddress, string? emergencyRelation, string? fatherName, string? motherName, string? education, string? course, string? award, string? bloodType, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!Number.Equals(number)) Number = number;
        if (title is not null && (Title?.Equals(title) != true)) Title = title;
        if (firstName is not null && (FirstName?.Equals(firstName) != true)) FirstName = firstName.Trim().ToUpper();
        if (middleName is not null && (MiddleName?.Equals(middleName) != true)) MiddleName = middleName.Trim().ToUpper();
        if (lastName is not null && (LastName?.Equals(lastName) != true)) LastName = lastName.Trim().ToUpper();
        if (extension is not null && (Extension?.Equals(extension) != true)) Extension = extension.Trim().ToUpper();

        if (gender is not null && (Gender?.Equals(gender) != true)) Gender = gender;
        if (phoneNumber is not null && (PhoneNumber?.Equals(phoneNumber) != true)) PhoneNumber = phoneNumber.Trim();
        if (email is not null && (Email?.Equals(email) != true)) Email = email;
        if (civilStatus is not null && (CivilStatus?.Equals(civilStatus) != true)) CivilStatus = civilStatus;
        if (address is not null && (Address?.Equals(address) != true)) Address = address.Trim();
        if (!BirthDate.Equals(birthDate)) BirthDate = birthDate!;
        if (birthPlace is not null && (BirthPlace?.Equals(birthPlace) != true)) BirthPlace = birthPlace.Trim();

        if (!HireDate.Equals(hireDate)) HireDate = hireDate;
        if (!RegularDate.Equals(regularDate)) RegularDate = regularDate;

        if (sss is not null && (Sss?.Equals(sss) != true)) Sss = sss.Trim().ToUpper();
        if (phic is not null && (Phic?.Equals(phic) != true)) Phic = phic.Trim().ToUpper();
        if (hdmf is not null && (Hdmf?.Equals(hdmf) != true)) Hdmf = hdmf.Trim().ToUpper();
        if (tin is not null && (Tin?.Equals(tin) != true)) Tin = tin.Trim().ToUpper();

        if (emergencyPerson is not null && (EmergencyPerson?.Equals(emergencyPerson) != true)) EmergencyPerson = emergencyPerson.Trim().ToUpper();
        if (emergencyNumber is not null && (EmergencyNumber?.Equals(emergencyNumber) != true)) EmergencyNumber = emergencyNumber.Trim().ToUpper();
        if (emergencyAddress is not null && (EmergencyAddress?.Equals(emergencyAddress) != true)) EmergencyAddress = emergencyAddress.Trim().ToUpper();
        if (emergencyRelation is not null && (EmergencyRelation?.Equals(emergencyRelation) != true)) EmergencyRelation = emergencyRelation.Trim().ToUpper();
        if (fatherName is not null && (FatherName?.Equals(fatherName) != true)) FatherName = fatherName.Trim().ToUpper();
        if (motherName is not null && (MotherName?.Equals(motherName) != true)) MotherName = motherName.Trim().ToUpper();

        if (education is not null && (Education?.Equals(education) != true)) Education = education.Trim().ToUpper();
        if (course is not null && (Course?.Equals(course) != true)) Course = course.Trim().ToUpper();
        if (award is not null && (Award?.Equals(award) != true)) Award = award.Trim().ToUpper();

        if (bloodType is not null && (BloodType?.Equals(bloodType) != true)) BloodType = bloodType.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
        return this;
    }

    public Employee Designation(DefaultIdType designationId, DateTime startDate, DateTime regularDate, string area, string department, string division, string section, string position, string employmentType, int salaryNumber, string salaryName, decimal salaryBase, decimal salaryStep, string rateType, int hoursPerDay, string taxType, string payType, DefaultIdType? scheduleId, string scheduleName, bool isActive)
    {
        if (!IsActive.Equals(isActive)) IsActive = isActive;

        if (!DesignationId.Equals(designationId)) DesignationId = designationId;

        if (!StartDate.Equals(startDate)) StartDate = startDate;

        if (area is not null && (Area?.Equals(area) != true)) Area = area;
        if (department is not null && (Department?.Equals(department) != true)) Department = department;
        if (division != null && (Division?.Equals(division) != true)) Division = division;
        if (section is not null && (Section?.Equals(section) != true)) Section = section;
        if (position is not null && (Position?.Equals(position) != true)) Position = position;

        if (employmentType is not null && (EmploymentType?.Equals(employmentType) != true)) EmploymentType = employmentType;
        if (!SalaryNumber.Equals(salaryNumber)) SalaryNumber = salaryNumber;
        if (salaryName is not null && (SalaryName?.Equals(salaryName) != true)) SalaryName = salaryName;
        if (payType is not null && (PayType?.Equals(payType) != true)) PayType = payType;
        if (rateType is not null && (RateType?.Equals(rateType) != true)) RateType = rateType;
        if (!HoursPerDay.Equals(hoursPerDay)) HoursPerDay = hoursPerDay;
        if (taxType is not null && (TaxType?.Equals(taxType) != true)) TaxType = taxType;

        if (scheduleId.HasValue && scheduleId.Value != DefaultIdType.Empty && !ScheduleId.Equals(scheduleId.Value)) ScheduleId = scheduleId.Value;
        if (scheduleName is not null && (ScheduleName?.Equals(scheduleName) != true)) ScheduleName = scheduleName;

        decimal ratePerDay = 0;
        decimal ratePerHour = 0;
        decimal salaryAmount = 0;

        if (regularDate.Equals(DateTime.MinValue))
            regularDate = startDate;

        int years = DateTimeFunctions.Years(regularDate, DateTime.Today);

        switch (rateType)
        {
            case "DAILY":
                //if (!RegularDate.Equals(regularDate)) RegularDate = regularDate;

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

                ratePerHour = ratePerDay / hoursPerDay;
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
                ratePerHour = salaryAmount / 22 / hoursPerDay;

                break;
        }

        if (!RatePerDay.Equals(ratePerDay)) RatePerDay = ratePerDay;
        if (!RatePerHour.Equals(ratePerHour)) RatePerHour = ratePerHour;
        if (!SalaryAmount.Equals(salaryAmount)) SalaryAmount = salaryAmount;

        return this;
    }

    public Employee Schedule(DefaultIdType scheduleId, string scheduleName)
    {
        if (!ScheduleId.Equals(scheduleId)) ScheduleId = scheduleId;
        if (scheduleName is not null && (ScheduleName?.Equals(scheduleName) != true)) ScheduleName = scheduleName;

        return this;
    }

    public Employee ClearImagePath()
    {
        ImagePath = null;

        return this;
    }
}