using System;
using SaveMeter.Shared.Abstractions.Contexts;

namespace SaveMeter.Shared.Abstractions.Messaging;

public interface IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }
}