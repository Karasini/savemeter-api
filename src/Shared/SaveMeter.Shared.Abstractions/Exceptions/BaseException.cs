using System;
using System.Net;

namespace SaveMeter.Shared.Abstractions.Exceptions;

public abstract class BaseException : Exception
{
    public virtual string Code { get; }

    public virtual HttpStatusCode StatusCode { get; }
    protected BaseException(string message) : base(message)
    {
    }
}