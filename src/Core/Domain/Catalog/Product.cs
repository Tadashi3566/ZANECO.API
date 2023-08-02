namespace ZANECO.API.Domain.Catalog;

public class Product : AuditableEntity, IAggregateRoot
{
    public virtual Brand Brand { get; private set; } = default!;
    public DefaultIdType BrandId { get; private set; }
    public string SKU { get; private set; } = default!;
    public string Barcode { get; private set; } = default!;

    public string Specification { get; private set; } = default!;
    public string UnitOfMeasurement { get; private set; } = default!;
    public bool? IsVatable { get; private set; }
    public decimal Rate { get; private set; }
    public string? ImagePath { get; private set; }

    public Product()
    {
        // Only needed for working with dapper (See GetProductViaDapperRequest)
        // If you're not using dapper it's better to remove this constructor.
    }

    public Product(DefaultIdType brandId, string sku, string barcode, string name, string specification, string unitOfMeasurement, bool isVatable, decimal rate, string? description = null, string? notes = null, string? imagePath = null)
    {
        BrandId = brandId;

        SKU = sku.Trim();
        Barcode = barcode.Trim();

        Name = name.Trim().ToUpper();
        Specification = specification.Trim();
        UnitOfMeasurement = unitOfMeasurement.Trim();

        IsVatable = isVatable;
        Rate = rate;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Product Update(DefaultIdType? brandId, string sku, string barcode, string name, string specification, string unitOfMeasurement, bool? isVatable, decimal rate, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (brandId.HasValue && brandId.Value != DefaultIdType.Empty && !BrandId.Equals(brandId.Value)) BrandId = brandId.Value;

        if (!SKU.Equals(sku)) SKU = sku;
        if (!Barcode.Equals(barcode)) Barcode = barcode;

        if (!Name.Equals(name)) Name = name.Trim().ToUpper();
        if (!Specification.Equals(specification)) Specification = specification;
        if (!UnitOfMeasurement.Equals(unitOfMeasurement)) UnitOfMeasurement = unitOfMeasurement;

        if (!IsVatable.Equals(isVatable)) IsVatable = isVatable;
        if (!Rate.Equals(rate)) Rate = rate;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Product ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}