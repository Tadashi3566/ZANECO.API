namespace ZANECO.API.Domain.AGMA;

public class Winner : AuditableEntity, IAggregateRoot
{
    public virtual Raffle Raffle { get; private set; } = default!;
    public DefaultIdType RaffleId { get; private set; }
    public string RaffleName { get; private set; }

    public virtual Prize Prize { get; private set; } = default!;
    public DefaultIdType PrizeId { get; private set; }
    public string PrizeName { get; private set; }

    public string Name { get; private set; }
    public string Address { get; private set; }
    public string? ImagePath { get; private set; }

    public Winner(DefaultIdType raffleId, string raffleName, DefaultIdType prizeId, string prizeName, string name, string address, string? description, string? notes, string? imagePath)
    {
        RaffleId = raffleId;
        RaffleName = raffleName;

        PrizeId = prizeId;
        PrizeName = prizeName;

        Name = name.Trim();
        Address = address.Trim();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
    }

    public Winner Update(string raffleName, string prizeName, string name, string address, string? description, string? notes, string? imagePath)
    {
        if (!RaffleName.Equals(raffleName)) RaffleName = raffleName.Trim();
        if (!PrizeName.Equals(prizeName)) PrizeName = prizeName.Trim();

        if (!Name.Equals(name)) Name = name.Trim();
        if (!Address.Equals(address)) Address = address.Trim();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;

        return this;
    }

    public Winner ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}