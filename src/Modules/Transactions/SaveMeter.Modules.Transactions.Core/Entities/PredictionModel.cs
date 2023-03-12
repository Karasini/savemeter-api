using System;
using SaveMeter.Shared.Abstractions.Kernel.Types;

namespace SaveMeter.Modules.Transactions.Core.Entities;

public class PredictionModel : Entity
{
    public byte[] Model { get; private set; }
    public Guid UserId { get; private set; }
 
    public PredictionModel(byte[] model, Guid userId)
    {
        Model = model;
        UserId = userId;
    }

    public void UpdateModel(byte[] data)
    {
        Model = data;
    }
}