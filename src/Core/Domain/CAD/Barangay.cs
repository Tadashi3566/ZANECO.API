using System.ComponentModel.DataAnnotations.Schema;

namespace ZANECO.API.Domain.CAD;

public class Barangay : AuditableEntity, IAggregateRoot
{
    public Barangay()
    {
    }

    [ForeignKey("AreaId")]
    public virtual Area Area { get; private set; } = default!;

    public DefaultIdType AreaId { get; private set; } = default!;
    public string AreaName { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public Barangay(DefaultIdType areaId, string areaName, string name, string? description = "", string? notes = "")
    {
        AreaId = areaId;
        AreaName = areaName;
        Name = name.Trim().ToUpper();
        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    }

    public Barangay Update(string areaName, string name, string? description = "", string? notes = "")
    {
        if (areaName is not null && AreaName != areaName) AreaName = areaName;
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        return this;
    }
}