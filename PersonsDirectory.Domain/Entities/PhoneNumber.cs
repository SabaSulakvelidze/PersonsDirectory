using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Domain.Entities;

public class PhoneNumber
{
    public int Id { get; set; }
    public PhoneType Type { get; set; }
    public string Number { get; set; } = null!;

    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
}