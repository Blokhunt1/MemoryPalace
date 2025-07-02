using System.IO.Compression;

namespace MemoryPalaceApp.Services;

public class ImageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly string _uploadsPath;

    public ImageService(IWebHostEnvironment environment)
    {
        _environment = environment;
        _uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
        
        if (!Directory.Exists(_uploadsPath))
        {
            Directory.CreateDirectory(_uploadsPath);
        }
    }

    public async Task<List<string>> ProcessZipFileAsync(Stream zipStream, string memoryPalaceName)
    {
        var imagePaths = new List<string>();
        var palaceFolder = Path.Combine(_uploadsPath, SanitizeFileName(memoryPalaceName));
        
        if (Directory.Exists(palaceFolder))
        {
            Directory.Delete(palaceFolder, true);
        }
        Directory.CreateDirectory(palaceFolder);

        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
        var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
        
        var imageEntries = archive.Entries
            .Where(entry => imageExtensions.Contains(Path.GetExtension(entry.Name).ToLowerInvariant()))
            .OrderBy(entry => entry.Name)
            .ToList();

        foreach (var entry in imageEntries)
        {
            var fileName = SanitizeFileName(entry.Name);
            var filePath = Path.Combine(palaceFolder, fileName);
            
            using var entryStream = entry.Open();
            using var fileStream = File.Create(filePath);
            await entryStream.CopyToAsync(fileStream);
            
            var relativePath = Path.Combine("uploads", SanitizeFileName(memoryPalaceName), fileName).Replace('\\', '/');
            imagePaths.Add(relativePath);
        }

        return imagePaths;
    }

    private static string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        return string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
    }
}