using System.ComponentModel.DataAnnotations.Schema;

namespace ZANECO.API.Domain.CAD;

public class Route : AuditableEntity, IAggregateRoot
{
    public Route()
    {
    }

    [ForeignKey("AreaId")]
    public virtual Area Area { get; private set; } = default!;

    public DefaultIdType AreaId { get; private set; } = default!;
    public string AreaName { get; private set; } = default!;
    public int Number { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public Route(DefaultIdType areaId, string areaName, int number, string code, string name, string? description = "", string? notes = "")
    {
        AreaId = areaId;
        AreaName = areaName;
        Number = number;
        Code = code;
        Name = name.Trim().ToUpper();
        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();
    }

    public Route Update(string areaName, int number, string code, string name, string? description = "", string? notes = "")
    {
        if (areaName is not null && AreaName != areaName) AreaName = areaName;

        if (Number != number) Number = number;
        if (code is not null && Code != code) Code = code;
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();

        return this;
    }
}