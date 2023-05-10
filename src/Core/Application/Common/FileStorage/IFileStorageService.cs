namespace ZANECO.API.Application.Common.FileStorage;

public interface IFileStorageService : ITransientService
{
    public Task<string> UploadAsync<T>(ImageUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    where T : class;

    public void Remove(string? path);
}