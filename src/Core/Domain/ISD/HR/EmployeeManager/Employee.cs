using ZANECO.API.Domain.Common;

namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Employee : AuditableEntity, IAggregateRoot
{
    public Employee()
    {
    }

    // Basic
    public bool IsActive { get; private set; }

    public int Number { get; private set; }
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

    public string? ImagePath { get; private set; }

    public string FullName()
    {
        if (Extension?.Length > 0)
            return $"{FirstName} {MiddleName} {LastName} {Extension}".Trim();
        else
            return $"{FirstName} {MiddleName} {LastName}".Trim();
    }

    public string FullInitialName()
    {
        if (MiddleName?.Length > 0)
        {
            return $"{FirstName} {MiddleName[..1]}. {LastName} {Extension}".Trim();
        }
        else
        {
            return $"{FirstName} {LastName} {Extension}".Trim();
        }
    }

    public string NameFull()
    {
        if (Extension?.Length > 0)
        {
            return $"{LastName} {Extension}, {FirstName} {MiddleName}".Trim();
        }
        else
        {
            return $"{LastName}, {FirstName} {MiddleName}".Trim();
        }
    }

    public string NameFullInitial()
    {
        if (MiddleName?.Length > 0)
        {
            return $"{LastName}, {FirstName} {MiddleName[..1]}.".Trim();
        }
        else
        {
            return $"{LastName}, {FirstName}".Trim();
        }
    }

    public Employee(int number, string title, string firstName, string middleName, string lastName, string? extension, string gender, string phoneNumber, string email, string civilStatus, string address, DateTime birthDate, string birthPlace, DateTime hireDate, DateTime regularDate, string sss, string phic, string hdmf, string tin, string emergencyPerson, string emergencyNumber, string emergencyAddress, string emergencyRelation, string fatherName, string motherName, string education, string course, string award, string bloodType, string? description, string? notes, string? imagePath)
    {
        Number = number;
        Title = title;
        FirstName = firstName.Trim().ToUpper();

        if (string.IsNullOrEmpty(middleName))
            MiddleName = string.Empty;
        else
            MiddleName = middleName!.Trim().ToUpper();

        LastName = lastName.Trim().ToUpper();

        if (extension is not null)
            Extension = extension!.Trim().ToUpper();
        else
            Extension = null;

        Gender = gender;
        PhoneNumber = phoneNumber.Trim();
        Email = email;
        CivilStatus = civilStatus;
        Address = address.Trim();
        BirthDate = birthDate;
        BirthPlace = birthPlace.Trim();

        HireDate = hireDate;
        RegularDate = regularDate;

        Sss = sss.Trim().ToUpper();
        Phic = phic.Trim().ToUpper();
        Hdmf = hdmf.Trim().ToUpper();
        Tin = tin.Trim().ToUpper();

        EmergencyPerson = emergencyPerson.Trim().ToUpper();
        EmergencyNumber = emergencyNumber.Trim().ToUpper();
        EmergencyAddress = emergencyAddress.Trim().ToUpper();
        EmergencyRelation = emergencyRelation.Trim().ToUpper();
        FatherName = fatherName.Trim().ToUpper();
        MotherName = motherName.Trim().ToUpper();

        Education = education.Trim().ToUpper();
        Course = course.Trim().ToUpper();
        Award = award.Trim().ToUpper();

        BloodType = bloodType.Trim().ToUpper();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
    }

    public Employee Update(int number, string title, string firstName, string middleName, string lastName, string? extension, string gender, string phoneNumber, string email, string civilStatus, string address, DateTime birthDate, string birthPlace, DateTime hireDate, DateTime regularDate, string sss, string phic, string hdmf, string tin, string emergencyPerson, string emergencyNumber, string emergencyAddress, string emergencyRelation, string fatherName, string motherName, string education, string course, string award, string bloodType, string? description, string? notes, string? imagePath)
    {
        if (!Number.Equals(number)) Number = number;
        if (title is not null && (Title is null || !Title.Equals(title))) Title = title;
        if (firstName is not null && (FirstName is null || !FirstName.Equals(firstName))) FirstName = firstName.Trim().ToUpper();
        if (middleName is not null && (MiddleName is null || !MiddleName.Equals(middleName))) MiddleName = middleName.Trim().ToUpper();
        if (lastName is not null && (LastName is null || !LastName.Equals(lastName))) LastName = lastName.Trim().ToUpper();
        if (extension is not null && (Extension is null || !Extension.Equals(extension))) Extension = extension.Trim().ToUpper();

        if (gender is not null && (Gender is null || !Gender.Equals(gender))) Gender = gender;
        if (phoneNumber is not null && (PhoneNumber is null || !PhoneNumber.Equals(phoneNumber))) PhoneNumber = phoneNumber.Trim();
        if (email is not null && (Email is null || !Email.Equals(email))) Email = email;
        if (civilStatus is not null && (CivilStatus is null || !CivilStatus.Equals(civilStatus))) CivilStatus = civilStatus;
        if (address is not null && (Address is null || !Address.Equals(address))) Address = address.Trim();
        if (!BirthDate.Equals(birthDate)) BirthDate = birthDate!;
        if (birthPlace is not null && (BirthPlace is null || !BirthPlace.Equals(birthPlace))) BirthPlace = birthPlace.Trim();

        if (!HireDate.Equals(hireDate)) HireDate = hireDate;
        if (!RegularDate.Equals(regularDate)) RegularDate = regularDate;

        if (sss is not null && (Sss is null || !Sss.Equals(sss))) Sss = sss.Trim().ToUpper();
        if (phic is not null && (Phic is null || !Phic.Equals(phic))) Phic = phic.Trim().ToUpper();
        if (hdmf is not null && (Hdmf is null || !Hdmf.Equals(hdmf))) Hdmf = hdmf.Trim().ToUpper();
        if (tin is not null && (Tin is null || !Tin.Equals(tin))) Tin = tin.Trim().ToUpper();

        if (emergencyPerson is not null && (EmergencyPerson is null || !EmergencyPerson.Equals(emergencyPerson))) EmergencyPerson = emergencyPerson.Trim().ToUpper();
        if (emergencyNumber is not null && (EmergencyNumber is null || !EmergencyNumber.Equals(emergencyNumber))) EmergencyNumber = emergencyNumber.Trim().ToUpper();
        if (emergencyAddress is not null && (EmergencyAddress is null || !EmergencyAddress.Equals(emergencyAddress))) EmergencyAddress = emergencyAddress.Trim().ToUpper();
        if (emergencyRelation is not null && (EmergencyRelation is null || !EmergencyRelation.Equals(emergencyRelation))) EmergencyRelation = emergencyRelation.Trim().ToUpper();
        if (fatherName is not null && (FatherName is null || !FatherName.Equals(fatherName))) FatherName = fatherName.Trim().ToUpper();
        if (motherName is not null && (MotherName is null || !MotherName.Equals(motherName))) MotherName = motherName.Trim().ToUpper();

        if (education is not null && (Education is null || !Education.Equals(education))) Education = education.Trim().ToUpper();
        if (course is not null && (Course is null || !Course.Equals(course))) Course = course.Trim().ToUpper();
        if (award is not null && (Award is null || !Award.Equals(award))) Award = award.Trim().ToUpper();

        if (bloodType is not null && (BloodType is null || !BloodType.Equals(bloodType))) BloodType = bloodType.Trim().ToUpper();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
        return this;
    }

    public Employee Designation(DefaultIdType designationId, DateTime startDate, DateTime regularDate, string area, string department, string division, string section, string position, string employmentType, int salaryNumber, string salaryName, decimal salaryBase, decimal salaryStep, string rateType, int hoursPerDay, string taxType, string payType, DefaultIdType? scheduleId, string scheduleName, bool isActive)
    {
        if (!IsActive.Equals(isActive)) IsActive = isActive;

        if (!DesignationId.Equals(designationId)) DesignationId = designationId;

        if (!StartDate.Equals(startDate)) StartDate = startDate;

        if (area is not null && (Area is null || !Area.Equals(area))) Area = area;
        if (department is not null && (Department is null || !Department.Equals(department))) Department = department;
        if (division != null && (Division is null || !Division.Equals(division))) Division = division;
        if (section is not null && (Section is null || !Section.Equals(section))) Section = section;
        if (position is not null && (Position is null || !Position.Equals(position))) Position = position;

        if (employmentType is not null && (EmploymentType is null || !EmploymentType.Equals(employmentType))) EmploymentType = employmentType;
        if (!SalaryNumber.Equals(salaryNumber)) SalaryNumber = salaryNumber;
        if (salaryName is not null && (SalaryName is null || !SalaryName.Equals(salaryName))) SalaryName = salaryName;
        if (payType is not null && (PayType is null || !PayType.Equals(payType))) PayType = payType;
        if (rateType is not null && (RateType is null || !RateType.Equals(rateType))) RateType = rateType;
        if (!HoursPerDay.Equals(hoursPerDay)) HoursPerDay = hoursPerDay;
        if (taxType is not null && (TaxType is null || !TaxType.Equals(taxType))) TaxType = taxType;

        if (scheduleId.HasValue && scheduleId.Value != DefaultIdType.Empty && !ScheduleId.Equals(scheduleId.Value)) ScheduleId = scheduleId.Value;
        if (scheduleName is not null && (ScheduleName is null || !ScheduleName.Equals(scheduleName))) ScheduleName = scheduleName;

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
        if (scheduleName is not null && (ScheduleName is null || !ScheduleName.Equals(scheduleName))) ScheduleName = scheduleName;

        return this;
    }

    public Employee ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}