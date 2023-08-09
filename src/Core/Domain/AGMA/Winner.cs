namespace ZANECO.API.Domain.AGMA;

public class Winner : AuditableEntity, IAggregateRoot
{
    public virtual Raffle Raffle { get; private set; } = default!;
    public DefaultIdType RaffleId { get; private set; }
    public string RaffleName { get; private set; }

    public virtual Prize Prize { get; private set; } = default!;
    public DefaultIdType PrizeId { get; private set; }
    public string PrizeName { get; private set; }

    public string Address { get; private set; }

    public Winner(DefaultIdType raffleId, string raffleName, DefaultIdType prizeId, string prizeName, string name, string address, string? description = null, string? notes = null, string? imagePath = null)
    {
        RaffleId = raffleId;
        RaffleName = raffleName;

        PrizeId = prizeId;
        PrizeName = prizeName;

        Name = name.Trim().ToUpper();
        Address = address.Trim();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Winner Update(string raffleName, string prizeName, string name, string address, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!RaffleName.Equals(raffleName)) RaffleName = raffleName.Trim();
        if (!PrizeName.Equals(prizeName)) PrizeName = prizeName.Trim();

        if (!Name.Equals(name)) Name = name.Trim().ToUpper();
        if (!Address.Equals(address)) Address = address.Trim();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Winner ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}