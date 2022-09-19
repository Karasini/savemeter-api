using System;
using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Shared.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}