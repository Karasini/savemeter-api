﻿using System.Threading;
using System.Threading.Tasks;

namespace SaveMeter.Shared.Abstractions.Queries;

public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}