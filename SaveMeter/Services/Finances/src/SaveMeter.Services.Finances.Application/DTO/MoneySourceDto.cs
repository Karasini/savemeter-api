using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;

namespace SaveMeter.Services.Finances.Application.DTO
{
    public record MoneySourceDto
    {
        public Guid Id { get; init; }
        public string Title { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public decimal NativeAmount { get; set; }
        public string NativeCurrency { get; set; }
        public DateTime StatusDate { get; set; }
    }
}
