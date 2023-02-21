using SaveMeter.Shared.Abstractions.Messaging;

namespace SaveMeter.Shared.Infrastructure.Messaging.Dispatchers;

internal record MessageEnvelope(IMessage Message, IMessageContext MessageContext);