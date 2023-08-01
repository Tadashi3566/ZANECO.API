namespace ZANECO.API.Domain.Catalog;

public class Customer : AuditableEntity, IAggregateRoot
{
    public Customer()
    {
    }

    public string Code { get; private set; } = default!;

    public string Address { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public decimal Investment { get; private set; }
    public decimal Sales { get; private set; }
    public int Points { get; private set; }
    public string? ImagePath { get; private set; }

    public Customer(string code, string name, string address, string phoneNumber, decimal investment, decimal sales, int points, string? description = null, string? notes = null, string? imagePath = null)
    {
        Code = code;
        Name = name.Trim().ToUpper();
        Address = address;
        PhoneNumber = phoneNumber;

        Investment = investment;
        Sales = sales;
        Points = points;

        if (description is not null) Description = description!.Trim();
        if (notes is not null) Notes = notes!.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Customer Update(string code, string name, string address, string phoneNumber, decimal investment, decimal sales, int points, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!Code.Equals(code)) Code = code;
        if (!Name.Equals(name)) Name = name.Trim().ToUpper();
        if (!Address.Equals(address)) Address = address;
        if (!PhoneNumber.Equals(phoneNumber)) PhoneNumber = phoneNumber;

        if (!Investment.Equals(investment)) Investment = investment;
        if (!Sales.Equals(sales)) Sales = sales;
        if (!Points.Equals(points)) Points = points;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Customer ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}