namespace ZANECO.API.Application.Identity.Users;

public class UpdateUserRequest
{
    public string Id { get; set; } = default!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
    public bool DeleteCurrentImage { get; set; } = default!;
}