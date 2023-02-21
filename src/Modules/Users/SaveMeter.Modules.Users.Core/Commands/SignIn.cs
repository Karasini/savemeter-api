﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Shared.Abstractions.Auth;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Users.Core.Commands;
internal record SignIn([Required][EmailAddress] string Email, [Required] string Password) : ICommand<JsonWebToken> {}
