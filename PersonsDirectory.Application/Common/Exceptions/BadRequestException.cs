namespace PersonsDirectory.Application.Common.Exceptions
{
    public class BadRequestException(string msg) : Exception(msg)
    {
    }
}
