namespace PersonsDirectory.Application.Common.Validation;

// Collects "field -> messages" as rules run.
public sealed class ValidationResult
{
    private readonly Dictionary<string, List<string>> _errors = [];

    public bool IsValid => _errors.Count == 0;

    public void Add(string field, string message)
    {
        if (!_errors.TryGetValue(field, out var list))
            _errors[field] = list = [];
        list.Add(message);
    }

    public IReadOnlyDictionary<string, string[]> ToDictionary() =>
        _errors.ToDictionary(kv => kv.Key, kv => kv.Value.ToArray());
}