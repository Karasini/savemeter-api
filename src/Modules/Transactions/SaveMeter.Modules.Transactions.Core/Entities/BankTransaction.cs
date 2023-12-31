﻿using System;
using System.Collections.Generic;
using SaveMeter.Shared.Abstractions.Kernel.Types;

namespace SaveMeter.Modules.Transactions.Core.Entities;

internal class BankTransaction : Entity
{
    public DateTime TransactionDate { get; set; }
    public string Customer { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string BankName { get; set; }
    public Guid UserId { get; set; }
}