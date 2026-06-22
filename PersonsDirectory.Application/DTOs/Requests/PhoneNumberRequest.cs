using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.DTOs.Requests
{
    public sealed class PhoneNumberRequest
    {
        public PhoneType Type { get; set; }
        public string Number { get; set; } = null!;
    }
}
