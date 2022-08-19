﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.Exception.Models;

namespace SaveMeter.Services.Finances.Application.Exceptions
{
    internal class MoneySourceNotFoundException : NotFoundException
    {
        public override string Code => "category_not_found";
    }
}
