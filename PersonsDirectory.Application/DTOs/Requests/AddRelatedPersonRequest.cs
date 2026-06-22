using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.DTOs.Requests
{
    public sealed class AddRelatedPersonRequest
    {
        public int RelatedPersonId { get; set; }
        public RelationType RelationType { get; set; }
    }
}
