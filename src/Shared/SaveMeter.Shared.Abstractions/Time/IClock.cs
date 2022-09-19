using System;

namespace SaveMeter.Shared.Abstractions.Time;

public interface IClock
{
    DateTime CurrentDate();
}