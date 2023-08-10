namespace ZANECO.API.Application.Common.Extensions;
public abstract class DtoExtension : DtoExtension<DefaultIdType>
{
}

public abstract class DtoExtension<TId>
{
    public TId Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Remarks { get; set; }
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}