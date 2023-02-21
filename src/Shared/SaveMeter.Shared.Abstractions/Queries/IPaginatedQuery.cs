using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Shared.Abstractions.Queries;

public interface IPaginatedQuery : IQuery
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

public interface IPaginatedQuery<TResult> : IPaginatedQuery, IQuery<TResult>
{
}
