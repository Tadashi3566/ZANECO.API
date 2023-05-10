namespace ZANECO.API.Application.ISD.Members;

public class MemberDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int IncrementId { get; set; } = default!;
    public int ApplicationId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Municipality { get; set; } = default!;
    public string Barangay { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime MembershipDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    public string? Status { get; set; }
}