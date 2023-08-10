namespace ZANECO.API.Application.Common.Extensions;
public class RequestExtension : RequestExtension<DefaultIdType>
{
}

public abstract class RequestExtension<TId>
{
    public TId Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public abstract class RequestWithImageExtension : RequestExtension
{
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}
