using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.DTOs.Responses
{
    public sealed class RelatedPersonResponse
    {
        public int RelatedPersonId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public RelationType RelationType { get; set; }
    }
}
