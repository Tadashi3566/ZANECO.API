namespace ZANECO.API.Application.App.Groups;

public class GroupDto : DtoExtension<GroupDto>, IDto
{
    public string Application { get; set; } = default!;
    public string Parent { get; set; } = default!;
    public string Tag { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public DefaultIdType? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }

}