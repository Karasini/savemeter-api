using Instapp.Common.Cqrs.Commands;
using Instapp.Common.Exception.Guards;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Application.Exceptions;
using SaveMeter.Services.Finances.Domain.Aggregates.FinancialGoal;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Application.Commands.CreateFinancialGoal
{
    internal class CreateFinancialGoalCommandHandler : ICommandHandler<CreateFinancialGoalCommand, FinancialGoalGroupDto>
    {
        private readonly IFinancialGoalGroupRepository _repository;

        public CreateFinancialGoalCommandHandler(IFinancialGoalGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<FinancialGoalGroupDto> Handle(CreateFinancialGoalCommand request, CancellationToken cancellationToken)
        {
            var goal = FinancialGoal.Create(request.Title, request.Amount);
            var goalGroup = await _repository.GetById(request.GoalGroupId);

            Guard.Against<FinancialGoalGroupNotFoundException>(goalGroup == null);

            goalGroup.AddGoal(goal);

            _repository.Update(goalGroup);

            return new FinancialGoalGroupDto
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
            };

        }
    }
}
