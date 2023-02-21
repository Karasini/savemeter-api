using System.Net;

namespace SaveMeter.Shared.Abstractions.Exceptions;

public class NotFoundException : BaseException
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException() : base("")
    {
    }
}