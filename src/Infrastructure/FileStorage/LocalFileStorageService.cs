using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using ZANECO.API.Application.Common.FileStorage;
using ZANECO.API.Domain.Common;
using ZANECO.API.Infrastructure.Common.Extensions;

namespace ZANECO.API.Infrastructure.FileStorage;

public class LocalFileStorageService : IFileStorageService
{
    public async Task<string> UploadAsync<T>(ImageUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    where T : class
    {
        if (request is null || request.Data is null)
        {
            return default!;
        }

        var a = supportedFileType.GetDescriptionList();

        if (request.Extension is null || !supportedFileType.GetDescriptionList().Contains(request.Extension.ToLower()))
            throw new InvalidOperationException("File Format Not Supported.");
        if (request.Name is null)
            throw new InvalidOperationException("Name is required.");

        string base64Data = Regex.Match(request.Data, "data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;

        var streamData = new MemoryStream(Convert.FromBase64String(base64Data));
        if (streamData.Length > 0)
        {
            string folder = typeof(T).Name;
            if (request.EmployeeId is not null)
            {
                folder += $"\\{request.EmployeeId}";
            }

            if (request.DateStr is not null)
            {
                folder += $"\\{request.DateStr}";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                folder = folder.Replace(@"\", "/");
            }

            string folderName = supportedFileType switch
            {
                FileType.Image => Path.Combine("Files", "Images", folder),
                _ => Path.Combine("Files", "Others", folder),
            };

            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            Directory.CreateDirectory(pathToSave);

            string fileName = request.Name.Trim('"');
            fileName = RemoveSpecialCharacters(fileName);
            fileName = fileName.ReplaceWhitespace("-");
            fileName += request.Extension.Trim();
            string fullPath = Path.Combine(pathToSave, fileName);
            string dbPath = Path.Combine(folderName, fileName);
            if (File.Exists(dbPath))
            {
                dbPath = NextAvailableFilename(dbPath);
                fullPath = NextAvailableFilename(fullPath);
            }

            using var stream = new FileStream(fullPath, FileMode.Create);
            await streamData.CopyToAsync(stream, cancellationToken);
            return dbPath.Replace("\\", "/");
        }
        else
        {
            return default!;
        }
    }

    public static string RemoveSpecialCharacters(string str)
    {
        return Regex.Replace(str, "[^a-zA-Z0-9_.]+", string.Empty, RegexOptions.Compiled);
    }

    public void Remove(string? path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    private const string NumberPattern = "-{0}";

    private static string NextAvailableFilename(string path)
    {
        if (!File.Exists(path))
        {
            return path;
        }

        if (Path.HasExtension(path))
        {
            return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path), StringComparison.Ordinal), NumberPattern));
        }

        return GetNextFilename(path + NumberPattern);
    }

    private static string GetNextFilename(string pattern)
    {
        string tmp = string.Format(pattern, 1);

        if (!File.Exists(tmp))
        {
            return tmp;
        }

        int min = 1, max = 2;

        while (File.Exists(string.Format(pattern, max)))
        {
            min = max;
            max *= 2;
        }

        while (max != min + 1)
        {
            int pivot = (max + min) / 2;
            if (File.Exists(string.Format(pattern, pivot)))
            {
                min = pivot;
            }
            else
            {
                max = pivot;
            }
        }

        return string.Format(pattern, max);
    }
}