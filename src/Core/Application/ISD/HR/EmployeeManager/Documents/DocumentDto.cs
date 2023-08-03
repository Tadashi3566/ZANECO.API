namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string EmployeeName { get; set; } = default!;
    public DateTime DocumentDate { get; set; } = default!;
    public string DocumentType { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public bool IsPublic { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Content { get; set; }
    public string? Raw { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? ImagePath { get; set; }
}