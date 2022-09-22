﻿using System.Collections.Generic;

namespace SaveMeter.Modules.Users.Core;

public class RegistrationOptions
{
    public bool Enabled { get; set; }
    public IEnumerable<string> InvalidEmailProviders { get; set; }
}