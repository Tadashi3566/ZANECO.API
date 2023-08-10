namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentDto : DtoExtension, IDto
{
    public DefaultIdType EmployeeId { get; set; }
    public string EmployeeName { get; set; } = default!;
    public DateTime DocumentDate { get; set; } = default!;
    public string DocumentType { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public bool IsPublic { get; set; } = default!;
    public string? Content { get; set; }
    public string? Raw { get; set; }
}