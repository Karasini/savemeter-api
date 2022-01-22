using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Application.DTO
{
    public record PaginatedDto<T>
    {
        public int CurrentPage { get; init; }
        public int TotalPages { get; init; }
        public int PageSize { get; init; }
        public long TotalCount { get; init; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public List<T> Items { get; init; }
    }
}
