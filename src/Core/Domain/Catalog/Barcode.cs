namespace ZANECO.API.Domain.Catalog;

public class Barcode : AuditableEntity, IAggregateRoot
{
    public Barcode()
    {
    }

    public virtual Product Product { get; private set; } = default!;
    public DefaultIdType ProductId { get; private set; }
    public string Code { get; private set; } = default!;

    public string Specification { get; private set; } = default!;
    public string UnitOfMeasurement { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public Barcode(DefaultIdType productId, string code, string name, string specification, string unitOfMeasurement, string? description = null, string? notes = null, string? imagePath = null)
    {
        ProductId = productId;
        Code = code.Trim();

        Name = name.Trim().ToUpper();
        Specification = specification.Trim();
        UnitOfMeasurement = unitOfMeasurement.Trim();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Barcode Update(string code, string name, string specification, string unitOfMeasurement, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (code is not null && !Code.Equals(code)) Code = code;
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (specification is not null && !Specification.Equals(specification)) Specification = specification;
        if (unitOfMeasurement is not null && !UnitOfMeasurement.Equals(unitOfMeasurement)) UnitOfMeasurement = unitOfMeasurement;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Barcode ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}