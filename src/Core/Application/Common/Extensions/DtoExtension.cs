namespace ZANECO.API.Application.Common.Extensions;
public class DtoExtension<T>
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Remarks { get; set; }
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}