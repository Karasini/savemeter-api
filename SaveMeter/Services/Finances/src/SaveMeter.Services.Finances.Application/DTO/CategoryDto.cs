using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Application.DTO
{
    public record CategoryDto
    {
        public Guid Id { get; init; }
        public string CategoryName { get; init; }
    }
}
