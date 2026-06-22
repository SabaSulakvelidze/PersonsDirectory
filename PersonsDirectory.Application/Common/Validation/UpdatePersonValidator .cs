using PersonsDirectory.Application.Common.Validation;
using PersonsDirectory.Application.DTOs.Requests;
using PersonsDirectory.Application.Persons.Dtos;

namespace PersonsDirectory.Application.Persons.Validation;

public sealed class UpdatePersonValidator : IValidator<UpdatePersonRequest>
{
    public ValidationResult Validate(UpdatePersonRequest r)
    {
        var result = new ValidationResult();

        var firstNameError = NameRules.Check(r.FirstName, "First name");
        if (firstNameError is not null) result.Add(nameof(r.FirstName), firstNameError);

        var lastNameError = NameRules.Check(r.LastName, "Last name");
        if (lastNameError is not null) result.Add(nameof(r.LastName), lastNameError);

        if (!Enum.IsDefined(r.Gender))
            result.Add(nameof(r.Gender), "Gender is invalid.");

        if (string.IsNullOrWhiteSpace(r.PersonalNumber) ||
            r.PersonalNumber.Length != 11 ||
            !r.PersonalNumber.All(char.IsDigit))
            result.Add(nameof(r.PersonalNumber), "Personal number must be exactly 11 digits.");

        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - r.DateOfBirth.Year;
        if (r.DateOfBirth > today.AddYears(-age)) age--;
        if (age < 18) result.Add(nameof(r.DateOfBirth), "Person must be at least 18 years old.");

        if (r.CityId <= 0)
            result.Add(nameof(r.CityId), "City is required.");

        for (int i = 0; i < r.PhoneNumbers.Count; i++)
        {
            var p = r.PhoneNumbers[i];
            if (!Enum.IsDefined(p.Type))
                result.Add($"PhoneNumbers[{i}].Type", "Phone type is invalid.");
            if (string.IsNullOrWhiteSpace(p.Number) || p.Number.Length is < 4 or > 50)
                result.Add($"PhoneNumbers[{i}].Number", "Phone number must be 4–50 characters.");
        }

        return result;
    }
}