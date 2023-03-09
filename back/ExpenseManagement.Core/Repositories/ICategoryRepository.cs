using ExpenseManagement.Core.Entities;

namespace ExpenseManagement.Core.Repositories
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Recupera todas as categorias cadastradas.
        /// </summary>
        /// <returns></returns>
        Task<List<Category>> GetAll();

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<Category> GetByFilter(string search);
    }
}
