using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.UoW;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo
{
    class MongoSeed
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryReferenceRepository _categoryReferenceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MongoSeed(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, ICategoryReferenceRepository categoryReferenceRepository)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _categoryReferenceRepository = categoryReferenceRepository;
        }

        public async Task Seed()
        {

            foreach (var category in CategoriesSeed.Categories)
            {
                var exists = await _categoryRepository.Exists(x => x.Id == category.Id);
                if (exists)
                {
                    _categoryRepository.Update(category);
                }
                else
                {
                    _categoryRepository.Add(category);
                }
            }

            await _unitOfWork.Commit();

            if (await _categoryReferenceRepository.Exists(x => x.Key != ""))
            {
                return;
            }

            foreach (var categoryReference in CategoryReferenceSeed.GenerateSeeds())
            {
                _categoryReferenceRepository.Add(categoryReference);
            }

            await _unitOfWork.Commit();
        }
    }
}
