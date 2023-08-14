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

    public DateTime Date { get; private set; }
    public string Transaction { get; private set; } = default!;
    public int Items { get; private set; }

    public decimal Gross { get; private set; }
    public decimal Vat { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal Net { get; private set; }

    public SaleItem(DefaultIdType saleId, DefaultIdType productId, DefaultIdType barcodeId, DefaultIdType discountId, int items, string name, decimal gross, decimal vat, decimal discountAmount, decimal net, string? description = null, string? notes = null, string? imagePath = null)
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

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public SaleItem Update(DefaultIdType barcodeId, DefaultIdType discountId, int items, string name, decimal gross, decimal vat, decimal discountAmount, decimal net, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!BarcodeId.Equals(barcodeId)) BarcodeId = barcodeId;
        if (!DiscountId.Equals(discountId)) DiscountId = discountId;

        if (!Items.Equals(items)) Items = items;
        if (!Name.Equals(name)) Name = name.Trim().ToUpper();

        if (!Gross.Equals(gross)) Gross = gross;
        if (!Vat.Equals(vat)) Vat = vat;
        if (!DiscountAmount.Equals(discountAmount)) DiscountAmount = discountAmount;
        if (!Net.Equals(net)) Net = net;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public SaleItem ClearImagePath()
    {
        ImagePath = null;

        return this;
    }
}