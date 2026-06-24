namespace PersonsDirectory.WebApi.Models;

public sealed class ApiError
{
    public int Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public IReadOnlyDictionary<string, string[]>? Errors { get; set; }
    public string? TraceId { get; set; }
}