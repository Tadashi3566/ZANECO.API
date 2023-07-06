namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Document : AuditableEntity, IAggregateRoot
{
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;
    public DateTime DocumentDate { get; private set; }
    public string DocumentType { get; private set; } = string.Empty;
    public string Reference { get; private set; } = string.Empty;
    public bool IsPublic { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public string Raw { get; private set; } = string.Empty;
    public string ImagePath { get; private set; } = default!;

    public Document(DefaultIdType employeeId, string employeeName, DateTime documentDate, string documentType, string reference, bool isPublic, string name, string? description, string? notes, string? imagePath)
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

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
    }

    public Document Update(string employeeName, DateTime documentDate, string documentType, string reference, bool isPublic, string name, string? description, string? notes, string? imagePath)
    {
        if (employeeName is not null && !EmployeeName.Equals(employeeName)) EmployeeName = employeeName;

        if (!DocumentDate.Equals(documentDate)) DocumentDate = documentDate;
        if (reference is not null && !Reference.Equals(reference)) Reference = reference.Trim().ToUpper();
        if (documentType is not null && !DocumentType.Equals(documentType)) DocumentType = documentType.Trim().ToUpper();
        if (!IsPublic.Equals(isPublic)) IsPublic = isPublic;

        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;

        return this;
    }

    public Document UpdateContent(string content, string raw)
    {
        if (content is not null && !Content.Equals(content)) Content = content;
        if (raw is not null && !Raw.Equals(raw)) Raw = raw;

        return this;
    }

    public Document ClearFilePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}