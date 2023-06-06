namespace ZANECO.API.Domain.AGMA;

public class Prize : AuditableEntity, IAggregateRoot
{
    public virtual Raffle Raffle { get; private set; } = default!;
    public DefaultIdType RaffleId { get; private set; } = default!;
    public string RaffleName { get; private set; } = default!;

    public string PrizeType { get; private set; } = default!;
    public int Winners { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public string? ImagePath { get; private set; }

    public Prize(DefaultIdType raffleId, string raffleName, string prizeType, int winners, string name, string? description, string? notes, string? imagePath)
    {
        RaffleId = raffleId;
        RaffleName = raffleName;

        PrizeType = prizeType;
        Winners = winners;
        Name = name.Trim();

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public Prize Update(string raffleName, string prizeType, int winners, string name, string? description, string? notes, string? imagePath)
    {
        if (raffleName is not null && !RaffleName.Equals(raffleName)) RaffleName = raffleName.Trim();

        if (prizeType is not null && !PrizeType.Equals(prizeType)) PrizeType = prizeType.Trim();
        if (!Winners.Equals(winners)) Winners = winners;
        if (name is not null && !Name.Equals(name)) Name = name.Trim();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;

        return this;
    }

    public Prize ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}