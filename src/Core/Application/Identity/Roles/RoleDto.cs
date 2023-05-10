namespace ZANECO.API.Application.Identity.Roles;

public class RoleDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public List<string>? Permissions { get; set; }
}