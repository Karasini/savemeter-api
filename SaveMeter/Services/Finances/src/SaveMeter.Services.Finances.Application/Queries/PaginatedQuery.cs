﻿using Instapp.Common.Cqrs.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Application.Queries
{
    public class PaginatedQuery<TResult> : QueryBase<TResult>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
