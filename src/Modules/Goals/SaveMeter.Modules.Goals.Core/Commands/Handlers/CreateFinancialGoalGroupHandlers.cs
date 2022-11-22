using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Modules.Goals.Core.DAL.Repositories;
using SaveMeter.Modules.Goals.Core.DTO;
using SaveMeter.Modules.Goals.Core.Entities;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Goals.Core.Commands.Handlers;

internal class CreateFinancialGoalGroupHandlers : ICommandHandler<CreateFinancialGoalGroup, FinancialGoalGroupDto>
{
    private readonly IFinancialGoalGroupRepository _repository;

    public CreateFinancialGoalGroupHandlers(IFinancialGoalGroupRepository repository)
    {
        _repository = repository;
    }

    public Task<FinancialGoalGroupDto> HandleAsync(CreateFinancialGoalGroup command, CancellationToken cancellationToken = default)
    {
        var goals = command.Goals.Select(x => FinancialGoal.Create(x.Title, x.Amount, x.Rank)).ToList();
        var goalGroup = FinancialGoalGroup.Create(command.Title, goals);
        
        _repository.Add(goalGroup);
        
        return Task.FromResult(new FinancialGoalGroupDto
        {
            Title = goalGroup.Title,
            Id = goalGroup.Id,
            TotalAmount = goalGroup.TotalAmount,
            Goals = goalGroup.Goals.Select(x => new FinancialGoalDto
            {
                Amount = x.Amount,
                Title = x.Title,
                Id = x.Id,
            }).ToList()
        });
    }
}