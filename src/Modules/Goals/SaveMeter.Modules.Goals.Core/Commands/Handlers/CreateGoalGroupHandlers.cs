using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Modules.Goals.Core.DTO;
using SaveMeter.Modules.Goals.Core.Entities;
using SaveMeter.Modules.Goals.Core.Repositories;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Goals.Core.Commands.Handlers;

internal class CreateGoalGroupHandlers : ICommandHandler<CreateGoalGroup, GoalsGroupDto>
{
    private readonly IGoalsGroupRepository _repository;

    public CreateGoalGroupHandlers(IGoalsGroupRepository repository)
    {
        _repository = repository;
    }

    public Task<GoalsGroupDto> HandleAsync(CreateGoalGroup command, CancellationToken cancellationToken = default)
    {
        var goals = command.Goals.Select(x => Goal.Create(x.Title, x.Amount, x.Rank)).ToList();
        var goalGroup = GoalsGroup.Create(command.Title, goals);
        
        _repository.Add(goalGroup);
        
        return Task.FromResult(new GoalsGroupDto
        {
            Title = goalGroup.Title,
            Id = goalGroup.Id,
            TotalAmount = goalGroup.TotalAmount,
            Goals = goalGroup.Goals.Select(x => new GoalDto
            {
                Amount = x.Amount,
                Title = x.Title,
                Id = x.Id,
                Rank = x.Rank,
            }).ToList()
        });
    }
}