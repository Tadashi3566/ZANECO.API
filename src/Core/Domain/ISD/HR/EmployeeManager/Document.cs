namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Document : AuditableEntity, IAggregateRoot
{
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;
    public DateTime DocumentDate { get; private set; }
    public string DocumentType { get; private set; } = default!;
    public string Reference { get; private set; } = default!;
    public bool IsPublic { get; private set; } = default!;

    public string? Content { get; private set; }
    public string? Raw { get; private set; }

    public Document(DefaultIdType employeeId, string employeeName, DateTime documentDate, string documentType, string reference, bool isPublic, string name, string? description = null, string? notes = null, string? imagePath = null)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
        DocumentDate = documentDate;
        DocumentType = documentType.Trim().ToUpper();
        Reference = reference.Trim().ToUpper();
        IsPublic = isPublic;

        Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Document Update(string employeeName, DateTime documentDate, string documentType, string reference, bool isPublic, string name, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!EmployeeName.Equals(employeeName)) EmployeeName = employeeName;

        if (!DocumentDate.Equals(documentDate)) DocumentDate = documentDate;
        if (!Reference.Equals(reference)) Reference = reference.Trim().ToUpper();
        if (!DocumentType.Equals(documentType)) DocumentType = documentType.Trim().ToUpper();
        if (!IsPublic.Equals(isPublic)) IsPublic = isPublic;

        if (!Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Document UpdateContent(string content, string raw)
    {
        if (content is not null && (Content?.Equals(content) != true)) Content = content;
        if (raw is not null && (Raw?.Equals(raw) != true)) Raw = raw;

        return this;
    }

    public Document ClearFilePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}