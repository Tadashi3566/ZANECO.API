namespace ZANECO.API.Domain.ISD;

public class Member : AuditableEntity, IAggregateRoot
{
    public double IncrementId { get; private set; }
    public double ApplicationId { get; private set; }
    public string Name { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string District { get; private set; } = default!;
    public string Municipality { get; private set; } = default!;
    public string Barangay { get; private set; } = default!;
    public string Gender { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public DateTime? BirthDate { get; private set; }
    public DateTime? ApplicationDate { get; private set; }
    public DateTime? MembershipDate { get; private set; }
    public string? ImagePath { get; private set; }

    public Member(double incrementId, double applicationId, string name, string address, string district, string municipality, string barangay, string gender, string phoneNumber, DateTime? birthDate, DateTime? applicationDate, DateTime? membershipDate, string? description, string? notes, string? imagePath)
    {
        IncrementId = incrementId;
        ApplicationId = applicationId;
        Name = name.Trim().ToUpper();
        Address = address.Trim().ToUpper();
        District = district;
        Municipality = municipality;
        Barangay = barangay;
        Gender = gender;
        PhoneNumber = phoneNumber.Trim();
        BirthDate = birthDate;
        ApplicationDate = applicationDate;
        MembershipDate = membershipDate;
        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
    }

    public Member Update(string name, string address, string district, string municipality, string barangay, string gender, string phoneNumber, DateTime? birthDate, DateTime? applicationDate, DateTime? membershipDate, string? description, string? notes, string? imagePath)
    {
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (address is not null && !Address.Equals(address)) Address = address.Trim().ToUpper();
        if (district is not null && !District.Equals(district)) District = district.Trim().ToUpper();
        if (municipality is not null && !Municipality.Equals(municipality)) Municipality = municipality.Trim().ToUpper();
        if (barangay is not null && !Barangay.Equals(barangay)) Barangay = barangay.Trim().ToUpper();
        if (gender is not null && !Gender.Equals(gender)) Gender = gender;
        if (phoneNumber is not null && !PhoneNumber.Equals(phoneNumber)) PhoneNumber = phoneNumber.Trim();

        if (birthDate is not null && !BirthDate.Equals(birthDate)) BirthDate = birthDate;
        if (applicationDate is not null && !ApplicationDate.Equals(applicationDate)) ApplicationDate = applicationDate;
        if (membershipDate is not null && !MembershipDate.Equals(applicationDate)) MembershipDate = membershipDate;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;

        return this;
    }

    public Member ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}