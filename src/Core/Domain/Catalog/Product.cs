namespace ZANECO.API.Domain.Catalog;

public class Product : AuditableEntity, IAggregateRoot
{
    public virtual Brand Brand { get; private set; } = default!;
    public DefaultIdType BrandId { get; private set; }
    public string SKU { get; private set; } = default!;
    public string Barcode { get; private set; } = default!;
    public string Name { get; private set; } = default!;
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

    public Product(DefaultIdType brandId, string sku, string barcode, string name, string specification, string unitOfMeasurement, bool isVatable, decimal rate, string? description, string? notes, string? imagePath)
    {
        BrandId = brandId;

        SKU = sku.Trim();
        Barcode = barcode.Trim();

        Name = name.Trim();
        Specification = specification.Trim();
        UnitOfMeasurement = unitOfMeasurement.Trim();

        IsVatable = isVatable;
        Rate = rate;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public Product Update(DefaultIdType? brandId, string sku, string barcode, string name, string specification, string unitOfMeasurement, bool? isVatable, decimal rate, string? description, string? notes, string? imagePath)
    {
        if (brandId.HasValue && brandId.Value != DefaultIdType.Empty && !BrandId.Equals(brandId.Value)) BrandId = brandId.Value;

        if (sku is not null && !SKU.Equals(sku)) SKU = sku;
        if (barcode is not null && !Barcode.Equals(barcode)) Barcode = barcode;

        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (specification is not null && !Specification.Equals(specification)) Specification = specification;
        if (unitOfMeasurement is not null && !UnitOfMeasurement.Equals(unitOfMeasurement)) UnitOfMeasurement = unitOfMeasurement;

        if (isVatable is not null && !IsVatable.Equals(isVatable)) IsVatable = isVatable;
        if (!Rate.Equals(rate)) Rate = rate;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;

        return this;
    }

    public Product ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}