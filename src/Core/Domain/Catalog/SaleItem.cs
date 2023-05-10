namespace ZANECO.API.Domain.Catalog;

public class SaleItem : AuditableEntity, IAggregateRoot
{
    public SaleItem()
    {
    }

    public virtual Sale Sale { get; set; } = default!;
    public DefaultIdType SaleId { get; private set; }
    public virtual Product Product { get; set; } = default!;
    public DefaultIdType ProductId { get; private set; }
    public virtual Barcode Barcode { get; set; } = default!;
    public DefaultIdType BarcodeId { get; private set; }
    public virtual Discount Discount { get; private set; } = default!;
    public DefaultIdType DiscountId { get; private set; }

    public DateTime Date { get; private set; } = default!;
    public string Transaction { get; private set; } = default!;
    public int Items { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public decimal Gross { get; private set; } = default!;
    public decimal Vat { get; private set; } = default!;
    public decimal DiscountAmount { get; private set; } = default!;
    public decimal Net { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public SaleItem(DefaultIdType saleId, DefaultIdType productId, DefaultIdType barcodeId, DefaultIdType discountId, int items, string name, decimal gross, decimal vat, decimal discountAmount, decimal net, string? description, string? notes, string? imagePath)
    {
        var dt = DateTime.Now;

        SaleId = saleId;
        ProductId = productId;
        BarcodeId = barcodeId;
        DiscountId = discountId;

        Date = dt;
        Transaction = $"ITEM{dt:yyyyMMddHHmmsss}";
        Items = items;
        Name = name.Trim().ToUpper();

        Gross = gross;
        Vat = vat;
        DiscountAmount = discountAmount;
        Net = net;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public SaleItem Update(DefaultIdType barcodeId, DefaultIdType discountId, int items, string name, decimal gross, decimal vat, decimal discountAmount, decimal net, string? description, string? notes, string? imagePath)
    {
        if (!BarcodeId.Equals(barcodeId)) BarcodeId = barcodeId;
        if (!DiscountId.Equals(discountId)) DiscountId = discountId;

        if (!Items.Equals(items)) Items = items;
        if (!Name.Equals(name)) Name = name.Trim().ToUpper();

        if (!Gross.Equals(gross)) Gross = gross;
        if (!Vat.Equals(vat)) Vat = vat;
        if (!DiscountAmount.Equals(discountAmount)) DiscountAmount = discountAmount;
        if (!Net.Equals(net)) Net = net;

        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;

        return this;
    }

    public SaleItem ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}