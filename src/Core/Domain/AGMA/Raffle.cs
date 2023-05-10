namespace ZANECO.API.Domain.AGMA;

public class Raffle : AuditableEntity, IAggregateRoot
{
    public Raffle()
    {
    }

    public string Name { get; private set; } = default!;
    public DateTime RaffleDate { get; private set; }

    public string? ImagePath { get; private set; }

    public Raffle(string name, DateTime raffleDate, string description, string notes, string? imagePath)
    {
        Name = name.Trim();
        RaffleDate = raffleDate;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public Raffle Update(string name, DateTime raffleDate, string description, string notes, string? imagePath)
    {
        if (name is not null && !Name.Equals(name)) Name = name.Trim();
        if (!RaffleDate.Equals(raffleDate)) RaffleDate = raffleDate!;

        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;

        return this;
    }

    public Raffle ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}