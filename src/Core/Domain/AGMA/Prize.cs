namespace ZANECO.API.Domain.AGMA;

public class Prize : AuditableEntity, IAggregateRoot
{
    public virtual Raffle Raffle { get; private set; } = default!;
    public DefaultIdType RaffleId { get; private set; }
    public string RaffleName { get; private set; }
    public string PrizeType { get; private set; }
    public int Winners { get; private set; }

    public Prize(DefaultIdType raffleId, string raffleName, string prizeType, int winners, string name, string? description = null, string? notes = null, string? imagePath = null)
    {
        RaffleId = raffleId;
        RaffleName = raffleName;

        PrizeType = prizeType;
        Winners = winners;

        Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Prize Update(string raffleName, string prizeType, int winners, string name, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!RaffleName.Equals(raffleName)) RaffleName = raffleName.Trim();

        if (!PrizeType.Equals(prizeType)) PrizeType = prizeType.Trim();
        if (!Winners.Equals(winners)) Winners = winners;

        if (!Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Prize ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}