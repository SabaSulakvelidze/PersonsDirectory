using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.DTOs.Requests
{
    public sealed class UpdatePersonRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public int CityId { get; set; }
        public List<PhoneNumberRequest> PhoneNumbers { get; set; } = [];
    }
}
