using System.Collections.Generic;

namespace SaveMeter.Shared.Abstractions.Queries;

public record PaginatedDto<T>
{
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int PageSize { get; init; }
    public long TotalCount { get; init; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
    public IReadOnlyList<T> Items { get; init; }
}