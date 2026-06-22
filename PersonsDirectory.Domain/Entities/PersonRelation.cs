using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Domain.Entities
{
    public class PersonRelation
    {
        public int Id { get; set; }
        public RelationType RelationType { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        public int RelatedPersonId { get; set; }
        public Person RelatedPerson { get; set; } = null!;
    }
}
