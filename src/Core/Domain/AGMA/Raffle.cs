namespace ZANECO.API.Domain.AGMA;

public class Raffle : AuditableEntity, IAggregateRoot
{
    public Raffle()
    {
    }

    public DateTime RaffleDate { get; private set; }

    public Raffle(string name, DateTime raffleDate, string? description = null, string? notes = null, string? imagePath = null)
    {
        RaffleDate = raffleDate;

        Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Raffle Update(string name, DateTime raffleDate, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!RaffleDate.Equals(raffleDate)) RaffleDate = raffleDate!;

        if (!Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Raffle ClearImagePath()
    {
        ImagePath = null;

        return this;
    }
}