using SaveMeter.Shared.Infrastructure;
using SaveMeter.Shared.Infrastructure.Mongo.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Modules.Categories.Core.Repositories;

namespace SaveMeter.Modules.Categories.Core.DAL;
internal class CategoryInitializer : IInitializer
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _repository;

    public CategoryInitializer(IUnitOfWork unitOfWork, ICategoryRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task InitAsync()
    {
        if (await _repository.AnyAsync()) return;

        CategoriesSeed.Categories.ToList().ForEach(x => _repository.Add(x));

        await _unitOfWork.Commit();
    }
}
