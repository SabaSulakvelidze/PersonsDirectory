using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; } 
        public int CityId { get; set; }
        public City City { get; set; } = null!;
        public string? ImagePath { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = [];

        public ICollection<PersonRelation> RelatedPersons { get; set; } = [];
    }
}
