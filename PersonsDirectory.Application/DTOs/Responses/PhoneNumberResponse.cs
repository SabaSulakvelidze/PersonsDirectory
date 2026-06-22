using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.DTOs.Responses
{
    public sealed class PhoneNumberResponse
    {
        public PhoneType Type { get; set; }
        public string Number { get; set; } = null!;
    }
}
