using SaveMeter.Shared.Abstractions.Messaging;

namespace SaveMeter.Shared.Abstractions.Commands;

//Marker
public interface ICommand<out TResult> : ICommand
{
}

public interface ICommand : IMessage
{
}