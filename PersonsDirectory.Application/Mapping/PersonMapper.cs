using PersonsDirectory.Application.DTOs.Requests;
using PersonsDirectory.Application.DTOs.Responses;
using PersonsDirectory.Application.Persons.Dtos;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Application.Mapping
{
    public static class PersonMapper
    {
        public static PersonDetailsResponse ToDetails(this Person p)
        {
            return new()
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
                    .Select(ph => new PhoneNumberResponse
                    {
                        Type = ph.Type,
                        Number = ph.Number
                    }).ToList(),
                RelatedPersons = p.RelatedPersons
                    .Select(r => new RelatedPersonResponse
                    {
                        RelatedPersonId = r.RelatedPersonId,
                        FirstName = r.RelatedPerson?.FirstName ?? string.Empty,
                        LastName = r.RelatedPerson?.LastName ?? string.Empty,
                        RelationType = r.RelationType
                    }).ToList()
            };
        }

        public static PersonListItemResponse ToListItem(this Person p)
        {
            return new()
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

        public static Person ToEntity(this CreatePersonRequest r)
        {
            return new()
            {
                FirstName = r.FirstName.Trim(),
                LastName = r.LastName.Trim(),
                Gender = r.Gender,
                PersonalNumber = r.PersonalNumber.Trim(),
                DateOfBirth = r.DateOfBirth,
                CityId = r.CityId,
                PhoneNumbers = r.PhoneNumbers
                    .Select(p => new PhoneNumber() { Type = p.Type, Number = p.Number.Trim() })
                    .ToList()
            };
        }

        public static void ApplyUpdate(this Person entity, UpdatePersonRequest r)
        {
            entity.FirstName = r.FirstName.Trim();
            entity.LastName = r.LastName.Trim();
            entity.Gender = r.Gender;
            entity.PersonalNumber = r.PersonalNumber.Trim();
            entity.DateOfBirth = r.DateOfBirth;
            entity.CityId = r.CityId;

            entity.PhoneNumbers.Clear();
            foreach (var p in r.PhoneNumbers)
                entity.PhoneNumbers.Add(new PhoneNumber { Type = p.Type, Number = p.Number.Trim() });
        }


    }
}
