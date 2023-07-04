﻿namespace ZANECO.API.Domain.AGMA;

public class Prize : AuditableEntity, IAggregateRoot
{
    public virtual Raffle Raffle { get; private set; } = default!;
    public DefaultIdType RaffleId { get; private set; }
    public string RaffleName { get; private set; }
    public string PrizeType { get; private set; }
    public int Winners { get; private set; }
    public string Name { get; private set; }

    public string? ImagePath { get; private set; }

    public Prize(DefaultIdType raffleId, string raffleName, string prizeType, int winners, string name, string? description, string? notes, string? imagePath)
    {
        RaffleId = raffleId;
        RaffleName = raffleName;

        PrizeType = prizeType;
        Winners = winners;
        Name = name.Trim();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
    }

    public Prize Update(string raffleName, string prizeType, int winners, string name, string? description, string? notes, string? imagePath)
    {
        if (!RaffleName.Equals(raffleName)) RaffleName = raffleName.Trim();

        if (!PrizeType.Equals(prizeType)) PrizeType = prizeType.Trim();
        if (!Winners.Equals(winners)) Winners = winners;
        if (!Name.Equals(name)) Name = name.Trim();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;

        return this;
    }

    public Prize ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}