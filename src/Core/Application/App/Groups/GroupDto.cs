namespace ZANECO.API.Application.App.Groups;

public class GroupDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Application { get; set; } = default!;
    public string Parent { get; set; } = default!;
    public string Tag { get; set; } = string.Empty;
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string? Manager { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}