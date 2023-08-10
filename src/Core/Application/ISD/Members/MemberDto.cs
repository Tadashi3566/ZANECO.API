namespace ZANECO.API.Application.ISD.Members;

public class MemberDto : DtoExtension, IDto
{
    public int IncrementId { get; set; } = default!;
    public int ApplicationId { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Municipality { get; set; } = default!;
    public string Barangay { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime MembershipDate { get; set; }
}