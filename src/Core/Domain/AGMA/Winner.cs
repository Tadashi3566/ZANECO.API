namespace ZANECO.API.Domain.AGMA;

public class Winner : AuditableEntity, IAggregateRoot
{
    public virtual Raffle Raffle { get; private set; } = default!;
    public DefaultIdType RaffleId { get; private set; } = default!;
    public string RaffleName { get; private set; } = default!;

    public virtual Prize Prize { get; private set; } = default!;
    public DefaultIdType PrizeId { get; private set; } = default!;
    public string PrizeName { get; private set; } = default!;

    public string Name { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public Winner(DefaultIdType raffleId, string raffleName, DefaultIdType prizeId, string prizeName, string name, string address, string? description, string? notes, string? imagePath)
    {
        RaffleId = raffleId;
        RaffleName = raffleName;

        PrizeId = prizeId;
        PrizeName = prizeName;

        Name = name.Trim();
        Address = address.Trim();

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public Winner Update(string raffleName, string prizeName, string name, string address, string? description, string? notes, string? imagePath)
    {
        if (raffleName is not null && !RaffleName.Equals(raffleName)) RaffleName = raffleName.Trim();
        if (prizeName is not null && !PrizeName.Equals(prizeName)) PrizeName = prizeName.Trim();

        if (name is not null && !Name.Equals(name)) Name = name.Trim();
        if (address is not null && !Address.Equals(address)) Address = address.Trim();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;

        return this;
    }

    public Winner ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}