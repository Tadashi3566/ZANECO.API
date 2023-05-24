namespace ZANECO.API.Domain.App;

public class Group : AuditableEntity, IAggregateRoot
{
    public string Application { get; private set; } = default!;
    public string Parent { get; private set; } = default!;
    public string Tag { get; private set; }
    public int Number { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public decimal Amount { get; private set; }
    public string? Manager { get; private set; }
    public string? ImagePath { get; private set; }

    public Group(string application, string parent, string tag, int number, string code, string name, decimal amount, string manager, string? description, string? notes, string? imagePath)
    {
        Application = application.ToUpper();
        Parent = parent.Trim().ToUpper();
        Tag = tag.Trim().ToUpper();

        Number = number;
        Code = code.Trim().ToUpper();
        Name = name.Trim();
        Amount = amount;
        Manager = manager.Trim();

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();
        ImagePath = imagePath;
    }

    public Group Update(string application, string parent, string tag, int number, string code, string name, decimal amount, string manager, string? description, string? notes, string? imagePath)
    {
        if (application is not null && !Application.Equals(application)) Application = application.ToUpper();
        if (parent is not null && !Parent.Equals(parent)) Parent = parent.Trim().ToUpper();
        if (tag is not null && !Tag.Equals(tag)) Tag = tag.Trim().ToUpper();

        if (!Number.Equals(number)) Number = number;
        if (code is not null && !Code.Equals(code)) Code = code.Trim().ToUpper();
        if (name is not null && !Name.Equals(name)) Name = name.Trim();
        if (!Amount.Equals(amount)) Amount = amount;
        if (manager is not null && !Manager!.Equals(manager)) Manager = manager.Trim();

        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();
        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;
        return this;
    }

    public Group ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}