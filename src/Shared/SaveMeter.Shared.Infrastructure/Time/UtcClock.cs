﻿using System;
using SaveMeter.Shared.Abstractions.Time;

namespace SaveMeter.Shared.Infrastructure.Time;

public class UtcClock : IClock
{
    public DateTime CurrentDate() => DateTime.UtcNow;
}