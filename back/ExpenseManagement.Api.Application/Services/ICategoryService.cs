using ExpenseManagement.Core.Entities;

namespace ExpenseManagement.Api.Application.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Recupera todas as categorias cadastradas.
        /// </summary>
        /// <returns>Lista de strings</returns>
        Task<List<Category>> GetAll();

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<Category> GetByFilter(string search);
    }
}
