using PersonsDirectory.Application.Common.Interfaces;

namespace PersonsDirectory.Infrastructure.Storage
{
    public sealed class FileStorage(string _rootPath) : IFileStorage
    {
        private const string SubFolder = "uploads/persons";

        public void Delete(string relativePath)
        {
            var absolute = Path.Combine(_rootPath, relativePath);
            if (File.Exists(absolute)) File.Delete(absolute);
        }

        public async Task<string> SaveAsync(Stream content, string fileName, CancellationToken ct = default)
        {
            var ext = Path.GetExtension(fileName);
            var unique = $"{Guid.NewGuid():N}{ext}";
            var relative = Path.Combine(SubFolder, unique).Replace("\\","/");
            var absolute = Path.Combine(_rootPath, relative);

            Directory.CreateDirectory(Path.GetDirectoryName(absolute)!);

            await using var fs = new FileStream(absolute, FileMode.Create, FileAccess.Write);
            await content.CopyToAsync(fs, ct);

            return relative;
        }
    }
}
