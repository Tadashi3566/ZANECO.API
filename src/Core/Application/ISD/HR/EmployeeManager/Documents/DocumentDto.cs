namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string EmployeeName { get; set; } = default!;
    public DateTime DocumentDate { get; set; } = default!;
    public string DocumentType { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public bool IsPublic { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Raw { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string ImagePath { get; set; } = default!;
}