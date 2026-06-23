using System.Text.RegularExpressions;

namespace PersonsDirectory.Application.Common.Validation;

// Encapsulates the "Georgian OR Latin, never mixed" rule from the task.
public static partial class NameRules
{
    [GeneratedRegex(@"^\p{IsBasicLatin}+$")]
    private static partial Regex LatinOnly();

    [GeneratedRegex(@"^[Ⴀ-ჿ]+$")]
    private static partial Regex GeorgianOnly();

    // Returns null if valid, otherwise an error message.
    public static string? Check(string? value, string field)
    {
        if (string.IsNullOrWhiteSpace(value))
            return $"{field} is required.";

        var v = value.Trim();
        if (v.Length < 2 || v.Length > 50)
            return $"{field} must be between 2 and 50 characters.";

        // Must be ENTIRELY Latin letters or ENTIRELY Georgian letters.
        bool latin = OnlyLatinLetters(v);
        bool georgian = GeorgianOnly().IsMatch(v);

        if (!latin && !georgian)
            return $"{field} must contain only Georgian or only Latin letters (no mixing).";

        return null;
    }

    private static bool OnlyLatinLetters(string v)
    {
        foreach (var ch in v)
            if (ch is < 'A' or (> 'Z' and < 'a') or > 'z')
                return false;
        return true;
    }
}