using ExpenseManagement.Core.Entities;

namespace ExpenseManagement.Core.Repositories
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IExpenseManagementRepository
    {
        /// <summary>
        /// Adiciona um gasto 
        /// </summary>
        /// <param name="spent">Gasto</param>
        /// <returns>void</returns>
        Task AddAsync(Spent spent);

        /// <summary>
        /// Recupera todos os gastos de um usuário dado seu código.
        /// </summary>
        /// <param name="codeUser">código do usuário</param>
        /// <returns>List Spent</returns>
        Task<List<Spent>> GetByCodeAsync(long codeUser);

        /// <summary>
        /// Recupera todos os gastos de um usuário baseado em um dia especifico.
        /// </summary>
        /// <param name="codeUser">código do usuário</param>
        /// <param name="dateFind">Data</param>
        /// <returns>List Spent</returns>
        Task<List<Spent>> GetByCodeAndDateAsync(long codeUser, DateTime dateFind);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeUser"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<Spent> GetCategoryContentDescriptionAsync(long codeUser, string description);
    }
}
