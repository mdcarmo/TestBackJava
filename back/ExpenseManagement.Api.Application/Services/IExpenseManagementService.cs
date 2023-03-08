using ExpenseManagement.Api.Application.Dto;

namespace ExpenseManagement.Api.Application.Services
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IExpenseManagementService
    {
        /// <summary>
        /// Adiciona uma despesa.
        /// </summary>
        /// <param name="viewModel">viewModel SpentDto</param>
        /// <returns>código da despesa</returns>
        Task<string> AddAsync(SpentDto dto);

        /// <summary>
        /// Altera um gasto
        /// </summary>
        /// <param name="id">identificador do gasto</param>
        /// <param name="spent">entidade alterada</param>
        /// <returns>void</returns>
        Task<bool> UpdateAsync(string id, SpentDto spent);

        /// <summary>
        /// Recupera todas as despesas de um usuário dado seu código.
        /// </summary>
        /// <param name="codeUser">código do usuário</param>
        /// <returns>List SpentDto</returns>
        Task<List<SpentDto>> GetByCodeAsync(long codeUser);

        /// <summary>
        /// Recupera o gasto pelo seu identificador.
        /// </summary>
        /// <param name="id">código da despesa</param>
        /// <returns>Spent</returns>
        Task<SpentDto> GetByIdAsync(string id);

        /// <summary>
        /// Recupera todos os gastos de um usuário baseado em um dia especifico.
        /// </summary>
        /// <param name="codeUser">código do usuário</param>
        /// <param name="dateFind">Data</param>
        /// <returns>List SpentDto</returns>
        Task<List<SpentDto>> GetByCodeAndDateAsync(long codeUser, DateTime dateFind);
    }
}
