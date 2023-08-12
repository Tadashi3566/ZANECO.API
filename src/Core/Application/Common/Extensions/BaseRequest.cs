namespace ZANECO.API.Application.Common.Extensions;

public class BaseRequest : BaseRequest<DefaultIdType>
{
}

public abstract class BaseRequest<TId>
{
    public TId Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public abstract class BaseRequestWithImage : BaseRequest
{
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}