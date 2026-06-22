namespace PersonsDirectory.Application.Common.Interfaces;

public interface IFileStorage
{
    Task<string> SaveAsync(Stream content, string fileName, CancellationToken ct = default);
    void Delete(string relativePath);
}