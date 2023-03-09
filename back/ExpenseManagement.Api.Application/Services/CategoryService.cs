using ExpenseManagement.Core.Entities;
using ExpenseManagement.Core.Repositories;

namespace ExpenseManagement.Api.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">repositório</param>
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<List<Category>> GetAll()
        {
            var listCategoriesFromDb = await _repository.GetAll();
            return listCategoriesFromDb;
        }

        public List<Category> GetByFilter(string search)
        {
            var listCategoriesFromDb = _repository.GetByFilter(search);
            return listCategoriesFromDb;
        }
    }
}
