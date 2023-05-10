namespace ZANECO.API.Application.Common.FileStorage;

public class ImageUploadRequest
{
    public string Name { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public string Data { get; set; } = default!;
}

public class ImageUploadRequestValidator : CustomValidator<ImageUploadRequest>
{
    public ImageUploadRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
                .WithMessage("Image Name cannot be empty!")
            .MaximumLength(150);

        RuleFor(p => p.Extension)
            .NotEmpty()
                .WithMessage("Image Extension cannot be empty!")
            .MaximumLength(5);

        RuleFor(p => p.Data)
            .NotEmpty()
                .WithMessage("Image Data cannot be empty!");
    }
}