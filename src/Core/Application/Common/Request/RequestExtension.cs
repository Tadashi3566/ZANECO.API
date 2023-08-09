namespace ZANECO.API.Application.Common.Request;
public class RequestExtension<T>
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class RequestWithImageExtension<T> : RequestExtension<T>
{
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}
