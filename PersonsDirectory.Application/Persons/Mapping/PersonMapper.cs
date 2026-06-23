using PersonsDirectory.Application.Persons.Commands.CreatePerson;
using PersonsDirectory.Application.Persons.Commands.UpdatePerson;
using PersonsDirectory.Application.Persons.Dtos;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Application.Persons.Mapping;

public static class PersonMapper
{
    
    public static PersonDetailsResponse ToDetails(this Person p) 
    {
        return new PersonDetailsResponse
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Gender = p.Gender,
            PersonalNumber = p.PersonalNumber,
            DateOfBirth = p.DateOfBirth,
            CityId = p.CityId,
            CityName = p.City?.Name ?? string.Empty,
            ImagePath = p.ImagePath,
            PhoneNumbers = p.PhoneNumbers
            .Select(ph => new PhoneNumberDto { Type = ph.Type, Number = ph.Number })
            .ToList(),
            RelatedPersons = p.RelatedPersons
            .Select(r => new RelatedPersonResponse
            {
                RelatedPersonId = r.RelatedPersonId,
                FirstName = r.RelatedPerson?.FirstName ?? string.Empty,
                LastName = r.RelatedPerson?.LastName ?? string.Empty,
                RelationType = r.RelationType
            })
            .ToList()
        };
    }

    public static PersonListItemResponse ToListItem(this Person p)
    { 
        return new PersonListItemResponse()
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Gender = p.Gender,
            PersonalNumber = p.PersonalNumber,
            DateOfBirth = p.DateOfBirth,
            CityName = p.City?.Name ?? string.Empty,
            ImagePath = p.ImagePath
        };
    }

    
    public static Person ToEntity(this CreatePersonCommand c)
    { 
        return new()
        {
            FirstName = c.FirstName.Trim(),
            LastName = c.LastName.Trim(),
            Gender = c.Gender,
            PersonalNumber = c.PersonalNumber.Trim(),
            DateOfBirth = c.DateOfBirth,
            CityId = c.CityId,
            PhoneNumbers = c.PhoneNumbers
            .Select(p => new PhoneNumber { Type = p.Type, Number = p.Number.Trim() })
            .ToList()
        };
    }

    public static void ApplyUpdate(this Person entity, UpdatePersonCommand c)
    {
        entity.FirstName = c.FirstName.Trim();
        entity.LastName = c.LastName.Trim();
        entity.Gender = c.Gender;
        entity.PersonalNumber = c.PersonalNumber.Trim();
        entity.DateOfBirth = c.DateOfBirth;
        entity.CityId = c.CityId;

        entity.PhoneNumbers.Clear();
        foreach (var p in c.PhoneNumbers)
            entity.PhoneNumbers.Add(new PhoneNumber { Type = p.Type, Number = p.Number.Trim() });
    }
}