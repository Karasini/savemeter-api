using Instapp.Common.Cqrs.Commands;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Domain.Aggregates.FinancialGoal;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Application.Commands.CreateFinancialGoalGroup
{
    internal class CreateFinancialGoalGroupCommandHandler : ICommandHandler<CreateFinancialGoalGroupCommand, FinancialGoalGroupDto>
    {
        private readonly IFinancialGoalGroupRepository _repository;

        public CreateFinancialGoalGroupCommandHandler(IFinancialGoalGroupRepository repository)
        {
            _repository = repository;
        }

        public Task<FinancialGoalGroupDto> Handle(CreateFinancialGoalGroupCommand request, CancellationToken cancellationToken)
        {
            var goals = request.Goals.Select(x => FinancialGoal.Create(x.Title, x.Amount, x.Rank)).ToList();
            var goalGroup = FinancialGoalGroup.Create(request.Title, goals);

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
}
