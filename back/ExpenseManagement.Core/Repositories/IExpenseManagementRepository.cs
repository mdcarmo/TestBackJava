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
        /// Altera um gasto
        /// </summary>
        /// <param name="id">identificador do gasto</param>
        /// <param name="spent">entidade alterada</param>
        /// <returns>void</returns>
        Task<bool> UpdateAsync(string id, Spent spent);

        /// <summary>
        /// Recupera todos os gastos de um usuário dado seu código.
        /// </summary>
        /// <param name="codeUser">código do usuário</param>
        /// <returns>List Spent</returns>
        Task<List<Spent>> GetByCodeAsync(long codeUser);

        /// <summary>
        /// Recupera o gasto pelo seu identificador.
        /// </summary>
        /// <param name="id">código da despesa</param>
        /// <returns>Spent</returns>
        Task<Spent> GetByIdAsync(string id);

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
